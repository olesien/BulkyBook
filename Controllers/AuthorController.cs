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

        //    //GET
        //    public IActionResult Edit(int? id)
        //    {
        //        if (id == null || id == 0)
        //        {
        //            return NotFound();

        //        }
        //        var categoryFromDb = _db.Categories.Find(id);
        //        if (categoryFromDb == null)
        //        {
        //            return NotFound();
        //        }
        //        return View(categoryFromDb);
        //    }

        //    //POST
        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public IActionResult Edit(Category obj)
        //    {
        //        if (obj.Name == obj.DisplayOrder.ToString())
        //        {
        //            ModelState.AddModelError("Name", "The DisplayOrder cannot exactly match the name.");
        //        }
        //        if (ModelState.IsValid)
        //        {
        //            _db.Categories.Update(obj);
        //            _db.SaveChanges();
        //            TempData["success"] = "Category edited successfully!";
        //            return RedirectToAction("Index");
        //        }
        //        return View(obj);
        //    }

        //    //GET
        //    public IActionResult Delete(int? id)
        //    {
        //        if (id == null || id == 0)
        //        {
        //            return NotFound();

        //        }
        //        var categoryFromDb = _db.Categories.Find(id);
        //        if (categoryFromDb == null)
        //        {
        //            return NotFound();
        //        }
        //        return View(categoryFromDb);
        //    }

        //    //DELETE
        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public IActionResult Delete(Category obj)
        //    {
        //        if (obj.Id == 0) { return NotFound(); }

        //        _db.Categories.Attach(obj);
        //        var category = _db.Categories.Remove(obj);
        //        _db.SaveChanges();
        //        TempData["success"] = "Category removed successfully";
        //        return RedirectToAction("Index");
        //    }
    }
}