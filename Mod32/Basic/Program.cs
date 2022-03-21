using Basic.Middlewares;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    // Look for static files in webroot
    WebRootPath = "Views"
});

var app = builder.Build();

if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    app.UseDeveloperExceptionPage();
}

app.MapGet("/", () =>  $"App name: {app.Environment.ApplicationName}. App running configuration: {app.Environment.EnvironmentName}");

app.MapGet("/about", () => $"Welcome to the {app.Environment.ApplicationName}");

app.MapGet("/test", async context => { await context.Response.WriteAsync($"App name: {app.Environment.ApplicationName}. App running configuration: {app.Environment.EnvironmentName}"); });
// обрабатываем ошибки HTTP
app.UseStatusCodePages();
// Поддержка статических файлов
app.UseStaticFiles();

app.UseMiddleware<LoggingMiddleware>();
app.Run();
