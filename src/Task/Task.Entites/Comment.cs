using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task.Entites
{
    public class Comment : BaseEntity
    {
        public string Body { get; set; }
        public Post Post { get; set; }
        public int PostId { get; set; }

    }
}
