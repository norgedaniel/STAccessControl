------------------------------------------------------------------------
Building forms with Tag Helpers.

	- Building forms easily with Tag Helpers
	- Generating URLs with the Anchor Tag Helper
	- Using Tag Helpers to add functionality to Razor

Tag Helpers are new to ASP.NET Core. They can be used to customize the HTML generated in our templates.
They can be added to an otherwise standard HTML element, such as an <input>, to customize its attributes based on our C# model.
Tag Helpers can also be standalone elements and can be used to generate completely customized HTML.

They simplify the process of generating correct element names and IDs so that model binding can occur seamlessly when the form is sent back to our application.

Beside of use Tag Helpers to work with forms, we’ll also see how we can use Tag Helpers to simplify other common tasks, such as generating links, 
conditionally displaying data in our application, and ensuring users see the latest version of an image file when they refresh their browser.

Related to forms, we can use Tag Helpers to generate labels, drop-downs, input elements, validation messages.

Tag Helpers integrate seamlessly into the standard HTML syntax by adding what look to be attributes, typically starting with asp-*.

Tag Helpers blend in so well with the HTML! This makes it easy to edit the files with any standard HTML text editor.

Example of using of Tag Helpers to help in the generation of form:

	<div class="form-group">
		<label asp-for="CurrencyFrom"></label>
		<input class="form-control" asp-for="CurrencyFrom" />
		<span asp-validation-for="CurrencyFrom"></span>
	</div>

Here we have created a label, input and validation for an specific property in our view model
asp-for on Inputs generates the correct type, value, name, and validation attributes for the model.

IMPORTANT: Usually the HTML tag <span> is used to show some validation message.

Tag Helpers are extra attributes on standard HTML elements (or new elements entirely) that work by modifying the HTML element they’re attached to. 
They let us easily integrate our server-side values, such as those exposed on our PageModel, with the generated HTML.

Tag Helpers are used to:

	- Automatically populate the value from the PageModel property.
	- Choose the correct id and name, so that when the form is POSTed back to the Razor Page, the property will be model-bound correctly.
	- Choose the correct input type to display (for example, a number input for the Quantity property).
	- Display any validation errors.

OJO: ME QUEDÉ EN LA PÁGINA 228 Creating forms using Tag Helpers. 