using System;
using SQLite;

namespace ReadNote.Tablas
{
    public class T_Alarmas
    {
        [PrimaryKey, AutoIncrement]
        public int id_alarma { get; set; }

        [Indexed] // Índice para mejorar rendimiento en búsquedas
        public int id_material { get; set; } // Cambio a int para ser clave foránea

        public DateTime fechHoraAla { get; set; }

        [MaxLength(255)]
        public string mensaje { get; set; }

        public bool tipoAlarma { get; set; }

        // Definir la relación con T_Material
        [Ignore]
        public T_Material Material { get; set; }
    }
}