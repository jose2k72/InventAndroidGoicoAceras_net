/*
CREATE TABLE [dbo].[AceraTable] (
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

    CONSTRAINT [PK_AceraTable] PRIMARY KEY CLUSTERED ([LtnId] ASC)
);
*/

using System.ComponentModel.DataAnnotations;

namespace Aceras.Models.MsSqlTables
{
    /// <summary>
    /// Representa la tabla final en SQL Server enriquecida con textos descriptivos.
    /// </summary>
    public class AceraTable
    {
        [Key]
        public int LtnId { get; set; }

        // Coordenadas Aplanadas (PascalCase de app.js)
        public double Lat { get; set; }
        public double Lng { get; set; }
        public double LocalEast { get; set; }
        public double LocalNorth { get; set; }

        // Metadatos
        public string Fecha { get; set; }
        public string Imagenes { get; set; }
        public long IdObject { get; set; }
        public string Localizacion { get; set; }
        public string Type { get; set; }

        // Distrito (Pares Valor/Texto)
        public string Distrito_Value { get; set; }
        public string Distrito_Text { get; set; }

        public string CodigoCamino { get; set; }
        public string NumBoleta { get; set; }

        // Condicion Meteorológica
        public string CondicionMeteorol_Value { get; set; }
        public string CondicionMeteorol_Text { get; set; }

        public string Observaciones { get; set; }
        public bool NoTieneAcera { get; set; }

        // Dimensiones
        public double Longitud { get; set; }
        public double Ancho { get; set; }
        public double Area { get; set; }

        // --- EVALUACIÓN ESTRUCTURAL ---
        public string EstructGrietas_Value { get; set; }
        public string EstructGrietas_Text { get; set; }

        public string EstructHuecos_Value { get; set; }
        public string EstructHuecos_Text { get; set; }

        public string EstructDesnud_Value { get; set; }
        public string EstructDesnud_Text { get; set; }

        public string EstructEscalon_Value { get; set; }
        public string EstructEscalon_Text { get; set; }

        public string EstructDrenaje_Value { get; set; }
        public string EstructDrenaje_Text { get; set; }

        public int TotalEstruct { get; set; }

        // --- EVALUACIÓN FUNCIONAL ---
        public string FuncPendTransv_Value { get; set; }
        public string FuncPendTransv_Text { get; set; }

        public string FuncPendLong_Value { get; set; }
        public string FuncPendLong_Text { get; set; }

        public string FuncAnchoLibre_Value { get; set; }
        public string FuncAnchoLibre_Text { get; set; }

        public string FuncObstrucion_Value { get; set; }
        public string FuncObstrucion_Text { get; set; }

        public string FuncAccesibilidad_Value { get; set; }
        public string FuncAccesibilidad_Text { get; set; }

        public string FuncRejillas_Value { get; set; }
        public string FuncRejillas_Text { get; set; }

        public double TotalFunc { get; set; }

        // --- FACTOR DE ACTIVIDAD ---
        public string ActividadProxEscuelas_Value { get; set; }
        public string ActividadProxEscuelas_Text { get; set; }

        public string ActividadProxServGob_Value { get; set; }
        public string ActividadProxServGob_Text { get; set; }

        public string ActividadProxTerminalBus_Value { get; set; }
        public string ActividadProxTerminalBus_Text { get; set; }

        public string ActividadProxCentroRecreacion_Value { get; set; }
        public string ActividadProxCentroRecreacion_Text { get; set; }

        public string ActividadProxHospital_Value { get; set; }
        public string ActividadProxHospital_Text { get; set; }

        public string ActividadProxGenTransito_Value { get; set; }
        public string ActividadProxGenTransito_Text { get; set; }

        public string ActividadProxAltaPoblacion_Value { get; set; }
        public string ActividadProxAltaPoblacion_Text { get; set; }

        public string ClasificacionVial_Value { get; set; }
        public string ClasificacionVial_Text { get; set; }

        public double TotalActividad { get; set; }

        public double IndiceCondicionAceras { get; set; }
    }
}