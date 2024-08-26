using API.Hubs;
using Application;
using Hangfire;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.ConfigureApplicationServices(builder.Configuration);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        policyBuilder =>
        {
            policyBuilder.WithOrigins("http://pitnik.s3-website-us-east-1.amazonaws.com", "http://localhost:4200")
                         .AllowAnyHeader()
                         .AllowAnyMethod()
                         .AllowCredentials(); // Phải cho phép credentials
        });
});

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using Bearer",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();
app.UseCors("AllowSpecificOrigins");
app.UseHttpsRedirection();

app.UseAuthorization();
app.MapHub<HubMessage>("HubMessage");
app.MapHub<HubInteraction>("HubInteraction");
app.MapHub<HubPresence>("HubPresence");
app.UseStaticFiles();
app.MapControllers();

app.Run();
