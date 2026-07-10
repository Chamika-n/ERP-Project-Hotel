using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GrandHotel.Models;
using GrandHotel.Data;
using System.Threading.Tasks;
using System.Linq;

namespace GrandHotel.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly HotelDbContext _context;

        public EmployeesController(HotelDbContext context)
        {
            _context = context;
        }

        // GET: EMPLOYEES
        public async Task<IActionResult> Index(string searchString)
        {
            var employees = from e in _context.Employees
                            select e;

            if (!string.IsNullOrEmpty(searchString))
            {
                employees = employees.Where(e =>
                    e.FullName.Contains(searchString) ||
                    e.Department.Contains(searchString) ||
                    e.Position.Contains(searchString));
            }

            return View(await employees.ToListAsync());
        }

        // GET: EMPLOYEES/Details/5
        public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
        if (employee == null)
        {
            return NotFound();
        }

        return View(employee);
    }

    // GET: EMPLOYEES/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: EMPLOYEES/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeId,FullName,NIC,Phone,Email,Position,Department,Salary,JoinDate,Status")] Employee employee)
    {
        if (ModelState.IsValid)
        {
            _context.Add(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(employee);
    }

    // GET: EMPLOYEES/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

    // POST: EMPLOYEES/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("EmployeeId,FullName,NIC,Phone,Email,Position,Department,Salary,JoinDate,Status")] Employee employee)
        {
            if (id != employee.EmployeeId)
            {
                return NotFound();
            }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(employee);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(employee.EmployeeId))
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
        return View(employee);
    }

        // GET: EMPLOYEES/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

    // POST: EMPLOYEES/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int? id)
        {
            return _context.Employees.Any(e => e.EmployeeId == id);
        }
    }
}
