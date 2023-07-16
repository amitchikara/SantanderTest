using BestStories.Core.Dtos;
using BestStories.Core.HttpServices;
using BestStories.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BestStories.Controllers
{
    [ApiController]
    [Route("beststories")]
    public class BestStoryController : ControllerBase
    {
        private readonly ILogger<BestStoryController> _logger;
        private readonly ITopStoriesService _topStoriesService;
        private readonly IHackerNewsHttpService _hackerNewsHttpService;
        private ICacheProvider _cacheProvider;


        public BestStoryController(ILogger<BestStoryController> logger, ITopStoriesService topStoriesService, IHackerNewsHttpService hackerNewsHttpService, ICacheProvider cacheProvider)
        {
            _logger = logger;
            _topStoriesService = topStoriesService;
            _hackerNewsHttpService = hackerNewsHttpService;
            _cacheProvider = cacheProvider;
        }


        [HttpGet("{numberOfStories}")]
        public async Task<ActionResult<List<StoryDto>>> GetTopStories(int numberOfStories)
        {
            var result = await _topStoriesService.Get(numberOfStories);
            if (result == null) { return NotFound(); }

            return Ok(result);
        }
    }
}
