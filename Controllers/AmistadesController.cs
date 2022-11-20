using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RedSocial.Models;

namespace RedSocial.Controllers
{
    public class AmistadesController : Controller
    {
        private readonly RedSocialBDContext _context;

        public AmistadesController(RedSocialBDContext context)
        {
            _context = context;
        }

        // GET: Amistades
        public async Task<IActionResult> Index()
        {
            var redSocialBDContext = _context.Amistad.Include(a => a.EmailReciverNavigation).Include(a => a.EmailSenderNavigation);
            return View(await redSocialBDContext.ToListAsync());
        }

        // GET: Amistades/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amistad = await _context.Amistad
                .Include(a => a.EmailReciverNavigation)
                .Include(a => a.EmailSenderNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (amistad == null)
            {
                return NotFound();
            }

            return View(amistad);
        }

        // GET: Amistades/Create
        public IActionResult Create()
        {
            ViewData["EmailReciver"] = new SelectList(_context.Usuario, "Email", "Email");
            ViewData["EmailSender"] = new SelectList(_context.Usuario, "Email", "Email");
            return View();
        }

        // POST: Amistades/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EmailSender,EmailReciver,Aceptada")] Amistad amistad)
        {
            if (ModelState.IsValid)
            {
                _context.Add(amistad);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmailReciver"] = new SelectList(_context.Usuario, "Email", "Email", amistad.EmailReciver);
            ViewData["EmailSender"] = new SelectList(_context.Usuario, "Email", "Email", amistad.EmailSender);
            return View(amistad);
        }

        // GET: Amistades/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amistad = await _context.Amistad.FindAsync(id);
            if (amistad == null)
            {
                return NotFound();
            }
            ViewData["EmailReciver"] = new SelectList(_context.Usuario, "Email", "Email", amistad.EmailReciver);
            ViewData["EmailSender"] = new SelectList(_context.Usuario, "Email", "Email", amistad.EmailSender);
            return View(amistad);
        }

        // POST: Amistades/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EmailSender,EmailReciver,Aceptada")] Amistad amistad)
        {
            if (id != amistad.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(amistad);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AmistadExists(amistad.Id))
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
            ViewData["EmailReciver"] = new SelectList(_context.Usuario, "Email", "Email", amistad.EmailReciver);
            ViewData["EmailSender"] = new SelectList(_context.Usuario, "Email", "Email", amistad.EmailSender);
            return View(amistad);
        }

        // GET: Amistades/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amistad = await _context.Amistad
                .Include(a => a.EmailReciverNavigation)
                .Include(a => a.EmailSenderNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (amistad == null)
            {
                return NotFound();
            }

            return View(amistad);
        }

        // POST: Amistades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var amistad = await _context.Amistad.FindAsync(id);
            _context.Amistad.Remove(amistad);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AmistadExists(int id)
        {
            return _context.Amistad.Any(e => e.Id == id);
        }
    }
}
