using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface ITaskStatusService
    {
        List<Model.TaskStatus> GetTaskStatuses();
        Model.TaskStatus GetTaskStatusByID(int ID);
        void CreateTaskStatus(Model.TaskStatus TaskStatus);
        void SaveTaskStatus();
    }
}
