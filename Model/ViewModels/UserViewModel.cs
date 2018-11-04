using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModels
{
    public class UserViewModel
    {
        public UserViewModel()
        {
            Roles = new List<IdentityRole>();
        }
        public string ID { get; set; }
        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string HashedPassword { get; set; }

        public string Salt { get; set; }

        [Required]
        public int RoleID { get; set; }

        [Required]
        public bool Active { get; set; }
        public string PhoneNumber { get; set; }

        public string RoleName { get; set; }
        public virtual ICollection<IdentityRole> Roles { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
