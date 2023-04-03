
# Library web

CRUD Web API to simulate a library (create, update, delete,
get) runs on .Net Core using EF Core

## Deployment
Change connection string in appsettings.json to your value.

To deploy this project run

In LibraryWeb.Api project 
```bash
  dotnet run
```

If migrations are missing:

Run in LibraryWeb.Migrations

```
    dotnet ef migrations add InitialIdentityServerPersistedGrantDbMigration -c PersistedGrantDbContext -o Migrations/IdentityServer/PersistedGrantDb
```

```
    dotnet ef migrations add InitialIdentityServerConfigurationDbMigration -c ConfigurationDbContext -o Migrations/IdentityServer/PersistedGrantDb
```

```
    dotnet ef migrations add init -c DatabaseContext
```

And update database
```
dotnet ef database update PersistedGrantDbContext
```
```
dotnet ef database update ConfigurationDbContext
```
```
dotnet ef database update DatabaseContext
```
