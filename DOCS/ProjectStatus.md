# Estado del Proyecto: Aceras.Extraction (Hitos y Arquitectura)

Fecha: 2026-03-19
Objetivo: Migración de datos capturados en Android (SQLite) a SQL Server (Latinogis)

## 1. Infraestructura y Portabilidad
*   **MSBuild Path Routing**: Se implementó `Directory.Build.props` en la raíz de `src.net.aceras`.
*   **Repositorios Vinculados**: Configuración dinámica de rutas para `Sgrin.Utils`, `F472LTN` y `YASoft` mediante variables globales calculadas desde la raíz del proyecto.
*   **NuGet Local**: Los paquetes se gestionan en la carpeta `/packages` del repositorio (`RestorePackagesPath`).

## 2. Modelo de Datos (Capas)
Se definieron tres niveles de abstracción para asegurar integridad y legibilidad:
1.  **`DataTable` (SQLite)**: Reflejo exacto de la tabla física en Android. Campo clave: `DATOS` (JSON).
2.  **`AceraDto` (JSON)**: Objeto de transferencia que mapea la estructura recibida de la app Vue (incluye objetos anidados `LatLng` y `LocalProj`).
3.  **`AceraTable` (SQL Server)**: Tabla de destino "aplanada".
    *   Usa el prefijo `LtnId` como clave primaria manual (no incremental).
    *   **Estructura de Pares**: Cada campo de evaluación tiene un campo `_Value` (peso) y un campo `_Text` (descripción literal extraída de `FormAcera.js`).

## 3. Lógica de Mapeo (`AceraMapper.cs`)
*   **Traducción de Diccionarios**: Contiene 18 mapeos que inyectan las descripciones de los selectores (Vue) directamente en las columnas de texto de SQL Server.
*   **Regla de Negocio `NoTieneAcera`**: Si el flag es `true`, el mapeador automáticamente limpia todos los campos de evaluación técnica (los pone en `string.Empty` o `0`) para evitar ruidos de datos.

## 4. UI de Extracción (Detalle de Formularios)
Se han diseñado dos formularios independientes para separar responsabilidades:

### `FrmSetDatas.cs` (Orquestador Principal)
*   **Responsabilidad**: Es el "puente" entre el origen (SQLite) y el destino (SQL Server).
*   **Controles de Origen**: Usa un `OpenFileDialog` filtrado (`.db`, `.sqlite`) para asegurar que el usuario seleccione la base de datos correcta de Android.
*   **Rol en el Proceso**: Una vez configuradas las rutas, este disparará el flujo que lee el SQLite, invoca al Mapper y guarda en SQL Server masivamente.

### `FrmMsSqlOpen.cs` (Gestor de Conexiones MS SQL)
*   **Responsabilidad**: Aislar la complejidad de conectarse a un servidor MS SQL Server real (No LocalDB).
*   **Modos de Autenticación**:
    *   **Autenticación de Windows**: Aprovecha las credenciales de red del usuario actual (`Integrated Security=True`). Al activarse, bloquea los campos de Usuario/Contraseña para evitar ruidos.
    *   **Autenticación de SQL Server**: Requiere usuario y contraseña explícita.
*   **Seguridad y Generación**: 
    *   Genera dinámicamente un `ConnectionString` incluyendo `TrustServerCertificate=True` para prevenir errores SSL en desarrollo.
    *   Expone el resultado mediante una propiedad pública `ConnectionString` que es capturada por el formulario padre tras un `DialogResult.OK`.

## 5. Estado de Implementación (Actualizado)
*   [x] **NLog 6.0 Integration**: Sistema de log thread-safe con triple canal (Info, Warn, Error).
*   [x] **SQLite Cipher Support**: Estrategia de apertura dual (con/sin clave) y despliegue de `e_sqlcipher.dll`.
*   [x] **WKT/GDAL Transformation**: Reproyección de coordenadas (4326 -> 32616) y escalado x1000 integrada.
*   [x] **Mapeo de Datos**: Flujo completo desde SQLite -> DTO -> SQL Server.
*   [x] **Estabilidad de Tipos**: Solucionado el error de `NullableDateTimeHandler` mediante el uso de strings para fechas en el DTO de SQLite.

## 6. Próximos Pasos (Optimización)
*   [ ] Implementar sistema de **Pausa/Reanudar** mediante `ManualResetEventSlim`.
*   [ ] Añadir barra de progreso visual (ProgressBar) vinculada al conteo de registros.
*   [ ] Refactorizar la lógica de inserción para usar transacciones (`SqlTransaction`) por lotes.
