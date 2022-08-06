---------------------------------------------------------------------
Rendering HTML using Razor views.

IMPORTANT: SOME CONCEPTS TO BE CLEAR:

- Razor Pages: (in plurar) generally refers to the page-based paradigm that combines routing, model binding, 
  and HTML generation using Razor views.

- Razor Page: A single Razor Page represents a single page or “endpoint”. 
  It typically consists of two files: a .cshtml file containing the Razor view, and a .cshtml.cs file containing the page’s PageModel.

- PageModel: The PageModel for a Razor Page is where most of the action happens. 
  It’s where we define the binding models for a page, which extracts data from the incoming request. 
  It’s also where we define the page’s page handlers.

- Page handler: Each Razor Page typically handles a single route, but it can handle multiple HTTP verbs like GET and POST. 
  Each page handler typically handles a single HTTP verb.

- Razor view: Razor views (also called Razor templates) are used to generate HTML. 
  They are typically used in the final stage of a Razor Page to generate the HTML response to send back to the user.

In ASP.NET Core, views are normally created using the Razor markup syntax which uses a mixture of HTML and C# to generate the final HTML.

When displaying data, we can use the Razor language to easily combine static HTML with values from your PageModel.

ASP.NET Core and the Razor templating language include a number of Tag Helpers that make generating HTML forms easy.

We can:
 - render values from your PageModel to the HTML.
 - use C# to control the generated output. 
 - extract the common elements of your views into sub-views called layouts and partial views.
 - compose all of them to create the final HTML page.

 
---------------------------------------------------------------------
Views: Rendering the user interface.

Once the page handler has executed, the PageModel should contain all the data required to render a view. 

The Razor view template uses the PageModel to generate the final response and returns it to the user via the middleware pipeline.

We’ll also separate the data required to build the  from the process of building it by using properties on the PageModel. 
These properties should contain all the dynamic data needed by the view to generate the final output.

Views shouldn’t call methods on the PageModel. 
The view should generally only be accessing data that has already been collected and exposed as properties.

Razor Page handlers indicate that the Razor view should be rendered by returning a PageResult (or by returning void).




