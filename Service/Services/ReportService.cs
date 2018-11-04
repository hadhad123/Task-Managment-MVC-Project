using Reports;
using Stimulsoft.Report;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Service
{
   public class ReportService : IReportService
    {
        private string ConnectionString;
        public ReportService()
        {
            ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }
        public byte[] DownloadReport()
        {
            Report Report = new Report();
          
            Report.Dictionary.Databases.Clear();
            Report.Dictionary.Databases.Add(new Stimulsoft.Report.Dictionary.StiSqlDatabase("Connection", "Connection", ConnectionString, false));
            Report.Render(false);

            System.IO.MemoryStream memoryStream = new System.IO.MemoryStream();
  
            Report.ExportDocument(Stimulsoft.Report.StiExportFormat.Pdf, memoryStream);

            byte[] fileBytes = memoryStream.ToArray();
            memoryStream.Flush();
            memoryStream.Close();
            return fileBytes;

        }
    }
}
