var React = require('react');
var NavigationReact = require('navigation-react');
var NavigationLink = NavigationReact.NavigationLink;
var RefreshLink = NavigationReact.RefreshLink;
var NavigationBackLink = NavigationReact.NavigationBackLink;

exports.Listing = React.createClass({displayName: "Listing",
	render: function() {
		var people = this.props.people.map(function (person) {
	        return (
                React.createElement("tr", null, 
                    React.createElement("td", null, React.createElement(NavigationLink, {action: "select", toData: { id: person.Id}}, person.Name)), 
                    React.createElement("td", null, person.DateOfBirth)
                )
            );
        });
        return (
            React.createElement("div", {id: "listing"}, 
                React.createElement("table", null, 
                    React.createElement("thead", null, 
                        React.createElement("tr", null, 
                            React.createElement("th", null, "Name"), 
                            React.createElement("th", null, "Date of Birth")
                        )
                    ), 
                    React.createElement("tbody", null, people)
                ), 
                React.createElement("div", {id: "pager"}, 
                    "Go to page", 
                    React.createElement(RefreshLink, {toData: { pageNumber: 1}, disableActive: true}, "1"), 
                    React.createElement(RefreshLink, {toData: { pageNumber: 2}, disableActive: true}, "2")
                )
            )
        );
	}
});

exports.Details = React.createClass({displayName: "Details",
    render: function() {
        var person = this.props.person;
        return (
            React.createElement("div", {id: "details"}, 
                React.createElement(NavigationBackLink, {distance: 1}, "People"), 
                React.createElement("div", {id: "info"}, 
                    React.createElement("h2", null, person.Name), 
                    React.createElement("div", {className: "label"}, "Date of Birth"), 
                    React.createElement("div", null, person.DateOfBirth), 
                    React.createElement("div", {className: "label"}, "Email"), 
                    React.createElement("div", null, person.Email), 
                    React.createElement("div", {className: "label"}, "Phone"), 
                    React.createElement("div", null, person.Phone)
                )
            )
        );
    }
});
