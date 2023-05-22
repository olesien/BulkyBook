﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulkyBookWeb.Models
{
    public class Category
    {
        public Category()
        {
            this.Books = new List<Book>();
        }
        [Key]
        public int Id { get; set; }
        [Required]

        public string Name { get; set; }

        [DisplayName("Display Order")]
        [Range(1, 100, ErrorMessage = "Display Order must be between and and 100 only!")]
        public int DisplayOrder { get; set; }


        public DateTime CreatedDateTime { get; set; } = DateTime.Now;

        public ICollection<Book> Books { get; set; }
    }
}