var React = require('react');
var NavigationReact = require('navigation-react');
var NavigationLink = NavigationReact.NavigationLink;
var RefreshLink = NavigationReact.RefreshLink;
var NavigationBackLink = NavigationReact.NavigationBackLink;

exports.Listing = React.createClass({
	render: function() {
		var people = this.props.People.map(function (person) {
	        return (
                <tr>
                    <td><NavigationLink action="select" toData={{ id: person.Id }}>{person.Name}</NavigationLink></td>
                    <td>{person.DateOfBirth}</td>
                </tr>
            );
        });
        return (
            <div id="listing">
                <table>
                    <thead>
                        <tr>
                            <th>Name</th>
                            <th>Date of Birth</th>
                        </tr>
                    </thead>
                    <tbody>{people}</tbody>
                </table>
                <div id="pager">
                    Go to page
                    <RefreshLink toData={{ pageNumber: 1 }} disableActive={true}>1</RefreshLink>
                    <RefreshLink toData={{ pageNumber: 2 }} disableActive={true}>2</RefreshLink>
                </div>
            </div>
        );
	}
});

exports.Details = React.createClass({
    render: function() {
        var person = this.props.Person;
        return (
            <div id="details">
                <NavigationBackLink distance={1}>People</NavigationBackLink>
                <div id="info">
                    <h2>{person.Name}</h2>
                    <div className="label">Date of Birth</div>
                    <div>{person.DateOfBirth}</div>
                    <div className="label">Email</div>
                    <div>{person.Email}</div>
                    <div className="label">Phone</div>
                    <div>{person.Phone}</div>
                </div>
            </div>
        );
    }
});
