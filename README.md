# ������������� ������ �����������

## ��������
###
###

## .NET

```bash
# �������� dotnet �� ��������� ������
$ dotnet tool update --global dotnet-ef
```
```bash
# ����������� ���������
$ dotnet ef dbcontext scaffold "Filename=sql-scripts/travel_db.db" Microsoft.EntityFrameworkCore.Sqlite --output-dir Models --namespace TravelAgency --data-annotations --context TravelDBContext
```

## DB (SQlite)

```bash
# ���������� ���� ������
$ sqlite3 travel_db.db -init travel_db.sql
```