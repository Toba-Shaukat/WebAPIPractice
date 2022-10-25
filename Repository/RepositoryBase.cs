using Contracts;
using Entities;
using Entities.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Repository
{
    public abstract class RepositoryBase<TModel> : IRepositoryBase<TModel> where TModel : class
    {
        private readonly RepositoryContext _context;

        public RepositoryBase(RepositoryContext context)
        {
            _context = context;
        }

        public void Add(TModel model)
        {
            //_context.Pages.Add(model)
            _context.Set<TModel>().Add(model);
        }

        //IEnumerable: processing will happen in the memory
        //IQueryable: processing wil happen in the database
        public IQueryable<TModel> GetAll(bool trackChanges)
        {
            var data = _context.Set<TModel>().AsQueryable();

            if (trackChanges)
            {
                return data;
            }

            return data.AsNoTracking();
        }

        public TModel GetById(int id)
        {
            return _context.Set<TModel>().Find(id);
        }

        public void Remove(TModel model)
        {
            _context.Set<TModel>().Remove(model);
        }

        public void RemoveById(int id)
        {
            TModel model = GetById(id);
            if (model != null)
                Remove(model);
        }

        public void Update(TModel model)
        {
            _context.Set<TModel>().Update(model);
        }
    }
}
