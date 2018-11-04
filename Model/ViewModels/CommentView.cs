using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModels
{
    public class CommentView
    {
        public string CommentDescription { get; set; }
        public int? ParentID { get; set; }
        public int TaskID { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
