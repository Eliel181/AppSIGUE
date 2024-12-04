using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Imagen
    {
        public int IdImagen { get; set; }
        public string Titulo { get; set; }
        public byte[] Foto { get; set; }
    }
}
