using Data.Infrastructure;
using Data.Repositories;
using Microsoft.AspNet.Identity.EntityFramework;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class RoleService : IRoleService
    {
        private readonly IDentityRoleRepository RoleRepository;
        private readonly IUnitOfWork unitOfWork;

        public RoleService(IDentityRoleRepository RoleRepository, IUnitOfWork unitOfWork)
        {
            this.RoleRepository = RoleRepository;
            this.unitOfWork = unitOfWork;
        }

        public List<IdentityRole> GetRoles()
        {
            List<IdentityRole> Roles = RoleRepository.GetAll().ToList();
            return Roles;
        }

        public IdentityRole GetRoleByID(string ID)
        {
            IdentityRole Role = RoleRepository.GetByID(ID);
            return Role;
        }

        public void CreateRole(IdentityRole Role)
        {
            IdentityRole Exists = RoleRepository.Get(x => x.Name == Role.Name).FirstOrDefault();
            if (Exists != null)
            {
                throw new Exception("Role already exists");
            }

            RoleRepository.Add(Role);
            SaveRole();
        }

        public void EditRole(IdentityRole Role)
        {
           /// RoleRepository.Edit(Role.Id,Role);
           // SaveRole();
        }

        public void SaveRole()
        {
            unitOfWork.Commit();
        }


    }
}
