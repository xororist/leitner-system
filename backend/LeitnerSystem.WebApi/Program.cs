using LeitnerSystem.Application;
using LeitnerSystem.Domain;
using LeitnerSystem.Infrastructure;
using LeitnerSystem.Infrastructure.MongoDatabase;

var builder = WebApplication.CreateBuilder(args);

var mongoDbSettings = builder.Configuration.GetSection("MongoDbSettings").Get<MongoDbSettings>();

if (mongoDbSettings is null)
{
    throw new InvalidOperationException("MongoDbSettings configuration section is missing or not properly configured.");
}

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowAllOrigins",
        policy =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
    
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("http://localhost:3000") 
            .AllowAnyMethod()
            .AllowAnyHeader());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDomain();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(mongoDbSettings);

builder.Services.AddControllers(); 

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers(); 

app.UseCors("AllowAllOrigins");
app.UseCors("AllowSpecificOrigin");

app.Run();