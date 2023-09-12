
# Настройка образца для использования SQL Server

1. Убедитесь, что ваши строки подключения в `appsettings.json` указывают на локальный экземпляр SQL Server.

2. Закомментируйте сервисы
```
//builder.Services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));
//builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

//builder.Services.AddScoped<IWordService, WordService>();

//builder.Logging.ClearProviders();
//builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
//builder.Logging.AddConsole();
```

3. Откройте командную строку в папке NotTasyuCupcake.ExampleFileWorker.ConsoleUI и выполните следующие команды:

```
Update-Database -Context DataContext -Project NotTasyuCupcake.ExampleFileWorker.Infrastructure -StartupProject NotTasyuCupcake.ExampleFileWorker.ConsoleUI
```

**Примечание.** Если вам нужно создать миграции, вы можете использовать эти команды:

```
Add-Migration -Name DataMigration -Context DataContext -Project NotTasyuCupcake.ExampleFileWorker.Infrastructure -StartupProject NotTasyuCupcake.ExampleFileWorker.ConsoleUI
```