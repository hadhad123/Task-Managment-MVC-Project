using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Model.ViewModels
{
    public class TaskViewModel
    {
        public int ID { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int TaskStatusID { get; set; }
        [Required]
        public string UserID { get; set; }
        [Required]
        public string AssignedUserID { get; set; }
        public string File { get; set; }
        public TaskStatus TaskStatus { get; set; }
        public List<Comment> Comments { get; set; }
        public ApplicationUser User { get; set; }
        public ApplicationUser AssignedUser { get; set; }


        public TaskViewModel()
        {
            Comments = new List<Comment>();
        }
    }
}