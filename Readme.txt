ASP.NET Core is a cross-platform, open source, web application framework to quickly build dynamic web applications.
It allows us to buils two kinds of this App:

1- Server-side rendered App: the classic web app, where the user send a request to the server and get a HTML page
   produced and rendered by the server to fill the request.

2- HTTP API: the user send the request; but the server just produce the expected response as a JSON or XML object
   that can be used on the client side to render the view for the user. This is the case for browser-based single-page
   App programmed with some javascripts packages like Angular, React...; or to be consumed by mobile app.


ASP.NET Core runs as a Console App that builds and runs it´s own web server named Kestrel.

ASP.NET Core take care of the user´s request through a chained list of middleware pipeline where eachone is specialized 
to attend some specific piece of the request: 
- search some specific static file for the user: some javascript or CSS file...
- call some specific library to process the request....

Part of these middleware pipeline is the process of Routing, where it´s decided the right piece of programn to take care of each
user´s request.

After the routing is set and ASP.NET Core knows how to process each user request, it´s possible to call the the right
class and method on the business logic classes.

At the end of the middleware pipeline is the endpoint middleware. This middleware is responsible for calling the code 
that generates the final response. In most applications that will be an MVC or Razor Pages block.

It´s a good practice to build an archictecture of different layers to devolop our App. Splitting our classes over diferents
project libraries: DataLayer, ServiceLayer, BusinessLayer, WebAppLayer.... 
This schema matches with the Indenpendency of Concern (IoC) principles.

A typical ASP.NET Core application consists of several layers. The ASP.NET Core framework code
handles requests from a client, dealing with the complex networking code. The framework then calls into handlers
(Razor Pages and Web API controllers) that we write using primitives provided by the framework. Finally, these
handlers call into our application’s domain logic, which are typically C# classes and objects without any
ASP.NET Core-specific dependencies.

IMPORTANT: Search information and differences between RESTful Service, WebService & MicroService.

ASP.NET Core has its own built-in Web Server named Kestrel. This server receives the user request and constructs the 
HttpContext object whish is the representation of all the data needed to process the request across the App.

Importants concepts to learn in order to work with ASP.NET Core:

- Model-binding
- Validation
- Routing
- Razor syntax
- Tag Helpers
- Dependency Injection
- Configuration
- Loggings
- Security best practices

The .csproj file it´s the project file where is defined the kind of project: web app, console app, library;
also the platform targets: Net.Core 3.1 or Net.Core 5.0 .... and finally the NuGet packages which the project depends on.

Ones of the most important files for a ASP.NET Core App are Program.cs and Startup.cs where the major configuration issues
take care.

All ASP.Net Core App starts allways as .Net Console App with the Program.cs file which contains the static void Main() function.

This function builds and runs an IHost instance containing the configuration and the Kestrel web server 
to listen the requests and send back the responses.

Program.cs is where it´s configured the infrastructure for the App: the HTTP Server, integration with IIS, configuration sources.

While Startup.cs it´s where we define the components needed for the processing of requests. It´s where we add aditional
features, register some classes for dependency injection...

We usually update more Startup.cs file instead Program.cs

The Startup class is responsible for configuring two main aspects of our application:

- Service registration: Any classes that our application depends on for providing functionality—both 
  those used by the framework and those specific to our application 
  must be registered so that they can be correctly instantiated at runtime.

- Middleware and endpoints: How our application handles and responds to requests.

There is one method inside the class for eachone of these goal: ConfigureServices() & Configure().

The IHostBuilder created in Program calls ConfigureServices and then Configure in this sequence.

Any services registered in the ConfigureServices method are available to the Configure method. Once configuration is
complete, an IHost is created by calling Build() on the IHostBuilder.

ASP.NET Core uses small, modular components for each distinct feature. This allows individual features to evolve separately, 
with only a loose coupling to others. These modular components are exposed as one or more services that are
used by the application.

Within the context of ASP.Net Core, service refers to any class that provides functionality to an application. 
These could be classes exposed by a library or code we’ve written for our application.

We have to avoid tighly copuples between the classes and modules in our projects that could break down the IoC principles.

One solution to this problem is to make it somebody else’s problem. When writing a service, 
we can declare our dependencies and let another class fill those dependencies for us. 
Our service can then focus on the functionality for which it was designed, 
instead of trying to work out how to build its dependencies. 
This technique is called dependency injection or the Inversion of Control (IoC) principle, 
and it is a well-recognized design pattern that is used extensively.

Typically, we’ll register the dependencies of our application into a “container,” which can then be used 
to create any service. This is true for both our own custom application services and the framework services 
used by ASP.NET Core. We must register each service with the container before it can be used in our application.

By adding a new service to this container, we ensure that whenever a class declares a dependency on our service, 
the IoC container knows how to provide it.

In the final configuration method of the Startup class, Configure, we define the middleware pipeline
for the application, which defines how our app handles HTTP requests.

Middleware consists of small components that execute in sequence when the application receives an HTTP request. 
They can perform a whole host of functions, such as logging, identifying the current user for a request, serving
static files, and handling errors.

The order in which the middleware are added to the builder is the order in which they’ll execute in the final pipeline. 
Middleware can only use objects created by previous middleware in the pipeline. 
It can’t access objects created by later middleware.

When some of the middleware module in the pipeline list finds it is reponsible to process some request, 
it proceed to build the reponse for the request and the next modules in the pipeline do not even know about the request.

The routing middleware and the endpoint middleware together are responsible for interpreting the request 
to determine which Razor Page to invoke, for reading parameters from the request, and for generating the final HTML. 

We need only to add the middleware to the pipeline and specify that we wish to use Razor Page endpoints 
by calling MapRazorPages. For each request, the routing middleware uses the request’s URL to determine which
Razor Page to invoke. The endpoint middleware actually executes the Razor Page to generate the HTML response.

At this point We’ve finally finished configuring our application with all the settings, services, and middleware it needs.

The final piece in the pipeline is the endpoint middleware which along with the routing middleware matches the URL´s path 
with the configured route in order to get which Razor Page to invoke.

IMPORTANT: A path is the remainder of the request URL, once the domain has been removed. 
For example, for a request to www.microsoft.com/account/manage, the path is /account/manage.

Razor Pages are stored in .cshtml files within the Pages folder of our project. 
In general, the routing middleware maps request URL paths to a single Razor Page by looking 
in the Pages folder of our project for a Razor Page with the same path.

For example, the /Privacy path from the URL will matches with the file Privacy.cshtml into the Pages folder.

Razor Pages use a templating syntax, called Razor, that combines static HTML with dynamic C# code and HTML generation.

Example of Razor page:

------------------------------------------------------------
@page
@model PrivacyModel
@{
	ViewData["Title"] = "Privacy Policy";
}
<h1>@ViewData["Title"]</h1>
<p>Use this page to detail our site's privacy policy.</p>

------------------------------------------------------------

The @page directive on the first line of the Razor Page is the most important. 
This directive must always be placed on the first line of the file, as it tells ASP.NET Core 
that the .cshtml file is a Razor Page.

@model PrivacyModel  defines which PageModel in our project the Razor Page is associated with.
The page model file is named as the Razor page adding .cs at the end.
In this case we has the razor page Privacy.cshtml and the corresponding page model will be Privacy.cshtml.cs

The last line shown above is some static HTML.

We can also write ordinary C# code in Razor templates by using this construct: @{ /* C# code here */ }

Any code between the curly braces will be executed but won’t be written to the response. For example:

@{
	ViewData["Title"] = "Privacy Policy";
}

just sets the title for the page into the ViewData dictionary.

<h1>@ViewData["Title"]</h1>   it´s the power of introduce dynamically C# variables into HTML tags.

Razon pages can work with "base" templates that define common elements for our App, like the header and footer.
This is know as Razor layout.

Then we can mix these base templates with the own Razor page template to produce the final HTML page to send to the user.

The base templates are used to be found into the Pages/Shared folder.

A Razor page has a associated file with the code behind containing the page model or the methods to run in response 
of the razor page request. The file has exension .cshtml.cs. The code behind class derives from PageModel class 
and usually this  class has one or two methods: OnGet() & OnPost() for each of the methods used by a HTTP request.

Page handlers are driven by convention. Page models can use dependency injection to interact with other services.

See page 54 from book ASP.Net Core in Action.

----------------------------------------------------------------------------
Next:

- What middleware is ?
- Serving static files using middleware
- Adding functionality using middleware
- Combining middleware to form a pipeline
- Handling exceptions and errors with middleware

In ASP.NET Core, middleware are C# classes or functions that handle an HTTP request or response. 
They are chained together, with the output of one middleware acting as the input to the next middleware, to form a pipeline.

Understanding how to build and compose middleware is key to adding functionality to your applications.

Middleware can:
- Handle an incoming HTTP request by generating an HTTP response.
- Process an incoming HTTP request, modify it, and pass it on to another piece of middleware.
- Process an outgoing HTTP response, modify it, and pass it on to either another piece of middleware or the ASP.NET Core web server.

The most important piece of middleware in most ASP.NET Core applications is the EndpointMiddleware class. 
This class normally generates all your HTML pages and API responses (for Web API applications).


----------------------------------------------------------------------------
Error Handling:

There is two middleware to handle the error thrown by some part of the App.

- app.UseDeveloperExceptionPage();  to try it in a develoment mode

- app.UseExceptionHandler("/Error");  to be used in Production mode

Its important to avoid use app.UseDeveloperExceptionPage();  in Production mode because some critic data of the App could be leaked to the user.


In app.UseExceptionHandler("/Error");   the path "/Error" is where the page to hand the error is.
When this middleware catches an Error, it redefines the original path request into the "/Error" path and re-excutes the pipeline flow
as this new path was the original request sent by the user.

It´s importan to keep the Error page handler the most simple possible, to avoid to get a new error again.

There are differents middleware to handle the errors. They should be study in details for get the more from them:

 - app.UseDeveloperExceptionPage();   

 - app.UseExceptionHandler("/Error");  

 - app.UseStatusCodePages();

 - app.UseStatusCodePagesWithReExecute("/{0}");


----------------------------------------------------------------------------
Razor pages and MVC design pattern.

As we already mentioned, a Razor page has an associated file having the code behind, which has for example the method
OnGet() to process the GET request for the page when is sent by a user.

Usually this method calls some App Logic class to get the data needed by the page. And at the end of the method usually
we should put the code: return Page(); indicating that the Razor view should be rendered.

This method can declare and fill some public variables, lists that will be available for the view of the page. Example:

public class CategoryModel : PageModel
{
   // service from the App Logic layer which this class receive by Dependency Injection through the constructor.
   private readonly ToDoService _service;

   // list available for the view of the page.
   public List<ToDoListModel> Items { get; set; }

   // constructor with Depedency Injection
   public CategoryModel(ToDoService service) { _service = service; }  

   // method to process the GET request from the user.
   // the category parameter is filled from the Razor page structure by a process called model binding.
   public ActionResult OnGet(string category)
   {
		Items = _service.GetItemsForCategory(category);
		return Page();
   }
}

The page handler is the central controller for the Razor Page. 
It receives an input from the user (the category method parameter), 
calls out to the “brains” of the application (the ToDoService) and passes data (by exposing the Items property) 
to the Razor view, which generates the HTML response. This looks like the Model-View-Controller (MVC) design pattern.

Order of events for MVC pattern in ASP.Net Core:

 1. The controller (the Razor Page handler) receives the request.
 2. Depending on the request, the controller either fetches the requested data from the application model using injected services, 
    or it updates the data that makes up the model.
 3. The controller selects a view to display and passes a representation of the model to it.
 4. The view uses the data contained in the model to generate the UI.

REMEMBER: ASP.NET Core implements Razor Page endpoints using a combination of RoutingMiddleware and EndpointMiddleware.

Routing takes the headers and path of a request and maps it against a preregistered list of patterns. 
These patterns each match a path to a single Razor Page and page handler.

Once a page handler is selected, the binding model (if applicable) is generated. 
This model is built based on the incoming request, the properties of the PageModel marked for binding, 
and the method parameters required by the page handler.

The role of the page handler as the controller in the MVC pattern is to coordinate the generation of a response to the request it’s handling. 
That means it should perform only a limited number of actions:

 - Validate that the data contained in the binding model provided is valid for the request.
 - Invoke the appropriate actions on the application model using services.
 - Select an appropriate response to generate based on the response from the application model.

The key benefit throughout this process is the separation of concerns:
 - The view is responsible only for taking some data and generating HTML.
 - The application model is responsible only for executing the required business logic.
 - The page handler (controller) is responsible only for validating the incoming request.
   and selecting which response is required, based on the output of the application model.

Razor pages is a kind of project built over the "ASP.Net Core MVC framework". Our definition of the file with our Page code behind
is translated into the "ASP.Net Core MVC framework". Although is recommended to work with the Razon Page approach, we can
choose to work with Razor pages project or with "ASP.Net Core MVC framework" project directly.

Also, to develop a Web API project we should work with the "ASP.Net Core MVC framework" project.

----------------------------------------------------------------------------
"ASP.Net Core MVC framework":

Instead of a PageModel and page handler, MVC uses the concept of controllers and action methods.

MVC controllers use explicit view models to pass data to a Razor view, rather than exposing the data as properties on itself 
(as Razor Pages do with page models).

Comparative:

Razor Page									ASP.Net Core MVC Framework
------------------------------				---------------------------------
Page model (code behind)					Controllers and Action Methods
Public properties with access				Explicit view model passing data
from the Razor view page					to the Razor view page.

OnGet() method should be void				The action method returns an ActionResult 
or returns an ActionResult					in a form of: View(viewModel);
in a form of: return Page();				to indicate that the HTML should
to indicate that the HTML should			be rendered.
be rendered.


----------------------------------------------------------------------------
Accepting parameters to page handlers.

The values needed by the page handlers for their work could be passed out as method parameters or as properties
marked with the [BindProperty] attribute. This last of property works for the POST request. Their will be filled by default 
just for POST request. If their are needed for GET request too, it´s necessary to specify by:

[BindProperty(SupportsGet = true)]

IMPORTANT: 
 - It´s not possible to overload methods to have multiple page handdlers on a Razor page with the same name.
 - When an action method uses model-bound properties or parameters, 
   it should always check that the provided model is valid using ModelState.IsValid.
 - Sentence  return Page();   indicates the related page should be rendered when a page handler returns IActionResult.
 - Helper method RedirectToPage() returns the value RedirectToPageResult when redirect to another page is needed.
   This will send a 302 redirect response to the user, indicating to the browser to navigate to the page indicated.
   Example: return RedirectToPage("./Index");
 - OnGet() could return void or IActionResult; whereas OnPost() usually returns IActionResult in order to invoke to render
   the page or to redirect to another page.
 - If we’re returning more than one type of result from a page handler, we’ll need to ensure your method returns an IActionResult.


----------------------------------------------------------------------------
Returning responses with ActionResults.












