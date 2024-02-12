# Автоматизация работы Турагенства

## Описание
###
###

## .NET

```bash
# обновить dotnet до последней версии
$ dotnet tool update --global dotnet-ef
```
```bash
# реверсивная инженерия
$ dotnet ef dbcontext scaffold "Filename=sql-scripts/travel_db.db" Microsoft.EntityFrameworkCore.Sqlite --output-dir Models --namespace TravelAgency --data-annotations --context TravelDBContext
```

## DB (SQlite)

```bash
# развернуть базу данных
$ sqlite3 travel_db.db -init travel_db.sql
```