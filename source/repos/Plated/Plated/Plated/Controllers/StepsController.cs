using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Plated.Data;
using Plated.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Plated.Controllers
{
    public class StepsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<StepsController> _logger;

        // constructor that connects to the database and logging system
        public StepsController(ApplicationDbContext context, ILogger<StepsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // show all steps in a list
        public async Task<IActionResult> Index()
        {
            var steps = _context.Steps.Include(s => s.Recipe);
            return View(await steps.ToListAsync());
        }

        // show one specific step by ID
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var step = await _context.Steps
                .Include(s => s.Recipe)
                .FirstOrDefaultAsync(m => m.StepId == id);

            if (step == null)
                return NotFound();

            return View(step);
        }

        // load the form to create a new step
        public IActionResult Create()
        {
            var recipes = _context.Recipes.ToList();

            // make sure at least one recipe exists
            if (recipes.Count == 0)
            {
                TempData["Error"] = "You must create a recipe before adding steps.";
                return RedirectToAction("Index", "Recipes");
            }

            // this creates the dropdown list for recipes
            ViewData["RecipeId"] = new SelectList(recipes, "RecipeId", "Title", recipes.First().RecipeId);
            return View();
        }

        // handle the form submission to create a step
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StepId,StepOrder,Description,RecipeId")] Step step)
        {
            _logger.LogInformation("✅ POST Create triggered");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(step);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Step created successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error creating step.");
                    ModelState.AddModelError("", "Something went wrong while saving the step.");
                }
            }

            // if something went wrong, reload the dropdown
            ViewData["RecipeId"] = new SelectList(_context.Recipes.ToList(), "RecipeId", "Title", step.RecipeId);
            return View(step);
        }

        // load the form to edit a step
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var step = await _context.Steps.FindAsync(id);
            if (step == null)
                return NotFound();

            ViewData["RecipeId"] = new SelectList(_context.Recipes.ToList(), "RecipeId", "Title", step.RecipeId);
            return View(step);
        }

        // handle the form submission to save the edits
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StepId,StepOrder,Description,RecipeId")] Step step)
        {
            if (id != step.StepId)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(step);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StepExists(step.StepId))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            // reload dropdown if model is invalid
            ViewData["RecipeId"] = new SelectList(_context.Recipes.ToList(), "RecipeId", "Title", step.RecipeId);
            return View(step);
        }

        // show a confirmation page before deleting a step
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var step = await _context.Steps
                .Include(s => s.Recipe)
                .FirstOrDefaultAsync(m => m.StepId == id);

            if (step == null)
                return NotFound();

            return View(step);
        }

        // actually delete the step once confirmed
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed([Bind("StepId")] Step step)
        {
            // grab the step from the database using the StepId passed from the form
            var stepToDelete = await _context.Steps.FindAsync(step.StepId);

            // if the step exists, remove it from the database
            if (stepToDelete != null)
            {
                _context.Steps.Remove(stepToDelete);
                await _context.SaveChangesAsync();
            }

            // go back to the step list after deleting
            return RedirectToAction(nameof(Index));
        }

        // helper method to check if a step exists in the database
        private bool StepExists(int id)
        {
            return _context.Steps.Any(e => e.StepId == id);
        }
    }
}
