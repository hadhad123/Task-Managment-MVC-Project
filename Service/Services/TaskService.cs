using Data.Infrastructure;
using Data.Repositories;
using Model;
using Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Service
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository TaskRepository;
        private readonly ITaskStatusRepository TaskStatusRepository;
        private readonly ICommentRepository CommentRepository;
        private readonly IUnitOfWork unitOfWork;

        public TaskService(ITaskRepository TaskRepository, ITaskStatusRepository TaskStatusRepository, ICommentRepository CommentRepository,IUnitOfWork unitOfWork)
        {
            this.TaskRepository = TaskRepository;
            this.TaskStatusRepository = TaskStatusRepository;
            this.CommentRepository = CommentRepository;
            this.unitOfWork = unitOfWork;
        }

        public List<TaskViewModel> GetTasks()
        {
            List<string> Includes = new List<string>();
            Includes.Add("User");
            Includes.Add("AssignedUser");
            Includes.Add("TaskStatus");
            Includes.Add("Comments");
            Includes.Add("Comments.Replies");

            List<Model.Task> Tasks = TaskRepository.GetAllWithIncludes(Includes).ToList();
            List<TaskViewModel> TaskViews = new List<TaskViewModel>();
         
            foreach(Model.Task tsk in Tasks)
            {
                TaskViewModel NewTaskViews = new TaskViewModel();
                NewTaskViews.ID = tsk.ID;
                NewTaskViews.Description = tsk.Description;
                NewTaskViews.TaskStatusID = tsk.TaskStatusID;
                NewTaskViews.UserID = tsk.UserID;
                NewTaskViews.AssignedUserID = tsk.AssignedUserID;
                NewTaskViews.TaskStatus = tsk.TaskStatus;
                NewTaskViews.Comments = tsk.Comments.Where(x => x.ParentCommentID == null).ToList();
                NewTaskViews.User = tsk.User;
                NewTaskViews.AssignedUser = tsk.AssignedUser;
                NewTaskViews.File = tsk.File;
                TaskViews.Add(NewTaskViews);
            }
            return TaskViews;
        }

        public TaskViewModel GetTaskByID(int ID)
        {
            Model.Task Task = TaskRepository.GetTaskDetails(ID);
            TaskViewModel TaskViewModel = new TaskViewModel()
            {
                ID = Task.ID,
                Description = Task.Description, 
                TaskStatusID = Task.TaskStatusID,
                TaskStatus = Task.TaskStatus,
                UserID = Task.UserID,
                User = Task.User,
                AssignedUserID = Task.AssignedUserID,
                AssignedUser = Task.AssignedUser,
                File = Task.File,
                Comments = Task.Comments
            };
            return TaskViewModel;
        }

        public void CreateTask(TaskViewModel TaskView, string UserId)
        {
            TaskView.UserID = UserId; //created by
            Model.Task Task = new Model.Task()
            {
                Description = TaskView.Description,
                TaskStatusID = TaskView.TaskStatusID,
                TaskStatus = TaskView.TaskStatus,
                UserID = TaskView.UserID,
                AssignedUserID = TaskView.AssignedUserID,
                File = TaskView.File,
                Comments = TaskView.Comments,
                CreationDate = DateTime.Now,
                LastUpdatedDate = DateTime.Now
            };
            TaskRepository.Add(Task);
            SaveTask();
        }

        public void EditTask(TaskViewModel TaskView)
        {
            TaskView.User = null;
            TaskView.AssignedUser = null;
            TaskView.TaskStatus = null;

            Model.Task OldTask = TaskRepository.GetByID(TaskView.ID);
            if (OldTask.File != null && TaskView.File == null)
                TaskView.File = OldTask.File;

            Model.Task Task = new Model.Task()
            {
                ID = TaskView.ID,
                Description = TaskView.Description,
                TaskStatusID = TaskView.TaskStatusID,
                TaskStatus = TaskView.TaskStatus,
                UserID = TaskView.UserID,
                User = TaskView.User,
                AssignedUserID = TaskView.AssignedUserID,
                AssignedUser = TaskView.AssignedUser,
                File = TaskView.File,
                Comments = TaskView.Comments
            };

            TaskRepository.Edit(Task.ID, Task);
            SaveTask();
        }

        public void DeleteTask(int ID)
        {
            Model.Task Task = TaskRepository.GetByID(ID);
            TaskRepository.Delete(Task);
            SaveTask();
        }
        public void SaveTask()
        {
            unitOfWork.Commit();
        }

        public List<TaskViewModel> CloseTask(int ID)
        {
            Model.Task Task = TaskRepository.GetByID(ID);
            Task.User = null;
            Task.AssignedUser = null;
            Task.TaskStatus = null;
            Task.TaskStatusID = TaskStatusRepository.Get(x => x.Status == "Closed").FirstOrDefault().ID;
            TaskRepository.Edit(Task.ID, Task);
            SaveTask();

            List<TaskViewModel> AllTasks = GetTasks();
            return AllTasks;
        }


        public List<TaskViewModel> AddComment(CommentView NewComment, string UserID)
        {
            Comment Comment = new Comment()
            {
                CommentDescription = NewComment.CommentDescription,
                UserID = UserID, //current logged in user
                ParentCommentID = NewComment.ParentID==0?null : NewComment.ParentID,
                CreationDate = DateTime.Now,
                TaskID= NewComment.TaskID

            };
            CommentRepository.Add(Comment);
            SaveTask();
            List<TaskViewModel> Tasks = GetTasks();
            return Tasks;
        }

    }
}
