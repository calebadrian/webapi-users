using System;

namespace keepr.Models
{
    public class VaultKeepReturnModel
    {
        public string vkId { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Pic { get; set; }
        public int Private { get; set; }
        public string userId { get; set; }
        public int keepCount { get; set; }
        public int shareCount { get; set; }
        public int viewCount { get; set; }
        
    }
}