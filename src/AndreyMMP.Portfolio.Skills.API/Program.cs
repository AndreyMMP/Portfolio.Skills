using AndreyMMP.Portfolio.Skills.Application.Interfaces;
using AndreyMMP.Portfolio.Skills.Application.Mapping;
using AndreyMMP.Portfolio.Skills.Application.Services;
using AndreyMMP.Portfolio.Skills.Data.Context;
using AndreyMMP.Portfolio.Skills.Data.Repository;
using AndreyMMP.Portfolio.Skills.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Add configuration and database context dependency injection

builder.Services.AddDbContext<PortfolioDbContext>(opt =>
{
    opt.UseSqlServer(PortfolioDbConfiguration.GetConnectionString());
    opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

builder.Services.AddScoped<ISkillService, SkillService>();
builder.Services.AddScoped<ISkillRepository, SkillRepository>();
builder.Services.AddAutoMapper(typeof(DomainToDTOMapping));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();