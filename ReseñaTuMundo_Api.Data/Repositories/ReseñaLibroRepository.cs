using Dapper;
using MySql.Data.MySqlClient;
using ReseñaTuMundo_Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReseñaTuMundo_Api.Data.Repositories
{
    public class ReseñaLibroRepository : IReseñaLibroRepository
    {
        private readonly MySqlConfiguration _connectionString;
        public ReseñaLibroRepository(MySqlConfiguration connectionString)
        {

            _connectionString = connectionString;

        }
        protected MySqlConnection dbConnection()
        {
            return new MySqlConnection(_connectionString.ConnectionString);
        }
        public async Task<bool> DeleteReseña(ReseñaLibro reseñaLibro)
        {
            var db = dbConnection();
            var sql = @"DELETE FROM reseñaslibros WHERE id_resena = @Id";
            var result = await db.ExecuteAsync(sql, new { Id = reseñaLibro.Id_resena });
            return result > 0;
        }

        public async Task<IEnumerable<ReseñaLibro>> GetAllReseña()
        {
            var db = dbConnection();
            var sql = @" SELECT id_resena, id_usuario, titulo_libro, resena, calificacion
                         FROM reseñaslibros
                        ";
            return await db.QueryAsync<ReseñaLibro>(sql, new { });
        }

        public async Task<ReseñaLibro> GetById(int id)
        {
            var db = dbConnection();
            var sql = @" SELECT id_resena, id_usuario, titulo_libro, resena, calificacion
                         FROM reseñaslibros
                         WHERE id_resena = @Id
                        ";
            return await db.QueryFirstOrDefaultAsync<ReseñaLibro>(sql, new { Id = id });
        }

        public async Task<bool> InsertReseña(ReseñaLibro reseñalibro)
        {
            var db = dbConnection();
            var sql = @" INSERT INTO reseñaslibros (id_usuario, titulo_libro, resena, calificacion)
                         VALUES (@Id_usuario, @Titulo_libro, @Resena, @Calificacion)
                        ";
            var result = await db.ExecuteAsync(sql, new
            { reseñalibro.Id_Usuario,reseñalibro.Titulo_Libro, reseñalibro.Resena, reseñalibro.Calificacion });
            return result > 0;
        }

        public async Task<bool> UpdateReseña(ReseñaLibro reseñaLibro)
        {
            var db = dbConnection();
            var sql = @" UPDATE  reseñaslibros 
                         SET id_usuario=@Id_usuario,
                             titulo_libro=@Titulo_libro,
                             resena=@Resena,
                             calificacion=@Calificacion
                         WHERE id_resena = @Id_resena
                        ";
            var result = await db.ExecuteAsync(sql, new
            { reseñaLibro.Id_resena, reseñaLibro.Id_Usuario, reseñaLibro.Titulo_Libro, reseñaLibro.Resena, reseñaLibro.Calificacion });
            return result > 0;
        }

       

    }
}
