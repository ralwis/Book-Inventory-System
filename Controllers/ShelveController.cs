using Book_Inventory_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Book_Inventory_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShelveController : Controller
    {
        private readonly DataContext dc;

        public ShelveController(DataContext dc)
        {
            this.dc = dc;
        }

        [HttpPost]
        public IActionResult AddShelve([FromBody] Shelve shelveName)
        {
            if (shelveName == null)
            {
                return BadRequest("Invalid data");
            }

            dc.Shelves.Add(shelveName);
            dc.SaveChanges();

            return Ok(shelveName);
        }

        [HttpGet]
        public IActionResult GetShelve()
        {
            var shelves = dc.Shelves.ToList();
            return Ok(shelves);
        }

        [HttpGet("{id}")]
        public IActionResult GetShelve(int id)
        {
            var shelve = dc.Shelves.Find(id);
            if (shelve == null)
            {
                return NotFound();
            }
            return Ok(shelve);
        }
    }
}
