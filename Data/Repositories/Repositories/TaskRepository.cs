using Data.Infrastructure;
using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class TaskRepository : EntityBaseRepository<Model.Task>, ITaskRepository
    {
        public TaskRepository(IDbFactory dbFactory)
            : base(dbFactory) { }

        public Model.Task GetTaskDetails(int ID)
        {
            Model.Task Task = DbContext.Tasks.Include("User").Include("TaskStatus").Include("AssignedUser").Include("Comments")
                .Where(x=>x.ID == ID).FirstOrDefault();
            return Task;
        }
    }
}
