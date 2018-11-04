using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Task : IEntityBase
    {
        public Task()
        {
            Comments = new List<Comment>();
        }
        public int ID { get; set; }
        public string Description { get; set; }
        public int TaskStatusID { get; set; }
        public string UserID { get; set; }
        public string AssignedUserID { get; set; }
        public string File { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public TaskStatus TaskStatus { get; set; }
        public ApplicationUser User { get; set; }
        public ApplicationUser AssignedUser { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
