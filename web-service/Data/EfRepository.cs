using dto;
using System;
using System.Data.Entity;
using System.Linq;

namespace web_service.Data
{
    public class EfRepository<T> : IRepository<T> where T : BaseEntity, new()
    {
        private readonly AppDbContext _context;
        private IDbSet<T> _entities;

        protected virtual IDbSet<T> Entities
        {
            get
            {
                if (_entities == null)
                {
                    _entities = _context.Set<T>();
                }
                return _entities;
            }
        }

        public virtual IQueryable<T> Table
        {
            get { return this.Entities; }
        }

        public EfRepository(AppDbContext context)
        {
            this._context = context;
        }

        public T Return(object id)
        {
            return Entities.Find(id);
        }

        public int Create(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");

            this.Entities.Add(entity);
            this._context.SaveChanges();
            return entity.Id;
        }
    }
}