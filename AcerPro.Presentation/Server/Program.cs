using AcerPro.Presentation.Server.Infrastructures;
using AcerPro.Presentation.Server.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
#region Configure Services
builder.Services.AddRepositories();
builder.Services.AddRequiredServices();
builder.Services.AddJobServices();
builder.Services.AddDatabases(builder.Configuration);
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization(); 

app.UseRouting();

app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();

// for integration test
public partial class Program { }