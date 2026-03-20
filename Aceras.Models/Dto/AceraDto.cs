using System;

namespace Aceras.Models.Dto
{
    /// <summary>
    /// Representa el objeto JSON guardado en la columna DATOS para registros de tipo 'Acera'.
    /// </summary>
    public class AceraDto
    {
        // Identificación y Tipo
        public string Type { get; set; }
        public string Distrito { get; set; }
        public string CodigoCamino { get; set; }
        public string NumBoleta { get; set; }
        public string CondicionMeteorol { get; set; }
        public string Observaciones { get; set; }

        // Flag de presencia
        public bool NoTieneAcera { get; set; }

        // Dimensiones
        public double Longitud { get; set; }
        public double Ancho { get; set; }
        public double Area { get; set; }

        // Evaluación Estructural (Valores seleccionados como string)
        public string EstructGrietas { get; set; }
        public string EstructHuecos { get; set; }
        public string EstructDesnud { get; set; }
        public string EstructEscalon { get; set; }
        public string EstructDrenaje { get; set; }
        public int TotalEstruct { get; set; }

        // Evaluación Funcional
        public string FuncPendTransv { get; set; }
        public string FuncPendLong { get; set; }
        public string FuncAnchoLibre { get; set; }
        public string FuncObstrucion { get; set; }
        public string FuncAccesibilidad { get; set; }
        public string FuncRejillas { get; set; }
        public double TotalFunc { get; set; }

        // Factor de Actividad
        public string ActividadProxEscuelas { get; set; }
        public string ActividadProxServGob { get; set; }
        public string ActividadProxTerminalBus { get; set; }
        public string ActividadProxCentroRecreacion { get; set; }
        public string ActividadProxHospital { get; set; }
        public string ActividadProxGenTransito { get; set; }
        public string ActividadProxAltaPoblacion { get; set; }
        public string ClasificacionVial { get; set; }
        public double TotalActividad { get; set; }

        // Resultado Final
        public double IndiceCondicionAceras { get; set; }

        // Coordenadas (PascalCase inyectado en app.js)
        public LatLng LatLng { get; set; }
        public LocalProj LocalProj { get; set; }

        // Metadatos finales
        public string Fecha { get; set; }
        public string Encuestador { get; set; }
        public string Imagenes { get; set; }
        public long IdObject { get; set; }
        public string Localizacion { get; set; }
    }

    public class LatLng
    {
        public double Lat { get; set; }
        public double Lng { get; set; }
    }

    public class LocalProj
    {
        public double East { get; set; }
        public double North { get; set; }
    }
}