using BestStories.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BestStories.Core.Services
{
    public interface ICacheProvider
    {
        Task<StoryDto> GetStoryFromCache(int storyId);
    }
}
