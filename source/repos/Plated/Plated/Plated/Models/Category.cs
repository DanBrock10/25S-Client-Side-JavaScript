using System.ComponentModel.DataAnnotations;

namespace Plated.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public Category()
        {
            Name = string.Empty;
        }
    }
}
