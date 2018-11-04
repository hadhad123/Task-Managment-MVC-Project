using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Comment : IEntityBase
    {
        public int ID { get; set; }
        public string CommentDescription { get; set; }
        public string UserID { get; set; }
        public int TaskID { get; set; }
        public int? ParentCommentID { get; set; }
        public DateTime CreationDate { get; set; }
        public ApplicationUser User { get; set; }
        public Task Task { get; set; }
        public Comment ParentComment { get; set; }
        public List<Comment> Replies { get; set; }
        public Comment()
        {
            Replies = new List<Comment>();
        }
    }
}
