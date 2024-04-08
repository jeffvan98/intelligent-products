using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var platform = builder.Configuration.GetValue<string>("Platform");
var connectionString = builder.Configuration.GetConnectionString("Default");

switch(platform) 
{
    case "Sqlite":
        builder.Services.AddDbContext<ProductIdentificationContext>(options => options.UseSqlite(connectionString));
        break;
    case "SqlServer":
        builder.Services.AddDbContext<ProductIdentificationContext>(options => options.UseSqlServer(connectionString));
        break;
    default:
        throw new ApplicationException($"Invalid platform {platform} specified.");
}

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
    
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();