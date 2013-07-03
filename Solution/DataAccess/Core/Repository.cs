using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Domain.Core;
using System;
using System.Collections.Generic;
using Domain.Core.Specification;

namespace DataAccess.Core
{
    /// <summary>
    /// Repository base class
    /// </summary>
    /// <typeparam name="TEntity">The type of underlying entity in this repository</typeparam>
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        #region Members

        private readonly IQueryableUnitOfWork _unitOfWork;

        #endregion

        #region Constructor

        /// <summary>
        /// Create a new instance of repository
        /// </summary>
        /// <param name="unitOfWork">Associated Unit Of Work</param>
        public Repository(IQueryableUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
                throw new ArgumentNullException("unitOfWork");

            _unitOfWork = unitOfWork;
        }

        #endregion

        #region IRepository Members

        /// <summary>
        /// <see cref="IRepository{TEntity}"/>
        /// </summary>
        public IUnitOfWork UnitOfWork
        {
            get { return _unitOfWork; }
        }

        /// <summary>
        /// <see cref="IRepository{TValueObject}"/>
        /// </summary>
        /// <param name="item"><see cref="IRepository{TValueObject}"/></param>
        public virtual void Add(TEntity item)
        {
            if (item != null)
                GetSet().Add(item); // add new item in this set
        }

        /// <summary>
        /// <see cref="IRepository{TValueObject}"/>
        /// </summary>
        /// <param name="item"><see cref="IRepository{TValueObject}"/></param>
        public virtual void Remove(TEntity item)
        {
            if (item != null)
            {
                //attach item if not exist
                _unitOfWork.Attach(item);

                //set as "removed"
                GetSet().Remove(item);
            }
        }

        /// <summary>
        /// <see cref="IRepository{TValueObject}"/>
        /// </summary>
        /// <param name="item"><see cref="IRepository{TValueObject}"/></param>
        public virtual void TrackItem(TEntity item)
        {
            if (item != null)
                _unitOfWork.Attach(item);
        }

        /// <summary>
        /// <see cref="IRepository{TValueObject}"/>
        /// </summary>
        /// <param name="item"><see cref="IRepository{TValueObject}"/></param>
        public virtual void Modify(TEntity item)
        {
            if (item != null)
                _unitOfWork.SetModified(item);
        }

        /// <summary>
        /// <see cref="IRepository{TValueObject}"/>
        /// </summary>
        /// <param name="id"><see cref="IRepository{TValueObject}"/></param>
        /// <returns><see cref="IRepository{TValueObject}"/></returns>
        public virtual TEntity Get(Guid id)
        {
            if (id != Guid.Empty)
                return GetSet().Find(id);
            return null;
        }

        /// <summary>
        /// <see cref="IRepository{TValueObject}"/>
        /// </summary>
        /// <returns><see cref="IRepository{TValueObject}"/></returns>
        public virtual IEnumerable<TEntity> GetAll()
        {
            return GetSet();
        }

        /// <summary>
        /// <see cref="IRepository{TValueObject}"/>
        /// </summary>
        /// <param name="specification"><see cref="IRepository{TValueObject}"/></param>
        /// <returns><see cref="IRepository{TValueObject}"/></returns>
        public virtual IEnumerable<TEntity> AllMatching(ISpecification<TEntity> specification)
        {
            return GetSet().Where(specification.SatisfiedBy());
        }

        /// <summary>
        /// <see cref="IRepository{TValueObject}"/>
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="pageIndex"><see cref="IRepository{TValueObject}"/></param>
        /// <param name="pageCount"><see cref="IRepository{TValueObject}"/></param>
        /// <param name="orderByExpression"><see cref="IRepository{TValueObject}"/></param>
        /// <param name="ascending"><see cref="IRepository{TValueObject}"/></param>
        /// <returns><see cref="IRepository{TValueObject}"/></returns>
        public virtual IEnumerable<TEntity> GetPaged<TProperty>(int pageIndex, int pageCount,
                                                                Expression<Func<TEntity, TProperty>> orderByExpression, bool ascending)
        {
            var set = GetSet();

            if (ascending)
            {
                return set.OrderBy(orderByExpression)
                          .Skip(pageCount*pageIndex)
                          .Take(pageCount);
            }

            return set.OrderByDescending(orderByExpression)
                      .Skip(pageCount*pageIndex)
                      .Take(pageCount);
        }

        /// <summary>
        /// <see cref="IRepository{TValueObject}"/>
        /// </summary>
        /// <param name="filter"><see cref="IRepository{TValueObject}"/></param>
        /// <returns><see cref="IRepository{TValueObject}"/></returns>
        public virtual IEnumerable<TEntity> GetFiltered(Expression<Func<TEntity, bool>> filter)
        {
            return GetSet().Where(filter);
        }

        /// <summary>
        /// <see cref="IRepository{TValueObject}"/>
        /// </summary>
        /// <param name="persisted"><see cref="IRepository{TValueObject}"/></param>
        /// <param name="current"><see cref="IRepository{TValueObject}"/></param>
        public virtual void Merge(TEntity persisted, TEntity current)
        {
            _unitOfWork.ApplyCurrentValues(persisted, current);
        }

        #endregion

        #region IDisposable Members

        /// <summary>
        /// <see cref="M:System.IDisposable.Dispose"/>
        /// </summary>
        public void Dispose()
        {
            if (_unitOfWork != null)
                _unitOfWork.Dispose();
        }

        #endregion

        #region Private Methods

        private IDbSet<TEntity> GetSet()
        {
            return _unitOfWork.CreateSet<TEntity>();
        }

        #endregion
    }
}
