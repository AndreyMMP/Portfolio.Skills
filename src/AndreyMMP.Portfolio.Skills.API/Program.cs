using AndreyMMP.Portfolio.Skills.API.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Add configuration and database context dependency injection

builder.Services.AddDbContext<PortfolioDbContext>(opt => opt.UseSqlServer(PortfolioDbConfiguration.GetConnectionString()));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
