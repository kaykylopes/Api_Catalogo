using Api_Catalogo.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Api_Catalogo.Repository
{
    public class Repository<T> where T : class
    {
        protected AppDbContext _uof;
        public Repository(AppDbContext context)
        {
            _uof = context;
        }

        public IQueryable<T> Get()
        {
            return _uof.Set<T>().AsNoTracking();
        }

        public T GetById(Expression<Func<T, bool>> predicate)
        {
            return _uof.Set<T>().SingleOrDefault(predicate);
        }
        public void Add(T entity)
        {
            _uof.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _uof.Set<T>().Remove(entity);
        }

        public void Update(T entity)
        {
            _uof.Entry(entity).State = EntityState.Modified;
            _uof.Set<T>().Update(entity);
        }
    }
}
