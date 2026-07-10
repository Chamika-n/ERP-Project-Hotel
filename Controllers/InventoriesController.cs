using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GrandHotel.Models;
using GrandHotel.Data;
using System.Threading.Tasks;
using System.Linq;

namespace GrandHotel.Controllers
{
    public class InventoriesController : Controller
    {
        private readonly HotelDbContext _context;

        public InventoriesController(HotelDbContext context)
        {
            _context = context;
        }

        // GET: INVENTORY
        public async Task<IActionResult> Index(string searchString)
        {
            var items = from i in _context.Inventory
                        select i;

            if (!string.IsNullOrEmpty(searchString))
            {
                items = items.Where(i =>
                    i.ItemName.Contains(searchString) ||
                    i.Category.Contains(searchString));
            }

            return View(await items.ToListAsync());
        }


        // GET: INVENTORY/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventory = await _context.Inventory
                .FirstOrDefaultAsync(m => m.ItemId == id);
            if (inventory == null)
            {
                return NotFound();
            }

            return View(inventory);
        }

        // GET: INVENTORY/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: INVENTORY/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItemId,ItemName,Category,Quantity,MinimumStock,UnitPrice,Supplier,LastUpdated")] Inventory inventory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inventory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(inventory);
        }

        // GET: INVENTORY/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventory = await _context.Inventory.FindAsync(id);
            if (inventory == null)
            {
                return NotFound();
            }
            return View(inventory);
        }

        // POST: INVENTORY/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("ItemId,ItemName,Category,Quantity,MinimumStock,UnitPrice,Supplier,LastUpdated")] Inventory inventory)
        {
            if (id != inventory.ItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inventory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InventoryExists(inventory.ItemId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(inventory);
        }

        // GET: INVENTORY/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventory = await _context.Inventory
                .FirstOrDefaultAsync(m => m.ItemId == id);
            if (inventory == null)
            {
                return NotFound();
            }

            return View(inventory);
        }

        // POST: INVENTORY/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var inventory = await _context.Inventory.FindAsync(id);
            if (inventory != null)
            {
                _context.Inventory.Remove(inventory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InventoryExists(int id)
        {
            return _context.Inventory.Any(e => e.ItemId == id);
        }
    }
}