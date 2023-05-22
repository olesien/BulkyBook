using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BulkyBookWeb.Models
{
    public class BookAuthors
    {
        public BookAuthors()
        {
            this.Book = Book;
            this.Author = Author;
        }
        [Key]
        public int Id { get; set; }
        [Required]

        [ForeignKey("Book")]
        public int BookId { get; set; }

        [ForeignKey("Author")]
        public int AuthorId { get; set; }

        public virtual Book Book { get; set; } = null;
        public virtual Author Author { get; set; } = null;
    }
}
