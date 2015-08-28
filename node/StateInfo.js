var React = require('react');
var Navigation = require('navigation');
var Component = require('./Component');

Navigation.settings.historyManager = new Navigation.HTML5HistoryManager();

exports.register = function(props) {
	Navigation.StateInfoConfig.build([
		{ key: 'masterDetails', initial: 'listing', states: [
			{ key: 'listing', route: '{pageNumber}', action: "SearchPeople", component: Component.Listing, defaults: { pageNumber: 1 }, trackCrumbTrail: false, transitions: [
				{ key: 'select', to: 'details' }]},
			{ key: 'details', route: 'person/{id}', action: "GetPerson", component: Component.Details, defaults: { id: 0 } }]
		}
	]);
	if (props) {
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
			render(asyncData);
		}
	}
}
