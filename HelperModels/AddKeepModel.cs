using System.ComponentModel.DataAnnotations;

namespace keepr.Models
{
    public class AddKeepModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string userId { get; set; }
    }
}