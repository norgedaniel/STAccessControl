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

When displaying data, we can use the Razor language to easily combine static HTML with values from our PageModel.

ASP.NET Core and the Razor templating language include a number of Tag Helpers that make generating HTML forms easy.

We can:
 - render values from our PageModel to the HTML.
 - use C# to control the generated output. 
 - extract the common elements of our views into sub-views called layouts and partial views.
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

Once the page handler has executed, the PageModel should contain all the data required to render a view.

Once the page handler has executed, the PageModel should contain all the data required to render a view.

Razor markup describes how to display data using a mixture of HTML and C#.
By combining the data in our view model with the Razor markup, HTML can be generated dynamically. Example:

	@foreach(var user in Model.ExistingUsers)
	{
		<li>
			<span>@user</span>
			<button>View</button>
		</li>
	}

There are the Tag Helpers which help to build the forms we need in our App. 
IMPORTANT: Get deep into Tag Helpers.

IMPORTANT: It’s possible to enable runtime compilation for Razor Pages. 
This allows us to modify our Razor Pages while our app is running, without having to explicitly stop and rebuild. Get deep into this option.


-------------------------------------------------------------------------------
Creating Razor views.

With ASP.NET Core, whenever we need to display an HTML response to the user, we should use a view to generate it.

As we have seen a Razor page is form by two files: the view which is into the .cshtml file and the page model that is into the .cshtml.cs file.

The Razor view contains the @page directive, which makes it a Razor Page.

The code-behind .cshtml.cs file contains the PageModel for an associated Razor Page.
It contains the page handlers that respond to requests, and it is where the Razor Page typically interacts with other parts of our application.

Even though the .cshtml and .cshtml.cs files share the same name, such as ToDoItem.cshtml and ToDoItem.cshtml.cs, 
it’s not the filename that’s linking them together. At the top of each Razor Page, just after the @page directive, 
is an @model directive with a Type, indicating which PageModel is associated with the Razor view. Example:

@page
@model ToDoItemModel

Once a request is routed to a Razor Page, as we covered in chapter 5, the framework looks for the @model directive to decide which PageModel to use. 
Based on the PageModel selected, it then binds to any properties in the PageModel marked with the [BindProperty] attribute 
and executes the appropriate page handler (based on the request’s HTTP verb).

In addition to the @page and @model directives, the Razor view file contains the Razor template that is executed to generate the HTML response.


-------------------------------------------------------------------------------
Introducing Razor templates.

Razor view templates contain a mixture of HTML and C# code interspersed with one another. The HTML markup lets we easily describe exactly 
what should be sent to the browser, whereas the C# code can be used to dynamically change what is rendered. Example:

	@page
	<!-- Arbitrary C# can be executed in a template. Variables remain in scope throughout the page. -->
	@{
		var tasks = new List<string>
		{ "Buy milk", "Buy eggs", "Buy bread" };
	}

	<!-- Standard HTML markup will be rendered to the output unchanged. -->
	<h1>Tasks to complete</h1>
	<ul>

	<!-- Mixing C# and HTML allows us to dynamically create HTML at runtime. -->

	@for(var i=0; i< tasks.Count; i++)
	{
		var task = tasks[i];
		<li>@i - @task</li>
	}

	</ul>


-------------------------------------------------------------------------------
Passing data to views.

There are several ways to pass data from the Page Model into the View Model. But the most of the time we should use the "Page Model properties".

Another useful way is throught the ViewData. This is a dictionary of objects with string keys that can be used to pass arbitrary 
data from the page handler to the view. In addition, it allows us to pass data to _layout files and this is the main reason
for using ViewData instead of setting properties on the PageModel.

"Page Model properties" does not allos to pass data to the _layout files.

Other ways are adding data into the request HttpContext and the @inject services (Dependency Injection); but its not recommended to use these ways.

Far and away the best approach for passing data from a page handler to a view is to use properties on the PageModel.
We can store anything there to hold the data we require.

The PageModel contains values to display in the UI, but the binding is only one-directional; the PageModel provides values to the UI, 
and once the UI is built and sent as a response, the PageModel is destroyed. It contains one or more page handlers, and exposes
data as properties for use in the Razor view.

We can access the PageModel instance itself from the Razor view using the Model property. For example, to display some Title property 
in the Razor view, we’d use <h1>@Model.Title</h1>.

The @model directive should be at the top of our view, just after the @page directive, and it has a lowercase m. 
The Model property can be accessed anywhere in the view and has an uppercase M.

In the vast majority of cases, using public properties on our PageModel is the way to go; it’s the standard mechanism for passing data 
between the page handler and the view. But in some circumstances, properties on our PageModel might not be the best fit. 
This is often the case when we want to pass data between view layouts.

A common example of passing data between layouts is the Title of the page, which we usually add to the ViewData dictionary begining the view file.

@{
  ViewData["Title"] = "Home Page";
}

<h2>@ViewData["Title"].</h2>

Always use PageModel properties where possible, as we benefit from strong typing and IntelliSense. 
Only fall back to ViewData for values that need to be accessed outside of our Razor view.


---------------------------------------------------------------------------------------
Using C# in Razor templates.

One of the most common requirements when working with Razor templates is to render a value we’ve calculated in C# to the HTML. 
If the code is a single statement, we can use the @ symbol to indicate we want to write the result to the HTML output.
Example: <p>Copyright @DateTime.Now.Year &copy;</p>

Here: @ indicates start of C# expression. Whitespace after Year indicates end of C# expression.

The @ symbol indicates where the C# code begins, and the expression ends at the end of the statement, in this case at the space.

To insert a C# block into the Razor template we should use:  @ {   } and inside we can write some HTML tags. Example:

	@foreach (var task in Model.Tasks)
	{
		<li>@task</li>
	}


---------------------------------------------------------------------------------------
Rendering HTML with Raw.

Sometime we could need to directly write out HTML contained in a string or in a variable. 
If we find ourself in this situation, first, stop. Do we really need to do this? 
If the values ou’re writing have been entered by a user, or were created based on values provided by users, 
there’s a serious risk of creating a security hole in our website.

If we really need to write the variable out to the HTML stream, we can do so using the Html property on the view page 
and calling the Raw method:  <li>@Html.Raw(task)</li>

WARNING: Using Html.Raw on user input creates a security risk that users could use to inject malicious code into our website. 
         Avoid using Html.Raw if possible.


---------------------------------------------------------------------------------------
Layouts, partial views, and _ViewStart.

There are the layouts and partial views in Razon pages which allow us to extract common code to reduce duplication. 
These kind of files make it easier to make changes to our HTML thar affect multiple pages at once.

A layout in Razor is a template that includes common code. It can’t be rendered directly, but it can be rendered in conjunction with normal Razor views.

An ASP.NET Core app can have multiple layouts, and layouts can reference other layouts. 
A common use for this is to have different layouts for different sections of our application. 

We’ll often use layouts across many different Razor Pages, so they’re typically placed in the Pages/Shared folder. 
We can name them anything we like, but there’s a common convention to use _Layout.cshtml as the filename for the base layout in our application. 
This is the default name used by the Razor Page templates in Visual Studio and the .NET CLI.

A common convention is to prefix our layout files with an underscore (_) to distinguish them from standard Razor templates in our Pages folder.

A layout file looks similar to a normal Razor template, with one exception: every layout must call the @RenderBody() function. 
This tells the templating engine where to insert the content from the child views.

Typically, our application will reference all our CSS and JavaScript files in the layout, as well as include all the common elements, 
such as headers and footers.

Layout files are not standalone Razor Pages and do not take part in routing, so they do not start with the @page directive.

Any contents in the view will be rendered inside the layout, where the call to @RenderBody() occurs.

By default, layouts only provide a single location where we can render content from the view, at the call to @RenderBody.


---------------------------------------------------------------------------------------
Overriding parent layouts using sections.

A common requirement when we start using multiple layouts in our application is to be able to render content from child views in more than one place in our layout.
This is achieved using sections that are defined in the view using an @section definition. 
The @section can be placed anywhere in the file, top or bottom, wherever is convenient. Example:

	@{
		Layout = "_TwoColumn";
	}

	@section Sidebar {
		<p>This is the sidebar content</p>
	}

	<p>This is the main content </p>

The sections will be rendered calling @RenderSection() into the layout file. This call can be done as required section or not:

@RenderSection("Sidebar", required: true)    or    @RenderSection("Sidebar", required: false)

When a section is called from the layout as requiered, it should be defined into the razor view. 
If it´s not required and the section is not defined; it will be ignored.

Example of layout calling @RenderBody(), @RenderSection() and making a nested layout:

	@{
     	Layout = "_Layout";                          --> here a nested layout
	}
	<div class="main-content">
		@RenderBody()
	</div>

	<div class="side-bar">
		@RenderSection("Sidebar", required: true)	--> calling section as requiered. It should be defined into the view.	
	</div>

	@RenderSection("Scripts", required: false)      --> calling section as optional. It does not need to be defined into the view.	

It’s common to have an optional section called Scripts in our layout pages. This can be used to render additional JavaScript 
that’s required by some views, but that isn’t needed on every view. 
A common example is the jQuery Unobtrusive Validation scripts for client-side validation.



---------------------------------------------------------------------------------------
Using partial views to encapsulate markup.

Partial views provide a means of breaking up a larger view into smaller, reusable chunks. 
This can be useful for both reducing the complexity in a large view by splitting it into multiple partial views, 
or for allowing we to reuse part of a view inside another. 

Partial views are a bit like Razor Pages without the PageModel and handlers. 
Partial views are purely about rendering small sections of HTML, rather than handling requests, model binding, and validation, 
and calling the application model. They are great for encapsulating small usable bits of HTML that we need to generate on multiple Razor Pages.

Partial views are rendered using the <partial /> Tag Helper, providing the name of the partial view to render and the data (the model) to render.

When we render a partial view without providing an absolute path or file extension, the framework tries to locate the view by searching the Pages folder, 
starting from the Razor Page that invoked it. For example, if our Razor Page is located at Pages/Agenda/ToDos/RecentToDos.chstml, 
the framework would look in the following places for a file called _ToDo.chstml:
	 Pages/Agenda/ToDos/ (the current Razor Page’s folder)
	 Pages/Agenda/
	 Pages/
	 Pages/Shared/
	 Views/Shared/
The first location that contains a file called _ToDo.cshtml will be selected. If we include the .cshtml file extension when we reference the partial view, 
the framework will only look in the current Razor Page’s folder. Also, if we provide an absolute path to the partial, 
such as /Pages/Agenda/ToDo.cshtml,  that’s the only place the framework will look.

Like layouts, partial views are typically named with a leading underscore.

Example of use of partial view:

	// the partial view
	@model ToDoItemViewModel
	<h2>@Model.Title</h2>
	<ul>
		@foreach (var task in Model.Tasks)
		{
			<li>@task</li>
		}
	</ul>

	// the view calling the partial view.
	@page
	@model RecentToDoListModel

	@foreach(var todo in Model.RecentItems)
	{
		<partial name="_ToDo" model="todo" />
	}

We can see the atributte name and model to identify the partial view and the model to be used.


---------------------------------------------------------------------------------------
Running code on every view with _ViewStart and _ViewImports.

The _ViewImports.cshtml file contains directives that will be inserted at the top of every view, basically the @using and @addTagHelper directives.

Example for _ViewImports.cshtml:

	@using WebApplication1
	@using WebApplication1.Pages
	@using WebApplication1.Models
	@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers

It´s important to remark that only these kind of directives can placed into this file. 
Other more common complex C# code should be placed into the _ViewStart.cshtml file instead.

The _ViewImports.cshtml file can be placed in any folder, and it will apply to all views and sub-folders in that folder. 
Typically, it’s placed in the root Pages folder so that it applies to every Razor Page and partial view in our app.

The _ViewStart.cshtml file is used to easly run common code at the start of every Razor Page. Typically to set the Layout for all the pages.

We can then omit the Layout statement from all pages that use the default layout. If a view needs to use a non-default layout, 
we can override it by setting the value in the Razor Page itself.

Any code in the _ViewStart.cshtml file runs before the view executes. 
_ViewStart.cshtml only runs for Razor Page views—it doesn’t run for layouts or partial views.

The names for these special Razor files are enforced rather than conventions we can change.

We must use the names _ViewStart.cshtml and _ViewImports.cshtml for the Razor engine to locate and execute them correctly. 
To apply them to all our app’s pages, add them to the root of the Pages folder, not to the Shared subfolder.

We can specify additional _ViewStart.cshtml or _ViewImports.cshtml files to run for a subset of our views by including them in a subfolder in Pages. 
The files in the subfolders will run after the files in the root Pages folder.


---------------------------------------------------------------------------------------
Selecting a view from an MVC controller.

	How MVC controllers use ViewResults to render Razor views ?  
	How to create a new Razor view ?  
	How the framework locates a Razor view to render ?

ViewResults are the MVC equivalent of Razor Page’s PageResult. 
The main difference is that a ViewResult includes a view name to render and a model to pass to the view template, 
while a PageResult always renders the Razor Page’s associated view and passes the PageModel to the view template.

In most cases we won’t create a ViewResult directly in our action methods. Instead, we’ll use one of the View helper methods on the Controller base class.
In the simplest case we can call the View method without any arguments. 
This helper method returns a ViewResult that will use conventions to find the view template to render, and will not supply a view model when executing the view.

When we do not specify the name of the view template to run, the framework try to find it based on the name of the controller and the name of the action method.

For example, given that the controller is called HomeController and the method is called Index, by default the Razor template engine looks 
for a template at the Views/Home/Index.cshtml location.

We can also explicitly pass the name of the template to run as a string to the View method. 
Example: return View("ListView") --> the framework will search for ListView.cshtml

We can even specify the complete path to the view file, relative to our application’s root folder, 
such as View("Views/global.cshtml"), which would look for the template at the Views/global.chtml location.

We can return just the view model. Example:

	public IActionResult Index()
	{
		var listViewModel = new ToDoListModel();
		return View(listViewModel);
	}

and also return the name of the view to be rendered plus the view model. Exmaple:

	public IActionResult View(int id)
	{
		var viewModel = new ViewToDoModel();
		return View("ViewToDo", viewModel);
	}

The process of run the razor views to render the HTML is the same for a Razor pages project and the MVC project.
We can use layouts, partial views, _ViewImports, and _ViewStart...











