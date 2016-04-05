var React = require('react');
var NavigationReact = require('navigation-react');
var NavigationLink = NavigationReact.NavigationLink;
var RefreshLink = NavigationReact.RefreshLink;
var NavigationBackLink = NavigationReact.NavigationBackLink;

exports.Listing = React.createClass({
    render: function() {
        var stateNavigator = this.props.stateNavigator;
        // Renders a list of people from the props.
        var people = this.props.people.map(function (person) {
            return (
                <tr key={person.Id}>
                    <td>
						<NavigationLink
							stateKey="person"
							navigationData={{id: person.Id}}
							stateNavigator={stateNavigator}>
							{person.Name}
						</NavigationLink></td>
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
                    <RefreshLink
						navigationData={{ pageNumber: 1 }}
						disableActive={true}
						stateNavigator={stateNavigator}>
						1
					</RefreshLink>
                    <RefreshLink
						navigationData={{ pageNumber: 2 }}
						disableActive={true}
						stateNavigator={stateNavigator}>
						2
					</RefreshLink>
                </div>
            </div>
        );
    }
});

exports.Details = React.createClass({
    render: function() {
        // Renders a person's details from the props.
        var person = this.props.person;
        return (
            <div id="details">
                <NavigationBackLink
					distance={1}
					stateNavigator={this.props.stateNavigator}>
					People
				</NavigationBackLink>
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
