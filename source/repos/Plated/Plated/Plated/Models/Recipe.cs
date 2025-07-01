using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Plated.Models
{
    public class Recipe
    {
        public int RecipeId { get; set; }

        [Required]
        public string Title { get; set; }

        public string Ingredients { get; set; }

        public string Instructions { get; set; }

        public string? ImagePath { get; set; }

        public ICollection<Step>? Steps { get; set; }

        public Recipe()
        {
            Title = string.Empty;
            Ingredients = string.Empty;
            Instructions = string.Empty;
            Steps = new List<Step>();
        }
    }
}
