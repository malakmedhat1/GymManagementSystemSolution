using GymManagmentBLL.Services.Interfaces;
using GymManagmentBLL.ViewModels.TrainerViewModel;
using GymManagmentDAL.Entities;
using GymManagmentDAL.Entities.Enums;
using GymManagmentDAL.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.Services.Classes
{
    internal class TrainerService : ITrainerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TrainerService(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }
        public bool CreateTrainer(CreateTrainerViewModel createTrainerViewModel)
        {
            
            try
            {
                // email and phone must be unique and Valid
                if (IsEmailExists(createTrainerViewModel.Email) || IsPhoneExists(createTrainerViewModel.PhoneNumber))
                    return false;

                // must have one specialty assigned
                if (!Enum.IsDefined(typeof(Specialties), createTrainerViewModel.Specialties))
                    return false;

                var newTrainer = new Trainer
                {
                    Name = createTrainerViewModel.FullName,
                    Email = createTrainerViewModel.Email,
                    Phone = createTrainerViewModel.PhoneNumber,
                    Specialties = createTrainerViewModel.Specialties,
                    DateOfBirth = createTrainerViewModel.DateOfBirth,
                    Address = new Address
                    {
                        BuildingNumber = createTrainerViewModel.BuildingNumber,
                        Street = createTrainerViewModel.Street,
                        City = createTrainerViewModel.City
                    },
                    createdAt = DateTime.Now
                };

                _unitOfWork.GetRepository<Trainer>().Add(newTrainer);
                return _unitOfWork.SaveChanges() > 0;

            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<TrainerViewModel> GetAllTrainers()
        {
            var trainers = _unitOfWork.GetRepository<Trainer>().GetAll();
            
            if (trainers == null || !trainers.Any()) return Array.Empty<TrainerViewModel>();
            
            return trainers.Select(t => new TrainerViewModel
            {
                Id = t.id,
                FullName = t.Name,
                Email = t.Email,
                PhoneNumber = t.Phone,
                Specialties = t.Specialties
            });

        }

        public TrainerViewModel? GetTrainerDetails(int TrainerId)
        {
            var trainer = _unitOfWork.GetRepository<Trainer>().GetById(TrainerId);
            if (trainer == null) return null;
            
            return new TrainerViewModel
            {
                Id = trainer.id,
                FullName = trainer.Name,
                Email = trainer.Email,
                PhoneNumber = trainer.Phone,
                Specialties = trainer.Specialties,
                DateOfBirth = trainer.DateOfBirth,
                Address = $"{trainer.Address.BuildingNumber} - {trainer.Address.Street} - {trainer.Address.City}",

            };
        }

        public UpdateTrainerViewModel? GetTrainerForUpdate(int TrainerId)
        {
            var trainer = _unitOfWork.GetRepository<Trainer>().GetById(TrainerId);
            if (trainer == null) return null;
            
            return new UpdateTrainerViewModel
            {
                FullName = trainer.Name,
                Email = trainer.Email,
                PhoneNumber = trainer.Phone,
                Specialties = trainer.Specialties,
                BuildingNumber = trainer.Address.BuildingNumber,
                Street = trainer.Address.Street,
                City = trainer.Address.City
            };
        }

        public bool UpdateTrainerDetails(int TrainerId, UpdateTrainerViewModel trainerToUpdateViewModel)
        {
            try
            {
                if (IsEmailExists(trainerToUpdateViewModel.Email) || IsPhoneExists(trainerToUpdateViewModel.PhoneNumber))
                    return false;
                
                var TrainerRepo = _unitOfWork.GetRepository<Trainer>();
                var trainer = TrainerRepo.GetById(TrainerId);
                if (trainer == null) return false;
                
                trainer.Name = trainerToUpdateViewModel.FullName;
                trainer.Email = trainerToUpdateViewModel.Email;
                trainer.Phone = trainerToUpdateViewModel.PhoneNumber;
                trainer.Specialties = trainerToUpdateViewModel.Specialties;
                trainer.Address.BuildingNumber = trainerToUpdateViewModel.BuildingNumber;
                trainer.Address.Street = trainerToUpdateViewModel.Street;
                trainer.Address.City = trainerToUpdateViewModel.City;
                trainer.updatedAt = DateTime.Now;
               
                
                TrainerRepo.Update(trainer);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }

        public bool RemoveTrainer(int TrainerId)
        {
            try
            {
                var TrainerRepo = _unitOfWork.GetRepository<Trainer>();
                var trainer = TrainerRepo.GetById(TrainerId);

                if (trainer == null) return false;

                // cannot delete trainer with future sessions
                if (trainer.TrainerSessions != null && trainer.TrainerSessions.Any())
                    return false;
                
                TrainerRepo.Delete(trainer);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }

       
        #region Helper Methods
        private bool IsEmailExists(string email)
        {
            return _unitOfWork.GetRepository<Trainer>().GetAll(t => t.Email == email).Any();
        }

        private bool IsPhoneExists(string phone)
        {
            return _unitOfWork.GetRepository<Trainer>().GetAll(t => t.Phone == phone).Any();
        }
        #endregion
    }
}
