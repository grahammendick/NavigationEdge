var Navigation = require('navigation');
var Component = require('./Component');

Navigation.settings.historyManager = new Navigation.HTML5HistoryManager();

exports.register = function() {
	Navigation.StateInfoConfig.build([
		{ key: 'masterDetails', initial: 'listing', states: [
			{ key: 'listing', route: '{pageNumber}', component: Component.Listing, defaults: { pageNumber: 1 }, trackCrumbTrail: false, transitions: [
				{ key: 'select', to: 'details' }]},
			{ key: 'details', route: 'person/{id}', component: Component.Details, defaults: { id: 0 } }]
		}
	]);
}