using RoutesList.Gen;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddRoutesList();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

IDictionary<string, string[]> dict = new Dictionary<string, string[]>();

string[] classes = dict["table"] = new string[2] { "table", "table-striped" };
app.UseRoutesList(options => {
    options.Tittle = "Title";
    options.Endpoint = "routes";
    options.SetTableClasses(classes);
});

app.Run();
