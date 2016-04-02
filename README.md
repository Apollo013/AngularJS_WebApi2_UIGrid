# AngularJS_WebApi2_UIGrid
Another small app containing an AngularJS web client that accesses resources through a Web Api 2 service. 
This app demonstrates a host of features listed below. Hosted on Azure. [http://angularuigrid.azurewebsites.net](angularuigrid.azurewebsites.net)


#####FRAMEWORKS & PLUGINS

| No.        | Description  |
| -----------|:-------------:|
| 1 | ASP.NET Web Api 2 |
| 2 | OWIN |
| 3 | Entity Framework 6.1.0 |
| 4 | Ninject |
| 5 | Dynamic Linq |
| 6 | AngularJS, ngRoute, ngAnimate - 1.4.9 |
| 7 | AngularJS Toaster |
| 8 | AngularJS Loading Bar |
| 9 | AngularJS UI-Grid |
| 10 | Hoe HTML Template |
| 11 | Bootstrap V4 alpha |
| 12 | jQuery |


#####WEB API / DOT NET / C# FEATURES
/**************************************************************************/
* Extension Methods
* Attribute Routing
* Forcing Https Requests
* Dynamic linq for sorting and filtering Linq Queries that makes use of extension methods.
* OWIN setup & configuration
* Repository, Unit of Work & Model Factory Patterns.
* Dependancy Injection using Ninject.
* Configuring domain entity tables using 'EntityTypeConfiguration' with ef migrations.
* Creating custom 'IHttpActionResult' responses.
* Unit Testing
* Generic Repository
* Exception Handling
* Use of Reflection to check property names passed to controllers.
* Logging using Microsoft.ApplicationInsights.Log4NetAppender
* Recursion
* CORS Enabled to accept only a single client from making requests, plus specifying 
* Authorize attribute used to authorize access to specific endpoints


#####ANGULARJS FEATURES
/**************************************************************************/
* AngularJS $Http Interceptor for configuring Requests & response errors
* Making Restful calls in AngularJS using $http.post, put, get, delete
* Solving the Angularjs 404 routing error using the web.Config file (see admin client)
* External Pagination with UI-Grid
* External Sorting with UI-Grid
* External Filtering with UI-Grid
* Displaying non-blocking notifications using Toastr
* Displaying loading bar during http requests


#####Structure
/**************************************************************************/

| Project Name        | Type           | Description  |
| ---------------- |:-------------:| -----:|
| AdminClient     | Empty Asp.Net Web Project | $1600 |
| BusinessServices      |  Class Library      |   $12 |
| DataAccessLayer | Class Library      |    $1 |
| DataService | Web Api 2 | Resource Provider |


