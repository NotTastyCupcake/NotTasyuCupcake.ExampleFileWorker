
# ��������� ������� ��� ������������� SQL Server

1. ���������, ��� ���� ������ ����������� � `appsettings.json` ��������� �� ��������� ��������� SQL Server.

2. ��������������� �������
```
//builder.Services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));
//builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

//builder.Services.AddScoped<IWordService, WordService>();

//builder.Logging.ClearProviders();
//builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
//builder.Logging.AddConsole();
```

3. �������� ��������� ������ � ����� NotTasyuCupcake.ExampleFileWorker.ConsoleUI � ��������� ��������� �������:

```
Update-Database -Context DataContext -Project NotTasyuCupcake.ExampleFileWorker.Infrastructure -StartupProject NotTasyuCupcake.ExampleFileWorker.ConsoleUI
```

**����������.** ���� ��� ����� ������� ��������, �� ������ ������������ ��� �������:

```
Add-Migration -Name DataMigration -Context DataContext -Project NotTasyuCupcake.ExampleFileWorker.Infrastructure -StartupProject NotTasyuCupcake.ExampleFileWorker.ConsoleUI
```