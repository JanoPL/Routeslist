# RoutesList

Library can be used to show a list of all the registered routes for the application.

Library show all Routes in table format - methods, Uri, Controller Name, Action, Full name of path or namespace

Compatible with:

- NET6
- Razors Pages
- ASP.NET projects with MVC
- Blazor Server App

## Features

- Endpoints with method, uri, controller name, action, full namespace path
- Endpoints for Razor pages with name, Relative Path, view engine path
- HTML5 output
- JSON output
- Custom class for table
- link to endpoints in table view

![Table list image](https://github.com/JanoPL/Routeslist/blob/master/Screenshots1.png?raw=true)

![Table json list image](https://github.com/JanoPL/Routeslist/blob/master/Screenshots2.png?raw=true)

## Installation

From nuget.org

```shell
Install-Package RoutesList 
```

## Usage

Just add ```services.AddRouteList``` to service ConfigureService method.

<b>Example:</b>
Startup.cs

```C#
\\...

public void ConfigureServices(IServiceCollection services)
{
    \\...
    services.AddRouteList();
}
```

and add to Configure method

<b>Example:</b>
Startup.cs

```C#
\\...


public void Configure(
    IApplicationBuilder app,
    IWebHostEnvironment env,
)
{
    \\...
    app.UseRoutesList();
}
```

Default Endpoint: ```http://your_application_address/routes```

<hr> 

### Usage for implicit Using Statements In .NET 6

<b>Example</b>: Program.cs

```
using RoutesList.Gen;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddRouteList(); <-- usage

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.UseRoutesList(); <-- usage

app.Run();

public partial class Program { }
```

<hr>

## options for UseRoutesList

In app.UseRoutesList you can pass options

|Name|Description|
|:--:|:---------:|
Endpoint | endpoint name
Title | Title for web site
StylePath | CSS style file path - not implemented

<b>Example:</b>
Startup.cs

```C#
public void Configure(
    IApplicationBuilder app,
    IWebHostEnvironment env,
)
{
    app.UseRoutesList(options => {
        options.Endpoint = "your_new_endpoints";
        options.Tittle = "Your new Title for site";
        options.StylePath = "";
    });
}
```

<hr>

## Contributing

Contributions are always welcome, whether adding/suggesting new features, bug fixes, documenting new file formats or simply editing some grammar. for this create new github issue and descript your problem and add issue tag for Bug/enhancement
