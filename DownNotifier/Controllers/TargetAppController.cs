using DownNotifier.Models;
using DownNotifierData.Repositories;
using DownNotifierEntities.DataTransferObjects;
using DownNotifierEntities.Entities;
using DownNotifierEntities.MailOperations;
using Microsoft.AspNetCore.Mvc;
using static DownNotifierEntities.Enums.Enums;

namespace DownNotifier.Controllers
{
    [UserFilter]
    public class TargetAppController : Controller
    {
        private readonly ITargetAppRepository _targetAppRepository;
        private readonly IUserRepository _userRepository;
        public TargetAppController(ITargetAppRepository targetAppRepository, IUserRepository userRepository)
        {
            _targetAppRepository = targetAppRepository;
            _userRepository = userRepository;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult AddTargetApp(TargetAppDto model)
        {
            int userId = Convert.ToInt32(HttpContext.Session.Get<string>(SessionKeys.UserId));
            EnumResult result = _targetAppRepository.AddTargetApp(model, userId);
            if (result == EnumResult.Basarili)
            {
                return Json(new ResultDto { Success = true, Message = "Transaction Successful" });
            }
            else if (result == EnumResult.Basarisiz)
            {
                return Json(new ResultDto { Success = false, Message = "Transaction Failed" });
            }
            else if (result == EnumResult.IcerideAyniIslemKayitli)
            {
                return Json(new ResultDto { Success = false, Message = "It cannot continue because Target App name and Target App Url are registered in the system." });
            }
            else
            {
                return Json(new ResultDto { Success = false, Message = "" });
            }
        }

        public IActionResult GetTargetApps(int targetAppId = 0)
        {
            List<TargetAppDto> data = _targetAppRepository.CheckTargetAppStatu(targetAppId);
            if (data != null)
            {
                return PartialView("~/Views/Shared/PartialViews/PartialTargetAppsTable.cshtml", data);
            }
            else
            {
                return PartialView("~/Views/Shared/PartialViews/PartialTargetAppsTable.cshtml", new List<TargetAppDto>());
            }
        }

        public IActionResult GetTargetApp(int targetAppId = 0)
        {
            List<TargetAppDto> data = _targetAppRepository.CheckTargetAppStatu(targetAppId);
            if (data.Count > 0)
            {
                return Json(new { data = data.FirstOrDefault(), success = true });
            }
            else
            {
                return Json(new { data = new TargetApp(), success = false });
            }
        }

        public IActionResult CheckTargetApp(string TargetAppUrl = "")
        {
            TargetApp data = _targetAppRepository.CheckTargetApp(TargetAppUrl);
            if (data != null)
            {
                if (data.Statu == TargetAppStatus.Close)
                {
                    User user = _userRepository.GetUser(data.UserId);
                    bool result = MailSendOperations.SendMail(user.Email, TargetAppUrl);
                    return Json(new { data = data, success = true, mailSent = result });
                }
                else
                {
                    return Json(new { data = data, success = true });
                }
            }
            else
            {
                return Json(new { data = new TargetApp(), success = false });
            }
        }

        [HttpPost]
        public IActionResult DeleteTargetApp(int targetAppId = 0)
        {
            bool result = _targetAppRepository.DeleteTargetApp(targetAppId);
            return Json(new { success = result });
        }


    }
}
