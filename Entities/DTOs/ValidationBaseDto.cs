using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public abstract record ValidationBaseDto
    {
        [Required(ErrorMessage = "Title is required")]
        [MaxLength(50, ErrorMessage = "Name can't exceed 50 characters")]
        [MinLength(2, ErrorMessage = "Name must be at least 2 characters long")]
        public  string Title { get; set; }
        [Required(ErrorMessage = "Price is required")]
        [Range(1,10000, ErrorMessage = "Price must be between 1 and 100000")]
        public decimal Price { get; set; }
    }
}
