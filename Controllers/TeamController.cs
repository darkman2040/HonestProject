using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HonestProject.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HonestProject.Controllers
{
    [Route("api/[controller]")]
    public class TeamController : Controller
    {
        ITeamRepository repository;

        public TeamController(ITeamRepository repository)
        {
            this.repository = repository;
        }

        [Authorize(Roles = "Site Administrator,Manager")]
        [HttpGet("managedTeams")]
        public IActionResult GetManagedTeams()
        {
            var userName = this.HttpContext.User.Identity.Name;
            ViewModels.Team[] teams = this.repository.GetManagedTeams(userName);
            if(repository.ErrorDetected)
            {
                 return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return new ObjectResult(teams);
        }

        [Authorize(Roles = "Site Administrator,Manager")]
        [HttpPost]
        public IActionResult Post([FromBody]ViewModels.RegisterTeam newTeam)
        {
            var userName = this.HttpContext.User.Identity.Name;
            ViewModels.Team viewTeam = repository.Save(newTeam, userName);
            if (!repository.ValidSubmission)
            {
                return BadRequest(newTeam);
            }

            if (repository.ErrorDetected)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return new ObjectResult(viewTeam);
        }
    }
}