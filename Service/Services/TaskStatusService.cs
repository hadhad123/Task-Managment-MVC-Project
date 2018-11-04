using Data.Infrastructure;
using Data.Repositories;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class TaskStatusService : ITaskStatusService
    {
        private readonly ITaskStatusRepository TaskStatusRepository;
        private readonly IUnitOfWork unitOfWork;

        public TaskStatusService(ITaskStatusRepository TaskStatusRepository, IUnitOfWork unitOfWork)
        {
            this.TaskStatusRepository = TaskStatusRepository;
            this.unitOfWork = unitOfWork;
        }

        public List<Model.TaskStatus> GetTaskStatuses()
        {
            List<Model.TaskStatus> TaskStatuss = TaskStatusRepository.GetAll().ToList();
            return TaskStatuss;
        }

        public Model.TaskStatus GetTaskStatusByID(int ID)
        {
            Model.TaskStatus TaskStatus = TaskStatusRepository.GetByID(ID);
            return TaskStatus;
        }

        public void CreateTaskStatus(Model.TaskStatus TaskStatus)
        {
            Model.TaskStatus Exists = TaskStatusRepository.Get(x => x.Status == TaskStatus.Status).FirstOrDefault();
            if (Exists != null)
            {
                throw new Exception("Status already Exists!");
            }

            TaskStatusRepository.Add(TaskStatus);
            SaveTaskStatus();
        }

        public void SaveTaskStatus()
        {
            unitOfWork.Commit();
        }


    }
}
