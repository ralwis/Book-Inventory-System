using System.ComponentModel.DataAnnotations;

namespace Book_Inventory_System.Models
{
    public class Shelve
    {
        [Key]
        public int Id { get; set; }
        public string ShelveName { get; set; }
    }
}
