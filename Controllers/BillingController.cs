using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GrandHotel.Models; // මෙන්න මේක අනිවාර්යයෙන්ම තියෙන්න ඕනේ

namespace GrandHotel.Controllers 
{
    public class BillingController : Controller
    {
        private readonly GrandHotelContext _context;

        public BillingController(GrandHotelContext context)
        {
            _context = context; 
        }

        public async Task<IActionResult> Index()
        {
            // මේ පේළියෙන් වෙන්නේ BillId අනුව Ascending (පොඩි එකේ සිට ලොකු එකට) අනුපිළිවෙලට දත්ත පෙළගැස්වීම
            var billingList = await _context.Billing
                                            .OrderBy(b => b.BillId)
                                            .ToListAsync();

            return View(billingList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BillId,GuestName,RoomNumber,CheckInDate,TotalAmount,PaymentStatus,PaymentMethod,PaymentDate")] Billing billing)
        {
            if (ModelState.IsValid)
            {
                _context.Add(billing);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(billing);
        }

        public IActionResult Payments()
        {
            // මෙතනදී අපි .Where() එක පාවිච්චි කරලා Status එක 'Paid' විතරක් තියෙන ඒව ගේනවා
            var payments = _context.Billing
                                   .Where(b => b.PaymentStatus == "Paid")
                                   .ToList();

            return View(payments);
        }
        // Edit (GET)
        public IActionResult Edit(int id)
        {
            var bill = _context.Billing.Find(id);
            return View(bill);
        }

        // Edit (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Billing billing)
        {
            _context.Update(billing);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // Delete
        // Delete (GET - තහවුරු කිරීමේ පිටුව පෙන්වීමට)
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var bill = await _context.Billing.FindAsync(id);
            if (bill == null) return NotFound();
            return View(bill);
        }

        // Delete (POST - දත්ත මැකීමට)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bill = await _context.Billing.FindAsync(id);
            if (bill != null)
            {
                _context.Billing.Remove(bill);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> MarkAsPaid(int id)
        {
            // 1. Database එකෙන් අදාල බිල් එක හොයාගන්නවා
            var bill = await _context.Billing.FindAsync(id);

            if (bill != null)
            {
                // 2. Status එක 'Paid' ලෙස වෙනස් කරනවා
                bill.Status = "Paid";

                // 3. Database එකට ඒ වෙනස සේව් කරනවා
                await _context.SaveChangesAsync();
            }

            // 4. ආයෙත් පේජ් එකටම යනවා
            return RedirectToAction(nameof(Index));
        }
    }
   
}