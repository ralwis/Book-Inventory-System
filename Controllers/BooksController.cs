using Book_Inventory_System.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Book_Inventory_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BooksController : ControllerBase
    {
        private readonly DataContext dc;

        public BooksController(DataContext dc)
        {
            this.dc = dc;
        }

        [HttpGet]
        public IActionResult GetAllBooks()
        {
            var books = dc.Books.ToList();
            return Ok(books);
        }

        [HttpGet("byShelve/{shelveId}")]
        public IActionResult GetBooksByShelve(int shelveId)
        {
            var books = dc.Books.Include(b => b.Shelve).Where(b => b.ShelveId == shelveId).ToList();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public IActionResult GetBookById(int id)
        {
            var book = dc.Books.Include(b => b.Shelve).FirstOrDefault(b => b.ID == id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpPost]
        public IActionResult AddNewBook([FromBody] Book book)
        {
            if (book == null)
            {
                return BadRequest("Invalid data");
            }

            var shelve = dc.Shelves.Find(book.ShelveId);
            if (shelve == null)
            {
                return BadRequest("Shelve ID does not exist.");
            }

            book.Shelve = shelve;

            dc.Books.Add(book);
            dc.SaveChanges();
            return Ok(dc.Books.ToList());
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Book book)
        {
            var existingBook = dc.Books.Find(id);
            if (existingBook == null)
            {
                return NotFound();
            }

            existingBook.Title = book.Title;
            existingBook.Author = book.Author;
            existingBook.ISBN = book.ISBN;
            existingBook.PublicationDate = book.PublicationDate;

            dc.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var book = dc.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }

            dc.Books.Remove(book);
            dc.SaveChanges();

            return NoContent();
        }
    }
}
