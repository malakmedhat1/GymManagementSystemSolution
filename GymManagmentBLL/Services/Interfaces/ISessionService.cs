using GymManagmentBLL.ViewModels.SeesionViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.Services.Interfaces
{
    internal interface ISessionService
    {
        IEnumerable<SessionViewModel> GetAllSessions();

        SessionViewModel? GetSessionById(int id);
        bool CreateSession(CreateSessionViewModel createSession);

       UpdateSessionViewModel? GetSessionToUpdate(int SessionId);
        bool UpdatingSession(UpdateSessionViewModel updateSession, int SessionId);
        bool DeleteSession(int SessionId);
    }
}
