using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReseñaTuMundo_Api.Model
{
    public class User
    {
        public int Id_Usuario { get; set; }
        public string Nombre_Usuario { get; set; }
        public string Contrasena { get; set; }
        public string Correo { get; set; }
    }
}
