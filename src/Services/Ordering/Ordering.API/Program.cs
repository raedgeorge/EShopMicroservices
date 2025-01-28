using Ordering.API;
using Ordering.Application;
using Ordering.InfraStructure;

var builder = WebApplication.CreateBuilder(args);

// add services to the container.

builder.Services.AddApplicationServices()
                .AddAPIServices()
                .AddInfratructureServices(configuration: builder.Configuration);

var app = builder.Build();

// configure the HTTP request pipeline.

app.UseApiServices();

app.Run();
