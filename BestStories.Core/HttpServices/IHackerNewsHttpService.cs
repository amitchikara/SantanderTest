using BestStories.Core.Dtos;
using BestStories.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BestStories.Core.HttpServices
{
    public interface IHackerNewsHttpService
    {
        Task<HttpServiceResponse<IEnumerable<int>>> GetStoryIds();
        Task<HttpServiceResponse<StoryDto>> GetStory(int sotryId);

    }
}
