Asisya Products API

Descripción
Aplicación fullstack para gestión de productos.
Backend: ASP.NET Core 8 + SQL Server
Frontend: React JS
Autenticación: JWT

Funcionalidades:

Login
CRUD de productos
Paginación
Rutas protegidas

REQUISITOS

NET 8 SDK
Node.js (v18+)
SQL Server
Git


CLONAR REPOSITORIO
git clone https://github.com/luisarmandoalk/AsisyaRepository.git
cd asisya-products-api

BACKEND (ASP.NET CORE)
Ir al proyecto Asisya.Api

Configurar conexión

Editar appsettings.json:

"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=AsisyaDb;Trusted_Connection=True;TrustServerCertificate=True;"
}

Crear base de datos
dotnet ef database update

Ejecutar API
dotnet run

Swagger disponible en:
https://localhost:xxxx/swagger


FRONTEND (REACT)

Ir al proyecto asisya-frontend

Instalar dependencias y ejecutar

npm start

Aplicación:
http://localhost:3000//login

LOGIN
Usar las siguientes credenciales
admin
123

GENERATE
Genera 100000 registros en la base de datos, requiere categoryId y supplierId válidos existente en base de datos. Se ejecuta desde el servicio Swagger del backend.

BULK
Carga masiva de registros. Se ejecuta desde el servicio Swagger del backend. Se usa un objeto JSON con la información necesaria.
[
  {
    "productName": "Producto 1",
    "unitPrice": 100,
    "categoryId": "11111111-1111-1111-1111-111111111111",
    "supplierId": "26a5dcc5-9060-446c-58f3-08de9b47ed46"
  },
  {
    "productName": "Producto 2",
    "unitPrice": 200,
    "categoryId": "11111111-1111-1111-1111-111111111111",
    "supplierId": "26a5dcc5-9060-446c-58f3-08de9b47ed46"
  }
]

ESCALABILIDAD (CLOUD)

Estrategia de escalamiento horizontal

La solución está diseñada para escalar horizontalmente en la nube siguiendo estos principios:

1. Stateless API

La API no mantiene estado en memoria.
Autenticación basada en JWT no requiere sesión en servidor.
Permite múltiples instancias.


2. Balanceo de carga

Distribuye tráfico entre múltiples instancias de la API


3. Escalamiento horizontal

Desplegar múltiples instancias del backend:

Azure App Service (scale out)
AWS ECS / Kubernetes

4. Base de datos

SQL Server en servicio administrado:
Azure SQL Database
AWS RDS
Escalado vertical o réplicas de lectura


5. Contenerización
La aplicación usa Docker 
Orquestación con Kubernetes

7. CDN (Frontend)

Servir React desde CDN mejora rendimiento 

Decisiones Arquitectónicas

1. Arquitectura del Backend

Se implementó una arquitectura en capas basada en principios de separación de responsabilidades:

API (Capa de presentación)
  Expone endpoints REST y maneja las solicitudes HTTP.

Capa de aplicación
  Contiene la lógica de negocio y servicios.

Capa Dominio
  Define las entidades del negocio y reglas principales.

Capa Infraestructura
  Maneja acceso a datos (Entity Framework Core, SQL Server).

Motivo:
Permite escalabilidad, mantenibilidad y facilita testing.

2. Uso de Entity Framework Core

Se utilizó EF Core como ORM para interactuar con SQL Server.

Motivo:

Reduce código repetitivo
Permite migraciones automáticas

3. Base de Datos Relacional (SQL Server)

Se eligió SQL Server como motor de base de datos por la disponibilidad de la herramienta

Motivo:

Soporte para relaciones complejas (FK)
Integridad referencial
Escalabilidad en entornos empresariales

4. Autenticación con JWT

Se implementó autenticación basada en JSON Web Tokens.

Motivo:

Permite APIs stateless
Facilita escalabilidad horizontal

5. Frontend como SPA (React)

Se desarrolló una Single Page Application usando React.

Motivo:

Navegación rápida sin recargas
Separación clara frontend/backend


6. Protección de Rutas (AuthGuard)

Se implementó un guard en React para proteger rutas privadas.

Motivo:

Evitar acceso sin autenticación

7. Paginación en Backend

La paginación se implementó del lado del servidor.

Motivo:

Mejora rendimiento con grandes volúmenes (100k+ registros)
Reduce carga en frontend

8. Bulk Insert con procesamiento por lotes

Se implementó inserción masiva en batches.

Motivo:

Evita consumo excesivo de memoria
Mejora rendimiento en cargas grandes

9. Data Seeding

Se agregaron datos iniciales (Category, Supplier).

Motivo:

Facilita pruebas
Evita errores de claves foráneas

10. API Stateless

La API no guarda estado en memoria.

Motivo:

Permite escalar horizontalmente
Compatible con balanceadores de carga

11. Pipeline CI/CD (GitHub Actions)

Se configuró pipeline para build, test y validaciones.

Motivo:

Automatización

12. Separación Frontend / Backend

El proyecto está dividido en dos aplicaciones independientes.

Motivo:

Despliegue independiente
Escalabilidad
Mejor organización del código

13. Uso de GUID como clave primaria

Se utilizó Guid en lugar de int.

Motivo:

Unicidad global
Mejor para sistemas distribuidos



Autor
Luis Armando Alquichire
