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

Understanding how to build and compose middleware is key to adding functionality to our applications.

Middleware can:
- Handle an incoming HTTP request by generating an HTTP response.
- Process an incoming HTTP request, modify it, and pass it on to another piece of middleware.
- Process an outgoing HTTP response, modify it, and pass it on to either another piece of middleware or the ASP.NET Core web server.

The most important piece of middleware in most ASP.NET Core applications is the EndpointMiddleware class. 
This class normally generates all our HTML pages and API responses (for Web API applications).


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
 - If we’re returning more than one type of result from a page handler, we’ll need to ensure our method returns an IActionResult.


----------------------------------------------------------------------------
Returning responses with ActionResults.

There are different types to return as ActionResults. The most used are:
 - PageResult for ask Razor page to render the correspond HTML.
 - ViewResult, the same for MVC approach App. 
 - NotFoundResult to send a raw 404 HTTP status code.
 - ContentResult to send some string as response. Useful for Web API App, to send JSON.
 - RedirectToPageResult to send a 302 HTTP redirect and instruct the client browser to go to another URL.
 - RedirectResult: the same as RedirectToPageResult, but the URL does not to be a Razor page.


----------------------------------------------------------------------------
Routing.

Routing in ASP.NET Core is the process of mapping an incoming HTTP request to a specific handler. 
In Razor Pages, the handler is a page handler method in a Razor Page. In MVC, the handler is an action method in a controller.

With Routing instead of take the query string to build a dynamic model binding, it decouples the URL into the Razor page to be call and the parameters to be filled:

Example of using classic query string approach: products?name=simple-widget    -->  select products.cshtml  and parameter: name="simple-widget"

Example of using Routing decoupling:  products/simple-widget  -->   select products.cshtml  and parameter:  {name}="simple-widget" 


There are two types or routing available in ASP.NET Core: "convention-based routing" and "attribute routing".

There are two middlewares to take care of routing: EndpointMiddleware  and  EndpointRoutingMiddleware

  - EndpointMiddleware: registers the endpoint into the routing configuration.

  - EndpointRoutingMiddleware: chooses which of the endpoints registered by the EndpointMiddleware should execute for a given request at runtime.

An endpoint in ASP.NET Core is some handler that returns a response. Each endpoint is associated with a URL pattern.

Razor Page handlers and MVC controller action methods typically make up the bulk of the endpoints in an application.

We can automatically register all the Razor Pages in our application using extensions such as MapRazorPages().

Additionally, we can register other endpoints explicitly using methods such as MapGet().

This some example to configure and set the Routing in an App:

// from Startup class
public void Configure(IApplicationBuilder app)
{
	// add the routing middleware to the pipeline
	app.UseRouting();

	// set some endpoints into the routing middleware providing some lambda configuration.
	app.UseEndpoints(endpoints =>
    {
		endpoints.MapRazorPages();				// setting automatically all our Razor pages.
		endpoints.MapHealthChecks("/healthz");	// set some health-check endpoint.
	}

	// Register an endpoint inline that returns “Hello World!” at the route /test.
	endpoints.MapGet("/test", async context =>
	{
		await context.Response.WriteAsync("Hello World!");
	}
}

Each endpoint is associated with a route template that defines which URLs the endpoint should match. 

A route template is a URL pattern that is used to match against request URLs. They’re strings of fixed values, like "/test" in the previous listing. 
They can also contain placeholders for variables.

The process:

When App starts, the EndpointMiddleware stores the registered routes and the corresponding endpoints in a dictionary. Examples:

	Route template						Razor page
	----------------------				-------------------------
	start-checkout						/Checkout/Start.cshtml
	Privacy								/Privacy.cshtml
	Index								/Index.cshtml

Then when a request arrives, the routing middleware checks it againts the route templates in order to find a Razor page to fill the request.

If some Razor page is found, the routing middleware records its name into the request HTTPContext. The next middleware can view which endpoint has been selected.

Finally the endpoint middleware executes the selected razor page. If there is no page to cover the request, the Dummy middleware runs and return the raw 404 code.

There is some important issue here: Middlewares placed before the Routing middleware will never know about the selected endpoint to attend the request. 
Conversely middlewares after the Routing middleware do will know it. Because of this, it´s important to put the AuthorizationMiddleware between the RoutingMiddleware
and the EndpointMiddleware. This is because the AuthorizationMiddleware could be to check about some required permissions the user could have in order to access
the razor page.

------------------------------------------------------------------------------
Convention-based routing vs. attribute routing.

Which approach we use will typically depend on whether we’re using Razor Pages or MVC controllers, and whether we’re building an API or a website (using HTML).

Convention-based routing is defined globally for our application and it´s most for App using MVC controllers to generate HTML. 
The downside is this approach makes more difficult the customizing some URL for a subset of controllers and actions. This is, the customizing is more hard to do it.

Throught the attribute-based routes allows more flexibility, as we can explicitly define what the URL for each action method should be. 
This method requires to place [Route] attributes on the action methods themselves. 
Despite this, the additional flexibility it provides can be very useful, especially when building Web APIs.

There is a third method, mixing these two methods and it is most usefull for Razor page projects.

Route templates define the structure of known URLs in our application. They’re strings with placeholders for variables that can contain optional values.

A single route template can match many different URLs. For example, the /customer/1 and /customer/2 URLs would both be matched by the "customer/{id}".

The route template syntax is powerful and contains many different features that are controlled by splitting a URL into multiple segments.

A segment is a small contiguous section of a URL. It’s separated from other URL segments by at least one character, often by the / character.

Routing involves matching the segments of a URL to a route template.

For each route template, we can define:
- Specific, expected strings
- Variable segments of the URL
- Optional segments of a URL
- Default values when an optional segment isn’t provided
- Constraints on segments of a URL, such as ensuring that it’s numeric

Alhough we’ll often only use one or two features here and there. 
For the most part, the default convention-based attribute route templates generated by Razor Pages will be all we need. 


-----------------------------------------------------------------------------------
Routing to Razor Pages.

Razor Pages uses attribute routing by creating route templates based on conventions. 
ASP.NET Core creates a route template for every Razor Page in our app during app startup, when we call MapRazorPages in the Configure method of Startup.cs.

Route templates are based on the file path relative to the Razor Pages root directory. 
Examples:  
			Pages/Products/View.cshtml   page has a route template of   Products/View
			Pages/Search/Products/StartSearch.cshtml   page has a route template of    Search/Products/StartSearch
			Pages/Privacy.cshtml    page has a route template of   Privacy

Steps:

 1- Requests to the URL /products/view match the route template "Products/View" which in turn corresponds to the View.cshtml Razor Page. 
 2- The RoutingMiddleware selects the View.cshtml Razor Page as the endpoint for the request. 
 3- The EndpointMiddleware executes the page’s handler once the request reaches it in the middleware pipeline.

IMPORTANT: Routing is not case sensitive, so the request URL does not need to have the same URL casing as the route template to match.


-----------------------------------------------------------------------------------
Customizing Razor Page route templates.

The routing middleware parses a route template by splitting it into a number of segments.
A segment is typically separated by the / character, but it can be any valid character. 
Each segment is either:  a literal value  or  a route parameter.
Example:  
			product/{category}/{name}

			product is a literal value

			{category} and {name}  are route parameters

Literal values must be matched exactly (ignoring case) by the request URL. Literal segments in ASP.NET Core are not case-sensitive.

Route parameters are sections of a URL that may vary but will still be a match for the template.
They are defined by giving them a name and placing them in braces, such as {category} or {name}. 
When used in this way, the parameters are required, so there must be a segment in the request URL that they correspond to, but the value can vary.

The ability to use route parameters gives us great flexibility. 
For example, the simple route template "{category}/{name}" could be used to match all the product-page URLs in an e-commerce application, such as:

	/bags/rucksack-a—   where category=bags   and   name=rucksack-a
    /shoes/black-size9  where category=shoes  and   name=black-size9

When a route template defines a route parameter, and the route matches a URL, the value associated with the parameter is captured and stored in a dictionary of values
associated with the request. These route values typically drive other behavior in the Razor Page, such as model binding.

Route values are the values extracted from a URL based on a given route template. 
Each route parameter in a template will have an associated route value and they are stored as a string pair in a dictionary. 
They can be used during model binding.


-----------------------------------------------------------------------------------
Adding a segment to a Razor Page route template.

To customize the Razor Page route template, we update the @page directive at the top of the Razor Page’s .cshtml file.

IMPORTANT: We must include the @page directive at the top of a Razor Page’s .cshtml file. 
           Without it, ASP.NET Core will not treat the file as a Razor Page, and we will not be able to view the page.

We can add a blanck space + some string to customize the Razor Page route template. 
Example: @page "Extra" set into the  Pages/Privacy.cshtml  cutomize the route to this page into "Privacy/Extra". Now the page will respond to this URL.

But the most use for this personalization of the route template is to add some route parameters.
Example: @page "{category}/{name}"    this will add those two parameters to the base razor page.


-----------------------------------------------------------------------------------
Replacing a Razor Page route template completely.

Some time is not enough the possibility of customize the route template adding parameters. Some time we need to replace the current route template.
To do this, just customize the @page directive, but starting with character "/".
Examples:  @page "/{category}/{name}" 

           @page "/checkout"      
		   in this case wherever we place our checkout Razor Page within the Pages folder, 
		   using this directive ensures it always has the route template "checkout", so it will always match the request URL /checkout.

This way we can think of custom route templates that start with "/" as absolute route templates, 
while other route templates are relative to their location in the file hierarchy.

Besides of this customize options, route templates have extra features that give we more control over our application’s URLs.
These features let us have optional URL segments, provide default values when a segment isn’t specified, 
or place additional constraints on the values that are valid for a given route parameter.


-----------------------------------------------------------------------------------
Using optional and default values.

Example:  product/{category}/{name=all}/{id?}

Here we have the same literal product segment and the required {category} parameter.

The {name=all} parameter is optional with default value. If the request does not send the value for name, it will take the value of all. 
The parameter always will be available for the controller.

The {id?} defines an optional parameter without default value. If the request does not send the value for id, 
the paramater will no be set and will no be available for the controller.

We can specify any number of route parameters in our templates, and these values will be available to we when it comes to model binding.

There’s no way to specify a value for the optional {id} parameter without also specifying the {category} and {name} parameters. 
We can only put an optional parameter (that doesn’t have a default) at the end of a route template. 


-----------------------------------------------------------------------------------
Adding additional constraints to route parameters.

We can add some constraints to out route parameters. We should use a char ":"   Example: {id:int} defines this parameter only accept an integer value.

We can also check more advanced constraints, for example, that an integer value has a particular minimum value, or that a string value has a maximum length. 

Other examples: {age:min(18)}  --> Matches integer values of 18 or greater.
				{name:length(6)}  --> Matches string values with a length of 6.
				{qty:int:max(10)?}  --> Optionally matches any integer of 10 or less.

We can combine multiple constraints by separating the constraints with colons ":", like the last example: {qty:int:max(10)?} 

IMPORTANT: Search more possible constraints available at Microsoft: http://mng.bz/xmae or 
https://docs.microsoft.com/en-us/aspnet/core/fundamentals/routing?view=aspnetcore-6.0#route-constraint-reference

If we have a well-designed set of URLs for our application, we will probably find that we don’t really need to use route constraints. 
Route constraints really come in useful when we have “overlapping” route templates.

The most of time, Attribute routing (used by Razor Pages and MVC controllers for building APIs) allows us to explicitly control the order the routing middleware looks 
at our route templates and no reask of confusing and mixed route appear. But, if we find ourself needing to manually control the order, 
this is a very strong indicator that our URLs are confusing.

If our route templates are well defined, such that each URL only maps to a single template, ASP.NET Core routing will work without any difficulties. 
Sticking to the builtin conventions as far as possible is the best way to stay on the happy path!



-----------------------------------------------------------------------------------
IMPORTANT:

It´s pending to finish Chapter 5 from Part 1. Starting on 5.4.3 Matching arbitrary URLs with the catch-all parameter.

Pending also Chapter 6: The Binding Model. Retrieving and validating user input.

