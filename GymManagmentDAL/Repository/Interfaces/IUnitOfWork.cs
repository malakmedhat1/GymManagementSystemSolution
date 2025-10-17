using GymManagmentDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        public ISessionRepository SessionRepository { get; }
        IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : BasicEntity, new();
        int SaveChanges();
    }
}
