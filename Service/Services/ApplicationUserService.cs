using Data.Infrastructure;
using Data.Repositories;
using IdentityProject.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Model;
using Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Service
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly IApplicationUserRepository UserRepository;
        private readonly IDentityRoleRepository RoleRepository;
        private readonly IEncryptionService EncryptionService;
        private readonly IUnitOfWork unitOfWork;
        private readonly ApplicationDbContext applicationDbContext;

        public ApplicationUserService(IApplicationUserRepository UserRepository, IDentityRoleRepository RoleRepository, IEncryptionService EncryptionService, IUnitOfWork unitOfWork)
        {
            this.UserRepository = UserRepository;
            this.RoleRepository = RoleRepository;
            this.EncryptionService = EncryptionService;
            this.unitOfWork = unitOfWork;

            applicationDbContext = new ApplicationDbContext();
        }

        public List<UserViewModel> GetUsers()
        {
            List<ApplicationUser> ApplicationUsers = UserRepository.GetAll().ToList();

            List<UserViewModel> ApplicationUsersView = new List<UserViewModel>();
            foreach (ApplicationUser ApplicationUser in ApplicationUsers)
            {
                UserViewModel MappedApplicationUser = new UserViewModel()
                {
                    ID = ApplicationUser.Id,
                    UserName = ApplicationUser.UserName,
                    Email = ApplicationUser.Email,
                    HashedPassword = ApplicationUser.PasswordHash,
                    Active = ApplicationUser.Active,
                };

                if (ApplicationUser.Roles.Count()>0)
                {
                    string RoleID = ApplicationUser.Roles.First().RoleId;
                    MappedApplicationUser.RoleName = RoleRepository.GetByID(RoleID).Name;
                }

                ApplicationUsersView.Add(MappedApplicationUser);
            }
            return ApplicationUsersView;
        }

        public UserViewModel GetUserByID(string ID)
        {
            ApplicationUser ApplicationUser = UserRepository.GetByID(ID);
            UserViewModel ApplicationUserView = new UserViewModel()
            {
                ID = ApplicationUser.Id,
                UserName = ApplicationUser.UserName,
                Email = ApplicationUser.Email,
                HashedPassword = ApplicationUser.PasswordHash,
                Active = ApplicationUser.Active,
               // Tasks = ApplicationUser.Tasks
            };
            return ApplicationUserView;
        }

        public void CreateUser(UserViewModel ApplicationUserView)
        {
            ApplicationUser Exists = UserRepository.Get(x => x.UserName == ApplicationUserView.UserName && x.Email == ApplicationUserView.Email).FirstOrDefault();
            if (Exists != null)
            {
                throw new Exception("ApplicationUsername & Email already in use");
            }
            string SecurityStamp = EncryptionService.CreateSalt();
            string HassedPassword = EncryptionService.EncryptPassword(ApplicationUserView.HashedPassword, SecurityStamp);
            ApplicationUserView.HashedPassword = HassedPassword;
            ApplicationUserView.Salt = SecurityStamp;

            ApplicationUser ApplicationUser = new ApplicationUser()
            {
                UserName = ApplicationUserView.UserName,
                Email = ApplicationUserView.Email,
                PasswordHash = ApplicationUserView.HashedPassword,
                SecurityStamp = ApplicationUserView.Salt,
                Active = ApplicationUserView.Active
            };

            UserRepository.Add(ApplicationUser);
            SaveUser();
        }

        public void EditUser(UserViewModel ApplicationUserView)
        {
            ApplicationUser ApplicationUser = new ApplicationUser()
            {
                Id = ApplicationUserView.ID,
                UserName = ApplicationUserView.UserName,
                Email = ApplicationUserView.Email,
                PasswordHash = ApplicationUserView.HashedPassword,
                SecurityStamp = ApplicationUserView.Salt,
                Active = ApplicationUserView.Active
            }; 
            //ApplicationUser OldApplicationUserData = UserRepository.GetByID(ApplicationUser.Id);
            //ApplicationUser.SecurityStamp = OldApplicationUserData.SecurityStamp;
            //string NewHassedPassword = EncryptionService.EncryptPassword(ApplicationUser.PasswordHash, ApplicationUser.SecurityStamp);
            ////check if password has been changed 
            //if (OldApplicationUserData.PasswordHash != NewHassedPassword)
            //{
            //    string salt = EncryptionService.CreateSalt();
            //    string HassedPassword = EncryptionService.EncryptPassword(ApplicationUser.PasswordHash, salt);
            //    ApplicationUser.PasswordHash = HassedPassword;
            //    ApplicationUser.SecurityStamp = salt;
            //}
            ////ApplicationUser.Role = null;
            ////ApplicationUser.Tasks = null;

            UserRepository.Edit(ApplicationUser.Id, ApplicationUser);
            SaveUser();
        }

        public void DeactivateUser(string ID)
        {
            ApplicationUser ApplicationUser = UserRepository.GetByID(ID);
            ApplicationUser.Active = !ApplicationUser.Active;
            //ApplicationUser.Role = null;
            //ApplicationUser.Tasks = null;
            UserRepository.Edit(ID, ApplicationUser);
            SaveUser();
        }
        public void SaveUser()
        {
            unitOfWork.Commit();
        }


    }
}
