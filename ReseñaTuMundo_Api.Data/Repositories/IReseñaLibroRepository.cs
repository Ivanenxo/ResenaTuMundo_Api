using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReseñaTuMundo_Api.Model;

namespace ReseñaTuMundo_Api.Data.Repositories
{
    public interface IReseñaLibroRepository
    {
        Task<IEnumerable<ReseñaLibro>> GetAllReseña();
        Task <ReseñaLibro> GetById(int id);
        Task<bool> InsertReseña(ReseñaLibro reseñalibro);
        Task<bool> UpdateReseña(ReseñaLibro reseñaLibro);
        Task<bool> DeleteReseña(ReseñaLibro reseñaLibro);

    }
}
