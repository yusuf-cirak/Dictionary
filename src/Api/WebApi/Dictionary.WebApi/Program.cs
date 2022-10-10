using System.Reflection;
using Dictionary.Application;
using Dictionary.Persistence;
using Dictionary.WebApi.Infrastructure.ActionFilters;
using Dictionary.WebApi.Infrastructure.Extensions;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(p => p.AddPolicy("BlazorClientPolicy", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));


builder.Services.AddRouting(config =>
{
    config.LowercaseUrls = true;
    config.LowercaseQueryStrings = true;
});

builder.Services.AddControllers(opt => opt.Filters.Add<ValidateModelStateFilter>())

    .AddJsonOptions(e=>e.JsonSerializerOptions.PropertyNamingPolicy=null)

    .AddFluentValidation(c => c.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly())).ConfigureApiBehaviorOptions(o=>o.SuppressModelStateInvalidFilter=true);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();


builder.Services.ConfigureAuth(builder.Configuration); // extension method

builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.ConfigureExceptionHandling(includeExceptionDetails:app.Environment.IsDevelopment());

app.UseAuthentication();
app.UseAuthorization();

app.UseCors("BlazorClientPolicy");

app.MapControllers();

app.Run();
