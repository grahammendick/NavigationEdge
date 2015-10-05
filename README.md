# Navigation Edge

This is an isomorphic web app built using the <a href="http://grahammendick.github.io/navigation/">Navigation router</a> and <a href="http://tjanczuk.github.io/edge/">Edge.js</a>. The UI is written in React and the server and data layer are in C#. It's isomorphic because it can run on both the server and the client. It renders the HTML content on the server first and then, once the JavaScript has downloaded, the client takes over control to prevent full page reloads. Isomorphic web applications are fast and SEO-friendly. You can try it out by throttling the Network using Chrome dev tools. The app remains usable even while the JavaScript is downloading.

## Run
Once you've cloned the repository, you should:

1. Install the node dependencies by running <code>npm install</code>
2. Install the C# dependencies  by opening and building the NavigationEdge.sln
3. Press F5 and browse to http://localhost:4065/ to see it in action
