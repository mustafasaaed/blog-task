using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task.Entites
{
    public class Post : BaseEntity
    {
        [Required(ErrorMessage = "Title: Field is required")]
        [StringLength(500, ErrorMessage = "Title: Length should not exceed 500 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description: Field is required")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Image Url Is Required")]
        [Display(Name = "Image Url")]
        public string ImageUrl { get; set; }
        public Category Category { get; set; }
    }
}
