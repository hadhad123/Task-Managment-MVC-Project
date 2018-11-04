using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Configurations
{
    public class TaskConfiguration : EntityBaseConfiguration<Model.Task>
    {
        public TaskConfiguration()
        {
            ToTable("Tasks");
            Property(t => t.Description).IsRequired().HasMaxLength(250);
            Property(t => t.UserID).IsRequired();
            Property(t => t.AssignedUserID).IsRequired();
            Property(t => t.TaskStatusID).IsRequired();
            Property(t => t.File).IsOptional();
        }
    }
}
