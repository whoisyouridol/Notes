using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notes.Areas.Identity.Data;
using Notes.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Notes.Controllers
{
    [Authorize]
    public class NotesController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public NotesController(UserManager<ApplicationUser> _userManager)
        {
            this._userManager = _userManager;
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(string title, string content, IFormFile file)
        {
                var currentUser = await _userManager.GetUserAsync(User);
                var newNoteTable = new Data.Notes()
                {
                    FilePath = WorkWithFile.DEFAULT_PATH + $@"\{Path.GetRandomFileName() + file.FileName}",
                    Title = title,
                    Content = content,
                    Author = currentUser.UserName,
                    TimeAdded = DateTime.Now
                };
                await WorkWithFile.SaveFileAsync(file, newNoteTable.FilePath);
                currentUser.ListOfNotes = new List<Data.Notes> { newNoteTable };
                await _userManager.UpdateAsync(currentUser);

                return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public async Task<ActionResult> ManageNotes()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var usersNotes = _userManager.Users.Where(x => x.Id == currentUser.Id).SelectMany(x => x.ListOfNotes);

            return View(usersNotes);
        }
        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var note = await _userManager.Users
                .Where(x => x.Id == currentUser.Id)
                .SelectMany(u => u.ListOfNotes)
                .Where(x => x.NotesId == id)
                .FirstOrDefaultAsync();

            return View(note);
        }

        public async Task<ActionResult> DownloadFile(int id)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var note = await _userManager.Users
                .Where(x => x.Id == currentUser.Id)
                .SelectMany(u => u.ListOfNotes)
                .Where(x => x.NotesId == id)
                .FirstOrDefaultAsync();
            var bytes = await System.IO.File.ReadAllBytesAsync(note.FilePath);

            return File(bytes, "application/force-download", Path.GetFileName(note.FilePath));
        }
    }
}