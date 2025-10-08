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
    internal class SessionRepository : ISessionRepository
    {
        private readonly GymDBContext _dbContext;
        public SessionRepository(GymDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public int Add(Session session)
        {
            _dbContext.Sessions.Add(session);
            return _dbContext.SaveChanges();
        }

        public int Delete(int id)
        {
            var session = _dbContext.Sessions.Find(id);
            if (session != null)
            {
                _dbContext.Sessions.Remove(session);
                return _dbContext.SaveChanges();
            }
            else
                return 0;
        }

        public IEnumerable<Session> GetAll()
        {
            return _dbContext.Sessions.ToList();
        }

        public Session? GetById(int id) => _dbContext.Sessions.Find(id);
        public int Update(Session session)
        {
            _dbContext.Sessions.Update(session);
            return _dbContext.SaveChanges();
        }
    }
}
