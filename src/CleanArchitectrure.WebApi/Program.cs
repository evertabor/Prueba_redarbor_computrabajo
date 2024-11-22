using CleanArchitectrure.Application.UseCases;
using CleanArchitectrure.Persistence;
using CleanArchitectrure.WebApi.Converters;
using CleanArchitectrure.WebApi.Extensions;
using CleanArchitectrure.WebApi.Extensions.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);


//Add methods Extensions
builder.Services.AddWebApi(builder.Configuration);
builder.Services.AddInjectionPersistence();
builder.Services.AddInjectionApplication();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.RoutePrefix = "swagger";
    });
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.AddMiddleware();

app.MapControllers();

app.Run();
