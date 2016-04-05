var React = require('react');
var ReactDOM = require('react-dom');
var Navigation = require('navigation');
var Component = require('./Component');

exports.createStateNavigator = function (props) {
	// Configures the Navigation router with the two routes.
	// This configuration is also used server side to power the JSON Api.
	var stateNavigator = new Navigation.StateNavigator([
		{key: 'people', route: '{pageNumber?}', component: Component.Listing, defaults: {pageNumber: 1}},
		{key: 'person', route: 'person/{id}', component: Component.Details, defaults: {id: 0}, trackCrumbTrail: true}
	], new Navigation.HTML5HistoryManager());
	// Client renders so React can catch up with the server rendered content.
	// Browsers that don't support HTML5 History won't get this progressive enhancement.
	if (props && window.history && window.history.pushState && window.XMLHttpRequest) {
		stateNavigator.start();
		render(stateNavigator, props);
		registerControllers(stateNavigator);
	}
	return stateNavigator;
}

function render(stateNavigator, props) {
	// Finds the State's Component and renders it into the HTML content placeholder.
	props.stateNavigator = stateNavigator;
	var component = React.createElement(stateNavigator.stateContext.state.component, props);
	ReactDOM.render(
		component,
		document.getElementById('content')
	);		
}
	
function registerControllers(stateNavigator) {
	// Adds navigating and navigated functions to the people and person States.
	// The navigating function issues an Ajax call for requested Url to get the JSON data.
	// The navigated function uses the JSON data to create props to render the State's Component.
	for (var key in stateNavigator.states) {
		var state = stateNavigator.states[key];
		state.navigating = function(data, url, navigate) {
			var req = new XMLHttpRequest();
			req.onreadystatechange = function() {
				if (req.readyState === 4) {
					navigate(JSON.parse(req.responseText));
				}
			};
			req.open('get', url);
			req.setRequestHeader('Content-Type', 'application/json');
			req.send(null);
		}
		state.navigated = function(data, asyncData) {
			var props = {};
			props[stateNavigator.stateContext.state.key] = asyncData;
			render(stateNavigator, props);
		}
	}
}
