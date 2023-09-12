// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NotTasyuCupcake.ExampleFileWorker.ApplicationCore.Interfaces;
using NotTasyuCupcake.ExampleFileWorker.ApplicationCore.Interfaces.Services;
using NotTasyuCupcake.ExampleFileWorker.ApplicationCore.Services;
using NotTasyuCupcake.ExampleFileWorker.ConsoleUI.View;
using NotTasyuCupcake.ExampleFileWorker.Infrastructure;
using NotTasyuCupcake.ExampleFileWorker.Infrastructure.Data;
using NotTasyuCupcake.ExampleFileWorker.Infrastructure.Services;

//Console.WriteLine("Hello, World!");


var builder = Host.CreateApplicationBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json", true);

var connectionString = builder.Configuration.GetConnectionString("WordsConnection");
Console.WriteLine(connectionString);

Dependencies.ConfigureServices(builder.Configuration, builder.Services);

builder.Services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));
builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
builder.Services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
builder.Services.AddScoped<IWordService, WordService>();
builder.Services.AddScoped<IFileWorkerService, FileWorkerService>();

builder.Services.AddHostedService<MainView>();

builder.Logging.ClearProviders();
builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
builder.Logging.AddConsole();

IHost host = builder.Build();
host.Run();