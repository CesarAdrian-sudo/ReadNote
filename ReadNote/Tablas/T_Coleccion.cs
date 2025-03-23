using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace ReadNote.Tablas
{
    
    public class T_Coleccion
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(255)]
        public String Nombre { get; set; }
        [MaxLength(255)]
        public String Descripcion { get; set; }
        [MaxLength(255)]
        public String TipoMaterial { get; set; }
        public String Contador { get; set; }
    }
}
