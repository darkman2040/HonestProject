using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HonestProject.Repositories;
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

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            return null;
        }

        [HttpPost]
        public IActionResult Post([FromBody] ViewModels.Site site)
        {
            ViewModels.Site returnSite = this.siteRepository.Save(site);

            if(!siteRepository.ValidSubmission)
            {
                return BadRequest(site);
            }

            return new ObjectResult(returnSite);
        }
    }
}