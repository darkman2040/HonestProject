using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HonestProject.ViewModels;
using HonestProject.DataModels;

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
            response.token = "blah";
            response.username = request.username;
            return new ObjectResult(response);
        }

    }
}
