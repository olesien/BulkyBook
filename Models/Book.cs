﻿using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BulkyBookWeb.Models
{
    public class Book
    {

        [BindProperty]
        [NotMapped]
        public int[] SelectedAuthors { get; set; }
        public Book()
        {
            this.Category = Category;
            this.Authors = new List<BookAuthors>();
        }

        [Key]
        public int Id { get; set; }
        [Required]

        public string Name { get; set; }

        public DateTime CreatedDateTime { get; set; } = DateTime.Now;

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public ICollection<BookAuthors> Authors { get; set; }

    }
}
