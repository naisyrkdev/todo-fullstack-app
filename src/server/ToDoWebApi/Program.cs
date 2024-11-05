using Domain;
using Infrastructure;
using Infrastructure.Helpers;
using Shared;
using TodoWebApi.Helpers;

var AvailableOrigins = "_availableOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AvailableOrigins,
                      policy =>
                      {
                          policy.AllowAnyOrigin()
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                      });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationServices();

builder.Services.AddInfrastructureServices();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDocument(settings => {
    settings.Title = "Todo API";
    settings.DocumentProcessors.Add(new AddAdditionalTypeProcessor<Todo>());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();
    var seeder = new DataSeeder(context);
    seeder.SeedData();
}

app.UseHttpsRedirection();

app.UseCors(AvailableOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
