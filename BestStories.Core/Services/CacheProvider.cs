using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using BestStories.Core.Dtos;
using BestStories.Core.HttpServices;
using System.Linq;

namespace BestStories.Core.Services
{
    public class CacheProvider : ICacheProvider
    {
        private static readonly SemaphoreSlim GetUsersSemaphore = new SemaphoreSlim(1, 1);
        private readonly IMemoryCache _cache;
        private readonly IHackerNewsHttpService _hackerNewsHttpService;

        public CacheProvider(IMemoryCache memoryCache, IHackerNewsHttpService hackerNewsHttpService)
        {
            _cache = memoryCache;
            _hackerNewsHttpService = hackerNewsHttpService;

        }
        public async Task<StoryDto> GetStoryFromCache(int storyId)
        {
            try
            {
                return await GetCachedStory(storyId, GetUsersSemaphore);
            }
            catch
            {
                throw;
            }
        }


        private async Task<StoryDto> GetCachedStory(int storyKey, SemaphoreSlim semaphore)
        {
            bool isAvaiable = _cache.TryGetValue(storyKey, out List<StoryDto> cachedStories);
            if (isAvaiable) 
                return cachedStories.Where(s => s.id == storyKey).First();
            try
            {
                await semaphore.WaitAsync();

                isAvaiable = _cache.TryGetValue(storyKey, out cachedStories);

                if (isAvaiable) 
                    return cachedStories.Where(s => s.id == storyKey).First();

                var result = await GetStoryFromTheService(storyKey);

                if (cachedStories == null) 
                    cachedStories = new List<StoryDto>();

                cachedStories.Add(result);

                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                    SlidingExpiration = TimeSpan.FromMinutes(2),
                    Size = 1024,
                };

                _cache.Set(storyKey, cachedStories, cacheEntryOptions);
            }
            catch
            {
                throw;
            }
            finally
            {
                semaphore.Release();
            }
            return cachedStories.Where(s => s.id == storyKey).First();
        }


        private async Task<StoryDto> GetStoryFromTheService(int storyKey)
        {

            var result = await _hackerNewsHttpService.GetStory(storyKey);
            return result.Content;
        }
    }
}
