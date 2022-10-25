# MarketMicroservices

FluentMigrator migrate command:
dotnet fm migrate -p PostgreSQL11_0 -c "Host=127.0.0.1;Port=5432;Database=products;Username=postgres;Password=1337;Pooling=true;MinPoolSize=0;MaxPoolSize=50" -a "./bin/Debug/net6.0/Products.Api.dll" -n "Products.Api.Data.Migrations"