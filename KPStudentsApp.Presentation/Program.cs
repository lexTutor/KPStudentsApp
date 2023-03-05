using FluentValidation.AspNetCore;
using KPStudentsApp.Application;
using KPStudentsApp.Application.Common.Models;
using KPStudentsApp.Infrastructure;
using KPStudentsApp.Infrastructure.Persistence;
using KPStudentsApp.Presentation.Middlewares;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
  {
      options.InvalidModelStateResponseFactory = c =>
      {
          var errors = c.ModelState.Values.Where(v => v.Errors.Count > 0)
          .SelectMany(v => v.Errors)
          .Select(v => v.ErrorMessage);

          return new BadRequestObjectResult(new Response<string>
          {
              Errors = errors.ToArray(),
              Message = "Invalid Model State"
          });
      };
  });

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services
.AddFluentValidationAutoValidation()
.AddFluentValidationClientsideAdapters();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    // Initialise and seed database
    using (var scope = app.Services.CreateScope())
    {
        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();
        await initialiser.InitialiseAsync();
        await initialiser.SeedAsync();
    }
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
