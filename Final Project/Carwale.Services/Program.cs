using Carwale.DAL.Data;
using Carwale.DAL.Interfaces;
using Carwale.DAL.Repositories;
using Carwale.Service.Services;
using Carwale.Services.Mappers;
using Carwale.Services.Services;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<DatabaseContext>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    var connectionString = configuration.GetConnectionString("DefaultConnection");

    if (string.IsNullOrEmpty(connectionString))
    {
        throw new InvalidOperationException("The 'DefaultConnection' connection string is missing or empty in the configuration.");
    }

    return new DatabaseContext(connectionString);
});

builder.Services.AddAutoMapper(typeof(StocksMappingProfile));

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddScoped<IStocksRepo, StocksRepo>();
builder.Services.AddControllers();

var app = builder.Build();


// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>();
app.MapGrpcService<StocksService>();

app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
