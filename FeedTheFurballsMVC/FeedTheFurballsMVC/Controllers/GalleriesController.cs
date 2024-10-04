using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FeedTheFurballsMVC.Models;

namespace FeedTheFurballsMVC.Controllers
{
    public class GalleriesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<GalleriesController> _logger;

        public GalleriesController(AppDbContext context, ILogger<GalleriesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Galleries
        public async Task<IActionResult> Index()
        {
            return View(await _context.Galleries.ToListAsync());
        }

        // GET: Galleries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gallery = await _context.Galleries
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gallery == null)
            {
                return NotFound();
            }

            return View(gallery);
        }

        // GET: Galleries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Galleries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile imageFile, [Bind("Id,ImageName,ImagePath")] Gallery gallery)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                var fileName = Path.GetFileName(imageFile.FileName);
                var filePath = Path.Combine("wwwroot/images", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                // Store the relative path in the database
                gallery.ImagePath = "/images/" + fileName;

                _context.Add(gallery);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(gallery);
        }


        // GET: Galleries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gallery = await _context.Galleries.FindAsync(id);
            if (gallery == null)
            {
                return NotFound();
            }
            return View(gallery);
        }

        // POST: Galleries/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IFormFile imageFile, [Bind("Id,ImageName,ImagePath")] Gallery gallery)
        {
            if (id != gallery.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (imageFile != null && imageFile.Length > 0)
                    {
                        // Delete old file if necessary
                        var oldGallery = await _context.Galleries.FindAsync(id);
                        if (oldGallery != null)
                        {
                            var oldFilePath = Path.Combine("wwwroot" + oldGallery.ImagePath);
                            if (System.IO.File.Exists(oldFilePath))
                            {
                                System.IO.File.Delete(oldFilePath);
                            }

                            // Save new file
                            var fileName = Path.GetFileName(imageFile.FileName);
                            var filePath = Path.Combine("wwwroot/images", fileName);

                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await imageFile.CopyToAsync(stream);
                            }

                            gallery.ImagePath = "/images/" + fileName; // Update the path
                        }
                    }

                    _context.Update(gallery);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GalleryExists(gallery.Id))
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
            return View(gallery);
        }


        // GET: Galleries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gallery = await _context.Galleries
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gallery == null)
            {
                return NotFound();
            }

            return View(gallery);
        }

        // POST: Galleries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gallery = await _context.Galleries.FindAsync(id);
            if (gallery != null)
            {
                // Delete file from the server
                var filePath = Path.Combine("wwwroot", gallery.ImagePath.TrimStart('/'));
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }

                _context.Galleries.Remove(gallery);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }


        private bool GalleryExists(int id)
        {
            return _context.Galleries.Any(e => e.Id == id);
        }

        // Public Gallery View Method in GalleriesController
        public async Task<IActionResult> PublicGallery()
        {
            _logger.LogInformation("Fetching public gallery images.");

            var galleries = await _context.Galleries.ToListAsync();
            if (galleries == null || !galleries.Any())
            {
                _logger.LogWarning("No images found in the gallery.");
                ViewBag.Message = "No images are currently available in the gallery.";
                return View("Index", new List<Gallery>()); // Pass an empty list if there are no images
            }

            _logger.LogInformation($"{galleries.Count} images found in the gallery.");
            return View("PublicGallery", galleries); // Pass the list of images
        }


    }
}
