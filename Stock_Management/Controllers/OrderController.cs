using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Stock_Management.Models;

namespace Stock_Management.Controllers
{
    public class OrderController : Controller
    {
        private readonly Ecommerce_Stock_ManagementContext _context;

        public OrderController(Ecommerce_Stock_ManagementContext context)
        {
            _context = context;
        }

        // GET: Order
        public async Task<IActionResult> Index()
        {
            return View(await _context.Order.ToListAsync());
        }

        // GET: Order/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Order/Create
        public IActionResult Create()
        {
            var vm = new OrderViewModel();
            ViewBag.CustomerN = new SelectList(_context.Customer, "CustomerId", "CustomerName");
            ViewBag.Products = new SelectList(_context.Product, "ProductId", "ProductName");
            return View(vm);
        }

        // POST: Order/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrderViewModel Ovm)
        { 
            if (ModelState.IsValid)
            {
                var customerName = _context.Customer.Find(Ovm.CustomerId).CustomerName;
                Ovm.Customer = customerName;
                var price = _context.Product.Find(Ovm.ProductId).ProductPrice;
                Ovm.ProductPrice = price;
                Ovm.TotalCost = price * Ovm.Quantitiy;
                _context.Add(new Order() { OrderId = Ovm.OrderId, OrderDate = Ovm.OrderDate});
                await _context.SaveChangesAsync();
                _context.Add(new OrderProduct() { OrderId = _context.Order.OrderByDescending(r => r.OrderId).Select(r => r.OrderId).First(), ProductId = Ovm.ProductId });
                _context.Add(new OrderDetails() { OrderId = _context.Order.OrderByDescending(r => r.OrderId).Select(r => r.OrderId).First(), Quantitiy = Ovm.Quantitiy, TotalCost = Ovm.TotalCost });
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.CustomerN = new SelectList(_context.Customer, "CustomerId", "CustomerName");
            ViewBag.Products = new SelectList(_context.Product, "ProductId", "ProductName");
            return View(Ovm);
        }

        // GET: Order/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: Order/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,OrderDate")] Order order)
        {
            if (id != order.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderId))
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
            return View(order);
        }

        // GET: Order/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Order.FindAsync(id);
            _context.Order.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.OrderId == id);
        }
    }
}
