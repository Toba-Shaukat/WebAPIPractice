using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryBase<TModel> where TModel : class
    {
        void Add(TModel model);
        void Update(TModel model);
        IQueryable<TModel> GetAll(bool trackChanges);
        IQueryable<TModel> FindByCondition(Expression<Func<TModel, bool>> expression, bool trackChanges);
        TModel GetById(int id);
        void Remove(TModel model);
        void RemoveById(int id);
    }
}
