using GymManagmentDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Repository.Interfaces
{
    public interface ISessionRepository : IGenericRepository<Session>
    {
        IEnumerable<Session> GetAllSessionWithTrainerAndCategory();
        Session? GetSessionByIdWithTrainerAndCategories(int id);
        int GetCountOfBookedSlots(int sessionId);
    }
}
