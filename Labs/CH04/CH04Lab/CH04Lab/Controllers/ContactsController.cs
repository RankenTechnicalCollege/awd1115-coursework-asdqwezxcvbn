using CH04Lab.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CH04Lab.Controllers
{
    public class ContactsController : Controller
    {
        private readonly ContactContext _context;

        public ContactsController(ContactContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var contacts = await _context.Contacts.Include(c => c.Category).ToListAsync();
            return View(contacts);
        }
        public IActionResult Add()
        {
            ViewBag.Categories = _context.Categories.ToList();
            return View("Edit", new Contact());
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Contact contact)
        {
            if (ModelState.IsValid)
            {
                if (contact.ContactId == 0)
                {
                    contact.DateAdded = DateTime.UtcNow;
                    _context.Contacts.Add(contact);
                }
                else
                {
                    _context.Contacts.Update(contact);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Categories = _context.Categories.ToList();
            return View(contact);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null) return NotFound();

            ViewBag.Categories = _context.Categories.ToList();
            return View(contact);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var contact = await _context.Contacts.Include(c => c.Category)
                .FirstOrDefaultAsync(c => c.ContactId == id);

            if (contact == null) return NotFound();

            return View(contact);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact != null)
            {
                _context.Contacts.Remove(contact);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
    }
}
