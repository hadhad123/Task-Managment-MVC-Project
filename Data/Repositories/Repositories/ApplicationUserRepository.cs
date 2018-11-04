using IdentityProject.Models;
using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class ApplicationUserRepository: IApplicationUserRepository
    {
       private ApplicationDbContext DBContext;
        public ApplicationUserRepository()
        {
            DBContext = new ApplicationDbContext();
        }
       public ApplicationUser GetByID(string ID)
       {
            ApplicationUser User = DBContext.Users.Where(x => x.Id == ID).FirstOrDefault();
            return User;
       }
       public List<ApplicationUser> GetAll()
       {
            List<ApplicationUser> Users = DBContext.Users.ToList();
            return Users;
       }
       public IQueryable<ApplicationUser> Get(Expression<Func<ApplicationUser, bool>> predicate)
       {
            return DBContext.Users.Where(predicate);
       }
       public void Add(ApplicationUser User)
       {
            DBContext.Users.Add(User);
       }
       public void Delete(ApplicationUser User)
       {
            DBContext.Users.Remove(User);
       }
       public void Edit(string ID, ApplicationUser User)
       {
           
       }
    }
}
