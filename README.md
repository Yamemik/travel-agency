# TravelAgency

## Описание

## .NET

```bash
	# update
$	dotnet tool update --global dotnet-ef
```

## DB

```bash
$ dotnet ef dbcontext scaffold "Filename=sql-scripts/travel_db.db" Microsoft.EntityFrameworkCore.Sqlite --output-dir Models --namespace TravelAgency --data-annotations --context TravelDBContext
$ sqlite3 travel_db.db -init travel_db.sql
```

