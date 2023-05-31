using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using System.Linq;
using System.Net;

namespace BulkyBookWeb.Controllers
{
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _db;

        public BookController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Book> objBookList = _db.Books
           .Include(x => x.Category)
           .Include(x => x.Authors)
           .ThenInclude(a => a.Author)
           .ToList();


            return View(objBookList);
        }

        ////GET
        public IActionResult Create()
        {
            IEnumerable<Category> objCategoryList = _db.Categories;
            IEnumerable<Author> objAuthorList = _db.Authors;
            ViewData["Categories"] = objCategoryList;
            ViewData["Authors"] = objAuthorList;
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Book obj)
        {
            ModelState.Remove("Category");
            var categoryFromDb = _db.Categories.Find(obj.CategoryId);

            if (categoryFromDb == null)
            {
                ModelState.AddModelError("CategoryId", "This ID does not exist");
            }

            if (ModelState.IsValid)
            {
                var AuthorIDs = obj.SelectedAuthors;
                ModelState.Remove("AuthorIDs");
                _db.Books.Add(obj);
                _db.SaveChanges();

                int bookId = obj.Id;

                List<BookAuthors> bookAuthorMappings = AuthorIDs.Select(authorId => new BookAuthors
                {
                    BookId = bookId,
                    AuthorId = authorId
                }).ToList();

                _db.BookAuthors.AddRange(bookAuthorMappings);
                _db.SaveChanges();
                TempData["success"] = "Book created successfully!";
                return RedirectToAction("Index");
            }
            IEnumerable<Category> objCategoryList = _db.Categories;
            ViewData["Categories"] = objCategoryList;
            return View(obj);
        }

        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();

            }

            var bookFromDb = _db.Books.Include(x => x.Category)
            .Include(x => x.Authors)
            .ThenInclude(a => a.Author).FirstOrDefault(book => book.Id == id);
            if (bookFromDb == null)
            {
                return NotFound();
            }
            bookFromDb.SelectedAuthors = bookFromDb.Authors.Select(author => author.Id).ToArray();
            IEnumerable<Category> objCategoryList = _db.Categories;
            IEnumerable<Author> objAuthorList = _db.Authors;
            ViewData["Categories"] = objCategoryList;
            ViewData["Authors"] = objAuthorList;
            return View(bookFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Book obj)
        {
            ModelState.Remove("Category");
            var categoryFromDb = _db.Categories.Find(obj.CategoryId);

            if (categoryFromDb == null)
            {
                ModelState.AddModelError("CategoryId", "This ID does not exist");
            }

            if (ModelState.IsValid)
            {
                var AuthorIDs = obj.SelectedAuthors;
                ModelState.Remove("AuthorIDs");
                _db.Books.Update(obj);
                _db.SaveChanges();

                int bookId = obj.Id;

                List<BookAuthors> bookAuthorMappings = AuthorIDs.Select(authorId => new BookAuthors
                {
                    BookId = bookId,
                    AuthorId = authorId
                }).ToList();

                //Get current bookauthors
                List<BookAuthors> existingBookAuthors = _db.BookAuthors.ToList();

                //Find if any are in bookauthors but not in the mapping, remove
                List<BookAuthors> authorsToRemove = existingBookAuthors
    .Where(existingAuthor => !bookAuthorMappings.Any(mapping => mapping.AuthorId == existingAuthor.AuthorId))
    .ToList();
                _db.BookAuthors.RemoveRange(authorsToRemove);

                //FInd if any are in the mapping, but not in bookauthors, add
                List<BookAuthors> authorsToAdd = bookAuthorMappings
    .Where(mapping => !existingBookAuthors.Any(existingAuthor => existingAuthor.AuthorId == mapping.AuthorId))
    .ToList();
                _db.BookAuthors.AddRange(authorsToAdd);

                //Save db changes
                _db.SaveChanges();
                TempData["success"] = "Book updated successfully!";
                return RedirectToAction("Index");
            }
            IEnumerable<Category> objCategoryList = _db.Categories;
            ViewData["Categories"] = objCategoryList;
            return View(obj); ;
        }

        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();

            }

            var bookFromDb = _db.Books.Include(x => x.Category)
            .Include(x => x.Authors)
            .ThenInclude(a => a.Author).FirstOrDefault(book => book.Id == id);
            if (bookFromDb == null)
            {
                return NotFound();
            }
            bookFromDb.SelectedAuthors = bookFromDb.Authors.Select(author => author.Id).ToArray();
            IEnumerable<Category> objCategoryList = _db.Categories;
            IEnumerable<Author> objAuthorList = _db.Authors;
            ViewData["Categories"] = objCategoryList;
            ViewData["Authors"] = objAuthorList;
            return View(bookFromDb);
        }

        //DELETE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Book obj)
        {
            if (obj.Id == 0) { return NotFound(); }

            _db.Books.Attach(obj);
            var book = _db.Books.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Book removed successfully";
            return RedirectToAction("Index");
        }
    }
}