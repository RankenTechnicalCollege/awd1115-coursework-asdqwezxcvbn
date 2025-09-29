using CH06Lab.Data;
using Microsoft.EntityFrameworkCore;
using CH06Lab.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CH06Lab.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("topic/{topicId}/category/{categoryId}/")]
        [HttpGet("topic/{topicId}/")]
        [HttpGet("category/{categoryId}/")]
        [HttpGet("")]
        public async Task<IActionResult> Index(string? topicId, string? categoryId)
        {
            var topics = await _context.Topics.OrderBy(t => t.Name).ToListAsync();
            var categories = await _context.Categories.OrderBy(c => c.Name).ToListAsync();

            var faqsQuery = _context.Faqs
                .Include(f => f.Topic)
                .Include(f => f.Category)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(topicId))
            {
                faqsQuery = faqsQuery.Where(f => f.TopicId == topicId);
            }

            if (!string.IsNullOrWhiteSpace(categoryId))
            {
                faqsQuery = faqsQuery.Where(f => f.CategoryId == categoryId);
            }

            var faqs = await faqsQuery.OrderBy(f => f.Question).ToListAsync();

            // Pass filter info to the view
            ViewData["Topics"] = topics;
            ViewData["Categories"] = categories;
            ViewData["SelectedTopic"] = topicId;
            ViewData["SelectedCategory"] = categoryId;

            return View(faqs);
        }
    }
}
