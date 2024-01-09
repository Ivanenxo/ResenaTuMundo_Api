using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReseñaTuMundo_Api.Model;

namespace ReseñaTuMundo_Api.Data.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUser();
        Task<User> GetById(int id);
        Task<bool> InsertUser(User user);
        Task<bool> UpdateUser(User user);
        Task<bool> DeleteUser(User user);
        Task<User> AuthenticateUser(string nombreUsuario, string contraseña);
    }
}
