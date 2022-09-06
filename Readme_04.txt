-----------------------------------------------------------------------------------------
Creating a Web API for mobile and client applications using MVC.

Web API is a server-side service that send data to a client side request, instead of HTML. This data usually go in JSON or XML format.
The client side can build HTML pages using the data received. This kind of App is known as SPA ( Client-side single-page application ).


A Web API exposes multiple URLs that client applications can send requests to and retrieve data from. 
These server endpoints can also be used to access and change data on a server. It’s typically accessed using HTTP.

NOTE: Although there has been an industry shift toward client-side frameworks, server-side rendering using Razor is still relevant. 
      Which approach we choose will depend largely on our preference for building HTML applications in the traditional manner versus using JavaScript on the client.
      In many cases, the best approach is build a tradicional Web App Server and adding later Web APIs capabilities.

React, Angular, Vue.js, Blazor are some of the most popular client side framework to build SPA.


---------------------------------------------------------------------------------------
Creating our first Web API project.

We need to learn how to create an ASP.NET Core Web API project and create our first Web API controllers. 
How to use controller action methods to handle HTTP requests, and how to use ActionResults to generate a response.

Web APIs are normally accessed from code by SPAs or mobile apps, but by accessing the URL in our web browser directly, we can view the data the API is returning.

ASP.NET Core 5.0 apps also include a useful endpoint for testing and exploring our Web API project in development called Swagger UI, as shown in figure 9.4. 
This lets us browse the endpoints in our application, view the expected responses, and experiment by sending requests.

A Web API project uses AddControllers() instead of AddRazorPages() in the ConfigureServices method. 
Also, the API controllers are added by calling MapControllers() in the UseEndpoints method. 

The default Web API template also adds the Swagger services and endpoints required by the Swagger UI. 
This tool provides a convenient web page for exploring and interacting with our Web API. 
By default, this UI is only enabled in development, but we can also enable it in production.

Example of Startup class for Web API project:

    public class Startup
    {

        public void ConfigureServices(IServiceCollection services)
        {
            // AddControllers adds the necessary services for API controllers to our application.
            services.AddControllers();
            ......
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            ......
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); } );

        }

    }

This Startup.cs file instructs our application to find all API controllers in our application and to configure them in the EndpointMiddleware. 
Each action method becomes an endpoint and can receive requests when the RoutingMiddleware maps an incoming URL to the action method.

NOTE We don’t have to use separate projects for Razor Pages and Web API controllers, but I prefer to do so where possible. 
There are certain aspects (such as error handling and authentication) that are made easier by taking this approach.

We can add a Web API controller to our project by creating a new .cs file anywhere in our project. 
Traditionally these are placed in a folder called Controllers, but that’s not a technical requirement. Following an example of Web API controller:

    // Web API controllers typically use the [ApiController] attribute to opt in to common conventions.
    // The ControllerBase class provides several useful functions for creating IActionResults.
    [ApiController]
    public class FruitController : ControllerBase
    {
        // The [HttpGet] attribute defines the route template used to call the action. In this case the route is: /fruit for a GET method request.
        // The name of the action method, Index, isn’t used for routing. It can be anything we like
        [HttpGet("fruit")]
        public IEnumerable<string> Index()
        {
            List<string> _fruit = _service.GetFruitList(); // we get from the App model, the data to response the user request.
            return _fruit;
        }
    }

Web APIs typically use the [ApiController] attribute on API controllers and derive from the ControllerBase class. 
The base class provides several helper methods for generating results, and the [ApiController] attribute automatically applies some common conventions.

In this example we return to the user, a list of string, which is in compliance of Web API definition. Usually data are sent in JSON format plus a "200 OK" code.
ASP.NET Core formats data as JSON by default. This is often do by returning an ActionResult.

Other times, we just want to return a raw HTTP status code, indicating whether the request was successful. 
For example, if an API call is made requesting details of a product that does not exist, we might want to return a 404 Not Found status code.

API controllers use model binding too to bind action method parameters to the incoming request.
The only difference is that there isn’t a PageModel with [BindProperty] properties. We can only bind to action method parameters.

Model binding and validation work in exactly the same way as for Razor Pages: we can bind the request to simple primitives, as well as to complex C# objects. 

Here an example of Web API action returning IActionResult to handle error conditions:

    // Defining the route template for the action method
    // The action method returns an ActionResult<string>, so it can return a string or an ActionResult.
    [HttpGet("fruit/{id}")]
    public ActionResult<string> View(int id)
    {
        if (id >= 0 && id < _fruit.Count)
        {
            // Returning the data directly will return the data with a 200 status code.
            // We could also return the response as: Ok(_fruit[id]), using the Ok helper method on the ControllerBase class.
            return _fruit[id];
        }
        
        // NotFound returns a NotFoundResult, which will send a 404 status code.
        return NotFound();

    }

Data returned from an action method is serialized into the response body, and it generates a response with status code 200 OK.

If the id is outside the bounds of the _fruit list, the method calls NotFound to create a NotFoundResult.
The [ApiController] attribute automatically converts the response into a standard ProblemDetails instance.
ProblemDetails is a web specification for providing machinereadable errors for HTTP .

IMPORTANT: Here the method signature says it should be return an ActionResult<string> and we are returning the string _fruit[id].
           There is no compilation error here because C# has an implicit conversions available here.
           Returning ActionResult<T> we can return either an instance of T or an ActionResult implementation like NotFoundResult from the same method.

We commonly return StatusCodeResult instances: 
    OkResult            matches with the 200 status code
    NotFoundResult      matches with the 404 status code
    BadRequestResult    matches with the 400 status code

In many cases the [ApiController] attribute can automatically generate 400 responses for us.

Once we’ve returned an ActionResult (or other object) from our controller, it’s serialized to an appropriate response. The kind of serialation depending on:
    - The formatters that our app supports.
    - The data we return from our method.
    - The data formats the requesting client can handle.


--------------------------------------------------------------------------------
Applying the MVC design pattern to a Web API.

ASP.NET Core is a single framework that we can use to build both traditional web applications and Web APIs.
The same underlying framework is used in conjunction with Web API controllers, Razor Pages, and MVC controllers with views. 

Consequently, even if we’re building an application that consists entirely of Web APIs, using no server-side rendering of HTML, the MVC design pattern still applies.
Whether we’re building traditional web applications or Web APIs, we can structure our application virtually identically.

The process of Routing, Model Binding and Validations are almost the same for a Razor Page project and Web API project.
The main changes are related to switching from Razor Pages to controllers and actions, but both approaches use the same general paradigms.

Continuar página 268






















