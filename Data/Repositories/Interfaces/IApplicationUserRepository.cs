using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public interface IApplicationUserRepository 
    {
        ApplicationUser GetByID(string ID);
        List<ApplicationUser> GetAll();
        IQueryable<ApplicationUser> Get(Expression<Func<ApplicationUser, bool>> predicate);
        void Add(ApplicationUser User);
        void Delete(ApplicationUser User);
        void Edit(string ID, ApplicationUser User);
    }
}
