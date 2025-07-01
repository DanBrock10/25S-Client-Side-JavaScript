using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plated.Models
{
    public class Ingredient
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Range(0.01, 1000)]
        public float Quantity { get; set; }

        public string? Unit { get; set; }

        public string? Notes { get; set; }

        // Foreign Key
        public int RecipeId { get; set; }

        // Navigation property
        public Recipe? Recipe { get; set; }

        public Ingredient()
        {
            Name = string.Empty;
        }
    }
}
