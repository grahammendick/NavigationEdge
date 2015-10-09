var React = require('react');
var Navigation = require('navigation');
var Component = require('./Component');

Navigation.settings.historyManager = new Navigation.HTML5HistoryManager();

exports.register = function(props) {
	Navigation.StateInfoConfig.build([
		{ key: 'masterDetails', initial: 'people', states: [
			{ key: 'people', route: '{pageNumber}', component: Component.Listing, defaults: { pageNumber: 1 }, trackCrumbTrail: false, transitions: [
				{ key: 'select', to: 'person' }]},
			{ key: 'person', route: 'person/{id}', component: Component.Details, defaults: { id: 0 } }]
		}
	]);
	if (props && window.history && window.history.pushState) {
		Navigation.start();
		render(props);
		registerNavigators();
	}
}

function render(props) {
	var component = React.createElement(Navigation.StateContext.state.component, props);
	React.render(
		component,
		document.getElementById('content')
	);		
}
	
function registerNavigators() {
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
