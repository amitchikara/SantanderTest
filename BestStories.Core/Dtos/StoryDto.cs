﻿using System;

namespace BestStories.Core.Dtos
{
    public class StoryDto
    {
        public string by { get; set; }
        public int descendants { get; set; }
        public int id { get; set; }
        public int score { get; set; }
        public int time { get; set; }
        public string title { get; set; }
        public string type { get; set; }
        public string url { get; set; }
    }
}
