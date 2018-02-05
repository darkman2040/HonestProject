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

        // GET api/values
        [HttpGet]
        public AuthenticateRequest Get()
        {
            AuthenticateRequest request = new AuthenticateRequest();
            request.username = "Test";
            request.password = "Test";
            return request;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/authenticate
        [HttpPost]
        public AuthenticateResponse Post([FromBody] AuthenticateRequest request)
        {
            AuthenticateResponse response = new AuthenticateResponse();
            //_context.User.First();
            response.token = "blah";
            response.username = request.username;
            return response;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
