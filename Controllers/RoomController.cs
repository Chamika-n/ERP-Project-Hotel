
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GrandHotel.Models;
using GrandHotel.Data;

public class RoomController : Controller
{
    private readonly HotelDbContext _context;

    public RoomController(HotelDbContext context)
    {
        _context = context;
    }

    // GET: ROOMS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Rooms.ToListAsync());
    }

    // GET: ROOMS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var room = await _context.Rooms
            .FirstOrDefaultAsync(m => m.Id == id);
        if (room == null)
        {
            return NotFound();
        }

        return View(room);
    }

    // GET: ROOMS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: ROOMS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,RoomNumber,RoomType,Price,Status")] Room room)
    {
        if (ModelState.IsValid)
        {
            _context.Add(room);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(room);
    }

    // GET: ROOMS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var room = await _context.Rooms.FindAsync(id);
        if (room == null)
        {
            return NotFound();
        }
        return View(room);
    }

    // POST: ROOMS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,RoomNumber,RoomType,Price,Status")] Room room)
    {
        if (id != room.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(room);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomExists(room.Id))
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
        return View(room);
    }

    // GET: ROOMS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var room = await _context.Rooms
            .FirstOrDefaultAsync(m => m.Id == id);
        if (room == null)
        {
            return NotFound();
        }

        return View(room);
    }

    // POST: ROOMS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var room = await _context.Rooms.FindAsync(id);
        if (room != null)
        {
            _context.Rooms.Remove(room);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool RoomExists(int? id)
    {
        return _context.Rooms.Any(e => e.Id == id);
    }
}
