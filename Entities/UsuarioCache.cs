﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public static class UsuarioCache
    {
        public static int IdUsuario { get; set; }
        public static string Nombre { get; set; }
        public static string Apellido { get; set; }
        public static string LoginName { get; set; }
        public static string Password { get; set; }
        public static string Rol { get; set; }
        public static bool Estado { get; set; }
        public static byte[] Foto { get; set; }
    }
}
