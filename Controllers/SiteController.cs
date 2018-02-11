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
    public class SiteController : Controller
    {
        private ISiteRepository siteRepository;

        public SiteController(ISiteRepository siteRepo)
        {
            this.siteRepository = siteRepo;
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            ViewModels.Site getSite = siteRepository.GetSite(id);

            if (!siteRepository.ValidSubmission)
            {
                return NotFound();
            }

            if (siteRepository.ErrorDetected)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return new ObjectResult(getSite);
        }

        [AllowAnonymous]
        [HttpGet("canRegisterSite")]
        public IActionResult CanRegisterSite()
        {
            bool canRegister = siteRepository.CanRegisterSite();
            if (siteRepository.ErrorDetected)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(canRegister);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Post([FromBody] ViewModels.RegisterSite site)
        {
            ViewModels.Site returnSite = this.siteRepository.Save(site);

            if (!siteRepository.ValidSubmission)
            {
                return BadRequest(site);
            }

            if (siteRepository.ErrorDetected)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return new ObjectResult(returnSite);
        }
    }
}