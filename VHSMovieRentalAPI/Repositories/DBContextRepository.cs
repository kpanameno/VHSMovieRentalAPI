using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using VHSMovieRentalAPI.Interfaces;
using VHSMovieRentalAPI.Models;

namespace VHSMovieRentalAPI.Repositories
{
    public class DBContextRepository<T> : IRepository<T> where T: class
    {

        protected readonly VHSMovieRentalDBContext oContext;

        public DBContextRepository(VHSMovieRentalDBContext oContext)
        {
            this.oContext = oContext;
        }

        protected void Save() => oContext.SaveChanges();

        public int Count(Func<T, bool> predicate)
        {
            return oContext.Set<T>().Where(predicate).Count();
        }

        public void Create(T entity)
        {
            oContext.Add(entity);
            Save();
        }

        public void Delete(T entity)
        {
            oContext.Remove(entity);
            Save();
        }

        public IEnumerable<T> Find(Func<T, bool> predicate)
        {
            return oContext.Set<T>().Where(predicate);
        }

        public IEnumerable<T> GetAll()
        {
            return oContext.Set<T>();
        }

        public T GetById(int id)
        {
            return oContext.Set<T>().Find(id);
        }

        public void Update(T entity)
        {
            oContext.Entry(entity).State = EntityState.Modified;
            Save();
        }
    }
}
