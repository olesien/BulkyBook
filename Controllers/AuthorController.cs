using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BulkyBookWeb.Controllers
{
    public class AuthorController : Controller
    {
        private readonly ApplicationDbContext _db;

        public AuthorController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Author> objBookList = _db.Authors.ToList();
            return View(objBookList);
        }

        //GET
        public IActionResult Create()
        {

            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Author obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The DisplayOrder cannot exactly match the name.");
            }
            if (ModelState.IsValid)
            {
                _db.Authors.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Author created successfully!";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();

            }
            var authorFromDb = _db.Authors.Find(id);
            if (authorFromDb == null)
            {
                return NotFound();
            }
            return View(authorFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Author obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The DisplayOrder cannot exactly match the name.");
            }
            if (ModelState.IsValid)
            {
                _db.Authors.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Author edited successfully!";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();

            }
            var authorFromDb = _db.Authors.Find(id);
            if (authorFromDb == null)
            {
                return NotFound();
            }
            return View(authorFromDb);
        }

        //DELETE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Author obj)
        {
            if (obj.Id == 0) { return NotFound(); }

            _db.Authors.Attach(obj);
            _db.Authors.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Author removed successfully";
            return RedirectToAction("Index");
        }
    }
}