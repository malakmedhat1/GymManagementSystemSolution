using GymManagmentBLL.ViewModels.TrainerViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.Services.Interfaces
{
    internal interface ITrainerService
    {
        IEnumerable<TrainerViewModel> GetAllTrainers();
        bool CreateTrainer(CreateTrainerViewModel createTrainerViewModel);
        TrainerViewModel? GetTrainerDetails(int TrainerId);
        UpdateTrainerViewModel? GetTrainerForUpdate(int TrainerId);
        bool UpdateTrainerDetails(int TrainerId, UpdateTrainerViewModel trainerToUpdateViewModel);
        bool RemoveTrainer(int TrainerId);
    }
}
