find /Volumes/s/projects/velto-erp/api -name '._*' -delete
## 1
dotnet new webapi --use-controllers -o TodoApi
dotnet dev-certs https --trust
dotnet run --launch-profile https

## 2
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL

## tables
CREATE TABLE users (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    tenant_id UUID REFERENCES tenants(id) ON DELETE CASCADE,
	role_id UUID REFERENCES roles(id) ON DELETE SET NULL,
    email varchar(255) UNIQUE NOT NULL,
    password_hash TEXT NOT NULL,
    time_zone TEXT DEFAULT NULL, -- kullanıcıya özel timezone (isteğe bağlı)
    created_at TIMESTAMPTZ DEFAULT NOW()
);