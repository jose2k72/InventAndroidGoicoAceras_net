using System.Collections.Generic;
using Aceras.Models.Dto;
using Aceras.Models.MsSqlTables;

namespace Aceras.Models.Mappers
{
    public static class AceraMapper
    {
        #region Diccionarios de Traducción (Extraídos de FormAcera.js)

        private static readonly Dictionary<string, string> DistritoMap = new Dictionary<string, string> {
            { "1", "1. GUADALUPE" }, { "2", "2. SAN FRANCISCO" }, { "3", "3. CALLE BLANCOS" },
            { "4", "4. MATA DE PLATANO" }, { "5", "5. IPIS" }, { "6", "6. RANCHO REDONDO" }, { "7", "7. PURRAL" }
        };

        private static readonly Dictionary<string, string> MeteorolMap = new Dictionary<string, string> {
            { "1", "Despejado" }, { "2", "Nublado" }, { "3", "Lluvioso" }
        };

        private static readonly Dictionary<string, string> GrietasMap = new Dictionary<string, string> {
            { "0", "Grieta no afecta la circulación" }, { "3", "Grieta 10mm - 25mm" }, { "5", "Grieta > 25mm" }
        };

        private static readonly Dictionary<string, string> HuecosMap = new Dictionary<string, string> {
            { "0", "Diámetro < 10cm y profundidad < 10mm" },
            { "2", "Diámetro 10cm - 30cm o profundidad 10mm - 30mm" },
            { "3", "Diámetro > 30cm o profundidad > 30mm" },
            { "5", "Diámetro > 30cm y profundidad > 30mm" }
        };

        private static readonly Dictionary<string, string> DesnudMap = new Dictionary<string, string> {
            { "0", "No afecta la circulación" }, { "4", "Moderado, deterioro superficie" }, { "5", "Severo, agregado suelto" }
        };

        private static readonly Dictionary<string, string> EscalonMap = new Dictionary<string, string> {
            { "0", "Escalonamiento < 2cm" }, { "4", "Escalonamiento 2cm - 5cm" }, { "7", "Escalonamiento > 5cm" }
        };

        private static readonly Dictionary<string, string> DrenajeMap = new Dictionary<string, string> {
            { "0", "Charco o sedimento < 10cm" }, { "2", "Charco o sedimento 10cm - 30cm" }, { "3", "Charco o sedimento > 30cm" }
        };

        private static readonly Dictionary<string, string> PendienteMap = new Dictionary<string, string> {
            { "0", "Pendiente < 2%" }, { "2", "Pendiente 2% - 3%" }, { "4", "Pendiente 3% - 5%" }, { "5", "Pendiente > 5%" }
        };

        private static readonly Dictionary<string, string> PendienteLongMap = new Dictionary<string, string> {
            { "0", "Pendiente < 5%" }, { "2", "Pendiente 5% - 8%" }, { "5", "Pendiente > 8%" }
        };

        private static readonly Dictionary<string, string> AnchoLibreMap = new Dictionary<string, string> {
            { "0", "Ancho entre 1.5m - 1.8m" }, { "4", "Ancho entre 1.2m - 1.5m" }, { "5", "Ancho < 1.2m" }
        };

        private static readonly Dictionary<string, string> ObstruccionMap = new Dictionary<string, string> {
            { "0", "Ancho reducido a 1.8m" }, { "2", "Ancho reducido a 1.5m" }, { "3", "Ancho reducido < 1.5m" }
        };

        private static readonly Dictionary<string, string> AccesibilidadMap = new Dictionary<string, string> {
            { "0", "Existen, no cumplen especificaciones" }, { "3", "Faltan algunas en el tramo" }, { "5", "No existen en el tramo" }
        };

        private static readonly Dictionary<string, string> RejillasMap = new Dictionary<string, string> {
            { "0", "Buena condición, no afectan circulación" }, { "1", "Regular condición, abertura 5cm - 8cm" }, { "2", "Faltan algunas, abertura > 8cm" }
        };

        private static readonly Dictionary<string, string> ProxEscuelasMap = new Dictionary<string, string> {
            { "0", "> 2000m" }, { "3", "1000m - 2000m" }, { "6", "500m - 1000m" }, { "10", "< 500m" }
        };

        private static readonly Dictionary<string, string> Prox500mMap = new Dictionary<string, string> {
            { "0", "> 500m" }, { "5", "< 500m" }, { "10", "< 500m" }
        };

        private static readonly Dictionary<string, string> ProxBusMap = new Dictionary<string, string> {
            { "0", "> 500m" }, { "5", "300m - 500m" }, { "10", "< 300m" }
        };

        private static readonly Dictionary<string, string> Prox1000mMap = new Dictionary<string, string> {
            { "0", "> 1000m" }, { "3", "500m - 1000m" }, { "5", "< 500m" }
        };

        private static readonly Dictionary<string, string> VialMap = new Dictionary<string, string> {
            { "5", "Terciario / Vol. Peat. Bajo" }, { "7", "Secundario / Vol. Peat. Medio" }, { "10", "Primario / Vol. Peat. Alto" }
        };

        #endregion

        public static AceraTable MapToTable(AceraDto dto)
        {
            if (dto == null) return null;

            var table = new AceraTable
            {
                // Aplanamiento de Coordenadas
                Lat = dto.LatLng?.Lat ?? 0,
                Lng = dto.LatLng?.Lng ?? 0,
                LocalEast = dto.LocalProj?.East ?? 0,
                LocalNorth = dto.LocalProj?.North ?? 0,

                // Metadatos y Generales
                Fecha = dto.Fecha,
                Imagenes = dto.Imagenes,
                IdObject = dto.IdObject,
                Localizacion = dto.Localizacion,
                Type = dto.Type,
                CodigoCamino = dto.CodigoCamino,
                NumBoleta = dto.NumBoleta,
                Observaciones = dto.Observaciones,
                NoTieneAcera = dto.NoTieneAcera,
                Longitud = dto.Longitud,
                Ancho = dto.Ancho,
                Area = dto.Area,

                // Mapeo de Distrito (Siempre se mapea)
                Distrito_Value = dto.Distrito,
                Distrito_Text = GetText(DistritoMap, dto.Distrito),

                CondicionMeteorol_Value = dto.CondicionMeteorol,
                CondicionMeteorol_Text = GetText(MeteorolMap, dto.CondicionMeteorol),
            };

            // REGLA DE NEGOCIO: Si NO tiene acera, limpiar campos técnicos
            if (dto.NoTieneAcera)
            {
                LimpiarCamposTecnicos(table);
            }
            else
            {
                MapearCamposTecnicos(table, dto);
            }

            return table;
        }

        private static void LimpiarCamposTecnicos(AceraTable table)
        {
            table.EstructGrietas_Value = string.Empty; table.EstructGrietas_Text = string.Empty;
            table.EstructHuecos_Value = string.Empty; table.EstructHuecos_Text = string.Empty;
            table.EstructDesnud_Value = string.Empty; table.EstructDesnud_Text = string.Empty;
            table.EstructEscalon_Value = string.Empty; table.EstructEscalon_Text = string.Empty;
            table.EstructDrenaje_Value = string.Empty; table.EstructDrenaje_Text = string.Empty;
            table.TotalEstruct = 0;

            table.FuncPendTransv_Value = string.Empty; table.FuncPendTransv_Text = string.Empty;
            table.FuncPendLong_Value = string.Empty; table.FuncPendLong_Text = string.Empty;
            table.FuncAnchoLibre_Value = string.Empty; table.FuncAnchoLibre_Text = string.Empty;
            table.FuncObstrucion_Value = string.Empty; table.FuncObstrucion_Text = string.Empty;
            table.FuncAccesibilidad_Value = string.Empty; table.FuncAccesibilidad_Text = string.Empty;
            table.FuncRejillas_Value = string.Empty; table.FuncRejillas_Text = string.Empty;
            table.TotalFunc = 0;

            table.ActividadProxEscuelas_Value = string.Empty; table.ActividadProxEscuelas_Text = string.Empty;
            table.ActividadProxServGob_Value = string.Empty; table.ActividadProxServGob_Text = string.Empty;
            table.ActividadProxTerminalBus_Value = string.Empty; table.ActividadProxTerminalBus_Text = string.Empty;
            table.ActividadProxCentroRecreacion_Value = string.Empty; table.ActividadProxCentroRecreacion_Text = string.Empty;
            table.ActividadProxHospital_Value = string.Empty; table.ActividadProxHospital_Text = string.Empty;
            table.ActividadProxGenTransito_Value = string.Empty; table.ActividadProxGenTransito_Text = string.Empty;
            table.ActividadProxAltaPoblacion_Value = string.Empty; table.ActividadProxAltaPoblacion_Text = string.Empty;
            table.ClasificacionVial_Value = string.Empty; table.ClasificacionVial_Text = string.Empty;
            table.TotalActividad = 0;

            table.IndiceCondicionAceras = 0;
        }

        private static void MapearCamposTecnicos(AceraTable table, AceraDto dto)
        {
            // Estructural
            table.EstructGrietas_Value = dto.EstructGrietas;
            table.EstructGrietas_Text = GetText(GrietasMap, dto.EstructGrietas);
            table.EstructHuecos_Value = dto.EstructHuecos;
            table.EstructHuecos_Text = GetText(HuecosMap, dto.EstructHuecos);
            table.EstructDesnud_Value = dto.EstructDesnud;
            table.EstructDesnud_Text = GetText(DesnudMap, dto.EstructDesnud);
            table.EstructEscalon_Value = dto.EstructEscalon;
            table.EstructEscalon_Text = GetText(EscalonMap, dto.EstructEscalon);
            table.EstructDrenaje_Value = dto.EstructDrenaje;
            table.EstructDrenaje_Text = GetText(DrenajeMap, dto.EstructDrenaje);
            table.TotalEstruct = dto.TotalEstruct;

            // Funcional
            table.FuncPendTransv_Value = dto.FuncPendTransv;
            table.FuncPendTransv_Text = GetText(PendienteMap, dto.FuncPendTransv);
            table.FuncPendLong_Value = dto.FuncPendLong;
            table.FuncPendLong_Text = GetText(PendienteLongMap, dto.FuncPendLong);
            table.FuncAnchoLibre_Value = dto.FuncAnchoLibre;
            table.FuncAnchoLibre_Text = GetText(AnchoLibreMap, dto.FuncAnchoLibre);
            table.FuncObstrucion_Value = dto.FuncObstrucion;
            table.FuncObstrucion_Text = GetText(ObstruccionMap, dto.FuncObstrucion);
            table.FuncAccesibilidad_Value = dto.FuncAccesibilidad;
            table.FuncAccesibilidad_Text = GetText(AccesibilidadMap, dto.FuncAccesibilidad);
            table.FuncRejillas_Value = dto.FuncRejillas;
            table.FuncRejillas_Text = GetText(RejillasMap, dto.FuncRejillas);
            table.TotalFunc = dto.TotalFunc;

            // Actividad
            table.ActividadProxEscuelas_Value = dto.ActividadProxEscuelas;
            table.ActividadProxEscuelas_Text = GetText(ProxEscuelasMap, dto.ActividadProxEscuelas);
            table.ActividadProxServGob_Value = dto.ActividadProxServGob;
            table.ActividadProxServGob_Text = GetText(Prox500mMap, dto.ActividadProxServGob);
            table.ActividadProxTerminalBus_Value = dto.ActividadProxTerminalBus;
            table.ActividadProxTerminalBus_Text = GetText(ProxBusMap, dto.ActividadProxTerminalBus);
            table.ActividadProxCentroRecreacion_Value = dto.ActividadProxCentroRecreacion;
            table.ActividadProxCentroRecreacion_Text = GetText(Prox1000mMap, dto.ActividadProxCentroRecreacion);
            table.ActividadProxHospital_Value = dto.ActividadProxHospital;
            table.ActividadProxHospital_Text = GetText(Prox1000mMap, dto.ActividadProxHospital);
            table.ActividadProxGenTransito_Value = dto.ActividadProxGenTransito;
            table.ActividadProxGenTransito_Text = GetText(Prox500mMap, dto.ActividadProxGenTransito);
            table.ActividadProxAltaPoblacion_Value = dto.ActividadProxAltaPoblacion;
            table.ActividadProxAltaPoblacion_Text = GetText(Prox500mMap, dto.ActividadProxAltaPoblacion);
            table.ClasificacionVial_Value = dto.ClasificacionVial;
            table.ClasificacionVial_Text = GetText(VialMap, dto.ClasificacionVial);
            table.TotalActividad = dto.TotalActividad;

            table.IndiceCondicionAceras = dto.IndiceCondicionAceras;
        }

        private static string GetText(Dictionary<string, string> map, string key)
        {
            if (string.IsNullOrEmpty(key)) return "N/A";
            return map.TryGetValue(key, out var text) ? text : $"Desconocido ({key})";
        }
    }
}
