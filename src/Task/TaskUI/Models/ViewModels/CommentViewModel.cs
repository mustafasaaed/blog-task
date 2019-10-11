using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskUI.Models.ViewModels
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public PostViewModel Post { get; set; }
        public int PostId { get; set; }
    }
}