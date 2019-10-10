using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskUI.Models.ViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(150, ErrorMessage = "Name :  Length should not exceed 150 characters")]
        public string Name { get; set; }
        public  List<PostViewModel> Posts { get; set; }


        public CategoryViewModel()
        {
            Posts = new List<PostViewModel>();
        }
    }
}