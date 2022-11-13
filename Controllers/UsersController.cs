using ActiveDirectoryEmulatorApi.Domain.Models;
using ActiveDirectoryEmulatorApi.Domain.Services;
using ActiveDirectoryEmulatorApi.Domain.Services.Communications;
using ActiveDirectoryEmulatorApi.Extensions;
using ActiveDirectoryEmulatorApi.Resources;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActiveDirectoryEmulatorApi.Controllers
{
    [Route("api/users")]
    //[Produces("application/json")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        /*****************************************************************/
        /*                         LIST OF USERS                         */
        /*****************************************************************/
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserResource>), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IEnumerable<UserResource>> GetAllAsync()
        {
            var users = await _userService.ListAsync();
            var resources = _mapper.Map<IEnumerable<User>, IEnumerable<UserResource>>(users);
            return resources;
        }

        /*****************************************************************/
        /*                          SAVE USER                            */
        /*****************************************************************/
        [HttpPost]
        [ProducesResponseType(typeof(UserResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PostAsync([FromBody] SaveUserResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var user = _mapper.Map<SaveUserResource, User>(resource);
            var result = await _userService.SaveAsync(user);

            if (!result.Success)
                return BadRequest(result.Message);

            var userResource = _mapper.Map<User, UserResource>(result.Resource);
            return Ok(userResource);

        }

        /*****************************************************************/
        /*                            LOGIN                              */
        /*****************************************************************/
        [HttpGet("/login/{email}/{password}")]
        [ProducesResponseType(typeof(UserResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<UserResource> Login(string email, string password)
        {
            var user = await _userService.Login(email, password);
            var resource = _mapper.Map<User, UserResource>(user);
            return resource;
        }

        /*****************************************************************/
        /*                          SAVE IMAGE                            */
        /*****************************************************************/
        [HttpPut("image/{email}")]
        [ProducesResponseType(typeof(UserResponse), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> SaveImage(string email, IFormFile file)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var result = await _userService.SaveImage(email, file);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(new UserResponse($"Image saved"));
        }

        /*****************************************************************/
        /*                          GET IMAGE                            */
        /*****************************************************************/
        [HttpGet("image/{email}")]
        [ProducesResponseType(typeof(FileContentResult), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetImageByEmailAsync(string email)
        {
            var img = await _userService.GetImageByEmail(email);
            return File(img, "image/jpeg");
        }

    }
}
