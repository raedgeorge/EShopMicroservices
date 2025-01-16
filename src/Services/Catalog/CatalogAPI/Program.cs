using BuildingBlocks.Behaviors;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddCarter();

var assembly = typeof(Program).Assembly;

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
});


builder.Services.AddValidatorsFromAssembly(assembly);

// configure Marten to connect to PostgreSQL
builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("Database")!);

}).UseLightweightSessions();

var app = builder.Build();

// Configure HTTP request pipeline
app.MapCarter();

app.Run();
