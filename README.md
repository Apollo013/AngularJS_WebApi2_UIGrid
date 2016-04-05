# AngularJS_WebApi2_UIGrid
Another small app containing an AngularJS web client that accesses resources through a Web Api 2 service. 
This app demonstrates a host of features listed below. Hosted on Azure. [HERE](http://angularuigrid.azurewebsites.net)

The primary goal of this is to show you how to implement the AngularJS UI-Grid plugin, and demonstrate how to page, sort and filter data on the server, rather that requesting all data from the server and allowing UI-Grids internal implementation to do these for us. However, this app also demonstrates other features including DI, security, logging & design patterns.

Before you start, you will need to specify a database connection in your Web.config file.

Developed using VS2015 Community

#####FRAMEWORKS / PLUGINS / LANGUAGES

| No.        | Description  |
| -----------|-------------|
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
| 13 | C# |
| 14 | Linq |
| 15 | Code First - Fluent Api |

#####WEB API / DOT NET / C# FEATURES

| No.        | Description  |
| -----------|-------------|
|1| Extension Methods |
|2| Forcing Https Requests |
|3| Dynamic linq for sorting and filtering Linq Queries that makes use of extension methods. |
|4| OWIN setup & configuration |
|5| Attribute Routing |
|6| Repository, Unit of Work & Model Factory Patterns. |
|7| Dependancy Injection |
|8| Configuring domain entity tables using 'EntityTypeConfiguration' with ef migrations. |
|9| Creating custom 'IHttpActionResult' responses |
|10| Unit Testing |
|11| Generic Repository |
|12| Exception Handling |
|13| Use of Reflection to check property names passed to controllers |
|14| Logging using Microsoft.ApplicationInsights.Log4NetAppender |
|15| Recursion |
|16| CORS Enabled to accept only a single client from making requests, plus specifying which verbs to accept |
|17| Authorize attribute used to authorize access to specific endpoints |


#####ANGULARJS FEATURES

| No.        | Description  |
| -----------|-------------|
|1| AngularJS $Http Interceptor for configuring Requests & response errors |
|2| Making Restful calls in AngularJS using $http.post, put, get, delete |
|3| Solving the Angularjs 404 routing error using the web.Config file (see admin client) |
|4| External Pagination with UI-Grid |
|5| External Sorting with UI-Grid |
|6| External Filtering with UI-Grid |
|7| Displaying non-blocking notifications using Toastr |
|8| Displaying loading bar during http requests |


#####Structure

| No.        | Project Name        | Project Type  | Description  |
| -----------| ---------------- |-------------| -------------|
|1 | AdminClient     | Empty Web Project | Web User Interface |
|2| BusinessServices      |  Class Library      | Business logic layer that communicates between the web api and repositories |
|3| DataAccessLayer | Class Library      | Responisible for communicating with the database |
|4| DataService | Web Api 2 | Our web api data service |
|5| Models.DomainEntities | Class Library | Models used for constructing our database tables |
|6| Models.ViewModels | Class Library | View models used for sending information to the client |
|7| Repositories | Class Library | Repository layer |
|8| UnitTests | Unit Test | Testing |


