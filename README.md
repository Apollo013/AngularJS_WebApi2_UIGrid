# AngularJS_WebApi2_UIGrid
Another small app containing an AngularJS web client that accesses resources through a Web Api 2 service. 
This app demonstrates a host of features listed below. Hosted on Azure. [http://angularuigrid.azurewebsites.net](angularuigrid.azurewebsites.net)


#####FRAMEWORKS & PLUGINS

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

| No.        | Project Name        | Project Type           | 
| -----------| ---------------- |-------------|
|1 | AdminClient     | Empty Web Project |
|2| BusinessServices      |  Class Library      | 
|3| DataAccessLayer | Class Library      | 
|4| DataService | Web Api 2 | 
|5| Models.DomainEntities | Class Library | 
|6| Models.ViewModels | Class Library | 
|7| Repositories | Class Library | 
|8| UnitTests | Unit Test | 


