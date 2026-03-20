# Recapitulación de Implementación: Extracción de Aceras (SQLite -> SQL Server)

Este documento detalla los hitos y soluciones técnicas aplicadas durante el desarrollo del motor de extracción para el proyecto Aceras.

## 1. Infraestructura de Logging (NLog)
Se ha implementado un sistema de logs robusto y multi-panel en `FrmProcess`:
*   **RichTextBoxTarget**: Un target personalizado de NLog que garantiza escrituras seguras entre hilos (`Thread-safe`) en los controles de la UI usando `Invoke`.
*   **Triple Canal**: Los mensajes se enrutan automáticamente a tres `RichTextBox` independientes:
    *   **Info**: Proceso general y éxito.
    *   **Warn**: Advertencias de rendimiento o inconsistencias.
    *   **Error**: Excepciones críticas y fallos de conexión.
*   **Layout Simplificado**: Se ajustó el formato para mostrar solo la hora y el mensaje `[HH:mm:ss] Mensaje`, eliminando el ruido del nombre de la clase y el nivel de log.

## 2. Conectividad SQLite y Cifrado (SQLCipher)
Para garantizar la compatibilidad con las bases de datos de Android (que pueden estar cifradas):
*   **Estrategia Dual**: `GetSqliteConnection()` intenta abrir la base de datos sin contraseña primero; si falla, reintenta con la clave configurada para SQLCipher.
*   **Baterías SQLitePCL**: Se añadió la inicialización manual `SQLitePCL.Batteries.Init()` necesaria para registrar los proveedores nativos en .NET Framework 4.8.1.
*   **Despliegue Nativo**: Se configuró el `.csproj` para importar `SQLitePCLRaw.lib.e_sqlcipher.targets`, asegurando que la DLL nativa `e_sqlcipher.dll` se copie al directorio de salida.

## 3. Explorador de SQL Server (`FrmMsSqlOpen`)
Se desarrolló un formulario dedicado para la configuración dinámica del destino:
*   **Gestión de Estados**: Los botones de "Nueva Base de Datos" y "Nueva Tabla" se bloquean dinámicamente hasta que se valida una conexión con el servidor.
*   **Limpieza Automática**: Incluye lógica para eliminar bases de datos o tablas creadas durante la exploración si el usuario cancela la operación, manteniendo el servidor limpio.
*   **Persistencia**: Capacidad de inicializarse a partir de una cadena de conexión existente, activando los controles correspondientes.

## 4. Motor de Procesamiento y Mapeo
El proceso de carga masiva en `FrmProcess.DoWork` incluye:
*   **Asincronismo**: Ejecución en un hilo de fondo (`Task.Run`) con soporte para cancelación mediante `CancellationToken`.
*   **Mapeo de Datos**: 
    *   Extracción de JSON binario desde SQLite.
    *   Transformación de coordenadas WKT (Reproyección 4326 -> 32616 y escalado factor 1000).
    *   Carga en dos tablas de SQL Server: Una para el elemento geográfico (`LtnElements_...`) y otra para los datos alfanuméricos (`...`).
*   **Corrección de Tipos**: Se cambió el tipo de la columna `FECHA` en el DTO de `DateTime?` a `string` para evitar fallos del `TypeHandler` de la librería `YASoft` con drivers de SQLite que entregan texto.

## 5. Solución de Conflictos de Dependencias
*   **NLog 6.0.1**: Se actualizó el proyecto de la versión 5.2 a la 6.x para cumplir con los requisitos de las librerías `Sgrin` y resolver errores de carga de ensamblado.
*   **Binding Redirects**: Se configuraron redirecciones en `App.config` para `System.Memory` (v4.0.1.1) y `System.Runtime.CompilerServices.Unsafe` (v4.0.4.1), resolviendo discrepancias de versiones en tiempo de ejecución.

## Próximos Pasos (Opcional)
*   **Pausa**: Implementar `ManualResetEventSlim` para permitir pausar el proceso sin perder el estado.
*   **Retry Logic**: Añadir reintentos automáticos en caso de micro-cortes de red con el SQL Server.
*   **Validación de Esquema**: Comparar la estructura de la tabla destino antes de iniciar la inserción masiva.
