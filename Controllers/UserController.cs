using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HonestProject.Repositories;
using Microsoft.AspNetCore.Http;

namespace HonestProject.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {

        IUserRepository userRepository;
        public UserController(IUserRepository repository)
        {
            userRepository = repository;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            ViewModels.User user = userRepository.GetUser(id);
            if(!userRepository.ValidSubmission)
            {
                return BadRequest(id);
            }

            if(!userRepository.ErrorDetected)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return new ObjectResult(user);
        }

        [HttpPost]
        public IActionResult Post([FromBody]ViewModels.User user)
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