using ActiveDirectoryEmulatorApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActiveDirectoryEmulatorApi.Domain.Persistence.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> ListAsync();
        Task<User> FindByEmailAndPassword(string email, string password);
        Task AddAsync(User _user);
        Task<User> FindByEmail(string email);
        void Update(User _user);
        // recibes correo y devuelves la img
        //(GET -> i: Correo, o: Img)
        //(POST -> i: Correo, Img, o: Img)

    }
}
