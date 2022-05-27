using IPDP_Stefan.Context;
using IPDP_Stefan.Interfaces;
using IPDP_Stefan.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace IPDP_Stefan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly ContextDb _context;
        public UserController(IUserService userService, ContextDb context)
        {
            this.userService = userService;
            _context = context;
        }

        [HttpPost]
        [Route("post")]
        public async Task<IActionResult> AddUser(User user)
        {
            var existingUser = _context.User.Find(user.Id);
            if (existingUser != null) return BadRequest("This User already exists.");
            await userService.AddUser(user).ConfigureAwait(false);
            return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + user.Id,
                user);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await userService.GetUserById(id).ConfigureAwait(false);
            if (user != null)
            {
                await userService.DeleteUser(user).ConfigureAwait(false);
                return Ok();
            }
            return NotFound();
        }

        [HttpPut]
        [Route("edit")]
        public async Task<IActionResult> EditUser(User user)
        {
            var existingUser = await userService.GetUserById(user.Id).ConfigureAwait(false);
            if (existingUser != null)
            {
                user.Id = existingUser.Id;
                await userService.EditUser(user).ConfigureAwait(false);
                return Ok();
            }
            return NotFound();
        }

        [HttpGet]
        [Route("get/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await userService.GetUserById(id).ConfigureAwait(false);
            if (user != null)
            {
                return Ok(user);
            }
            return NotFound($"can not find User with id:{id}");
        }
        [HttpGet]
        [Route("get/byUserName/{userName}")]
        public async Task<IActionResult> GetUserByUserName(string userName)
        {
            try
            {
                var user = await userService.GetUserByUserName(userName).ConfigureAwait(false);
                if (user != null)
                {
                    return Ok(user.UserName);
                }
                return NotFound($"can not find User with name:{userName}");
            }
            catch (Exception e)
            {
                return NotFound($"can not find User with name:{userName}");
            }
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok(await userService.GetAllUsers().ConfigureAwait(false));
        }

    }
}
