using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Election.Models;

namespace Election.Controllers
{
    public class ElectionInfoesController : Controller
    {
        private readonly ElectiondbContext _context;

        public ElectionInfoesController(ElectiondbContext context)
        {
            _context = context;
        }

        // GET: ElectionInfoes
        public async Task<IActionResult> Index()
        {
            var electiondbContext = _context.ElectionInfo.Include(e => e.CreateByNavigation).Include(e => e.District);
            return View(await electiondbContext.ToListAsync());
        }

        // GET: ElectionInfoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var electionInfo = await _context.ElectionInfo
                .Include(e => e.CreateByNavigation)
                .Include(e => e.District)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (electionInfo == null)
            {
                return NotFound();
            }

            return View(electionInfo);
        }

        // GET: ElectionInfoes/Create
        public IActionResult Create()
        {
            ViewData["CreateBy"] = new SelectList(_context.Admin, "Id", "UserName");
            ViewData["DistrictId"] = new SelectList(_context.District, "Id", "Name");
            return View();
        }

        // POST: ElectionInfoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,StartDate,EndDate,DistrictId,CreateBy")] ElectionInfo electionInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(electionInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CreateBy"] = new SelectList(_context.Admin, "Id", "UserName", electionInfo.CreateBy);
            ViewData["DistrictId"] = new SelectList(_context.District, "Id", "Name", electionInfo.DistrictId);
            return View(electionInfo);
        }

        // GET: ElectionInfoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var electionInfo = await _context.ElectionInfo.FindAsync(id);
            if (electionInfo == null)
            {
                return NotFound();
            }
            ViewData["CreateBy"] = new SelectList(_context.Admin, "Id", "UserName", electionInfo.CreateBy);
            ViewData["DistrictId"] = new SelectList(_context.District, "Id", "Name", electionInfo.DistrictId);
            return View(electionInfo);
        }

        // POST: ElectionInfoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,StartDate,EndDate,DistrictId,CreateBy")] ElectionInfo electionInfo)
        {
            if (id != electionInfo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(electionInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ElectionInfoExists(electionInfo.Id))
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
            ViewData["CreateBy"] = new SelectList(_context.Admin, "Id", "UserName", electionInfo.CreateBy);
            ViewData["DistrictId"] = new SelectList(_context.District, "Id", "Name", electionInfo.DistrictId);
            return View(electionInfo);
        }

        // GET: ElectionInfoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var electionInfo = await _context.ElectionInfo
                .Include(e => e.CreateByNavigation)
                .Include(e => e.District)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (electionInfo == null)
            {
                return NotFound();
            }

            return View(electionInfo);
        }

        // POST: ElectionInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var electionInfo = await _context.ElectionInfo.FindAsync(id);
            _context.ElectionInfo.Remove(electionInfo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ElectionInfoExists(int id)
        {
            return _context.ElectionInfo.Any(e => e.Id == id);
        }
    }
}
