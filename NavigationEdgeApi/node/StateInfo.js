var React = require('react');
var Navigation = require('navigation');
var Component = require('./Component');

Navigation.settings.historyManager = new Navigation.HTML5HistoryManager();

exports.register = function (props) {
	// Configures the Navigation router with the two routes.
	// This configuration is also used server side to power the JSON Api.
	Navigation.StateInfoConfig.build([
		{ key: 'masterDetails', initial: 'people', states: [
			{ key: 'people', route: '{pageNumber}', component: Component.Listing, defaults: { pageNumber: 1 }, trackCrumbTrail: false, transitions: [
				{ key: 'select', to: 'person' }]},
			{ key: 'person', route: 'person/{id}', component: Component.Details, defaults: { id: 0 } }]
		}
	]);
	// Client renders so React can catch up with the server rendered content.
	// Browsers that don't support HTML5 History won't get this progressive enhancement.
	if (props && window.history && window.history.pushState && window.XMLHttpRequest) {
		Navigation.start();
		render(props);
		registerNavigators();
	}
}

function render(props) {
	// Finds the State's Component and renders it into the HTML content placeholder.
	var component = React.createElement(Navigation.StateContext.state.component, props);
	React.render(
		component,
		document.getElementById('content')
	);		
}
	
function registerNavigators() {
	// Adds navigating and navigated functions to the people and person States.
	// The navigating function issues an Ajax call for requested Url to get the JSON data.
	// The navigated function uses the JSON data to create props to render the State's Component.
	var states = Navigation.StateInfoConfig.dialogs.masterDetails.states;
	for(var key in states) {
		var state = states[key];
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
			props[Navigation.StateContext.state.key] = asyncData;
			render(props);
		}
	}
}
