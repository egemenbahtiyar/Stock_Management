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
    public class StorageController : Controller
    {
        private readonly Ecommerce_Stock_ManagementContext _context;

        public StorageController(Ecommerce_Stock_ManagementContext context)
        {
            _context = context;
        }

        // GET: Storage
        public async Task<IActionResult> Index()
        {
            //var sql = @"select * from Storage";
            //var entities = _context.Storage.FromSqlRaw("select * from Storage");
            return View(await _context.Storage.FromSqlRaw("select * from Storage").ToListAsync<Storage>());
        }

        // GET: Storage/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storage = await _context.Storage
                .FirstOrDefaultAsync(m => m.StorageId == id);
            if (storage == null)
            {
                return NotFound();
            }

            return View(storage);
        }

        // GET: Storage/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Storage/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StorageId,StorageName,StorageLocation")] Storage storage)
        {
            if (ModelState.IsValid)
            {
                var sql = @"Insert into Storage (StorageName,StorageLocation) Values(@NewStorageName,@NewStorageLocation)";
                _context.Database.ExecuteSqlRaw(sql, new SqlParameter("@NewStorageName", storage.StorageName), new SqlParameter("@NewStorageLocation", storage.StorageLocation));
                //_context.Add(storage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(storage);
        }

        // GET: Storage/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storage = await _context.Storage.FindAsync(id);
            if (storage == null)
            {
                return NotFound();
            }
            return View(storage);
        }

        // POST: Storage/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StorageId,StorageName,StorageLocation")] Storage storage)
        {
            if (id != storage.StorageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var sql = @"Update Storage SET StorageName = @NewStorageName,StorageLocation=@NewStorageLocation WHERE StorageId = @Id";
                    _context.Database
                        .ExecuteSqlRaw(sql,
                        new SqlParameter("@NewStorageName",storage.StorageName),
                        new SqlParameter("@NewStorageLocation",storage.StorageLocation),
                        new SqlParameter("@Id",storage.StorageId));
                    //_context.Update(storage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StorageExists(storage.StorageId))
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
            return View(storage);
        }

        // GET: Storage/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storage = await _context.Storage
                .FirstOrDefaultAsync(m => m.StorageId == id);
            if (storage == null)
            {
                return NotFound();
            }

            return View(storage);
        }

        // POST: Storage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //var storage = await _context.Storage.FindAsync(id);
            _context.Database.ExecuteSqlRaw("Delete from Storage where StorageID=" + id);
            //_context.Storage.Remove(storage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StorageExists(int id)
        {
            return _context.Storage.Any(e => e.StorageId == id);
        }
    }
}
