using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Sneaker_Be.AddTransientCollection;
using Sneaker_Be.Context;
using Sneaker_Be.Entities;
using Sneaker_Be.Services;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
    
builder.Services.AddSingleton<DapperContext>();
builder.Services.AddControllers();

builder.Services.ConfigureTransient();
builder.Services.ConfigureAuthen(builder.Configuration);

//builder.Services.AddTransient<ISmsSender, MessageServices>();
//builder.Services.Configure<SMSOptions>(builder.Configuration);

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(options => options
.WithOrigins("http://localhost:4200")
.AllowAnyMethod()
.AllowAnyHeader()
);

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
