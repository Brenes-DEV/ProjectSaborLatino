# Sabor Latino

Repositorio único del proyecto, dividido en tres partes:

| Carpeta       | Contenido                                       |
|---------------|-------------------------------------------------|
| `backendSL/`  | API en ASP.NET Core (.NET 10) con EF Core       |
| `frontendSL/` | Interfaz web en React + Vite + TypeScript       |
| `dbSL/`       | Diagramas, datos de prueba y consultas SQL      |

## Requisitos

- Visual Studio 2026 con la carga "Desarrollo de ASP.NET y web" (.NET 10 y SQL Server LocalDB)
- Herramienta de migraciones: `dotnet tool install --global dotnet-ef`
- Node.js LTS (para el frontend)

## Primera vez

```
git clone https://github.com/Brenes-DEV/ProjectSaborLatino.git
cd ProjectSaborLatino/backendSL
dotnet restore
dotnet ef database update
```

## Backend

Abrir `ProjectSaborLatino.slnx` en Visual Studio y presionar F5, o desde terminal:

```
cd backendSL
dotnet run
```

La API queda en `https://localhost:7154` y `http://localhost:5238`.

## Base de datos

Motor: **SQL Server LocalDB** (`(localdb)\MSSQLLocalDB`, base `SaborLatinoDb`).
Las tablas se crean y actualizan con las **migraciones de Entity Framework** en `backendSL/Migrations/`.
Después de cada `git pull`, si hay migraciones nuevas:

```
cd backendSL
dotnet ef database update
```

## Frontend

Ver `frontendSL/README.md`.

## Forma de trabajo en grupo

1. Antes de empezar: `git pull` en `main`.
2. Crear una rama para cada tarea: `git switch -c feature/nombre-de-la-tarea`.
3. Subir la rama y abrir un Pull Request hacia `main`.
4. Otro integrante lo revisa y lo une.
