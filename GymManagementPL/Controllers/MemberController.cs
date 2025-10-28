using GymManagmentBLL.Services.Interfaces;
using GymManagmentBLL.ViewModels.MemberViewModel;
using GymManagmentDAL.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    public class MemberController : Controller
    {
        private readonly IMemberService _memberService;

        public MemberController(IMemberService memberService) {
            _memberService = memberService;
        }

        #region GetAllMembers
        public ActionResult Index()
        {
            var members = _memberService.GetAllMembers();
            return View(members);
        }

        #endregion

        #region GetMemberData
        
        public ActionResult MemberDetails(int id)
        {
            if(id <= 0) return RedirectToAction(nameof(Index));
            
            var member = _memberService.GetMemberDetails(id);
            
            if (member is null) return RedirectToAction(nameof(Index));

            return View(member);
        }

        public ActionResult HealthRecordDetails(int id)
        {
            if (id <= 0) return RedirectToAction(nameof(Index));
            
            var healthRecord = _memberService.GetMemberHealthRecordDetails(id);
            
            if (healthRecord is null) return RedirectToAction(nameof(Index));
            return View(healthRecord);
        }
        #endregion

        #region AddMember

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult CreateMember(CreateMemberViewModel createMember)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("DataInvalid", "Check Data And Missing Fields.");
                return View(nameof(Create), createMember);
            }
           bool Result = _memberService.CreateMember(createMember);
            if(Result)
            {
                TempData["SuccessMessage"] = "Member Created Successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to Create Member.";
            }
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region EditMember

        public ActionResult MemberEdit(int id)
        {
            if (id <= 0)
            {
                @TempData["ErrorMessage"] = "Invalid Member Id.";
                return RedirectToAction(nameof(Index));
            }

            var member = _memberService.GetMemberForUpdate(id);
            if (member is null)
            {
                @TempData["ErrorMessage"] = "Member Not Found.";
                return RedirectToAction(nameof(Index));
            }
            return View(member);
        }
        [HttpPost]
        public ActionResult MemberEdit([FromRoute]int id,MemberToUpdateViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);
            
            var Result = _memberService.UpdateMemberDetails(id,viewModel);
            
            if (Result)
            {
                TempData["SuccessMessage"] = "Member Updated Successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to Update Member.";
            }
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region DeleteMember

        public ActionResult Delete(int id)
        {
            if (id <= 0)
            {
                @TempData["ErrorMessage"] = "Invalid Member Id.";
                return RedirectToAction(nameof(Index));
            }
            var Result = _memberService.GetMemberDetails(id);
            if (Result is null)
            {
                @TempData["ErrorMessage"] = "Member Not Found.";
                return RedirectToAction(nameof(Index));
            }
            @ViewBag.MemberId = id;
            return View();
        }
        [HttpPost]
        public ActionResult DeleteConfirm(int id)
        {
            if (id <= 0)
            {
                @TempData["ErrorMessage"] = "Invalid Member Id.";
                return RedirectToAction(nameof(Index));
            }
            
            var Result = _memberService.RemoveMember(id);
            if (Result)
            {
                TempData["SuccessMessage"] = "Member Deleted Successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to Delete Member.";
            }

            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}
