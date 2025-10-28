using GymManagmentBLL.Services.Interfaces;
using GymManagmentBLL.ViewModels.AnalyticsViewModel;
using GymManagmentDAL.Entities;
using GymManagmentDAL.Repository.Classes;
using GymManagmentDAL.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.Services.Classes
{
    public class AnalyticsService : IAnalyticService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AnalyticsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public AnalyticsViewModel GetAnalyticsData()
        {
            var Sessions = _unitOfWork.SessionRepository.GetAll();
            return new AnalyticsViewModel
            {
                TotalMembers = _unitOfWork.GetRepository<Member>().GetAll().Count(),
                ActiveMembers = _unitOfWork.GetRepository<MemberShip>().GetAll(m => m.Status == "Active").Count(),
                TotalTrainers = _unitOfWork.GetRepository<Trainer>().GetAll().Count(),
                UpcomingSessions = Sessions.Count(s => s.StartDate > DateTime.Now),
                OngoingSessions = Sessions.Count(s => s.StartDate<= DateTime.Now && s.EndDate > DateTime.Now),
                CompletedSessions = Sessions.Count(s => s.EndDate < DateTime.Now)
            };
        }
    }
}
