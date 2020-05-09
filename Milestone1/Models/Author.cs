using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Milestone1.Models.CustomAttributes;

namespace Milestone1.Models
{
    public class Author
    {
        public int Id { get; set; }


  
       
        [Required]
        [NotContainsDigits]  //Custom attribues
      
        public string Name { get; set; }

        [Required]
        [Remote(action: "VerifyName", controller: "Author")] //remote
        public string Surname { get; set; }

        [Required]
        public DateTime year { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public int phoneNumber { get; set; }
        [Required]
        public DateTime CreationDateBook { get; set; }
    

        public IList<AuthorBooks> AuthorBooks { get; set; }

      

    }
}
