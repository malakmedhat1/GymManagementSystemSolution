using GymManagmentBLL.Services.Interfaces;
using GymManagmentBLL.ViewModels.PlanViewModel;
using GymManagmentDAL.Entities;
using GymManagmentDAL.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.Services.Classes
{
    internal class PlanService : IPlanService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PlanService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public IEnumerable<PlanViewModel> GetAllPlans()
        {
            var plans = _unitOfWork.GetRepository<Plan>().GetAll();
            if (plans == null || !plans.Any()) return [];

            return plans.Select(p => new PlanViewModel
            {
                Id = p.id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                DurationDays = p.DurationDays,
                IsActive = p.IsActive
            });
        }

        public PlanViewModel? GetPlanDetails(int PlanId) 
        {
            var plan = _unitOfWork.GetRepository<Plan>().GetById(PlanId);
            if (plan == null) return null;
            
            return new PlanViewModel
            {
                Id = plan.id,
                Name = plan.Name,
                Description = plan.Description,
                Price = plan.Price,
                DurationDays = plan.DurationDays,
                IsActive = plan.IsActive
            };
        }

        public UpdatePLanViewModel? GetPlanToUpdate(int PlanId)
        {
            var plan = _unitOfWork.GetRepository<Plan>().GetById(PlanId);
            if (plan is null || plan.IsActive == false || HasActiveMemberships(PlanId)) return null;
            
            return new UpdatePLanViewModel
            {
                PlanName = plan.Name,
                Description = plan.Description,
                Price = plan.Price,
                DurationDays = plan.DurationDays
            };
        }
        public bool UpdatePlanDetails(int PlanId, UpdatePLanViewModel updatePLan)
        {
            var PlanRepo = _unitOfWork.GetRepository<Plan>();
            var plan = PlanRepo.GetById(PlanId);

            if (plan is null || HasActiveMemberships(PlanId)) return false;

            plan.Description = updatePLan.Description;
            plan.Price = updatePLan.Price;
            plan.DurationDays = updatePLan.DurationDays;
            plan.updatedAt = DateTime.Now;

            PlanRepo.Update(plan);

            return _unitOfWork.SaveChanges() > 0;
        }

        public bool TooglePlanStatus(int PlanId)
        {
            var PlanRepo = _unitOfWork.GetRepository<Plan>();
            var plan = PlanRepo.GetById(PlanId);
            
            if (plan is null || HasActiveMemberships(PlanId)) return false;
            
            plan.IsActive = plan.IsActive == true ? false : true;
            try 
            {
                PlanRepo.Update(plan);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch 
            {
                return false;
            }
            
        }



        #region Helper Methods
         private bool HasActiveMemberships(int planId)
        {    return _unitOfWork.GetRepository<MemberShip>()
                    .GetAll(ms => ms.PlanId == planId && ms.Status == "Active")
                    .Any();
        }
        #endregion
    }
}
