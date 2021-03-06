using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WeatherWeb.Models;

namespace WeatherWeb.Controllers
{
    public class FavoriteCitiesController : Controller
    {
        private readonly WeatherContext _context;

        public FavoriteCitiesController(WeatherContext context)
        {
            _context = context;
        }

        // GET: FavoriteCities
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("LoggedInUser") != null)
            {
                ViewBag.User = HttpContext.Session.GetString("LoggedInUser");

                string usernameFromTempData = TempData["UsernameAsTempData"].ToString();
                return View(_context.FavoriteCities.Where(f => f.Username.Equals(usernameFromTempData)).ToList());
            }
            else
            {
                TempData["LoginFirst"] = "You need to login first";
                return RedirectToAction("Login", "Login");
            }

        }

        // GET: FavoriteCities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var favoriteCities = await _context.FavoriteCities
                .Include(f => f.UsernameNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (favoriteCities == null)
            {
                return NotFound();
            }

            return View(favoriteCities);
        }

        // GET: FavoriteCities/Create
        public IActionResult Create()
        {
            ViewData["Username"] = new SelectList(_context.Users, "Username", "Username");
            return View();
        }

        // POST: FavoriteCities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CityName,Date,MinTemp,MaxTemp,Precipitation,Humidity,WindSpeed,Username")] FavoriteCities favoriteCities)
        {
            if (ModelState.IsValid)
            {
                _context.Add(favoriteCities);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Username"] = new SelectList(_context.Users, "Username", "Username", favoriteCities.Username);
            return View(favoriteCities);
        }

        // GET: FavoriteCities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var favoriteCities = await _context.FavoriteCities.FindAsync(id);
            if (favoriteCities == null)
            {
                return NotFound();
            }
            ViewData["Username"] = new SelectList(_context.Users, "Username", "Username", favoriteCities.Username);
            return View(favoriteCities);
        }

        // POST: FavoriteCities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CityName,Date,MinTemp,MaxTemp,Precipitation,Humidity,WindSpeed,Username")] FavoriteCities favoriteCities)
        {
            if (id != favoriteCities.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(favoriteCities);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FavoriteCitiesExists(favoriteCities.Id))
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
            ViewData["Username"] = new SelectList(_context.Users, "Username", "Username", favoriteCities.Username);
            return View(favoriteCities);
        }

        // GET: FavoriteCities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var favoriteCities = await _context.FavoriteCities
                .Include(f => f.UsernameNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (favoriteCities == null)
            {
                return NotFound();
            }

            return View(favoriteCities);
        }

        // POST: FavoriteCities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var favoriteCities = await _context.FavoriteCities.FindAsync(id);
            _context.FavoriteCities.Remove(favoriteCities);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FavoriteCitiesExists(int id)
        {
            return _context.FavoriteCities.Any(e => e.Id == id);
        }
    }
}
