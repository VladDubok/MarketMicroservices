using System.Reflection;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = new ServiceCollection()
    .AddFluentMigratorCore()
    .ConfigureRunner(rb => rb
        .AddPostgres11_0()
        .WithGlobalConnectionString("Host=127.0.0.1;Port=5432;Database=products;Username=postgres;Password=1337;Pooling=true;MinPoolSize=0;MaxPoolSize=50")
        .ScanIn(Assembly.GetExecutingAssembly()).For.Migrations())
    .AddLogging(lb => lb.AddFluentMigratorConsole())
    .BuildServiceProvider(false);

var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

//runner.MigrateUp(1);
