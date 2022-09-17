using Report_BL.Controller.GetDeals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Report_BL.Controller.MainInfo
{
    public static class FirstInfo
    {
        
        public static Report_BL.ReportModel.FirstInfo Get(string reportType, string filePath)
        {
            #region Словарь для первичной информации
            var dicFirstInfo = new Dictionary<string, Func<string, Report_BL.ReportModel.FirstInfo>>();

            // MT4Tester
            dicFirstInfo.Add(
                    "MT4Tester",
                    new Func<string, Report_BL.ReportModel.FirstInfo>
                    (Report_BL.Controller.MainInfo.MT4Tester.GetFirstInfoMT4.GetSymbolDateMagic));
            // MT4History
            dicFirstInfo.Add(
                "MT4History",
                new Func<string, Report_BL.ReportModel.FirstInfo>
                (Report_BL.Controller.MainInfo.MT4History.GetFirstInfoMT4History.GetSymbolDateMagic));
            #endregion
             try
            {
                var result = dicFirstInfo[reportType](filePath);
                return result;
            }
            catch(Exception e)
            {
                return null;
            }
        }
    }
}
