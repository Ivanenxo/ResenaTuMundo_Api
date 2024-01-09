using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReseñaTuMundo_Api.Model
{
    public class ReseñaLibro
    {
        public int Id_resena { get; set; }
        public int Id_Usuario { get; set; }
        public string Titulo_Libro { get; set; }
        public string Resena {  get; set; }
        public string Calificacion { get; set; }
    }
}
