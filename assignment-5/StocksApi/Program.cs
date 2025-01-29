using MySql.Data.MySqlClient;
using StocksApi.BAL.Services;
using StocksApi.DAL.Interfaces;
using StocksApi.DAL.Repositories;
using StocksApi.DAL.Data;
using StocksApi.BAL.Interfaces;
using StocksApi.Mappers;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();


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

builder.Services.AddScoped<IStocksRepo, StocksRepo>();
builder.Services.AddScoped<IStocksServices, StocksServices>();

builder.Services.AddAutoMapper(typeof(FiltersMappingProfile));
builder.Services.AddAutoMapper(typeof(StocksMappingProfile));

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("https://carwale.com") 
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});


builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors("AllowSpecificOrigin");

app.UseRouting();
app.MapControllers(); 

app.Run();
