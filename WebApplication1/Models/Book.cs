using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Book
    {
        public int bookId { get; set; }
        [Key]
        public String title { get; set; }
        public String author { get; set; }
        public String isbn { get; set; } 
        public String imageLink { get; set; }
        public int rackNumber { get; set; }
        public int availableCopies{ get; set; }
        public int totalCopies { get; set; }
    }

}

