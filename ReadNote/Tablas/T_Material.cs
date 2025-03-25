using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using Xamarin.Forms;

namespace ReadNote.Tablas
{
    public class T_Material
    {
        [PrimaryKey, AutoIncrement]
        public int IdMaterial { get; set; }

        [MaxLength (1)]
        public bool TipoMaterial { get; set; }

        [MaxLength(50)]
        public string NombreMaterial { get; set; }

        [MaxLength(50)]
        public string Autor { get; set; }

        [MaxLength(255)]
        public string DescripcionMaterial { get; set; }

        [MaxLength(50)]
        public string CategoriaMaterial { get; set; }
        [MaxLength(10)]
        public int noPaginasMaterial { get; set; }

        [MaxLength(1)]

        public bool EstadoMaterial { get; set; }

        [MaxLength(50)]

        public string nivelLector { get; set; }

        [MaxLength(50)]

        public string TiempoLectura { get; set; }

        [MaxLength(255)]

        public DateTime fechaCreacion { get; set; }
    }
}
