using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public interface ITaskRepository : IEntityBaseRepository<Model.Task>
    {
        Model.Task GetTaskDetails(int ID);
    }
}
