using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.API.Database;

namespace SocialMedia.API.Controllers
{
    //resharper
    [ApiController]
    [Route("[controller]")]
    public class CommentsController : ControllerBase
    {
        private readonly SocialMediaDbContext _socialMediaContext;

        public CommentsController(SocialMediaDbContext socialMediaContext)
        {
            _socialMediaContext = socialMediaContext;
        }
    }
}
