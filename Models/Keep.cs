using System;

namespace keepr.Models
{
    public class Keep
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string userId { get; set; }
        public int keepCount { get; set; }
        public int shareCount { get; set; }
        public int viewCount { get; set; }
        
    }
}