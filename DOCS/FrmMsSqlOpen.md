# Documentación Técnica: FrmMsSqlOpen.cs

Componente de selección de servidor, base de datos y tabla para el proceso de extracción de datos.

## 1. Propósito
El propósito de este formulario es proporcionar una interfaz de usuario independiente y reutilizable para configurar conexiones hacia **Microsoft SQL Server**. Está diseñado para evitar errores de escritura manual de cadenas de conexión, permitiendo al usuario explorar visualmente los catálogos del servidor.

## 2. Características de Interfaz (UI)
*   **Configuración de Servidor**: Campo de texto para dirección/instancia.
*   **Selector de Autenticación**:
    *   **Autenticación de Windows**: Bloquea y limpia credenciales secundarias.
    *   **Autenticación SQL Server**: Habilita campos de Usuario y Password con enmascaramiento.
*   **Explorador Bi-Panel**:
    *   **Listado de Bases de Datos (Izquierda)**: Carga dinámica tras pulsar "Refrescar". Filtra automáticamente las bases de datos del sistema (`master`, `model`, `msdb`, `tempdb`).
    *   **Listado de Tablas (Derecha)**: Carga reactiva inmediata al seleccionar cualquier base de datos de la izquierda.
*   **Gestión de Estructura**: Permite crear nuevas bases de datos y tablas mock (con una sola columna de ID) directamente desde la UI.
*   **Consistencia Visual**: Incluye soporte para extensiones de formularios LatinoGIS (`this.FormFixedNoGrow()`).

## 3. Implementación Técnica
### Tecnologías Utilizadas:
*   **Acceso a Datos**: `System.Data.SqlClient` (Nativo .NET 4.8.1).
*   **Micro-ORM**: `Dapper` (para simplificar la conversión de metadatos de SQL a listas tipadas).
*   **Consultas de Metadatos**:
    *   *Bases de Datos*: `SELECT name FROM sys.databases WHERE database_id > 4 ORDER BY name`.
    *   *Tablas*: `SELECT name FROM sys.tables ORDER BY name`.

### Propiedades de Salida (Output):
*   `ConnectionString`: Cadena de conexión completa lista para instanciar un `SqlConnection`.
*   `SelectedTable`: Nombre de la tabla de destino seleccionada (opcional).

## 4. Seguridad
*   La cadena de conexión inyecta automáticamente `TrustServerCertificate=True` para prevenir rechazos por certificados SSL no validados en entornos locales o de desarrollo.
*   El tiempo de espera de conexión (`ConnectTimeout`) está limitado a 10 segundos para no bloquear la UI en caso de servidores erróneos.
