using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using static Milestone1.Controllers.ClientsController;
namespace Milestone1.Models
{
    public class Client : IValidatableObject
    {
        public int Id { get; set; }
        [StringLength(20, ErrorMessage = "Name length should be less than 20")]
        [OnlyStringAttribute]
        [Required]
        public string FirstName { get; set; }
        [StringLength(20, ErrorMessage = "LastName length should be less than 20")]
        [OnlyStringAttribute]
        [Required]
        public string LastName { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }

        public ICollection<Book> Books { get; set; }

         public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (this.FirstName =="123"|| this.LastName =="123")
                errors.Add(new ValidationResult("Invalid last name "));

            return errors;
        }
        
    }
}
