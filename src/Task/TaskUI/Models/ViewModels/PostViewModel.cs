using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskUI.Models.ViewModels
{
    public class PostViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title: Field is required")]
        [StringLength(500, ErrorMessage = "Title: Length should not exceed 500 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description: Field is required")]
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        [Required(ErrorMessage = "Image Url Is Required")]
        [Display(Name = "Photo")]
        public HttpPostedFileBase Photo { get; set; }
        public CategoryViewModel Category { get; set; }
        public int CategoryId { get; set; }


    }
}