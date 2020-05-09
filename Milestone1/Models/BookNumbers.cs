using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Milestone1.Models
{
    public class BookNumbers
    {
        public int Id { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string Number { get; set; }

        //One-to-One
        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
