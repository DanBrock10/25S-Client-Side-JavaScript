using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Plated.Data;
using Plated.Models;

namespace Plated.Controllers
{
    public class IngredientsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IngredientsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Ingredients
        public async Task<IActionResult> Index()
        {
            var ingredients = await _context.Ingredients
                .Include(i => i.Recipe)
                .ToListAsync();

            return View(ingredients);
        }

        // GET: Ingredients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var ingredient = await _context.Ingredients
                .Include(i => i.Recipe)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (ingredient == null)
                return NotFound();

            return View(ingredient);
        }

        // GET: Ingredients/Create
        public IActionResult Create()
        {
            ViewData["RecipeId"] = new SelectList(_context.Recipes, "RecipeId", "Title");
            ViewData["Unit"] = new SelectList(new[] { "g", "ml", "tbsp", "tsp", "cup", "oz" });
            return View();
        }

        // POST: Ingredients/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Quantity,Unit,Notes,RecipeId")] Ingredient ingredient)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ingredient);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["RecipeId"] = new SelectList(_context.Recipes, "RecipeId", "Title", ingredient.RecipeId);
            ViewData["Unit"] = new SelectList(new[] { "g", "ml", "tbsp", "tsp", "cup", "oz" }, ingredient.Unit);
            return View(ingredient);
        }

        // GET: Ingredients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var ingredient = await _context.Ingredients.FindAsync(id);
            if (ingredient == null)
                return NotFound();

            ViewData["RecipeId"] = new SelectList(_context.Recipes, "RecipeId", "Title", ingredient.RecipeId);
            ViewData["Unit"] = new SelectList(new[] { "g", "ml", "tbsp", "tsp", "cup", "oz" }, ingredient.Unit);
            return View(ingredient);
        }

        // POST: Ingredients/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Quantity,Unit,Notes,RecipeId")] Ingredient ingredient)
        {
            if (id != ingredient.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ingredient);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IngredientExists(ingredient.Id))
                        return NotFound();
                    else
                        throw;
                }
            }

            ViewData["RecipeId"] = new SelectList(_context.Recipes, "RecipeId", "Title", ingredient.RecipeId);
            ViewData["Unit"] = new SelectList(new[] { "g", "ml", "tbsp", "tsp", "cup", "oz" }, ingredient.Unit);
            return View(ingredient);
        }

        // GET: Ingredients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var ingredient = await _context.Ingredients
                .Include(i => i.Recipe)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (ingredient == null)
                return NotFound();

            return View(ingredient);
        }

        // POST: Ingredients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ingredient = await _context.Ingredients.FindAsync(id);
            if (ingredient != null)
            {
                _context.Ingredients.Remove(ingredient);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool IngredientExists(int id)
        {
            return _context.Ingredients.Any(e => e.Id == id);
        }
    }
}
