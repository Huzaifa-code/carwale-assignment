using Carwale.Mappers;
using Carwale.GrpcServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();

builder.Services.AddAutoMapper(typeof(FiltersMappingProfile));
builder.Services.AddAutoMapper(typeof(StocksMappingProfile));
builder.Services.AddAutoMapper(typeof(CitiesMappingProfile));


// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("https://stg.carwale.com") 
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});


builder.Services.AddControllers();

builder.Services.AddGrpcClient<StocksServ.StocksServClient>(o =>
{
    o.Address = new Uri("http://localhost:5169");
});


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
