# Desafio

Desafio de gerenciamento de perfils para vaga na empresa GS3.

## Executar projeto frontend (pasta Web)

```bash
ng serve -open
```

## Executar projeto backend (pasta API)

```bash
dotnet run
```

## Usuários administrador para login (acesso total)

admin@admin.com

admin123

## Banco utilizado para o projeto

SQLite

## Caso o banco não tenha sido inicializado

```bash
dotnet ef migrations add InitDatabase
```

```bash
dotnet ef database update
```