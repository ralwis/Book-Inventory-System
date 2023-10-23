using System.ComponentModel.DataAnnotations;

namespace Book_Inventory_System.Models
{
    public class Book
    {
        [Key]
        public int ID { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? ISBN { get; set; }
        public DateTime PublicationDate { get; set; }
        public int ShelveId { get; set; }
        public Shelve Shelve { get; set; }
    }
}
