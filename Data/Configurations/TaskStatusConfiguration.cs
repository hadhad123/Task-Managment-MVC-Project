using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Configurations
{
    public class TaskStatusConfiguration : EntityBaseConfiguration<Model.TaskStatus>
    {
        public TaskStatusConfiguration()
        {
            Property(ts => ts.Status).IsRequired().HasMaxLength(50);
        }
    }
}
