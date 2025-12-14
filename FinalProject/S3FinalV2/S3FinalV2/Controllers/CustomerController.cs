using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using S3FinalV2.Data;
using S3FinalV2.Models;
using System.Security.Claims;

namespace S3FinalV2.Controllers
{
    [Authorize(Roles = "Customer")]
    public class CustomerController : Controller
    {
        private readonly MechTrackDbContext _context;

        public CustomerController(MechTrackDbContext context)
        {
            _context = context;
        }

        // GET: /Customer/Orders
        public async Task<IActionResult> Orders()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.UserId == userId);

            // Auto-create customer profile if missing
            if (customer == null)
            {
                customer = new Customers
                {
                    UserId = userId!,
                    Name = User.Identity?.Name ?? "Customer",
                    Email = User.Identity?.Name ?? "unknown@email.com",
                    Phone = "Not Provided",
                    VehicleInfo = "Not Provided"
                };

                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();
            }

            var orders = await _context.AssignedJobs
                .Include(a => a.Jobs)
                .Include(a => a.MechanicAssignments)
                    .ThenInclude(ma => ma.Mechanic)
                .Where(a => a.CustomerId == customer.CustomerId)
                .OrderByDescending(a => a.CreatedDate)
                .ToListAsync();

            return View(orders);
        }

        // GET: /Customer/OrderDetails/5
        public async Task<IActionResult> OrderDetails(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (customer == null)
                return RedirectToAction(nameof(Orders));

            var order = await _context.AssignedJobs
                .Include(a => a.Jobs)
                .Include(a => a.MechanicAssignments)
                    .ThenInclude(ma => ma.Mechanic)
                .FirstOrDefaultAsync(a =>
                    a.AssignedJobId == id &&
                    a.CustomerId == customer.CustomerId);

            if (order == null)
                return NotFound();

            return View(order);
        }
    }
}