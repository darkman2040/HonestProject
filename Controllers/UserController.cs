using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HonestProject.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;

namespace HonestProject.Controllers
{

    [Route("api/[controller]")]
    public class UserController : Controller
    {
        IUserRepository userRepository;
        IConfiguration configuration;
        public UserController(IUserRepository repository, IConfiguration configuration)
        {
            this.userRepository = repository;
            this.configuration = configuration;
        }



        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            ViewModels.User user = userRepository.GetUser(id);
            if (!userRepository.ValidSubmission)
            {
                return BadRequest(id);
            }

            if (userRepository.ErrorDetected)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return new ObjectResult(user);
        }

        [HttpGet("isSingleSiteConfig")]
        public IActionResult IsSingleSiteConfig()
        {
            return Ok(this.configuration["SingleSiteMode"].ToLower() == "true");
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Post([FromBody]ViewModels.RegisterUser user)
        {
            ViewModels.User returnedUser = userRepository.Save(user);
            if (!userRepository.ValidSubmission)
            {
                return BadRequest(user);
            }

            if (userRepository.ErrorDetected)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return new ObjectResult(returnedUser);
        }
    }
}