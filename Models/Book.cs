using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BulkyBookWeb.Models
{
    public class Book
    {
        public Book()
        {
            this.Category = Category;
        }
        [Key]
        public int Id { get; set; }
        [Required]

        public string Name { get; set; }

        public DateTime CreatedDateTime { get; set; } = DateTime.Now;

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        [ForeignKey("Author")]
        public int AuthorId { get; set; }
        public virtual Author Author { get; set; }
    }
}
