using DownNotifier.Models;
using DownNotifierData.Repositories;
using DownNotifierEntities.DataTransferObjects;
using DownNotifierEntities.Entities;
using Microsoft.AspNetCore.Mvc;
using static DownNotifierEntities.Enums.Enums;

namespace DownNotifier.Controllers
{
    [UserFilter]
    public class ParameterController : Controller
    {
        private readonly IParameterRepository _parameterRepository;
        public ParameterController(IParameterRepository parameterRepository)
        {
            _parameterRepository = parameterRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UpdateParameter(Parameter model)
        {
            int userId = Convert.ToInt32(HttpContext.Session.Get<string>(SessionKeys.UserId));
            EnumResult result = _parameterRepository.UpdateParameter(model, userId);
            if (result == EnumResult.Basarili)
            {
                return Json(new ResultDto { Success = true, Message = "Transaction Successful" });
            }
            else if (result == EnumResult.Basarisiz)
            {
                return Json(new ResultDto { Success = false, Message = "Transaction Failed" });
            }
            else
            {
                return Json(new ResultDto { Success = false, Message = "" });
            }
        }

        public IActionResult GetParameters()
        {
            List<Parameter> data = _parameterRepository.ParameterList();
            if (data != null)
            {
                return PartialView("~/Views/Shared/PartialViews/PartialParametersTable.cshtml", data);
            }
            else
            {
                return PartialView("~/Views/Shared/PartialViews/PartialParametersTable.cshtml", new List<Parameter>());
            }
        }

        public IActionResult GetParameter(int parameterId = 0)
        {
            Parameter data = _parameterRepository.GetParameter(parameterId);
            if (data != null)
            {
                return Json(new { success = true, data = data });
            }
            else
            {
                return Json(new { success = false });
            }
        }
    }
}
