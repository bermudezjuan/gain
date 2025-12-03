# gain

# Dependencias

SDK de .NET: Versión .NET 8.0
Base de Datos: Una instancia local de SQL Server (Ej: SQL Server Express o LocalDB).
Herramientas de EF Core: tener las herramientas globales instaladas:
    dotnet tool install --global dotnet-ef

# Configurar la Base de Datos
Verificar la Cadena de Conexión

Edita el archivo appsettings.json (o appsettings.Development.json) dentro del proyecto gain-api para asegurar que la cadena de conexión apunte a tu instancia local de SQL Server.

Busca la sección "ConnectionStrings" y verifica GainConnectionString.

"ConnectionStrings": {
  "GainConnectionString": "Server=(localdb)\\mssqllocaldb;Database=GainDb;Trusted_Connection=True;MultipleActiveResultSets=true"
}

# Ejecutar la Migración

Ejecuta estos comandos desde la raíz de la solución o desde la carpeta gain-api.

Estos comandos utilizarán el proyecto gain-api como inicio (para leer la conexión) y el proyecto core (para encontrar las migraciones).

cd gain-api
dotnet ef database update --startup-project ./gain-api.csproj --project ../core/core.csproj

# Script de Creación de la vista

CREATE VIEW VW_AuditoriaReporte AS
SELECT
    A.Id AS AuditoriaId,
    A.Titulo,
    A.FechaCreacion,
    A.Estado,
    SUM(CASE
            WHEN H.Severidad = 3 THEN 1
            ELSE 0
        END) AS HallazgosAlta,
    SUM(CASE
            WHEN H.Severidad = 2 THEN 1
            ELSE 0
        END) AS HallazgosMedia,
    SUM(CASE
            WHEN H.Severidad = 1 THEN 1
            ELSE 0
        END) AS HallazgosBaja,
    COUNT(H.Id) AS HallazgosTotal
FROM
    Auditorias A
        INNER JOIN
    Hallazgos AH ON A.Id = AH.AuditoriaId
        INNER JOIN
    Hallazgos H ON AH.Id = H.Id
WHERE
    A.Estado = 3
GROUP BY
    A.Id, A.Titulo, A.FechaCreacion, A.Estado

# Ejecutar el Proyecto

Iniciar la API 

Ejecuta este comando desde la carpeta gain-api: dotnet run

# Acceder a Swagger
La API se ejecutará en un puerto local (generalmente 5000 o 7000).

Abre tu navegador.

Navega a la URL de Swagger/OpenAPI para probar los endpoints:

https://localhost:<PORT>/swagger (Ej: https://localhost:7001/swagger)

# Descripcion de la solución:

Arquitectura de Capas y Tecnología BaseLa solución se estructura alrededor de una arquitectura de capas, separando claramente la presentación/interfaz (API) de la lógica de negocio y el acceso a datos.
## Presentación/API gain-api (ASP.NET Core Web API)Maneja las solicitudes HTTP (enrutamiento, Model Binding), inyección de dependencias y serialización JSON. Utiliza Swagger/OpenAPI para la documentación y prueba de endpoints.

## Acceso a Datos/Dominio core (Clases, Interfaces, EF Core) Contiene las Entidades de Dominio (Auditoria, Responsable), el DbContext, las interfaces de servicio y la lógica de acceso a datos (servicios genéricos).

##Persistencia Entity Framework Core (EF Core) Es el ORM (Mapeador Objeto-Relacional) que gestiona la conexión a SQL Server, la creación del esquema (Code First a través de Migraciones) y las consultas LINQ.

# Componentes Clave y Patrones Utilizados
##  Manejo de Datos y Patrones Genéricos
Servicio Genérico (IBaseService<T> / BaseService<T>): Se implementa un patrón de Repositorio o Servicio base genérico, restringido por la entidad base (BaseEntity), para centralizar las operaciones CRUD básicas y reutilizarlas en todas las entidades (DRY - Don't Repeat Yourself).

Servicio Especializado (IAuditoriaService): Implementa la interfaz genérica y añade lógica de negocio específica para Auditoria, como la búsqueda por múltiples parámetros y la asignación de hallazgos/responsables. Esto adhiere al Principio de Segregación de Interfaces (ISP).

Vistas de Base de Datos (SQL): Se mapean Vistas de la base de datos a Entidades sin clave (Keyless Entities) en EF Core para consultas de resumen complejas (ej., conteo de severidad de hallazgos), optimizando el rendimiento de los informes sin modificar el modelo transaccional.

## Validación y Mapeo
FluentValidation: Se utiliza para la validación explícita de los DTOs (ej., AuditoriaDto). Se implementó un Action Filter (ValidationFilter<T>) para mover el chequeo de validación fuera de los métodos del controlador, haciendo el código más limpio y modular.

AutoMapper: Es fundamental para mapear las transferencias de datos, especialmente:

Deserialización: DTOs de entrada (UpdateAuditoriaDto) a Entidades (Auditoria).

Proyección: Se utiliza .ProjectTo<DTO> en las consultas LINQ (ej., obtener Auditorías por Responsable) para ejecutar el mapeo a nivel de base de datos. Esto es clave para evitar ciclos de objeto (System.Text.Json.JsonException) y optimizar la carga de datos.

## Diseño de Endpoints
Actualizaciones Parciales (PATCH): Se utiliza el verbo HTTP PATCH para operaciones que modifican parcialmente un recurso (ej., AsignarResponsable), siguiendo las mejores prácticas de REST.

Búsqueda Flexible: Los endpoints de búsqueda utilizan el verbo HTTP GET con parámetros opcionales en la cadena de consulta (Query String), permitiendo filtrar dinámicamente usando LINQ. El sistema de Model Binding de ASP.NET Core maneja automáticamente la conversión de Query String a tipos complejos como enum (para el estado).