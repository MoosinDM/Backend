using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TalendVerification.Data;
using TalendVerification.Models;

namespace TalendVerification.Controllers
{
    //[Authorize]
    public class RequesterController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RequesterController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Requester
        [Authorize]
        public async Task<IActionResult> Index()
        {
              return _context.Requesters != null ? 
                          View(await _context.Requesters.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Requesters'  is null.");
        }

        // GET: Requester/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Requesters == null)
            {
                return NotFound();
            }

            var requesterEntity = await _context.Requesters
                .FirstOrDefaultAsync(m => m.Id == id);
            if (requesterEntity == null)
            {
                return NotFound();
            }

            return View(requesterEntity);
        }

        // GET: Requester/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Requester/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ReqType,UserName,Address,Email,MobileNumber,AdharNumber,DocPath,Status,SubDate")] RequesterEntity requesterEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(requesterEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(requesterEntity);
        }

        // GET: Requester/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Requesters == null)
            {
                return NotFound();
            }

            var requesterEntity = await _context.Requesters.FindAsync(id);
            if (requesterEntity == null)
            {
                return NotFound();
            }
            return View(requesterEntity);
        }

        // POST: Requester/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ReqType,UserName,Address,Email,MobileNumber,AdharNumber,DocPath,Status,SubDate")] RequesterEntity requesterEntity)
        {
            if (id != requesterEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(requesterEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RequesterEntityExists(requesterEntity.Id))
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
            return View(requesterEntity);
        }

        // GET: Requester/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Requesters == null)
            {
                return NotFound();
            }

            var requesterEntity = await _context.Requesters
                .FirstOrDefaultAsync(m => m.Id == id);
            if (requesterEntity == null)
            {
                return NotFound();
            }

            return View(requesterEntity);
        }

        // POST: Requester/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Requesters == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Requesters'  is null.");
            }
            var requesterEntity = await _context.Requesters.FindAsync(id);
            if (requesterEntity != null)
            {
                _context.Requesters.Remove(requesterEntity);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RequesterEntityExists(int id)
        {
          return (_context.Requesters?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
