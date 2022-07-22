using System.ComponentModel.DataAnnotations;

namespace Books.Domain
{
    public class Book
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public int? NumberOfCopies { get; set; }
    }
}
