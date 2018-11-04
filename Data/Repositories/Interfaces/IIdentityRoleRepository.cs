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
    public interface IDentityRoleRepository 
    {
        IdentityRole GetByID(string ID);
        List<IdentityRole> GetAll();
        IQueryable<IdentityRole> Get(Expression<Func<IdentityRole, bool>> predicate);
        void Add(IdentityRole IdentityRole);
        void Delete(IdentityRole IdentityRole);
        void Edit(string ID, IdentityRole IdentityRole);
    }
}
