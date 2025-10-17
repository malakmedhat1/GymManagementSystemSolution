using GymManagmentDAL.Data.Contexts;
using GymManagmentDAL.Entities;
using GymManagmentDAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Repository.Classes
{
    public class SessionRepository : GenericRepository<Session>, ISessionRepository
    {
        private readonly GymDBContext _dbContext;

        public SessionRepository(GymDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Session> GetAllSessionWithTrainerAndCategory()
        {
            return _dbContext.Sessions.Include(x=>x.SessionTrainer).Include(x=>x.Category).ToList();
        }

        public int GetCountOfBookedSlots(int sessionId)
        {
            return _dbContext.MemberSessions.Count(x => x.SessionId == sessionId);
        }

        public Session? GetSessionByIdWithTrainerAndCategories(int id)
        {
            return _dbContext.Sessions.Include(x => x.SessionTrainer)
                                      .Include(x => x.Category)
                                      .FirstOrDefault(x=>x.id==id);
        }
    }
}
