# RoutesList

RoutesList is a handy tool that lets you see all the routes in your .NET application in a clear and concise way.
You can easily inspect the methods, URIs, controllers, actions and middleware of your routes,
and customize the output to suit your needs.
RoutesList works with .NET Core app 3.1, .NET5.0, .NET 6, .NET7.0, Razor Pages, ASP.NET MVC and Blazor Server projects.

Compatible with:

- NET Core app 3.1.x, .NET 5.0.x, .NET 6.0.x, .NET 7.0.x
- Razors Pages
- ASP.NET projects with MVC
- Blazor Server App

## Features

- Endpoints with method, uri, controller name, action, full namespace path
- Endpoints from Razor pages with name, Relative Path, view engine path
- Endpoints from Blazor components
- HTML5 output 
- JSON output
- Custom class for table
- Link to endpoints in table view
- Autodetect what application is using MVC, Razor pages, Blazor components

![Table list image](https://github.com/JanoPL/Routeslist/blob/master/Screenshots1.png?raw=true)
[![FOSSA Status](https://app.fossa.com/api/projects/git%2Bgithub.com%2FJanoPL%2FRouteslist.svg?type=shield)](https://app.fossa.com/projects/git%2Bgithub.com%2FJanoPL%2FRouteslist?ref=badge_shield)

![Table json list image](https://github.com/JanoPL/Routeslist/blob/master/Screenshots2.png?raw=true)

![Table list image blazor](https://github.com/JanoPL/Routeslist/blob/master/Screenshots3.png?raw=true)

![Table json list image blazor](https://github.com/JanoPL/Routeslist/blob/master/Screenshots4.png?raw=true)

## Installation

From nuget.org

```shell
Install-Package RoutesList 
```

## Usage

Just add ```services.AddRoutesList``` to service ConfigureService method.

Example:

Startup.cs

```C#
\\...
public void ConfigureServices(IServiceCollection services)
{
    \\...
    services.AddRoutesList();
}
```

and add to Configure method

Example:

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

### Usage for implicit Using Statements In .NET 6

Example:

Program.cs

```C#
using RoutesList.Gen;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddRoutesList(); <-- usage

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

### Usage for implicit Using Statements In .NET 6/7 and with Blazor component

```C#
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddSingleton<WeatherForecastService>();

            builder.Services.AddRoutesList();

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

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.UseRoutesList(options => {
                options.SetAppAssembly(typeof(Program).Assembly); <-- setup current application webassembly with blazor component
            });

            app.Run();
        }
    }
```

## options for UseRoutesList

In app.UseRoutesList you can pass options

|Name             | Description                                                     |
|:---------------:|:---------------------------------------------------------------:|
| Endpoint        | endpoint name                                                   |
| Title           | Title for web site                                              |
| SetTableClasses | Add template classes for table                                  |
| SetAppAssembly  | Set current application assembly together with blazor component |

### Example:
Startup.cs

```C#
public void Configure(
    IApplicationBuilder app,
    IWebHostEnvironment env,
)
{
    IDictionary<string, string[]> dict = new Dictionary<string, string[]>();
    string[] classes = dict["table"] = new string[2] { "table", "table-striped" };

    app.UseRoutesList(options => {
        options.Endpoint = "your_new_endpoints";
        options.Tittle = "Your new Title for site";
        options.SetTableClasses(classes);
        options.SetAppAssembly(typeof(Program).Assembly)
    });
}
```

### Example .NET6 | .NET7:
Program.cs

```C#
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Services.AddRazorPages();
    builder.Services.AddControllersWithViews();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error");
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseAuthorization();

    app.MapGet("/hi", () => "Hello!");

    app.MapDefaultControllerRoute();
    app.MapRazorPages();

    app.UseRoutesList(options => {
        options.Endpoint = "your_new_endpoints";
        options.Tittle = "Your new Title for site";
        options.SetTableClasses(classes);
        options.SetAppAssembly(typeof(Program).Assembly)
    });

    app.Run();
```

## Contributing

Contributions are always welcome, whether adding/suggesting new features, bug fixes, documenting new file formats or simply editing some grammar. for this create new github issue and descript your problem and add issue tag for Bug/enhancement


## License
[![FOSSA Status](https://app.fossa.com/api/projects/git%2Bgithub.com%2FJanoPL%2FRouteslist.svg?type=large)](https://app.fossa.com/projects/git%2Bgithub.com%2FJanoPL%2FRouteslist?ref=badge_large)