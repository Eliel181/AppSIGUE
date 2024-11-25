using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }
        public string Rol { get; set; }
        public bool Estado { get; set; }
        public byte[] Foto { get; set; }
    }
}
