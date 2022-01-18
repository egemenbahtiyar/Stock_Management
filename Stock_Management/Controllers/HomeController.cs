using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Stock_Management.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Stock_Management.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Ecommerce_Stock_ManagementContext _context;

        public HomeController(ILogger<HomeController> logger,Ecommerce_Stock_ManagementContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index(HomeIndexViewModel Hvm)
        {
            Hvm.NoOfCategories = _context.Category.FromSqlRaw("select * from Category").Count();
            Hvm.NoOfCustomers = _context.Customer.FromSqlRaw("select * from Customer") .Count();
            Hvm.NoOfEmployees = _context.Employee.FromSqlRaw("select * from Employee").Count();
            Hvm.NoOfOrders = _context.Order.FromSqlRaw("select * from [order]").Count();
            Hvm.NoOfProducts = _context.Product.FromSqlRaw("select * from Product").Count();
            Hvm.NoOfStorages = _context.Storage.FromSqlRaw("select * from Storage").Count();
            return View(Hvm);
        }

        public IActionResult Contact()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
