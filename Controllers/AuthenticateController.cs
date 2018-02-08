using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HonestProject.ViewModels;
using HonestProject.DataModels;
using Microsoft.AspNetCore.Http;

namespace HonestProject.Controllers
{
    [Route("api/[controller]")]
    public class AuthenticateController : Controller
    {

        HonestProjectContext _context;

        public AuthenticateController(HonestProjectContext context)
        {
            _context = context;
        }

        // POST api/authenticate
        [HttpPost]
        public IActionResult Post([FromBody] AuthenticateRequest request)
        {
            AuthenticateResponse response = new AuthenticateResponse();
            DataModels.User user = _context.User.Where(x => x.EmailAddress == request.username && x.PasswordHash == request.password).FirstOrDefault();

            if(user == null)
            {
                return new ObjectResult(response);
            }
            response.token = "blah";
            response.userId = user.PublicIdentifier;
            return new ObjectResult(response);
        }

    }
}
