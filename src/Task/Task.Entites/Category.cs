using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task.Entites
{
    public class Category : BaseEntity
    {
        [Required]
        [StringLength(150, ErrorMessage = "Name :  Length should not exceed 150 characters")]
        public string Name { get; set; }
        public virtual IList<Post> Posts { get; set; }
    }
}
