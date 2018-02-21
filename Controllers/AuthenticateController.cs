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
using HonestProject.Utilities;

namespace HonestProject.Controllers
{
    [Route("api/[controller]")]
    public class AuthenticateController : Controller
    {

        HonestProjectContext _context;
        IConfiguration _configuration;
        IPasswordHashUtility hashUtility;
        IJwtUtilities jwtUtilities;

        public AuthenticateController(HonestProjectContext context, IConfiguration configuration, IPasswordHashUtility hashUtility, IJwtUtilities jwtUtilities)
        {
            _context = context;
            _configuration = configuration;
            this.hashUtility = hashUtility;
            this.jwtUtilities = jwtUtilities;
        }

        // POST api/authenticate
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Post([FromBody] AuthenticateRequest request)
        {
            AuthenticateResponse response = new AuthenticateResponse();
            DataModels.User user = _context.User.Include(x => x.Role).Where(x => x.EmailAddress == request.username).FirstOrDefault();

            if (user == null)
            {
                return new ObjectResult(response);
            }

            if (!hashUtility.CheckMatch(user.PasswordHash, request.password))
            {
                return new ObjectResult(response);
            }
            response.token = jwtUtilities.GenereateJwtToken(user);
            response.userId = user.PublicIdentifier;
            if (request.getRefreshToken)
            {
                response.refreshToken = jwtUtilities.GenerateRefreshToken(user);
                user.RefreshTokenList = response.refreshToken;
                this._context.User.Update(user);
                this._context.SaveChanges();
            }
            return new ObjectResult(response);
        }

        [AllowAnonymous]
        [HttpPost("tokens/{refreshToken}/refresh")]
        public IActionResult RefreshAccessToken(string refreshToken)
        {
            DataModels.User user = this._context.User.Include(x => x.Role).Where(x => x.RefreshTokenList == refreshToken).FirstOrDefault();
            if(user == null)
            {
                return BadRequest();
            }
            else
            {
                return new ObjectResult(new RefreshResponse(jwtUtilities.GenereateJwtToken(user)));
            }
        }
    }
}
