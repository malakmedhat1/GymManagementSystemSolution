using GymManagmentBLL.ViewModels.PlanViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.Services.Interfaces
{
    internal interface IPlanService
    {
        IEnumerable<PlanViewModel> GetAllPlans();
        PlanViewModel? GetPlanDetails(int PlanId);
        UpdatePLanViewModel? GetPlanToUpdate(int PlanId);
        bool UpdatePlanDetails(int PlanId, UpdatePLanViewModel updatePLan);
        bool TooglePlanStatus(int PlanId);
    }
}
