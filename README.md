# Navigation Edge

This is the an isomorphic web app built using the <a href="http://grahammendick.github.io/navigation/">Navigation router</a> and <a href="http://tjanczuk.github.io/edge/">Edge.js</a>. The UI is written in React and the server and data layer are in C#. It's isomorphic because it can run on both the server and the client. It renders the HTML content on the server first and then, once the JavaScript has downloaded, the client takes over control to prevent full page reloads. Isomorphic web applications are fast and SEO-friendly. You can try it out by throttling the Network using Chrome dev tools. The app remains usable even while the JavaScript is downloading.

## Run
Once you've cloned the repository, you should:

1. Install the node dependencies by running <code>npm install</code>
2. Install the C# dependencies  by opening and building the NavigationEdge.sln
3. Press F5 and browse to http://localhost:2602/ to see it in action

## Code
Let's break down the flow through the app when a browser request is first received by the C# web server. Because we're using a JavaScript router, the C# doesn't know how to route the request to a Controller. Instead, there's a single catch-all C# route registered that sends all requests to the Navigation Controller. The Navigation Controller calls to Node using Edge.js and passes in the requested Url. Node uses the Navigation router to return the data held in the request along with the name of the C# action that will service the request. Node acts like the ControllerFactory and ValueProvider in one.

The Navigation Controller passes control to this C# action method passing in the data. This method calls through to the C# data layer to retrieve the props needed for the React Component rendering. The Controller then uses Edge.js to call Node for a second time passing in the these props. Node uses the Navigation router to find the Component for the requested Url. React renders the Component from the props and the resulting Html string is passed back to the C#.

The Navigation Controller sends the Html and props to its View. The View writes out the Html so that the page is fully rendered on the server. It also writes out the props to make them available to the client side JavaScript. When the page loads on the client, the user can start using the app straight away. Until the JavaScript loads, clicking a Hyperlink causes a full page refresh and the cycle already described begins again on the server.

Once the JavaScript loads, a different cycle begins. The JavaScript picks up the props sent from the server and, on the client, uses the Navigation router and React to render the right Component. This allows React's view of the world to match up with the server rendered Html. The app's now become a SPA because clicking a Hyperlink no longer results in a full page refresh. Instead, the click's intercepted on the client and sent to the server using Ajax. 

When this Ajax request is received by the web server, it reaches the Navigation Controller and the props are received from Node as already described. This time, however, the server rendering stage is skipped and the props are immediately returned to the client. The Client then passes the props to the Component and React renders the changes to the DOM.
