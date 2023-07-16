using BestStories.Controllers;
using BestStories.Core.Dtos;
using BestStories.Core.HttpServices;
using BestStories.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BestStories.Service.Test
{
    public class BestStoryControllerTest
    {
        private Mock<ITopStoriesService> mockTopStoryService;
        private Mock<ILogger<BestStoryController>> mockLogger;
        private Mock<IHackerNewsHttpService> mockHackerNewsHttpService;
        private Mock<ICacheProvider> mockCacheProvider;


        [SetUp]
        public void Setup()
        {
            mockTopStoryService = new Mock<ITopStoriesService>();
            mockLogger = new Mock<ILogger<BestStoryController>>();
            mockHackerNewsHttpService = new Mock<IHackerNewsHttpService>();
            mockCacheProvider = new Mock<ICacheProvider>();
        }

        [Test]
        public async Task When_GetTopSories_IsCalled_Then_OkResult()
        {
            // Arrange
            int numberOfStories = 2;

            mockTopStoryService
              .Setup(repo => repo.Get(numberOfStories))
              .ReturnsAsync(GetMockStories());

            var controller = new BestStoryController(mockLogger.Object, mockTopStoryService.Object, mockHackerNewsHttpService.Object, mockCacheProvider.Object);

            // Act
            var response = await controller.GetTopStories(numberOfStories);


            // Assert
            Assert.That(((ObjectResult)response.Result).StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task When_GetTopSories_IsCalled_Then_StoriesReturned()
        {
            // Arrange
            int numberOfStories = 2;

            mockTopStoryService
              .Setup(repo => repo.Get(numberOfStories))
              .ReturnsAsync(GetMockStories());

            var controller = new BestStoryController(mockLogger.Object, mockTopStoryService.Object, mockHackerNewsHttpService.Object, mockCacheProvider.Object);

            // Act
            var response = await controller.GetTopStories(numberOfStories);


            // Assert
            Assert.NotNull(((ObjectResult)response.Result).Value);
        }

        private List<StoryDto> GetMockStories()
        {
            return new List<StoryDto>()
            {
                new StoryDto()
                {
                    id = 1,
                    score = 1,
                    by = "Test user 1",
                    title = "Title 1",
                    type = "User Story",
                    url = "Test Url",
                    descendants = 0
                },
                new StoryDto()
                {
                   id = 2,
                    score = 2,
                    by = "Test user 2",
                    title = "Title 2",
                    type = "User Story",
                    url = "Test Url",
                    descendants = 0
                }
            };
        }
    }
}