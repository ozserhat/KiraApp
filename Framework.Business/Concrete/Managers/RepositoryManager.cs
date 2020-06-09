using Framework.Core.DataAccess;
using Framework.Core.DataAccess.EntityFramework;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;
using Framework.Business.Abstract;
using Framework.Core.Entities;

namespace Framework.Business.Concrete.Managers
{
   public class RepositoryManager<T> : IRepository<T> where T : IEntity
    {
        private UnitOfWork _unitOfWork;
        public RepositoryManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
        }

        protected ISession Session { get { return _unitOfWork.Session; } }

        public IQueryable<T> GetAll()
        {
            return Session.Query<T>();
        }

        public T GetById(int id)
        {
            return Session.Get<T>(id);
        }

        public void Create(T entity)
        {
            Session.Save(entity);
        }

        public void Update(T entity)
        {
            Session.Update(entity);
        }

        public void Delete(int id)
        {
            Session.Delete(Session.Load<T>(id));
        }
    }
}
