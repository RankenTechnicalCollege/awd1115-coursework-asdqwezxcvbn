using CH04Lab.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CH04Lab.Controllers
{
    public class HomeController : Controller
    {
        private readonly ContactContext _context;

        public HomeController(ContactContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var contacts = await _context.Contacts.Include(c => c.Category).ToListAsync();
            return View(contacts);
        }

        public async Task<IActionResult> Details(int id)
        {
            var contact = await _context.Contacts
                .Include(c => c.Category)
                .FirstOrDefaultAsync(c => c.ContactId == id);

            if (contact == null) return NotFound();

            return View(contact);
        }

        public IActionResult Add()
        {
            ViewBag.Categories = _context.Categories
                .Select(c => new SelectListItem
                {
                    Value = c.CategoryId.ToString(),
                    Text = c.Name
                })
                .ToList();

            return View("Edit", new Contact());
        }

        [HttpPost]
        public async Task<IActionResult> Add(Contact contact)
        {
            if (ModelState.IsValid)
            {
                contact.DateAdded = DateTime.UtcNow;
                _context.Contacts.Add(contact);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Categories = _context.Categories
                .Select(c => new SelectListItem
                {
                    Value = c.CategoryId.ToString(),
                    Text = c.Name
                })
                .ToList();

            return View("Edit", contact);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null) return NotFound();

            ViewBag.Categories = _context.Categories
                .Select(c => new SelectListItem
                {
                    Value = c.CategoryId.ToString(),
                    Text = c.Name
                })
                .ToList();

            return View(contact);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Contact contact)
        {
            if (ModelState.IsValid)
            {
                _context.Contacts.Update(contact);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Categories = _context.Categories
                .Select(c => new SelectListItem
                {
                    Value = c.CategoryId.ToString(),
                    Text = c.Name
                })
                .ToList();

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