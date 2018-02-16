using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HonestProject.ViewModels;
using HonestProject.DataModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.EntityFrameworkCore;

namespace HonestProject.Controllers
{
    [Route("api/[controller]")]
    public class AuthenticateController : Controller
    {

        HonestProjectContext _context;
        IConfiguration _configuration;

        public AuthenticateController(HonestProjectContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // POST api/authenticate
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Post([FromBody] AuthenticateRequest request)
        {
            AuthenticateResponse response = new AuthenticateResponse();
            DataModels.User user = _context.User.Include(x => x.Role).Where(x => x.EmailAddress == request.username && x.PasswordHash == request.password).FirstOrDefault();

            if (user == null)
            {
                return new ObjectResult(response);
            }
            var claims = new[] { new Claim(ClaimTypes.Name, request.username), new Claim(ClaimTypes.Role, user.Role.Name) };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration["SecurityKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
            issuer: "yourdomain.com",
            audience: "yourdomain.com",
            claims: claims,
            expires: DateTime.Now.AddMinutes(90),
            signingCredentials: creds);
            response.token = new JwtSecurityTokenHandler().WriteToken(token);
            response.userId = user.PublicIdentifier;
            return new ObjectResult(response);
        }

    }
}
