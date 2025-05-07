find /Volumes/s/projects/velto-erp/api -name '._*' -delete
## 1
dotnet new webapi --use-controllers -o TodoApi
dotnet dev-certs https --trust
dotnet run --launch-profile https

## 2
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL