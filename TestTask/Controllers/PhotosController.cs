using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
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

        // GET: api/Photos
        [Route("/photos")]
        [HttpGet]
        public async Task<ActionResult<Photo>> GetPhotos()
        {
            return _context.Photos != null ? View(await _context.Photos.ToListAsync()) : Problem("Фото не найдены");
        }
        public async Task<IActionResult> Copying(int? id)
        {
            if (id == null || _context.Photos == null)
            {
                return NotFound();
            }

            var photos = await _context.Photos.FirstOrDefaultAsync(p => p.Id == id);
            if (photos == null)
            {
                return NotFound();
            }
            if (!photos.isCopied)
            {
                photos.isCopied = true;
                isCopiedPhoto isCopiedPhoto = new isCopiedPhoto();
                isCopiedPhoto.photoId = photos.Id;
                _context2.Add(isCopiedPhoto);
                await _context.SaveChangesAsync();
                await _context2.SaveChangesAsync();
            }
            return RedirectToAction(nameof(GetPhotos));
        }

        // POST: api/Photos
        [Route("/photos/new")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Desription,Image,isCopied")] Photo photos)
        {
            if (ModelState.IsValid)
            {
                photos.uploadTime = DateTime.Now;
                photos.ImageSrc = photos.getSrc();
                _context.Add(photos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(GetPhotos));
            }
            return View();
        }
        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ImageSrc,uploadTime,CopyCheck,Disription")] Photo photo)
        {
            _context.Update(photo);
            await _context.SaveChangesAsync();

            //return View(photos);
            return RedirectToAction(nameof(GetPhotos));
        }

        // DELETE: api/Photos/5
        [HttpDelete, ActionName("Delete")]
        public async Task<IActionResult> DeletePhoto(int id)
        {
            if (_context.Photos == null)
            {
                return Problem("Фото не найдены");
            }
            var photo = await _context.Photos.FindAsync(id);
            if (photo == null)
            {
                return NotFound();
            }

            System.IO.File.Delete(photo.ImageSrc);
            if (photo.isCopied)
            {
                var isCopiedPhoto = _context2.isCopiedPhotos.Where(p => p.photoId == id).FirstOrDefault();
                _context2.isCopiedPhotos.Remove(isCopiedPhoto);
            }
            _context.Photos.Remove(photo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PhotoExists(int id)
        {
            return (_context.Photos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

