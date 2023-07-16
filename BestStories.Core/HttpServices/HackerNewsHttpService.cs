using BestStories.Core.Dtos;
using BestStories.Core.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BestStories.Core.HttpServices
{
    public class HackerNewsHttpService : GenericHttpService, IHackerNewsHttpService
    {
        public HackerNewsHttpService(
           ILogger<HackerNewsHttpService> logger,
           HttpClient httpClient) : base(logger, httpClient,
            "https://hacker-news.firebaseio.com")
        {
        }

        public async Task<HttpServiceResponse<IEnumerable<int>>> GetStoryIds()
        {
            return await GetAll<int>($"v0/beststories.json");
        }

        public async Task<HttpServiceResponse<StoryDto>> GetStory(int sotryId)
        {
            return await Get<StoryDto>($"v0/item/{sotryId}.json");
        }
    }
}
