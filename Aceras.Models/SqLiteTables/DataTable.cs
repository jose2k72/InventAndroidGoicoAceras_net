using System;

/*
CREATE TABLE "DATOS" (
	`ID`	INTEGER PRIMARY KEY AUTOINCREMENT,
	`IDOBJECT`	INTEGER,
	`DATOS`	TEXT,
	`FECHA`	DATETIME,
	`SINCRONIZADO`	BOOLEAN,
	`IMEI`	TEXT,
	`ANDROID_ID`	TEXT,
	`LATITUD`	FLOAT,
	`LONGITUD`	FLOAT,
	`LATITUDGPS`	FLOAT,
	`LONGITUDGPS`	FLOAT,
	`LAYER`	TEXT,
	`IDLAYER`	INTEGER,
	`IDPREDIO`	INTEGER
);
*/

namespace Aceras.Models.SqLiteTables
{
	/// <summary>
	/// Representa la tabla "DATOS" de la base de datos SQLite.
	/// </summary>
	public class DataTable
	{
		public long ID { get; set; }

		public long? IDOBJECT { get; set; }

		public string DATOS { get; set; }

		public string FECHA { get; set; }

		public bool? SINCRONIZADO { get; set; }

		public string IMEI { get; set; }

		public string ANDROID_ID { get; set; }

		public double? LATITUD { get; set; }

		public double? LONGITUD { get; set; }

		public double? LATITUDGPS { get; set; }

		public double? LONGITUDGPS { get; set; }

		public string LAYER { get; set; }

		public long? IDLAYER { get; set; }

		public long? IDPREDIO { get; set; }
	}
}