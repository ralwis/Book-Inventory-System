using Book_Inventory_System.Models;
using Microsoft.AspNetCore.Mvc;

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
    }
}
