## How this Carwale project was created 

```bash
dotnet new webapi -o api
```

```bash
cd ./api
```

### Start the server : 
```bash
dotnet watch run
```

or 
```
dotnet run
```

### Open swagger docs at :

[localhost:5204/swagger](localhost:5204/swagger)


## Swagger DOCS :

Installing swaggers package

```bash
dotnet add package Swashbuckle.AspNetCore
```


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

##  GRPC Client :

```
dotnet add package Grpc.Net.Client
dotnet add package Google.Protobuf
dotnet add package Grpc.Tools
dotnet add package Grpc.Net.ClientFactory
```



## Connecting mysql database :



```
 dotnet add package MySql.Data
```

```
 dotnet add package Dapper
```


Swagger : 

![](https://i.imgur.com/fQCdGpP.png)

![](https://i.imgur.com/TDQyjKj.png)