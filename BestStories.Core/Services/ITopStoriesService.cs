using BestStories.Core.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BestStories.Core.Services
{
    public interface ITopStoriesService
    {
        Task<IEnumerable<StoryDto>>  Get(int numberOfStories);
    }
}
