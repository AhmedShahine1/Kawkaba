using Kawkaba.BusinessLayer.Interfaces;
using Kawkaba.Core.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kawkaba.Areas.Support.Controllers
{
    [Area("Support")]
    public class RequestResponseViewerController : Controller
    {
        private readonly IRequestResponseService _requestResponseService;

        public RequestResponseViewerController(IRequestResponseService requestResponseService)
        {
            _requestResponseService = requestResponseService;
        }
        [Authorize(Policy = "Support Developer")]
        public async Task<IActionResult> Index()
        {
            var logs = await _requestResponseService.GetAllLogsAsync();
            return View(logs);
        }

        public async Task<IActionResult> GetLogs()
        {
            var logs = await _requestResponseService.GetAllLogsAsync();
            var logDtos = logs.Select(log => new
            {
                log.Timestamp,
                log.RequestUrl,
                log.HttpMethod,
                log.RequestBody,
                ResponseBody = JsonFormatter.FormatJson(log.ResponseBody)
            });
            return Json(logDtos);
        }
    }
}