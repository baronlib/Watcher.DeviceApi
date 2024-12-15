using Watcher.DeviceApi.Models;
using Watcher.DeviceLibrary;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<Location>(builder.Configuration.GetSection("Location"));

builder.Services.AddSingleton<IDeviceRepository, InMemoryDeviceRepository>();
builder.Services.AddSingleton<IDeviceTimer, DeviceTimer>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
