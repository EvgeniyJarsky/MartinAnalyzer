using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report_BL.Controller.GetDeals
{
    public class GetDeals
    {
        public static void Get(Report_BL.ReportModel.Report report)
        {
            if (report.ReportType == "MT4Tester")
                Report_BL.Controller.GetDeals.TesterMT4.GetDealsMT4Tester.Get(report);
        }
    }
}
