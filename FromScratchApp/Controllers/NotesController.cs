using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FromScratchApp.Data;
using FromScratchApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FromScratchApp.Controllers
{
    [Authorize]
    public class NotesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        private UserManager<ApplicationUser> _userManager { get; set; }

        public NotesController(ApplicationDbContext context, 
            UserManager<ApplicationUser> userManager,
            IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index()
        {
            string userId = _userManager.GetUserId(User);
            return View(_context.Notes.Where(u => u.UserId == userId).ToList());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(NoteCreateViewModel note)
        {
            string userId = _userManager.GetUserId(User);

            note.UserId = userId;

            if (ModelState.IsValid)
            {
                var category = _context.Category.Where(c => c.Name == note.Category).FirstOrDefault();
                List<NoteFiles> uniqueFileNames = new List<NoteFiles>();
                string filePath = null;
                if (note.Files != null && note.Files.Count > 0)
                {
                    foreach (IFormFile file in note.Files)
                    {
                        string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
                        string uniqueFileName = Guid.NewGuid().ToString() + "_" + System.IO.Path.GetFileName(file.FileName);
                        uniqueFileNames.Add(new NoteFiles() { FileName = uniqueFileName } );
                        filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        file.CopyTo(new FileStream(filePath, FileMode.Create));
                    }
                }
                Note newNote = new Note
                {
                    UserId = note.UserId,
                    Category = note.Category,
                    Title = note.Title,
                    Description = note.Description,
                    FilePaths = uniqueFileNames
                };

                if (category == null) 
                {
                    ModelState.AddModelError("Category", "Category is not allowed");
                    return View(note);
                }

                _context.Add(newNote);
                _context.SaveChanges();
            }
            return View(note);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            Note note;
            if(id != null)
            {
                note = _context.Notes.Where(n => n.Id == id).FirstOrDefault();
                return View(note);
            }
            return View();
        }

        [HttpPost]
        public IActionResult Edit(Note note)
        {
            _context.Update(note);
            _context.SaveChanges();

            return View(note);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            Note note;
            if (id != null)
            {
                note = _context.Notes.Where(n => n.Id == id).FirstOrDefault();
                return View(note);
            }
            return View();
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var note = _context.Notes.Where(n => n.Id == id).FirstOrDefault();
            _context.Remove(note);
            _context.SaveChanges();

            return Redirect("/Notes/Index");
        }


        public IActionResult Details(int id)
        {
            var note = _context.Notes.Include(n => n.FilePaths).Where(n => n.Id == id).FirstOrDefault();

            return View(note);
        }


    }
}