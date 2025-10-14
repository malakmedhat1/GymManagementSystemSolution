using GymManagmentDAL.Data.Contexts;
using GymManagmentDAL.Entities;
using GymManagmentDAL.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Repository.Classes
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GymDBContext _dBContext;

        public UnitOfWork(GymDBContext dBContext)
        {
            _dBContext = dBContext;
        }

        private readonly Dictionary<Type, object> _repositories = new();

        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : BasicEntity, new()
        {
            var EntitytType = typeof(TEntity);
            if (_repositories.ContainsKey(EntitytType))
                return (IGenericRepository<TEntity>)_repositories[EntitytType];
            
            var newRepository = new GenericRepository<TEntity>(_dBContext);
            _repositories[EntitytType] = newRepository;
            return (IGenericRepository<TEntity>) newRepository;
        }

        public int SaveChanges()
        {
            return _dBContext.SaveChanges();
        }
    }
}
