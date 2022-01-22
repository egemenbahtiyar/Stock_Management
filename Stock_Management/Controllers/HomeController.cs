using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
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
            string connectionstring = "Server=.\\SQLExpress;Database=Ecommerce_Stock_Management;Trusted_Connection=True;";
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                connection.Open();

                String sql = "select * from [Total Price Of Sales]";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Hvm.TotalCostOfOrders = reader.GetDecimal(0);

                        }
                    }
                }
            }
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                connection.Open();

                String sql = "select * from [Top-selling product]";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Hvm.TopSellingProduct = reader.GetString(0);

                        }
                    }
                }
            }
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                connection.Open();

                String sql = "select * from [Last Added Product Name]";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Hvm.LastAddedProduct = reader.GetString(0);

                        }
                    }
                }
            }
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                connection.Open();

                String sql = "EXEC EmployeeFromKonya @City = 'Konya'";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Hvm.EmployeesFromKonya = reader.GetInt32(0);

                        }
                    }
                }
            }
            //Hvm.TotalCostOfOrders = _context.Database.ExecuteSqlCommand("select * from [Total Price Of Sales]");
            //Hvm.TopSellingProduct = _context.Database.ExecuteSqlRaw("select * from [Top-selling product]").ToString();
            //Hvm.LastAddedProduct = _context.Database.ExecuteSqlRaw("select * from [Last Added Product Name]").ToString();
            //Hvm.EmployeesFromKonya = _context.Database.ExecuteSqlRaw("EXEC EmployeeFromKonya @City = 'Konya'");
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
