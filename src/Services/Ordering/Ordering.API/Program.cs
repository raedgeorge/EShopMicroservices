using Ordering.API;
using Ordering.Application;
using Ordering.InfraStructure;
using Ordering.InfraStructure.Data.Extensions;

var builder = WebApplication.CreateBuilder(args);

// add services to the container.

builder.Services.AddApplicationServices()
                .AddAPIServices()
                .AddInfratructureServices(configuration: builder.Configuration);

var app = builder.Build();

// configure the HTTP request pipeline.

app.UseApiServices();


if (app.Environment.IsDevelopment())
{
    await app.InitializeDatabaseAsync();   
}


app.Run();
