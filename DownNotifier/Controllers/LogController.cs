using DownNotifierData.Repositories;
using DownNotifierEntities.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DownNotifier.Controllers
{
    [UserFilter]
    public class LogController : Controller
    {
        private readonly ILogRepository _logRepository;
        public LogController(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetLogs()
        {
            List<Log> data = _logRepository.LogList();
            if (data != null)
            {
                return PartialView("~/Views/Shared/PartialViews/PartialLogsTable.cshtml", data);
            }
            else
            {
                return PartialView("~/Views/Shared/PartialViews/PartialLogsTable.cshtml", new List<Log>());
            }
        }
    }
}
