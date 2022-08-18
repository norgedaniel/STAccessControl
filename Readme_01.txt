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
Matching arbitrary URLs with the catch-all parameter.

The option of catch-all parameter makes possible to get as a string parameter, the remainder of a URL starting at some position, including the "/" character.

Example:  @page "/{currency}/convert/{**others}"   the string "{**others}" is the catch-all parameter and makes possible to grab the string
after "convert/" as parameters making possible to process the following requests:

/USD/convert/GBP—Show USD with exchange rate to GBP

/USD/convert/GBP/EUR—Show USD with exchange rates to GBP and EUR

/USD/convert/GBP/EUR/CAD—Show USD with exchange rates for GBP, EUR, and CAD

where the amount of currency to convert are variable and separated by the char "/".

Catch-all parameters can be declared using either one or two asterisks inside the parameter definition, like {*others} or {**others}.

Where possible, to avoid confusion, avoid defining route templates with catch-all parameters that overlap other route templates.



-----------------------------------------------------------------------------------
Generating URLs from route parameters.

We can need to do the reverse of the routing process to get the route from the URL request. We could need to build some URL from a routing template.

In our pages model, we have access to the object Url, which has the overloaded method Page() to build the new URL we need. Exmaple:

	var url = Url.Page("Currency/View", new { code = "USD" });

The path to the new Razor page culd be relative as shown into this example, or coudl be absolute starting from the Pages folder: "/Currency/View"

To generate an URL from some MVC controller we need to call the method Action() instead. We should specify the action and controller name
plus any data needed by the controller. Example:

	var url = Url.Action("View", "Currency", new { code = "USD" });	

"View" is the action and "Currency" is te controller. The new URL is "/Currency/View/USD".
    
If the URL we need refers to a Razor Page, use the Page method. If the destination is an MVC action, use the Action method.



-----------------------------------------------------------------------------------
Generating URLs with ActionResults.
 
This is usefull when we need to automatically redirect the user to a new URL. In this case we can use an ActionResult to handle the URL generation instead.
Example:

	public IActionResult OnGetRedirectToPage()
	{
		return RedirectToPage("Currency/View", new { id = 5 });	
	}
 
The RedirectToPage method generates a RedirectToPageResult with the generated URL.

We can use a similar method, RedirectToAction, to automatically redirect to an MVC action instead. 
Just as with the Page and Action methods, it is the destination that controls whether we need to use RedirectToPage or RedirectToAction. 
RedirectToAction is only necessary if we’re using MVC controllers to generate HTML instead of Razor Pages.



-----------------------------------------------------------------------------------
Generating URLs from other parts of our application.

TIP Where possible, try to keep knowledge of the frontend application design out of our business logic. 
This pattern is known generally as the Dependency Inversion principle.

The LinkGenerator class lets us generate URL from our view, this is: out of any Page model or MVC controller handler.
This class is able to update automatically the generated URL if the routes in our application change.

The LinkGenerator class is available in any part of our application, so we can use it inside middleware and any other services. 
We can use it from Razor Pages and MVC too, if we wish, but the IUrlHelper is typically easier and hides some details of using the LinkGenerator.

LinkGenerator has various methods for generating URLs, such as GetPathByPage, GetPathByAction, and GetUriByPage.
It´s recommended keep stick to the IUrlHelper where possible.

LinkGenerator can be accessed using dependency injection. See some example on page 148.



-----------------------------------------------------------------------------------
Selecting a page handler to invoke.

Razor Pages can have multiple handlers, so if the RoutingMiddleware selects a Razor Page, the EndpointMiddleware still needs to know how to choose which handler to execute.

Razor Pages can contain any number of page handlers, but only one runs in response to a given request.

When the EndpointMiddleware executes a selected Razor Page, it selects a page handler to invoke based on two variables:
	- The HTTP verb used in the request (for example GET, POST, or DELETE)
	- The value of the handler route value

From this, the syntax for a Razor page handler is:

		On{verb}[handler][Async]

verb matches with: GET or POST or DELETE.

handler is optional and it can come into the query string, example: /Search?handler=CustomSearch
or it can placed as a parameter into the @page directive at the begining of the page model file:
    @page "{handler}"

Async is optional and it´s to spcify if the corresponding async method should be called.

Then we can have for example the following page handlers:

	- OnGet: Invoked for GET requests that don’t specify a handler value.
	
	- OnPostAsync: Invoked for POST requests that don’t specify a handler value. 
	               Returns a Task, so it uses the Async suffix, which is ignored for routing purposes.
	
	- OnPostCustomSearch: Invoked for POST requests that specify a handler value of "CustomSearch".

But what happens if we get a request that doesn’t match any of our page handlers ?

If a page handler does not match a request’s HTTP verb and handler value, an implicit page handler is executed that renders the associated Razor view. 
Implicit page handlers take part in model binding and use page filters but execute no logic.


-----------------------------------------------------------------------------------
Customizing conventions with Razor Pages.

It could be good to persnolize the conventions of ASP.Net Core in order to ensure the URL be always un lowecase, ending with a "/".
We can do this by set some values for the object RouteOptions into out services object of the Startup class:

	public void ConfigureServices(IServiceCollection services)
	{
		services.AddRazorPages();

		services.Configure<RouteOptions>(options =>
		{
			options.AppendTrailingSlash = true;
			options.LowercaseUrls = true;
			options.LowercaseQueryStrings = true;
		});

	}




-----------------------------------------------------------------------------------
The Binding Model. Retrieving and validating user input.

- Using request values to create binding models.
- Customizing the model-binding process.
- Validating user input using DataAnnotations attributes.

The Binding Model stands for retreive the parameters from the user request and then they can be used in our Razor Pages.

We’ll see how to take the data posted in the form or in the URL and bind them to C# objects. 
These objects are passed to our Razor Page handlers as method parameters or are set as properties on our Razor Page PageModel

Another part of the Binding Model is to ensure the data that come from the user are valid and make sense for the Application.

We can think of the binding models as the input to a Razor Page, taking the user’s raw HTTP request and making it available to our code 
by populating “plain old CLR objects” (POCOs).


-----------------------------------------------------------------------------------
Understanding the models in Razor Pages and MVC.

When the routing middleware receive some user request, it try to route it to the right razor page setting the route parameters got from the URL.

The route parameters parsed from the request are used to build a binding model, setting the binding model parameters throught a custom POCO class.

After binding, the model is validated to check it has acceptable values. The result of the validation is stored in ModelState.

The binding model custom class along with the ModelState are set as properties on the corresponding PageModel.
The binding model could also be passed as a parameter to the page handler.

IMPORTANT: ASP.NET Core Razor Pages uses several different models, most of which are POCOs, and the application model, which is more of a concept around a collection of services.
Each of the models in ASP.NET Core is responsible for handling a different aspect of the overall request:

- Binding model: is all the information that’s provided by the user when making a request, as well as additional contextual data. 
                 This includes things like route parameters parsed from the URL, the query string, and form or JSON data in the request body.
				 The binding model itself is one or more POCO objects that we define. 
				 Binding models in Razor Pages are typically defined by creating a public property on the page’s PageModel 
				 and decorating it with the [BindProperty] attribute. They can also be passed to a page handler as parameters.
				 The Razor Pages infrastructure inspects the binding model before the page handler executes to check whether the provided values are valid, 
				 though the page handler will execute even if they’re not.

- Application model: It’s typically a whole group of different services and classes and is more of a concept. 
                     Anything needed to perform some sort of business action in our application.
					 It may include the domain model (which represents the thing our app is trying to describe) 
					 and database models (which represent the data stored in a database), as well as any other, additional services.

- Page model: The PageModel of a Razor Page serves two main functions: it acts as the controller for the application by exposing page handler methods, 
              and it acts as the view model for a Razor view.
			  The PageModel base class that we derive our Razor Pages from contains various helper properties and methods. 
			  One of these, the ModelState property, contains the result of the model validation as a series of key-value pairs. 

These three models make up the bulk of any Razor Pages application, handling the input, business logic, and output of each page handler.

The important point about all these models is that their responsibilities are well defined and distinct. 
Keeping them separate and avoiding reuse helps to ensure our application stays agile and easy to update.

The obvious exception to this separation is the PageModel, as it is where the binding models and page handlers are defined, 
and it also holds the data required for rendering the view.



-----------------------------------------------------------------------------------
From request to model: Making the request useful.

	- How ASP.NET Core creates binding models from a request.
	- How to bind simple types, like int and string, as well as complex classes.
	- How to choose which parts of a request are used in the binding model.

Page handlers are normal C# methods, so the ASP.NET Core framework needs to be able to call them in the usual way.

When page handlers accept parameters as part of their method signature, the framework needs a way to generate those objects. 

The model binding comes in the way of how ASP.NET Core turn the HTTP request string into a .NET objects.
These objects are passed as method parameters to the page handler being executed 
or are set as properties of the PageModel that are marked with the [BindProperty] attribute.

The model binder is responsible for looking through the request that comes in and finding values to use. 
It then creates objects of the appropriate type and assigns these values to our model in a process called binding.

Model binding in Razor Pages and MVC is a one-way population of objects from the request.

For Security reason Model binding will try to fill automatically any parameter received by the page handler methods 
and the properties decorated with the [BindProperty] attribute when a POST or PUT request is received.

For GET request we need to specify it into the data annotation: [BindProperty(SupportsGet = true)]

It´s recommended to keep all the model binding in a single, nested class, which can be called InputModel. Example:

	public class IndexModel: PageModel
	{
		[BindProperty]
		public InputModel Input { get; set; }

		public void OnGet()
		{
		}

		public class InputModel
		{
			public string Category { get; set; }
			public string Username { get; set; }
			public ProductModel Model { get; set; }
		}
	}

ASP.NET Core automatically populates our binding models for us using properties of the request, such as the request URL, 
any headers sent in the HTTP request, any data explicitly POSTed in the request body.

By default, ASP.NET Core uses three different binding sources when creating our binding models. 
It looks through each of these in order and takes the first value it finds (if any) that matches the name of the binding model:

	- Form values—Sent in the body of an HTTP request when a form is sent to the server using a POST.

	- Route values—Obtained from URL segments or through default values after 	matching a route.

	- Query string values—Passed at the end of the URL, not used during routing.

The model binder checks each binding source to see if it contains a value that could be set on the model. 
Alternatively, the model can also choose the specific source the value should come from. 
Once each property is bound, the model is validated and is set as a property on the PageModel or is passed as a parameter to the page handler.

Setting properties on the PageModel and marking them with [BindProperty] is the approach we’ll see most often in examples. 
If we use this approach, we’ll be able to access the binding model when the view is rendered.

Adding parameters to page handler methods, provides more separation between the different MVC stages, 
because we won’t be able to access the parameters outside of the page handler. On the downside, if we do need to display those values in the Razor view, 
we’ll have to manually copy the parameters across to properties that can be accessed in the view.

If I’m creating a form, I will favor the [BindProperty] approach, as I typically need access to the request values inside the Razor view.

For simple pages, where the binding model is a product ID for example, I tend to favor the page handler parameter approach for its simplicity, 
especially if the handler is for a GET request.

The big advantage of using model binding is that we don’t have to write the code to parse requests and map the data ourself. 

Model binding is great for reducing repetitive code. 
Take advantage of it whenever possible and we’ll rarely find ourself having to access the Request object directly.

The key thing to appreciate is that we didn’t have to write any extra code to try to extract the number from the URL when the method executed. 
All we needed to do was create a method parameter (or public property) with the right name and let model binding do its magic.

Each of the binding sources (forms, route values, query string) store values as name-value pairs. 
If none of the binding sources contain the required value, the binding model is set to a new, default instance of the type instead.

IMPORTANT:

	- To consider the behavior of our page handler when model binding fails to bind our method parameters. 
	  If none of the binding sources contain the value, the value passed to the method could be null or could unexpectedly have a default value (for value types).
        
	- To consider the order which the binding sources are consulted to produce the binding, specially when we work with more than property binded.

	- The default model binder isn’t case sensitive, so a binding value of QTY=50 will happily bind to the qty parameter.

	- All the values in any of the binding sources comes as string. The model binder will convert pretty much any primitive .NET type 
	  such as int, float, decimal (and string obviously), plus anything that has a TypeConverter.
	  The model binder will convert complex types by traversing any properties our binding models expose.

	- For a class to be model-bound, it must have a default public constructor. We can only bind properties that are public and settable.

	- As long as each property exposes a type that can be modelbound, the binder can traverse it with ease. 
	  This way we can bind complex hierarchical models whose properties are themselves complex models.

	- It´s possible to bind collections and dictionaries too.

	- ASP.NET Core supports uploading files by exposing the IFormFile interface, either as a method parameter to our page handler, or using the [BindProperty] approach.
	  Example: public void OnPost(IFormFile file);
	  We can also use an IEnumerable<IFormFile> if we need to accept multiple files: public void OnPost(IEnumerable<IFormFile> file);
	  The use of IFormFile is well when the user will be upload small files. 
	  The whole content of the file is buffered in memory and on disk before we receive it. Better yet, avoid file uploads entirely!


Example. Let´s say we have a Razor page to convert some amount of money from one currency into another. 
We could customize the route template throught the @page directive and program the OnGet page handler to receive the three needed parameters:

	@page "/{currencyIn}/{currencyOut}"

	public class ConvertModel : PageModel
	{
		public void OnGet(string currencyIn, string currencyOut, int qty)
		{
			/* method implementation */
		}
	}

From this example several different request can come:

	URL (route values)						HTTP body data (form values)					Parameter values bound
	-----------------------------------		----------------------------------------		---------------------------------------------
	/GBP/USD																				currencyIn=GBP
																							currencyOut=USD qty=0

	/GBP/USD?currencyIn=CAD					QTY=50											currencyIn=GBP
																							currencyOut=USD qty=50

	/GBP/USD?qty=100						qty=50											currencyIn=GBP
																							currencyOut=USD qty=50

	/GBP/USD?qty=100						currencyIn=CAD&currencyOut=EUR&qty=50			currencyIn=CAD
																							currencyOut=EUR qty=50

It’s more common to have our values all come from the request body as form values, maybe with an ID from URL route values.

IMPORTANT: Related to some user uploaded file. Never use posted filenames in our code. 
		   Users can use them to attack our website and access files they shouldn’t be able to.


-------------------------------------------------------------------------
Choosing a binding source.

We have the choice to specify where some of our properties binding come from. We can decorate those attributes with data annotations like this:

	public class PhotosModel: PageModel
	{
		public void OnPost(
			[FromHeader] string userId,
			[FromBody] List<Photo> photos)
		{
			/* method implementation */
		}
	}

By default the [FromBody] points to read JSON from the body of the request. 
But we can use other formats too, depending on which InputFormatters we configure the framework to use.

The binding annotations we can use are:

	- [FromHeader]	Bind to a header value
	- [FromQuery]	Bind to a query string value
	- [FromRoute]	Bind to route parameters
	- [FromForm]	Bind to form data posted in the body of the request
	- [FromBody]	Bind to the request’s body content. Only one attribute can be decorated with this.

Only one parameter may use the [FromBody] attribute. This attribute will consume the incoming request as HTTP request bodies can only be safely read once. 

The [FromBody] and [FromForm] attributes are effectively mutually exclusive.

There are annotations to spacify also the kind of processing for binding:

	- [BindNever]		The model binder will skip this parameter completely.
	- [BindRequired]	If the parameter was not provided, or was empty, the binder will add a validation error.
	- [FromServices]	This is used to indicate the parameter should be provided using dependency injection.


-----------------------------------------------------------------------------
Handling user input with model validation.

	- What is validation, and why do we need it?
	- Using DataAnnotations attributes to describe the data we expect
	- How to validate our binding models in page handlers

We should always validate data provided by users before we use it in our methods. 

Validation is needed to check for non-malicious errors:

	- Data should be formatted correctly (email fields have a valid email format).
	- Numbers might need to be in a particular range (we can’t buy -1 copies of some book!).
	- Some values may be required but others are optional (name may be required for a profile but phone number is optional).
	- Values must conform to our business requirements (we can’t convert a currency to itself, it needs to be converted to a different currency).



-----------------------------------------------------------------------------
Using DataAnnotations attributes for validation.

Validation attributes, or more precisely DataAnnotations attributes, allow us to specify the rules that our binding model should conform to. 
They provide metadata about our model by describing the sort of data the binding model should contain.

The great thing about these attributes is that they clearly declare the expected state of the model. 
By looking at these attributes, we know what the properties will, or should, contain. 
They also provide hooks for the ASP.NET Core framework to validate that the data set on the model during model binding is valid.

We can find a lot of DataAnnotations to apply to our model into the System.ComponentModel.DataAnnotations namespace. Someones of more used are:

	- [CreditCard]					Validates that a property has a valid credit card format.
	- [EmailAddress]				Validates that a property has a valid email address format.
	- [StringLength(max)]			Validates that a string has at most max number of characters.
	- [MinLength(min)]				Validates that a collection has at least the min number of items.
	- [Phone]						Validates that a property has a valid phone number format.
	- [Range(min, max)]				Validates that a property has a value between min and max.
	- [RegularExpression(regex)]	Validates that a property conforms to the regex regular expression pattern.
	- [Url]							Validates that a property has a valid URL format.
	- [Required]					Indicates the property must not be null.
	- [Compare]						Allows us to confirm that two properties have the same value (for example, Email and ConfirmEmail).

Another way to implement our data validation is the use of the popular FluentValidation library.

IMPORTANT: DataAnnotations are good for input validation of properties in isolation, but not so good for validating business rules. 
           We’ll most likely need to perform this validation outside the DataAnnotations framework.


-----------------------------------------------------------------------------
Validating on the server for safety.

Validation of the binding model occurs before the page handler executes, but note that the handler always executes, whether the validation failed or succeeded. 
It’s the responsibility of the page handler to check the result of the validation.

Validation happens automatically, but handling validation failures is the responsibility of the page handler.

The Razor Pages framework stores the output of the validation attempt in a property on the PageModel called ModelState.
This property is a ModelStateDictionary object which contains a list of all the validation errors that occurred after model binding, 
as well as some utility properties for working with it.

Example of use of ModelState:

	public IActionResult OnPost()
	{
		if (!ModelState.IsValid)
		{
			return Page();
		}
	
		/* Save to the database, update user, return success */
	
		return RedirectToPage("Success");
	
	}

If the ModelState property indicates an error occurred there are messages related to these errors into the ModelState object.
These error messages can be customized when we set the corresponding DataAnnotation. Example:
	[Required(ErrorMessage="The Name is required.")]



-----------------------------------------------------------------------------
-----------------------------------------------------------------------------
IMPORTANT: POST-REDIRECT-GET pattern.

The POST-REDIRECT-GET design pattern is a web development pattern that prevents users from accidently submitting the same form multiple times. 
Users typically submit a form using the standard browser POST mechanism, sending data to the server.
This is the normal way by which we might take a payment, for example.

If a server takes the naive approach and responds with a 200 OK response and some HTML to display, the user will still be on the same URL. 
If the user then refreshes their browser, they will be making an additional POST to the server, potentially making another payment!

The POST-REDIRECT-GET pattern says that in response to a successful POST, we should return a REDIRECT response to a new URL, which will be followed by the
browser making a GET to the new URL. If the user refreshes their browser now, they’ll be refreshing the final GET call to the new URL. 
No additional POST is made, so no additional payments or side effects should occur.

To implement this design pattern we can use RedirectToPage("Success");  in Razor pages   or  RedirectToPageResult() in MVC projects.

-----------------------------------------------------------------------------
-----------------------------------------------------------------------------

For Razor pages is used to let the request handler to decide what to do when validation fails. 
This allows the user to see the problem and potentially correct it. 

For Web API projects is better the response for a validation error ocurrs automatically, since there are no direct interaction between the user and the application.

Also, by including the IsValid check explicitly in our page handlers, it’s easier to control what happens when additional validation checks fail. 
For example, if the user tries to update a product, the DataAnnotations validation won’t know whether a product with the requested ID exists, 
only whether the ID has the correct format. By moving the validation to the handler method, we can treat data and business rule validation failures in the same way.



-----------------------------------------------------------------------------
Validating on the client for user experience.

There are some DataAnnotations attributes to use with out properties model which instruct the Razon engine 
to generate the appropiate HTML for make some validation on the browser client side. 
With this approach, the user sees any errors with their form immediately, even before the request is sent to the server.

When we use Razor Pages to generate our HTML, we get much of this validation for free. 
It automatically configures client-side validation for most of the built-in attributes without requiring additional work.

Unfortunately, if we’ve used custom ValidationAttributes, these will only run on the server by default; 
we need to do some additional wiring up of the attribute to make it work on the client side too.



-----------------------------------------------------------------------------
Organizing our binding models in Razor Pages.

There is an example in page 184 of the book about a good pattern for model binding for a lage to update a record of some entity of our model.

This Razor Page displays a form for a product with a given ID and allows us to edit the details using a POST request.

- The page model receives a ProductService injected using DI which provides access to the application model.

- Only bind a single property with [BindProperty]. It´s good having a single property decorated with [BindProperty] for model binding in general. 
  When more than one value needs to be bound, a separate class is created: InputModel, to hold the values. That single property is decorated with [BindProperty].
  Decorating a single property like this makes it harder to forget to add the attribute, and it means all of our Razor Pages use the same pattern.

- Define our binding model as a nested class inside the Razor Page. It´s usually and always named: InputModel.
  The binding model is normally highly specific to that single page, so doing this keeps everything we’re working on together.
  Again, this adds consistency to our Razor Pages

- The id parameter is modelbound from the route template for both OnGet and OnPost handlers.

- We do not use [BindProperties] that can be applied to the Razor Page PageModel directly, causing all properties in our model to be model-bound, 
  which can leave us open to over-posting attacks if we’re not careful. 
  It´s better avoid the use of [BindProperties] and stick to binding a single property with [BindProperty] instead.

- We accept route parameters in the page handler. 
  For simple route parameters, such as the id passed into the OnGet and OnPost handlers we add parameters to the page handler method itself.
  This avoids the clunky SupportsGet=true syntax for GET requests.

- We always validate before using data.


public class EditProductModel : PageModel
{
	
	// service to be injected by DI for access to our applicaiotn model
	private readonly ProductService _productService;
	
	// constructor used by DI.
	public EditProductModel(ProductService productService)
	{
		_productService = productService;
	}


	// A single property is marked with BindProperty
	[BindProperty]
	public InputModel Input { get; set; }


	// id parameter is modelbound from the route template
	public IActionResult OnGet(int id)
	{
		// Load the product details from the application model.
		var product = _productService.GetProduct(id);

		// Build an instance of the InputModel for editing in the form from the existing product’s details.
		// this object is the model used by view to render the HTML for user.
		Input = new InputModel
		{
			Name = product.ProductName,
			Price = product.SellPrice,
		};
	
		return Page();

	}


	// id parameter is modelbound from the route template
	public IActionResult OnPost(int id)
	{
		// If the request was not valid, redisplay the form without saving.
		if (!ModelState.IsValid)
		{
			return Page();
		}

		// Update the product in the application model using the ProductService.
		_productService.UpdateProduct(id, Input.Name, Input.Price);

		// Redirect to a new page using the POST-REDIRECTGET pattern.
		return RedirectToPage("Index");

	}

	// Define the InputModel as a nested class in the Razor Page. This is the class for [BindProperty] annotation
	public class InputModel
	{

		[Required]
		public string Name { get; set; }

		[Range(0, int.MaxValue)]
		public decimal Price { get; set; }

	}

}

As summary:

- ASP.NET Core framework uses model binding to simplify the process of extracting values from a request and turning them into normal .NET objects we can quickly work with. 

- The most important aspect of this chapter is the focus on validation and the use of DataAnnotations can make it easy to add validation to our models.

- Razor Pages uses three distinct models, each responsible for a different aspect of a request. The binding model encapsulates data sent as part of a request. 
  The application model represents the state of the application. 
  The PageModel is the backing class for the Razor Page, and it exposes the data used by the Razor view to generate a response.

- Model binding extracts values from a request and uses them to create .NET objects the page handler can use when they execute.

- Any properties on the PageModel marked with the [BindProperty] attribute, and method parameters of the page handlers, will take part in model binding.

- Properties decorated with [BindProperty] are not bound for GET requests. To bind GET requests, we must use [BindProperty(SupportsGet = true)] instead.

- By default, there are three binding sources: POSTed form values, route values, and the query string. 
  The binder will interrogate these in order when trying to bind our binding models.

- When binding values to models, the names of the parameters and properties aren’t case sensitive.

- We can bind to simple types or to the properties of complex types.

- To bind complex types, they must have a default constructor and public, settable properties.

- Simple types must be convertible to strings to be bound automatically; for example, numbers, dates, and Boolean values.

- Collections and dictionaries can be bound using the [index]=value and [key]=value syntax, respectively.

- We can customize the binding source for a binding model using [From*] attributes applied to the method, such as [FromHeader] and [FromBody]. 
  These can be used to bind to nondefault binding sources, such as headers or JSON body content.

- In contrast to the previous version of ASP.NET, the [FromBody] attribute is required when binding JSON properties (previously it was not required).

- Validation is necessary to check for security threats. 
  Check that data is formatted correctly and confirm that it conforms to expected values and that it meets our business rules.

- ASP.NET Core provides DataAnnotations attributes to allow us to declaratively define the expected values.

- Validation occurs automatically after model binding, but we must manually check the result of the validation and act accordingly in our page handler 
  by interrogating the ModelState property.

- Client-side validation provides a better user experience than server-side validation alone, but we should always use server-side validation.

- Client-side validation uses JavaScript and attributes applied to our HTML elements to validate form values.

