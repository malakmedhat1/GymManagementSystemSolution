using GymManagmentBLL.ViewModels.MemberViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.Services.Interfaces
{
    public interface IMemberService
    {
        IEnumerable<MemberViewModel> GetAllMembers();
        bool CreateMember(CreateMemberViewModel createMemberViewModel);
        MemberViewModel? GetMemberDetails(int MemberId);
        HealthRecordViewModel? GetMemberHealthRecordDetails(int MemberId);
        MemberToUpdateViewModel? GetMemberForUpdate(int MemberId);
        bool UpdateMemberDetails(int MemberId, MemberToUpdateViewModel memberToUpdateViewModel);
        bool RemoveMember(int MemberId);
    }
}
