# ProductosApi

API REST con operaciones CRUD sobre la entidad **Producto**, desarrollada en .NET 10 con SQLite y documentada con Swagger.

## Tecnologías
- .NET 10 (ASP.NET Core Web API con Controllers)
- SQLite (`Microsoft.Data.Sqlite`)
- Swagger (`Swashbuckle.AspNetCore`)

## Estructura
- `Models/Producto.cs`: entidad
- `Repositories/IProductoRepository.cs`: interface
- `Repositories/ProductoRepository.cs`: implementación (conexión, consultas, insert, actualizar, eliminar, buscar)
- `Controllers/ProductosController.cs`: endpoints
- `Data/DbInicializador.cs`: crea la base y la tabla al iniciar

## Script de la tabla
```sql
CREATE TABLE IF NOT EXISTS productos (
    id     INTEGER PRIMARY KEY AUTOINCREMENT,
    nombre TEXT    NOT NULL,
    precio REAL    NOT NULL,
    stock  INTEGER NOT NULL
);
```

## Ejecución
```bash
dotnet restore
dotnet run
```
Abrir `http://localhost:<puerto>/swagger`. La base `tienda.db` se crea automáticamente.

## Endpoints
| Método | Ruta | Descripción | Respuesta |
|--------|------|-------------|-----------|
| GET | /api/productos | Listar todos | 200 |
| GET | /api/productos/{id} | Buscar por id | 200 / 404 |
| POST | /api/productos | Insertar | 201 / 400 |
| PUT | /api/productos/{id} | Actualizar | 204 / 404 |
| DELETE | /api/productos/{id} | Eliminar | 204 / 404 |

## Evidencias
Capturas de las pruebas en Swagger en la carpeta [`capturas`](capturas).