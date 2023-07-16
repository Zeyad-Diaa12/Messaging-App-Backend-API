using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialApi.Data;
using SocialApi.Models;

namespace SocialApi.Controllers
{
    [Authorize]
    public class UsersController : BaseController
    {
        private readonly DataContext _dataContext;

        public UsersController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> Get()
        {
            return Ok(await _dataContext.Users.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> Get(int id)
        {
            return Ok(await _dataContext.Users.FindAsync(id));
        }
    }
}
