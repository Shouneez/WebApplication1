using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Dtoos
{
    public class CreateBookDtoo
    {
        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        [Required]
        [StringLength(255)]
        public string Author { get; set; }

        [Required]
        [StringLength(13)]
        public string ISBN { get; set; }

        public int RackNumber { get; set; }

        public int AvailableCopies { get; set; }

        public int TotalCopies { get; set; }
    }
}
