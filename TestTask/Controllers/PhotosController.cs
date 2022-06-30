using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;

namespace TestTask.Controllers
{
    public class PhotosController : Controller
    {
        private readonly AppFirstDBContent _context;
        private readonly AppSecondDBContent _context2;

        public PhotosController(AppFirstDBContent context, AppSecondDBContent context2)
        {
            _context = context;
            _context2 = context2;
        }

        // GET: PhotosController2
        public async Task<IActionResult> Index()
        {
            return _context.Photos != null ?
                        View(await _context.Photos.ToListAsync()) :
                        Problem("Entity set 'AppDBContent.Photos'  is null.");
        }

        // GET: PhotosController2/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Photos == null)
            {
                return NotFound();
            }

            var photo = await _context.Photos
                .FirstOrDefaultAsync(p => p.Id == id);
            if (photo == null)
            {
                return NotFound();
            }
            if (!photo.isCopied)
            {
                photo.isCopied = true;
                isCopiedPhoto isCopiedPhoto = new isCopiedPhoto();
                isCopiedPhoto.photoId = photo.Id;
                isCopiedPhoto.photoDescription = photo.Description;
                isCopiedPhoto.photoData = isCopiedPhoto.GetPhoto(photo.ImageSrc);
                _context2.Add(isCopiedPhoto);
                await _context.SaveChangesAsync();
                await _context2.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: PhotosController2/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PhotosController2/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,Image,isCopied")] Photo photo)
        {
            if (ModelState.IsValid)
            {
                photo.uploadTime = DateTime.Now;
                photo.ImageSrc = photo.getSrc(photo);
                _context.Photos.Add(photo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: PhotosController2/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Photos == null)
            {
                return NotFound();
            }

            var photo = await _context.Photos.FindAsync(id);
            if (photo == null)
            {
                return NotFound();
            }
            return View(photo);
        }

        // POST: PhotosController2/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ImageSrc,uploadTime,isCopied,Description")] Photo photo)
        {
            _context.Update(photo);
            await _context.SaveChangesAsync();

            //return View(photos);
            return RedirectToAction(nameof(Index));
        }

        // GET: PhotosController2/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Photos == null)
            {
                return NotFound();
            }

            var photo = await _context.Photos
                .FirstOrDefaultAsync(p => p.Id == id);
            if (photo == null)
            {
                return NotFound();
            }

            return View(photo);
        }

        // POST: PhotosController2/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Photos == null)
            {
                return Problem("Entity set 'AppDBContent.Photos'  is null.");
            }
            var photo = await _context.Photos.FindAsync(id);
            if (photo != null)
            {
                string path = Directory.GetCurrentDirectory() + "/wwwroot/Photos/" + photo.ImageSrc;
                System.IO.File.Delete(path);
                _context.Photos.Remove(photo);
                await _context.SaveChangesAsync();
                if (photo.isCopied)
                {
                    var isCopiedPhoto = await _context2.isCopiedPhotos.Where(c => c.photoId == id).FirstAsync();
                    _context2.isCopiedPhotos.Remove(isCopiedPhoto);
                    await _context2.SaveChangesAsync();
                }

            }
            return RedirectToAction(nameof(Index));
        }
        private bool PhotoExists(int id)
        {
            return (_context.Photos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
