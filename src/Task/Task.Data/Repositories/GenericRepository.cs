using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Task.Core.Interfaces;
using Task.Data.Interfaces;
using Task.Entites;

namespace Task.Data.Repositories
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly DbSet<TEntity> _entities;
        private readonly ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _entities = dbContext.Set<TEntity>();
            _context = dbContext;
        }

        public TEntity GetById(object id)
        {
            var entityId = Convert.ToInt32(id);
            return _entities.Where(e => e.IsDeleted == false && e.Id == entityId).FirstOrDefault();
        }

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = _entities;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public void Insert(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _entities.Add(entity);
        }

        public void Delete(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            entity.IsDeleted = true;
        }
    }
}
