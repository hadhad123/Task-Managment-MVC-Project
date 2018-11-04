using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class TaskStatus : IEntityBase
    {
        public int ID { get; set; }
        public string Status { get; set; }
        public ICollection<Task> Tasks { get; set; }
        public TaskStatus()
        {
            Tasks = new List<Task>();
        }
    }
}
