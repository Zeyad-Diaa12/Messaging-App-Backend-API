using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialApi.Data;
using SocialApi.DTOs;
using SocialApi.Interfaces;
using SocialApi.Models;
using System.Security.Claims;

namespace SocialApi.Controllers
{
    [Authorize]
    public class UsersController : BaseController
    {
        private readonly IUserRepository _userRpeo;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRpeo, IMapper mapper)
        {
            _userRpeo = userRpeo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> Get()
        { 
            return Ok(await _userRpeo.GetAllMembersAsync());
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetByUsername(string username)
        { 
            return Ok(await _userRpeo.GetMemberByUserNameAsync(username));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser(MemberUpdateDto memberData)
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user =await _userRpeo.GetUserByUserNameAsync(username);

            if(user == null)
            {
                return NotFound();
            }

            _mapper.Map(memberData, user);

            if (await _userRpeo.SaveAllChanges()) return NoContent();

            return BadRequest("Failed To Update User");


        }
    }
}
