using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialApi.Data;
using SocialApi.Models;

namespace SocialApi.Controllers
{
    public class ErrorController : BaseController
    {
        private readonly DataContext _dataContext;
        public ErrorController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [Authorize]
        [HttpGet("unauth")]
        public ActionResult<string> GetUnauthorized()
        {
            return "Your Not Authorized!";
        }

        [HttpGet("not-found")]
        public ActionResult<AppUser> GetNotFound()
        {
            var resp = _dataContext.Users.Find(-1);

            if (resp == null) return NotFound();
            
            return resp;
        }

        [HttpGet("server-error")]
        public ActionResult<string> GetServerError()
        {
            var resp = _dataContext.Users.Find(-1);

            var respReturn = resp.ToString();

            return respReturn;
        }

        [HttpGet("bad-request")]
        public ActionResult<string> GetBadRequest() 
        {
            return BadRequest("Bad Request!!!");
        }
    }
}
