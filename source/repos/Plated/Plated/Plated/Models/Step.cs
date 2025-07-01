using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation; // ✅ Needed for [ValidateNever]

namespace Plated.Models
{
    public class Step
    {
        public int StepId { get; set; }

        [Required]
        public int StepOrder { get; set; }

        [Required]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please select a recipe.")]
        [Display(Name = "Recipe")]
        public int RecipeId { get; set; }

        [ForeignKey("RecipeId")]
        [ValidateNever] // ✅ Prevents validation on navigation property
        public Recipe Recipe { get; set; }

        public Step()
        {
            Description = string.Empty;
            Recipe = null!;
        }
    }
}
