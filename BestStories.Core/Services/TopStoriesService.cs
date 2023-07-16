using BestStories.Core.Dtos;
using BestStories.Core.HttpServices;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BestStories.Core.Services
{
    public class TopStoriesService : ITopStoriesService
    {
        private readonly ILogger<TopStoriesService> _logger;
        private readonly IHackerNewsHttpService _hackerNewsHttpService;
        private ICacheProvider _cacheProvider;

        public TopStoriesService(ILogger<TopStoriesService> logger, IHackerNewsHttpService hackerNewsHttpService, ICacheProvider cacheProvider)
        {
            _logger = logger;
            _hackerNewsHttpService = hackerNewsHttpService;
            _cacheProvider = cacheProvider;
        }

        public async Task<IEnumerable<StoryDto>> Get(int numberOfStories)
        {
            _logger.LogInformation($"Getting top {numberOfStories} stories");


            var response = await _hackerNewsHttpService.GetStoryIds();
            int[] topStoryIds = response.Content.ToArray();

            List<StoryDto> topStories = new List<StoryDto>();

            if (topStoryIds.Length < numberOfStories)
                numberOfStories = topStoryIds.Length;

            for (int i = 0; i < numberOfStories; i++)
            {
                topStories.Add(_cacheProvider.GetStoryFromCache(topStoryIds[i]).Result);
            }

            return topStories.OrderBy(s => s.score).Take(numberOfStories);
        }
    }
}
