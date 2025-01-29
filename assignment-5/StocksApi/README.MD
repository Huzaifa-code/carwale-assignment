## How this StocksApi project was created 

```bash
dotnet new webapi -o api
```

```bash
cd ./api
```


```bash
dotnet watch run
```


## Swagger DOCS :

Installing swaggers package

```bash
dotnet add package Swashbuckle.AspNetCore
```

Check [http://localhost:5204/swagger](http://localhost:5273/swagger/index.html)

In `Program.cs` add the following code :

```cs
builder.Services.AddSwaggerGen();
```

```cs
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
```


## Connecting mysql database :



```
 dotnet add package MySql.Data
```

```
 dotnet add package Dapper
```