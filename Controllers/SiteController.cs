using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HonestProject.Controllers
{
    [Route("api/[controller]")]
    public class SiteController : Controller
    {

        [HttpGet("{id}")]
        public ViewModels.Site Get(int id)
        {
            return null;
        }

        [HttpPost]
        public ViewModels.Site Post([FromBody] ViewModels.Site newSite)
        {
            return null;
        }
    }
}