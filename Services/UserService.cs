using ActiveDirectoryEmulatorApi.Domain.Models;
using ActiveDirectoryEmulatorApi.Domain.Persistence.Repositories;
using ActiveDirectoryEmulatorApi.Domain.Services;
using ActiveDirectoryEmulatorApi.Domain.Services.Communications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ActiveDirectoryEmulatorApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<User>> ListAsync()
        {
            return await _userRepository.ListAsync();
        }

        public async Task<User> Login(string email, string password)
        {
            var user = await _userRepository.FindByEmailAndPassword(email, password);
            if (user is null)
            {
                throw  new Exception($"There are no users with the credentials");
            }
            return user;
        }

        public async Task<UserResponse> SaveAsync(User _user)
        {
            var isUser = await _userRepository.FindByEmail(_user.Email);
            if (isUser is not null)
            {
                return new UserResponse("There already exists a user with the email: " + _user.Email);
            }
            try
            {
                await _userRepository.AddAsync(_user);
                await _unitOfWork.CompleteAsync();

                return new UserResponse(_user);
            }
            catch (Exception ex)
            {
                return new UserResponse($"An error ocurred while saving the user: {ex.Message}");
            }
        }

        public async Task<UserResponse> SaveImage(string email, IFormFile file)
        {
            var user = await _userRepository.FindByEmail(email);
            if (user is null)
            {
                return new UserResponse("No user with the email: " + email);
            }
            //user.Image = System.IO.File.ReadAllBytes(file);
            using (var memoryStream = new MemoryStream())
            {
                await file.OpenReadStream().CopyToAsync(memoryStream);

                var fileBytes = memoryStream.ToArray();
                user.Image = Convert.ToBase64String(fileBytes);
            }
            try
            {
                _userRepository.Update(user);
                await _unitOfWork.CompleteAsync();
                return new UserResponse(user);
            }
            catch (Exception ex)
            {
                return new UserResponse($"An error ocurred while saving the Image: {ex.Message}");

            }
        }

        public async Task<byte[]> GetImageByEmail(string email)
        {
            var user = await _userRepository.FindByEmail(email);
            if (user is null)
            {
                throw new Exception("No user with the email: " + email);
            }
            if(user.Image is null)
            {
                throw new Exception("No image for user: " + email);
            }

            return Convert.FromBase64String(user.Image);
            //return System.IO.File.ReadAllBytes(user.Image);
            //return File(b, "image/png");
        }

    }
}
