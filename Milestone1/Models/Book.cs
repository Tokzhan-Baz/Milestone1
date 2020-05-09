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
        public string Type { get; set; }

        //One-to-Many
        public int ClientId { get; set; }
        public Client Client { get; set; }

        //One-to-One
        public BookNumbers CarNumber { get; set; }

        //Many-to-Many
        public IList<AuthorBooks> AuthorBooks { get; set; }
    }
}
