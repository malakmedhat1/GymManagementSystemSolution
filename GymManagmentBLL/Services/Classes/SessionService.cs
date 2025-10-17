using AutoMapper;
using GymManagmentBLL.Services.Interfaces;
using GymManagmentBLL.ViewModels.SeesionViewModel;
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
    internal class SessionService : ISessionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SessionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            Mapper = mapper;
        }

        public IMapper Mapper { get; }

        

        public IEnumerable<SessionViewModel> GetAllSessions()
        {
            var sessions = _unitOfWork.SessionRepository.GetAllSessionWithTrainerAndCategory();
            if (sessions is null || !sessions.Any()) return [];

            //return sessions.Select(x => new SessionViewModel()
            //{
            //    Id = x.id,
            //    Capacity = x.Capacity,
            //    Description = x.Description,
            //    EndDate = x.EndDate,
            //    StartDate = x.StartDate,
            //    TrainerName = x.SessionTrainer.Name,
            //    CategoryName = x.Category.CategoryName,
            //    AvailableSlots = x.Capacity - _unitOfWork.SessionRepository.GetCountOfBookedSlots(x.id)
            //});
            var MappedSessions = Mapper.Map<IEnumerable<Session>, IEnumerable<SessionViewModel>>(sessions);
            return MappedSessions;
        }

        public SessionViewModel? GetSessionById(int id)
        {
            var Session = _unitOfWork.SessionRepository.GetSessionByIdWithTrainerAndCategories(id);
            if (Session is null) return null;

            //return new SessionViewModel()
            //{
            //    Id = Session.id,
            //    Capacity = Session.Capacity,
            //    Description = Session.Description,
            //    EndDate = Session.EndDate,
            //    StartDate = Session.StartDate,
            //    TrainerName = Session.SessionTrainer.Name,
            //    CategoryName = Session.Category.CategoryName,
            //    AvailableSlots = Session.Capacity - _unitOfWork.SessionRepository.GetCountOfBookedSlots(Session.id)
            //};

            var MappedSessions = Mapper.Map<Session,SessionViewModel>(Session);
            return MappedSessions;
        }

        public bool CreateSession(CreateSessionViewModel createSession)
        {
            try
            {
                if (!IsTrainerExists(createSession.TrainerId) || !IsCategoryExists(createSession.CategoryId) || !IsValidDateRange(createSession.StartDate, createSession.EndDate))
                    return false;

                var session = Mapper.Map<CreateSessionViewModel, Session>(createSession);
                _unitOfWork.SessionRepository.Add(session);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
            
        }

        #region HelperMethods
        private bool IsTrainerExists(int trainerId)
        {
            return _unitOfWork.GetRepository<Trainer>().GetById(trainerId) is not null;
        }

        private bool IsCategoryExists(int categoryId)
        {
            return _unitOfWork.GetRepository<Category>().GetById(categoryId) is not null;
        }

        private bool IsValidDateRange(DateTime startDate, DateTime endDate)
        {
            return startDate < endDate && startDate > DateTime.Now;
        }

        public UpdateSessionViewModel? GetSessionToUpdate(int SessionId)
        {
            var Session = _unitOfWork.SessionRepository.GetById(SessionId);
            if (!IsSessionAvailableForUpdate(Session)) return null;
            return Mapper.Map<UpdateSessionViewModel>(Session);

        }

        public bool UpdatingSession(UpdateSessionViewModel updateSession, int SessionId)
        {
            try
            {
                var Session = _unitOfWork.SessionRepository.GetById(SessionId);
                if (!IsSessionAvailableForUpdate(Session!)) return false;
                if (!IsTrainerExists(updateSession.TrainerId) || !IsValidDateRange(updateSession.StartDate, updateSession.EndDate))
                    return false;

                Mapper.Map(updateSession, Session);
                Session!.updatedAt = DateTime.Now;
                return _unitOfWork.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
            
        }

        private bool IsSessionAvailableForUpdate(Session session)
        {
            if (session == null) return false;

            if(session.EndDate < DateTime.Now) return false;

            if (session.StartDate <= DateTime.Now) return false;

            var HasActiveBooking = _unitOfWork.SessionRepository.GetCountOfBookedSlots(session.id) > 0;

            if (HasActiveBooking) return false;
            
            return true;
        }
        private bool IsSessionAvailableForDelete(Session session)
        {
            if (session == null) return false;

            if(session.StartDate < DateTime.Now) return false;

            if (session.StartDate <= DateTime.Now && session.EndDate > DateTime.Now) return false;

            var HasActiveBooking = _unitOfWork.SessionRepository.GetCountOfBookedSlots(session.id) > 0;

            if (HasActiveBooking) return false;
            
            return true;
        }

        public bool DeleteSession(int SessionId)
        {
            try
            {
                var Session = _unitOfWork.SessionRepository.GetById(SessionId);
                if (!IsSessionAvailableForDelete(Session!)) return false;
                _unitOfWork.SessionRepository.Delete(Session!);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }
}
