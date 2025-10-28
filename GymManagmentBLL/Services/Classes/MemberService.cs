using GymManagmentBLL.Services.Interfaces;
using GymManagmentBLL.ViewModels.MemberViewModel;
using GymManagmentDAL.Entities;
using GymManagmentDAL.Repository.Classes;
using GymManagmentDAL.Repository.Interfaces;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.Services.Classes
{
    public class MemberService : IMemberService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MemberService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool CreateMember(CreateMemberViewModel createMemberViewModel)
        {
            try
            {
                if (IsEmailExists(createMemberViewModel.Email) || IsPhoneExists(createMemberViewModel.Phone))
                    return false;

                var member = new Member()
                {
                    Name = createMemberViewModel.Name,
                    Email = createMemberViewModel.Email,
                    Phone = createMemberViewModel.Phone,
                    Gender = createMemberViewModel.Gender,
                    DateOfBirth = createMemberViewModel.DateOfBirth,
                    Address = new Address()
                    {
                        BuildingNumber = createMemberViewModel.BuildingNumber,
                        Street = createMemberViewModel.Street,
                        City = createMemberViewModel.City
                    },
                    HealthRecord = new HealthRecord()
                    {
                        Height = createMemberViewModel.HealthRecordViewModel.Height,
                        Weight = createMemberViewModel.HealthRecordViewModel.Weight,
                        BloodType = createMemberViewModel.HealthRecordViewModel.BloodType,
                        Note = createMemberViewModel.HealthRecordViewModel.Note
                    }
                };
                _unitOfWork.GetRepository<Member>().Add(member);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<MemberViewModel> GetAllMembers()
        {

            var members = _unitOfWork.GetRepository<Member>().GetAll();
            if (members == null || !members.Any())
                return [];

            // Member  - MemberViewModel => Mapping
            //var memberViewModels = new List<MemberViewModel>();
            #region Way 01
            //foreach (var member in members)
            //{
            //    var memberViewModel = new MemberViewModel()
            //    {
            //        Id = member.Id,
            //        Name = member.Name,
            //        Photo = member.Photo,
            //        Email = member.Email,
            //        Phone = member.Phone
            //    };
            //    memberViewModels.Add(memberViewModel);
            //} 
            #endregion

            #region Way 02
            var memberViewModels = members.Select(member => new MemberViewModel()
            {
                Id = member.id,
                Name = member.Name,
                Photo = member.Photo,
                Email = member.Email,
                Phone = member.Phone,
            });
            #endregion
            return memberViewModels;
        }

        public MemberViewModel? GetMemberDetails(int MemberId)
        {
            var member = _unitOfWork.GetRepository<Member>().GetById(MemberId);

            if (member == null)
                return null;

            var ViewModel = new MemberViewModel()
            {
                Id = member.id,
                Name = member.Name,
                Photo = member.Photo,
                Email = member.Email,
                Phone = member.Phone,
                Gender = member.Gender.ToString(),
                DateOfBirth = member.DateOfBirth.ToShortDateString(),
                Address = $"{member.Address.BuildingNumber} - {member.Address.Street} - {member.Address.City}",
            };
            var Activemembership = _unitOfWork.GetRepository<MemberShip>()
                .GetAll(m => m.MemberId == MemberId && m.Status == "Active")
                .FirstOrDefault();

            if (Activemembership is not null)
            {
                ViewModel.MembershipStartDate = Activemembership.createdAt.ToShortDateString();
                ViewModel.MembershipEndDate = Activemembership.EndDate.ToShortDateString();
                var plan = _unitOfWork.GetRepository<Plan>().GetById(Activemembership.PlanId);
                ViewModel.PlanName = plan?.Name;
            }

            return ViewModel;
        }

        public HealthRecordViewModel? GetMemberHealthRecordDetails(int MemberId)
        {
            var MemberHealthRecord = _unitOfWork.GetRepository<HealthRecord>().GetById(MemberId);
            
            if (MemberHealthRecord == null)
                return null;

            // Map HealthRecord to HealthRecordViewModel
            return new HealthRecordViewModel()
            {
                Height = MemberHealthRecord.Height,
                Weight = MemberHealthRecord.Weight,
                BloodType = MemberHealthRecord.BloodType,
                Note = MemberHealthRecord.Note
            };
        }

        public MemberToUpdateViewModel? GetMemberForUpdate(int MemberId)
        {
            var member = _unitOfWork.GetRepository<Member>().GetById(MemberId);

            if (member == null)
                return null;

            return new MemberToUpdateViewModel()
            {
                Name = member.Name,
                Email = member.Email,
                Phone = member.Phone,
                BuildingNumber = member.Address.BuildingNumber,
                Street = member.Address.Street,
                City = member.Address.City,
                Photo = member.Photo
            };
        }

        public bool UpdateMemberDetails(int MemberId, MemberToUpdateViewModel memberToUpdateViewModel)
        {
            try
            {
                
                var emailExists = _unitOfWork.GetRepository<Member>()
                    .GetAll(m => m.Email == memberToUpdateViewModel.Email && m.id != MemberId)
                    .Any();

                var phoneExists = _unitOfWork.GetRepository<Member>()
                    .GetAll(m => m.Phone == memberToUpdateViewModel.Phone && m.id != MemberId)
                    .Any();

                if (emailExists || phoneExists) return false;

                var MemberRepo = _unitOfWork.GetRepository<Member>();
                var member = MemberRepo.GetById(MemberId);
                if (member == null)
                    return false;

                member.Email = memberToUpdateViewModel.Email;
                member.Phone = memberToUpdateViewModel.Phone;
                member.Address.BuildingNumber = memberToUpdateViewModel.BuildingNumber;
                member.Address.Street = memberToUpdateViewModel.Street;
                member.Address.City = memberToUpdateViewModel.City;
                member.updatedAt = DateTime.Now;

                MemberRepo.Update(member);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }

        public bool RemoveMember(int MemberId)
        {
            try
            {
                var MemberRepo = _unitOfWork.GetRepository<Member>();

                var member = MemberRepo.GetById(MemberId);
                
                if (member == null)
                    return false;

                var SessionIds = _unitOfWork.GetRepository<MemberSession>()
                    .GetAll(sb => sb.MemberId == MemberId).Select(sb=>sb.SessionId);

                var HasActiveMemberSession = _unitOfWork.GetRepository<Session>()
                    .GetAll(m => SessionIds.Contains(m.id) && m.StartDate > DateTime.Now).Any();

                //var ActiveMemberSession = _unitOfWork.GetRepository<MemberShip>()
                //    .GetAll(m => m.MemberId == MemberId && m.Status == "Active")
                //    .Any();

                if (HasActiveMemberSession)
                    return false;

                var MemberShipRepo = _unitOfWork.GetRepository<MemberShip>();
                var memberShips = MemberShipRepo.GetAll(m => m.MemberId == MemberId);

                if (memberShips.Any())
                {
                    foreach (var memberShip in memberShips)
                    {
                        MemberShipRepo.Delete(memberShip);
                    }
                }
                MemberRepo.Delete(member);
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
           return _unitOfWork.GetRepository<Member>().GetAll(m => m.Email == email).Any();
        }

        private bool IsPhoneExists(string phone)
        {
            return _unitOfWork.GetRepository<Member>().GetAll(m => m.Phone == phone).Any();
        }

        
        #endregion

    }
}
        
