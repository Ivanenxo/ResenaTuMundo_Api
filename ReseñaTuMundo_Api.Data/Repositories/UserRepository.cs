using Dapper;
using MySql.Data.MySqlClient;
using ReseñaTuMundo_Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReseñaTuMundo_Api.Data.Utilities;

namespace ReseñaTuMundo_Api.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MySqlConfiguration _connectionString;
        
        
        
        
        
        public UserRepository(MySqlConfiguration connectionString)
        {

            _connectionString = connectionString;

        }

        protected MySqlConnection dbConnection()
        {
            return new MySqlConnection(_connectionString.ConnectionString);
        }

        public async Task<bool> DeleteUser(User user)
        {
            var db = dbConnection();
            var sql = @"DELETE FROM usuarios WHERE id_usuario = @Id";
            var result = await db.ExecuteAsync(sql, new {Id = user.Id_Usuario});
            return result > 0;
        }

        public async Task<IEnumerable<User>> GetAllUser()
        {
            var db = dbConnection();
            var sql = @" SELECT id_usuario, nombre_usuario, contrasena, correo
                       FROM usuarios
                        ";
            return await db.QueryAsync<User>(sql, new {});
        }

        public async Task<User> GetById(int id)
        {
            var db = dbConnection();
            var sql = @" SELECT id_usuario, nombre_usuario, contrasena, correo
                         FROM usuarios
                         WHERE id_usuario = @Id
                        ";
            return await db.QueryFirstOrDefaultAsync<User>(sql, new { Id = id });
        }

        public async Task<bool> InsertUser(User user)
        {
            var db = dbConnection();
            var sql = @" INSERT INTO usuarios (nombre_usuario, contrasena, correo)
                         VALUES (@Nombre_usuario, @Contrasena, @Correo)
                        ";
            var result = await db.ExecuteAsync(sql, new
            { user.Nombre_Usuario,user.Contrasena,user.Correo});
            return result > 0;

        }

        public async Task<bool> UpdateUser(User user)
        {
            var db = dbConnection();
            var sql = @" UPDATE  usuarios 
                         SET nombre_usuario=@Nombre_usuario,
                             contrasena=@Contrasena,
                             correo=@Correo 
                         WHERE id_usuario = @Id_Usuario
                        ";
            var result = await db.ExecuteAsync(sql, new
            { user.Nombre_Usuario, user.Contrasena, user.Correo,user.Id_Usuario });
            return result > 0;
        }
        public async Task<User> AuthenticateUser(string nombreUsuario, string contraseña)
        {
            // Verificar si el usuario y la contraseña coinciden en la base de datos
            using (var db = dbConnection())
            {
                var sql = @"SELECT id_usuario, nombre_usuario, contrasena, correo
                    FROM usuarios
                    WHERE nombre_usuario = @Nombre_Usuario";

                var userFromDb = await db.QueryFirstOrDefaultAsync<User>(sql, new { Nombre_Usuario = nombreUsuario });

                if (userFromDb != null && (contraseña == userFromDb.Contrasena))
                {
                    return userFromDb;
                }
                

                return null;
            }
        }
    }
}
