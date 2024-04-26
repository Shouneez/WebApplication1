using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class BorrowBook
    {
        [Key]
        public int BorrowID { get; set; }

        public int UserID { get; set; }

        public int BookID { get; set; }

        public DateTime BorrowDate { get; set; }

        public DateTime ReturnDate { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        public User User { get; set; }

        public Book Book { get; set; }
    }
}
