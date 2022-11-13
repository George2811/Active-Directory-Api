using ActiveDirectoryEmulatorApi.Domain.Models;
using ActiveDirectoryEmulatorApi.Domain.Persistence.Contexts;
using ActiveDirectoryEmulatorApi.Domain.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActiveDirectoryEmulatorApi.Persistence.Repository
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(User _user)
        {
            await _context.Users.AddAsync(_user);
        }

        public async Task<User> FindByEmail(string email)
        {
            IEnumerable<User> users = await _context.Users
                            .Where(u => u.Email == email)
                            .ToListAsync();
            return users.Count() > 0 ? users.First() : null;
        }

        public async Task<User> FindByEmailAndPassword(string email, string password)
        {
            IEnumerable<User> users = await _context.Users
                .Where(u => u.Email == email)
                .Where(u => u.Password == password)
                .ToListAsync();
            return users.Count() > 0 ? users.First() : null;
        }

        public async Task<IEnumerable<User>> ListAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public void Update(User _user)
        {
            _context.Users.Update(_user);
        }
    }
}
