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
    public class ProjectController : Controller
    {
        IProjectRepository repository;
        public ProjectController(IProjectRepository repository)
        {
            this.repository = repository;
        }
        [HttpGet("GetProjects")]
        [Authorize(Roles = "Site Administrator,Manager,Team Leader")]
        public IActionResult GetProjects()
        {
            ViewModels.Project[] projects = repository.GetProjectsForTeam(this.HttpContext.User.Identity.Name);
            if(repository.ErrorDetected)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return new ObjectResult(projects);
        }
    }
}