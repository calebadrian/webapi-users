using System;
using System.ComponentModel.DataAnnotations;

namespace keepr.Models
{
    public class AddVaultModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Private { get; set; }
        public string userId { get; set; }
    }
}