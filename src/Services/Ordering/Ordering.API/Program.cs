using Ordering.API;
using Ordering.Application;
using Ordering.InfraStructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices()
                .AddAPIServices()
                .AddInfratructureServices(configuration: builder.Configuration);

// add services to the container.

var app = builder.Build();

// configure the HTTP request pipeline.

app.UseApiServices();

app.Run();
