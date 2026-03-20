using Aceras.Models.Dto;
using Aceras.Models.Mappers;
using Aceras.Models.SqLiteTables;
using Dapper;
using LtnToWkt;
using LtnToWkt.Model;
using Microsoft.Data.Sqlite;
using NLog;
using NLog.Config;
using NLog.Targets;
using OLatino;
using Sgrin.Models;
using Sgrin.Wkt;
using Sgrin.Wkt.Gdal;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using YASoft.Dapper.DapperQueryUtils;
using YASoft.Utils.Static.Serialization;

namespace Extract.To.Ltn
{
    public partial class FrmProcess : Form
    {
        private string _sqlitePath;
        private string _connString;
        private string _targetTable;

        private const string SqlitePassword = "supersecretpassword"; // Mantener vacío si no hay clave. SQLCipher la usará si se rellena.

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private CancellationTokenSource _cts;

        public FrmProcess(string sqlitePath, string connString, string targetTable)
        {
            InitializeComponent();
            
            // Inicializar motor SQLite con soporte SQLCipher
            SQLitePCL.Batteries.Init();

            _sqlitePath = sqlitePath;
            _connString = connString;
            _targetTable = targetTable;

            SetupLogging();
        }

        private void SetupLogging()
        {
            var config = new LoggingConfiguration();
            var simpleLayout = @"[${date:format=HH\:mm\:ss}] ${message}${onexception:${newline}${exception:format=tostring}}";

            // Target para Info (Log General)
            var infoTarget = new RichTextBoxTarget(txtLogInfo) { Name = "info", Layout = simpleLayout };
            config.AddTarget(infoTarget);
            config.AddRule(LogLevel.Info, LogLevel.Info, infoTarget);

            // Target para Warn
            var warnTarget = new RichTextBoxTarget(txtLogWarn) { Name = "warn", Layout = simpleLayout };
            config.AddTarget(warnTarget);
            config.AddRule(LogLevel.Warn, LogLevel.Warn, warnTarget);

            // Target para Error
            var errTarget = new RichTextBoxTarget(txtLogErr) { Name = "error", Layout = simpleLayout };
            config.AddTarget(errTarget);
            config.AddRule(LogLevel.Error, LogLevel.Fatal, errTarget);

            LogManager.Configuration = config;
        }

        private void FrmProcess_Load(object sender, EventArgs e)
        {
            this.FormFixedNoGrow();
        }

        private void FrmProcess_Shown(object sender, EventArgs e)
        {
            // Este es el punto de inicio para el proceso masivo
            // La UI ya es visible, podemos comenzar la extracción
            StartExtraction();
        }

        private async void StartExtraction()
        {
            _cts = new CancellationTokenSource();
            btnParar.Enabled = true;
            btnCerrar.Enabled = false;

            Logger.Info("Iniciando proceso de extracción...");
            Logger.Info($"Origen: {_sqlitePath}");
            Logger.Info($"Destino: {_targetTable}");

            try
            {
                await Task.Run(() => DoWork(_cts.Token), _cts.Token);
                Logger.Info("Proceso finalizado correctamente.");
            }
            catch (OperationCanceledException)
            {
                Logger.Warn("El proceso fue detenido por el usuario.");
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error crítico durante la extracción.");
            }
            finally
            {
                btnParar.Enabled = false;
                btnCerrar.Enabled = true;

                // Habilitar exportación si hay datos
                btnExportInfo.Enabled = txtLogInfo.TextLength > 0;
                btnExportWarn.Enabled = txtLogWarn.TextLength > 0;
                btnExportErr.Enabled = txtLogErr.TextLength > 0;
            }
        }

        private void DoWork(CancellationToken token)
        {
            _targetTable = _targetTable.Replace(new[] { "dbo.","[dbo]." }, "", StringComparison.InvariantCultureIgnoreCase);
            _targetTable = _targetTable.Replace(new[] { "[","]" }, "");

            var ltnTable = $"LtnElements_{_targetTable}";
            var dataTable = $"{_targetTable}";


            var converter = Wkt2WktCoordProjGdalConverter.FromEpsgCode(4326, 32616, ignoreZ: true, preSwapXy: true, postScaleFactor: 1000);
            object objRef;
            var layer = 1;

            try
            {
                Logger.Info("Validando conexión a base de datos destino...");
                using (var sqlConnection = new SqlConnection(_connString))
                {
                    sqlConnection.Open();

                    sqlConnection.GrantLtnElementsTable(ltnTable, overwrite: true);
                    GrantDataTable(sqlConnection, dataTable);
                    GrantLtnConfig(sqlConnection, dataTable, layer);


                    Logger.Info("Validando conexión a base de datos origen...");
                    using (var sqliteConnection = GetSqliteConnection())
                    {
                        Logger.Info("Conexión SQLite exitosa.");

                        var ltnId = 0;
                        
                        foreach (var srcData in sqliteConnection.GetData<Aceras.Models.SqLiteTables.DataTable>("DATOS"))
                        {
                            ltnId++;
                            Logger.Info($"Extrayendo punto {ltnId}...");

                            var aceraDto = DataJsonSerialization<AceraDto>.ReadDataFromJsonString(srcData.DATOS, indented: false,
                                typeNameHandling: false);

                            Logger.Info($"Mapeando DTO");
                            var acera = AceraMapper.MapToTable(aceraDto);

                            var wkt = $"POINT({acera.Lng} {acera.Lat})";
                            Logger.Info($"Creando Wkt: {wkt}");

                            Logger.Info($"Reproyectando 4326 => 32616 y escalando en 1000");
                            wkt = converter.WktTransform(wkt);
                            Logger.Info($"Wkt reproyectado y esacalado: {wkt}");

                            Logger.Info($"Generando Ltn Point");
                            var ltnElement = LtnWktMapper.WKTToLtn(LtnNType.EPoint, wkt);
                            var dbLtnElement = ltnElement.ToDbLtnElement();

                            dbLtnElement.LtnId = ltnId;
                            dbLtnElement.Layer = (short)layer;
                            acera.LtnId = ltnId;
                            
                            Logger.Info($"Almacenando Ltn en: {ltnTable}");
                            objRef = dbLtnElement; // Para debugging, inspeccionar este objeto en tiempo de ejecución
                            sqlConnection.InsertData(ltnTable, ref objRef, keyName: "FP");

                            Logger.Info($"Almacenando Dato vinculado en: {dataTable}");
                            objRef = acera; // Para debugging, inspeccionar este objeto en tiempo de ejecución
                            sqlConnection.InsertData(dataTable, ref objRef);

                            Thread.Sleep(3);
                        }

                        // Simulador de carga por ahora
                        //for (int i = 1; i <= 100; i++)
                        //{
                        //    token.ThrowIfCancellationRequested();

                        //    Logger.Info($"Procesando lote {i}...");

                        //    if (i % 10 == 0) Logger.Warn($"Advertencia en lote {i}: Latencia detectada.");
                        //    if (i % 25 == 0) Logger.Error(new Exception("Error simulado"), $"Fallo no crítico en lote {i}.");

                        //    Thread.Sleep(100); // Simulación de carga
                        //}

                    }

                }

                
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error crítico: No se pudo abrir el archivo SQLite origen.");
                throw; // Detener el proceso si no se puede conectar
            }
        }

        private void GrantDataTable(SqlConnection conn, string tableName)
        {
            var (sqlDropDataTable, sqlCreateDataTable) = CreateDataTableSqlStr(tableName);

            conn.Execute(sqlDropDataTable);
            conn.Execute(sqlCreateDataTable);
        }

        private void GrantLtnConfig(SqlConnection conn, string tableName, int layer)
        {
            var sql = $@"
            IF OBJECT_ID('LtnConfig', 'U') IS NULL
            BEGIN
                CREATE TABLE LtnConfig (
                    Layer int NOT NULL,
                    TableName nvarchar(50),
                    LtnJoin nvarchar(250),
                    SQLQUERY nvarchar(250),
                    CONSTRAINT PK_LtnConfig PRIMARY KEY (Layer)
                )
            END
            ";

            conn.Execute(sql);

            var config = conn.GetData<dbLtnConfig>("LtnConfig", queryCondition: $"Layer = {layer}").FirstOrDefault();

            if (config != null)
            {
                config.TableName = tableName;
                conn.UpdateData("LtnConfig", config, keyName: "Layer");
            }
            else
            {
                config = new dbLtnConfig
                {
                    Layer = layer,
                    TableName = tableName
                };

                object objRef = config;
                conn.InsertData("LtnConfig", ref objRef);
            }
        }

        private (string, string) CreateDataTableSqlStr(string tableName)
        {
            var sqlDropDataTable = $@"
            IF OBJECT_ID('{tableName}', 'U') IS NOT NULL 
                DROP TABLE {tableName};
            ";

            var sqlCreateDataTable = $@"
            CREATE TABLE {tableName} (
                [LtnId] INT NOT NULL, -- PK Manual (no incremental)

                -- Coordenadas Aplanadas
                [Lat] FLOAT NOT NULL,
                [Lng] FLOAT NOT NULL,
                [LocalEast] FLOAT NOT NULL,
                [LocalNorth] FLOAT NOT NULL,

                -- Metadatos
                [Fecha] NVARCHAR(50) NULL,
                [Imagenes] NVARCHAR(MAX) NULL,
                [IdObject] BIGINT NOT NULL,
                [Localizacion] NVARCHAR(255) NULL,
                [Type] NVARCHAR(50) NULL,

                -- Distrito (Pares Valor/Texto)
                [Distrito_Value] NVARCHAR(20) NULL,
                [Distrito_Text] NVARCHAR(100) NULL,

                [CodigoCamino] NVARCHAR(100) NULL,
                [NumBoleta] NVARCHAR(50) NULL,

                -- Condición Meteorológica
                [CondicionMeteorol_Value] NVARCHAR(10) NULL,
                [CondicionMeteorol_Text] NVARCHAR(50) NULL,

                [Observaciones] NVARCHAR(MAX) NULL,
                [NoTieneAcera] BIT NOT NULL, -- Equivalente a bool

                -- Dimensiones
                [Longitud] FLOAT NOT NULL,
                [Ancho] FLOAT NOT NULL,
                [Area] FLOAT NOT NULL,

                -- --- EVALUACIÓN ESTRUCTURAL ---
                [EstructGrietas_Value] NVARCHAR(10) NULL,
                [EstructGrietas_Text] NVARCHAR(255) NULL,
                [EstructHuecos_Value] NVARCHAR(10) NULL,
                [EstructHuecos_Text] NVARCHAR(255) NULL,
                [EstructDesnud_Value] NVARCHAR(10) NULL,
                [EstructDesnud_Text] NVARCHAR(255) NULL,
                [EstructEscalon_Value] NVARCHAR(10) NULL,
                [EstructEscalon_Text] NVARCHAR(255) NULL,
                [EstructDrenaje_Value] NVARCHAR(10) NULL,
                [EstructDrenaje_Text] NVARCHAR(255) NULL,
                [TotalEstruct] INT NOT NULL,

                -- --- EVALUACIÓN FUNCIONAL ---
                [FuncPendTransv_Value] NVARCHAR(10) NULL,
                [FuncPendTransv_Text] NVARCHAR(255) NULL,
                [FuncPendLong_Value] NVARCHAR(10) NULL,
                [FuncPendLong_Text] NVARCHAR(255) NULL,
                [FuncAnchoLibre_Value] NVARCHAR(10) NULL,
                [FuncAnchoLibre_Text] NVARCHAR(255) NULL,
                [FuncObstrucion_Value] NVARCHAR(10) NULL,
                [FuncObstrucion_Text] NVARCHAR(255) NULL,
                [FuncAccesibilidad_Value] NVARCHAR(10) NULL,
                [FuncAccesibilidad_Text] NVARCHAR(255) NULL,
                [FuncRejillas_Value] NVARCHAR(10) NULL,
                [FuncRejillas_Text] NVARCHAR(255) NULL,
                [TotalFunc] FLOAT NOT NULL,

                -- --- FACTOR DE ACTIVIDAD ---
                [ActividadProxEscuelas_Value] NVARCHAR(10) NULL,
                [ActividadProxEscuelas_Text] NVARCHAR(255) NULL,
                [ActividadProxServGob_Value] NVARCHAR(10) NULL,
                [ActividadProxServGob_Text] NVARCHAR(255) NULL,
                [ActividadProxTerminalBus_Value] NVARCHAR(10) NULL,
                [ActividadProxTerminalBus_Text] NVARCHAR(255) NULL,
                [ActividadProxCentroRecreacion_Value] NVARCHAR(10) NULL,
                [ActividadProxCentroRecreacion_Text] NVARCHAR(255) NULL,
                [ActividadProxHospital_Value] NVARCHAR(10) NULL,
                [ActividadProxHospital_Text] NVARCHAR(255) NULL,
                [ActividadProxGenTransito_Value] NVARCHAR(10) NULL,
                [ActividadProxGenTransito_Text] NVARCHAR(255) NULL,
                [ActividadProxAltaPoblacion_Value] NVARCHAR(10) NULL,
                [ActividadProxAltaPoblacion_Text] NVARCHAR(255) NULL,
                [ClasificacionVial_Value] NVARCHAR(10) NULL,
                [ClasificacionVial_Text] NVARCHAR(255) NULL,
                [TotalActividad] FLOAT NOT NULL,

                [IndiceCondicionAceras] FLOAT NOT NULL,

                CONSTRAINT [PK_{tableName}] PRIMARY KEY CLUSTERED ([LtnId] ASC)
            );
            ";

            return (sqlDropDataTable, sqlCreateDataTable);
        }

        private void btnParar_Click(object sender, EventArgs e)
        {
            _cts?.Cancel();
            btnParar.Enabled = false;
            Logger.Warn("Solicitando detención del proceso...");
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            RichTextBox targetBox = null;

            if (btn == btnExportInfo) targetBox = txtLogInfo;
            else if (btn == btnExportWarn) targetBox = txtLogWarn;
            else if (btn == btnExportErr) targetBox = txtLogErr;

            if (targetBox == null || targetBox.TextLength == 0) return;

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Archivos de texto (*.txt)|*.txt";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    System.IO.File.WriteAllText(sfd.FileName, targetBox.Text);
                    MessageBox.Show("Log exportado con éxito.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        /// <summary>
        /// Intenta abrir una conexión SQLite probando primero sin clave (por retrocompatibilidad)
        /// y luego con la clave configurada si la primera falla.
        /// </summary>
        private SqliteConnection GetSqliteConnection()
        {
            // Intento 1: Sin Password
            try
            {
                var conn = new SqliteConnection($"Data Source={_sqlitePath}");
                conn.Open();
                conn.Execute("SELECT 1;"); // Validar acceso real antes de dar el OK
                return conn;
            }
            catch (SqliteException ex) when (ex.SqliteErrorCode == 26 || ex.Message.Contains("encrypted") || ex.Message.Contains("not a database"))
            {
                // Falló por encriptación, procedemos al segundo intento
                Logger.Warn("Base de datos encriptada detectada. Reintentando con clave...");
            }
            catch (Exception ex)
            {
                // Error de otro tipo (ej: archivo no existe), no reintentamos
                throw new Exception($"Fallo de acceso a SQLite: {ex.Message}", ex);
            }

            // Intento 2: Con Password
            if (!string.IsNullOrEmpty(SqlitePassword))
            {
                var conn = new SqliteConnection($"Data Source={_sqlitePath};Password={SqlitePassword}");
                conn.Open();
                conn.Execute("SELECT 1;"); // Si falla aquí, la clave es incorrecta
                return conn;
            }

            throw new Exception("La base de datos está protegida y no se ha configurado ninguna clave.");
        }

        /// <summary>
        /// Custom NLog Target para redirigir logs a un RichTextBox de forma segura entre hilos.
        /// </summary>
        public class RichTextBoxTarget : TargetWithLayout
        {
            private readonly RichTextBox _textBox;

            public RichTextBoxTarget(RichTextBox textBox)
            {
                _textBox = textBox;
            }

            protected override void Write(LogEventInfo logEvent)
            {
                string message = RenderLogEvent(Layout, logEvent) + Environment.NewLine;

                if (_textBox.InvokeRequired)
                {
                    _textBox.BeginInvoke(new Action(() => AppendText(message)));
                }
                else
                {
                    AppendText(message);
                }
            }

            private void AppendText(string message)
            {
                _textBox.AppendText(message);
                _textBox.SelectionStart = _textBox.Text.Length;
                _textBox.ScrollToCaret();
            }
        }
    }
}
