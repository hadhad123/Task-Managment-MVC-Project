using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace TaskManagment.Controllers
{
    [Authorize]
    public class ReportsController : Controller
    {
        private readonly IReportService ReportService;
        public ReportsController(IReportService ReportService)
        {
            this.ReportService = ReportService;
        }
        // GET: Reports
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DownloadReport()
        {
            string fileName = "AllTasks.pdf";
            byte[] fileBytes = ReportService.DownloadReport();
            return File(fileBytes, "application/pdf", fileName);

        }
    }
}