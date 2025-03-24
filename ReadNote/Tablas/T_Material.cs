using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace ReadNote.Tablas
{
    public class T_Material
    {
        [PrimaryKey, AutoIncrement]
        public int IdMaterial { get; set; }

        [MaxLength(255)]
        public string NombreMaterial { get; set; }
        [MaxLength(255)]
        public string Descripcion { get; set; }
        [MaxLength(255)]
        public int no_paginas { get; set; }
    }
}
