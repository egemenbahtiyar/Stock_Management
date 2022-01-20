using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
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
        public IActionResult Index()
        {
            List<OrderIndexViewModel> OvmList = new List<OrderIndexViewModel>();
            string connectionstring = "Server=.\\SQLExpress;Database=Ecommerce_Stock_Management;Trusted_Connection=True;";
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                connection.Open();

                String sql = "select ProductName,ProductPrice,CustomerName,TotalCost,OrderDate,Quantitiy,O.OrderID from [order] as O" +
                    " inner join Customer as C on C.CustomerID = O.CustomerId" +
                    " inner join Order_Product as OP on OP.OrderID = O.OrderID" +
                    " inner join Product as P on OP.ProductID = P.ProductID" +
                    " inner join OrderDetails as OD on O.OrderID =OD.OrderID";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            OvmList.Add(new OrderIndexViewModel { 
                                ProductName = reader.GetString(0), 
                                ProductPrice = reader.GetDecimal(1),
                                CustomerName= reader.GetString(2),
                                TotalCost=reader.GetDecimal(3),
                                OrderDate=reader.GetDateTime(4),
                                Quantitiy=reader.GetInt32(5),
                                OrderId=reader.GetInt32(6)});

                        }
                    }
                }
            }
            //OvmList = _context.Order.Include("OrderDetails").Include("Order_Product").Include("Customer").ToList();


            return View(OvmList);
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
        public IActionResult Create(OrderViewModel Ovm)
        { 
            if (ModelState.IsValid)
            {
                //_context.Database.ExecuteSqlRaw("Update Customer Set OrderID = " + Ovm.OrderId + "where CustomerID=" + Ovm.CustomerId);
                var price = _context.Product.Find(Ovm.ProductId).ProductPrice;
                Ovm.ProductPrice = price;
                Ovm.TotalCost = price * Ovm.Quantitiy;
                var entitiy = new Order() { OrderId = Ovm.OrderId, OrderDate = Ovm.OrderDate,CustomerId=Ovm.CustomerId };
                _context.Add(entitiy);
                _context.SaveChanges();
                _context.Add(new OrderProduct() { OrderId = _context.Order.OrderByDescending(r => r.OrderId).Select(r => r.OrderId).First(), ProductId = Ovm.ProductId });
                _context.Add(new OrderDetails() { OrderId = _context.Order.OrderByDescending(r => r.OrderId).Select(r => r.OrderId).First(), Quantitiy = Ovm.Quantitiy, TotalCost = Ovm.TotalCost });
                _context.SaveChanges();
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
