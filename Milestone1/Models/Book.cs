using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Milestone1.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        [Display(Name = "Genre")]
        public string Type { get; set; }
        [Required]
        [Display(Name = "Photo")]
        public string Poster { get; set; }

        [Required]
        public string Description { get; set; }

        //One-to-Many
        public int ClientId { get; set; }
        public Client Client { get; set; }

        //One-to-One
        public BookNumbers BookNumber { get; set; }

        //Many-to-Many
        public IList<AuthorBooks> AuthorBooks { get; set; }
    }
}
