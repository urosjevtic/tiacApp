using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TiacApp.Application.Service.Interface;

namespace TiacApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SocialMediaController : ControllerBase
    {
        private readonly ISocialMediaService _socialMediaService;

        public SocialMediaController(ISocialMediaService socialMediaService)
        {
            _socialMediaService = socialMediaService;
        }

        [HttpPost]
        public async Task<ActionResult<object>> AddSocialMedia([FromBody] object newSocialMedia)
        {
            var result = await _socialMediaService.AddSocialMedia(newSocialMedia);
            return Ok(result);
        }
    }
}
