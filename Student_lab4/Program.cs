using Microsoft.EntityFrameworkCore;
using Student_lab4;
using Student_lab4.Configuration;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using JWT;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<StudentContext>(opt => opt.UseInMemoryDatabase("StudentList"));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<JwtSettings>();
builder.Services.ConfigureIdentity();
builder.Services.ConfigureJWT(new JwtSettings(builder.Configuration));
builder.Services.ConfigureCors();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme.
 Enter 'Bearer' and then your token in the text input below.
 Example: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
 {
 {
 new OpenApiSecurityScheme
 {
 Reference = new OpenApiReference
 {
 Type = ReferenceType.SecurityScheme,
Id = "Bearer"
 },
 Scheme = "oauth2",
 Name = "Bearer",
 In = ParameterLocation.Header,
 },
 new List<string>()
 }
 });
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Student API",
        Description = "demo Student API",
        Version = "v1",
        Contact = new OpenApiContact
        {
            Name = "Example Contact",
            Url = new Uri("https://adres_strony")
        },
    });
});


var app = builder.Build();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Student API V1");
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.AddUsers();

app.Run();