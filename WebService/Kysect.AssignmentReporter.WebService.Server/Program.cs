using System.Linq;
using Kysect.AssignmentReporter.WebService.DAL.Database;
using Kysect.AssignmentReporter.WebService.Server.Repository;
using Kysect.AssignmentReporter.WebService.Server.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.ResolveConflictingActions (apiDescriptions => apiDescriptions.First());
});
builder.Services.AddScoped<IRepository, DropboxRepository>();
builder.Services.AddScoped<ReportsService>();
builder.Services.AddScoped<EntitiesService>();
builder.Services.AddDbContext<AssignmentReporterContext>(opt =>
{
    opt.UseSqlite("Data Source=Application.db;Cache=Shared");
});

WebApplication? app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();