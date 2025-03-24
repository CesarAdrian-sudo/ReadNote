using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace ReadNote.Tablas
{
    public class T_MaterialColeccion
    {
        [PrimaryKey, AutoIncrement]
        public int IdMaterialColeccion { get; set; }
        [MaxLength(255)]
        public int Id_Material { get; set; }
        [MaxLength(255)]
        public int Id_Coleccion { get; set; }
        [MaxLength(255)]
        public DateTime Fecha { get; set; }
        public DateTime Hora { get; set; }

    }
}
