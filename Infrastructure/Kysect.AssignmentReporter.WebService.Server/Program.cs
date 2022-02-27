using System;
using System.Linq;
using Kysect.AssignmentReporter.WebService.DAL.Database;
using Kysect.AssignmentReporter.WebService.Server.Controllers;
using Kysect.AssignmentReporter.WebService.Server.Repository;
using Kysect.AssignmentReporter.WebService.Server.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddControllers()
    .AddApplicationPart(typeof(SubjectController).Assembly);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddRazorPages();
builder.Services.AddSwaggerGen(c =>
{
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
});
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IRepository, DropboxRepository>();
builder.Services.AddScoped<ReportsService>();
builder.Services.AddScoped<EntitiesService>();
var connectionString = builder.Configuration.GetSection("ConnectionString").Value;
builder.Services.AddDbContext<AssignmentReporterContext>(opt =>
{
    opt.UseSqlite(connectionString);
});

WebApplication? app = builder.Build();

app.UseBlazorFrameworkFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// app.UseAuthorization();
app.UseRouting();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();