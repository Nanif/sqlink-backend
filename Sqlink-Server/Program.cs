using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Sqlink_Server.GeneratedModels;
using Sqlink_Server.Handlers;

var builder = WebApplication.CreateBuilder(args);

var cs = builder.Configuration["SqlinkDBConnectionString"];
builder.Services.AddDbContext<MyDBContext>(options => options.UseNpgsql(cs));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,
        options => builder.Configuration.Bind("JwtSettings", options));

builder.Services.AddSingleton<IJWTAuthenticationManager>(new JWTAuthenticationManager(builder.Configuration["JwtSettings:IssuerSigningKey"]));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
