using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public interface IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {
        IQueryable<T> GetAllWithIncludes(List<string> includes);
        IQueryable<T> All { get; }
        IQueryable<T> GetAll();
        T GetByID(int id);
        IQueryable<T> Get(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Delete(T entity);
        void Edit(int ID,T entity);
    }
}
