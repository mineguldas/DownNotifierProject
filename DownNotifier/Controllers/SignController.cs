using DownNotifier.Models;
using DownNotifierData.Repositories;
using DownNotifierEntities.DataTransferObjects;
using DownNotifierEntities.Entities;
using DownNotifierEntities.MailOperations;
using Microsoft.AspNetCore.Mvc;
using static DownNotifierEntities.Enums.Enums;

namespace DownNotifier.Controllers
{
    public class SignController : Controller
    {
        private readonly IUserRepository _userRepository;
        public SignController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IActionResult SignIn()
        {
            HttpContext.Session.Set(SessionKeys.UserId, "");
            return View("~/Views/SignIn/Index.cshtml");
        }

        public IActionResult SignUp()
        {
            HttpContext.Session.Set(SessionKeys.UserId, "");
            return View("~/Views/SignUp/Index.cshtml");
        }

        [HttpPost]
        public IActionResult SignUp(UserSignDto model)
        {
            if (!string.IsNullOrEmpty(model.Email) && MailSendOperations.IsValidEmail(model.Email))
            {
                EnumResult result = _userRepository.AddUser(model);
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
                    return Json(new ResultDto { Success = false, Message = "It cannot be continued because the email is registered in the system. Try a different Email address." });
                }
                else
                {
                    return Json(new ResultDto { Success = false, Message = "" });
                }
            }
            else
            {
                return Json(new ResultDto { Success = false, Message = "Registration could not be completed because you entered an invalid email address." });
            }
        }


        [HttpPost]
        public IActionResult SignIn(UserSignDto model)
        {
            User result = _userRepository.GetUserSignIn(model);
            if (result != null)
            {
                HttpContext.Session.Set(SessionKeys.UserId, result.Id.ToString());
                return Json(new ResultDto { Success = true, Message = "Transaction Successful" });
            }
            else
            {
                return Json(new ResultDto { Success = false, Message = "There are no records matching the email address and password." });
            }
        }
    }
}
