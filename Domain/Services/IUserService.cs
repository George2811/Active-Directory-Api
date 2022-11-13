using ActiveDirectoryEmulatorApi.Domain.Models;
using ActiveDirectoryEmulatorApi.Domain.Services.Communications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActiveDirectoryEmulatorApi.Domain.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> ListAsync();
        Task<UserResponse> SaveAsync(User _user);
        Task<User> Login(string email, string password);

        // recibes correo y devuelves la img
        //(GET -> i: Correo, o: Img)
        //(POST -> i: Correo, Img, o: Img)

        Task<byte[]> GetImageByEmail(string email);
        Task<UserResponse> SaveImage(string email, IFormFile file);
    }
}
