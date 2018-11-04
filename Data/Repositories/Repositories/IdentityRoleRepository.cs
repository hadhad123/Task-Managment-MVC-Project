using Data.Infrastructure;
using IdentityProject.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class IdentityRoleRepository : IDentityRoleRepository
    {

        private ApplicationDbContext DBContext;
        public IdentityRoleRepository()
        {
            DBContext = new ApplicationDbContext();
        }
        public IdentityRole GetByID(string ID)
        {
            IdentityRole IdentityRole = DBContext.Roles.Where(x => x.Id == ID).FirstOrDefault();
            return IdentityRole;
        }
        public List<IdentityRole> GetAll()
        {
            List<IdentityRole> Roles = DBContext.Roles.ToList();
            return Roles;
        }
        public IQueryable<IdentityRole> Get(Expression<Func<IdentityRole, bool>> predicate)
        {
            return DBContext.Roles.Where(predicate);
        }
        public void Add(IdentityRole IdentityRole)
        {
            DBContext.Roles.Add(IdentityRole);
        }
        public void Delete(IdentityRole IdentityRole)
        {
            DBContext.Roles.Remove(IdentityRole);
        }
        public void Edit(string ID, IdentityRole IdentityRole)
        {

        }
    }
}
