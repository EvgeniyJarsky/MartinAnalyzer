using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report_BL.DataCollection
{
    public static class ClearAllData
    {
        /// <summary>
        /// Очистить все Data данные
        /// </summary>
        public static void ClearAll()
        {
            ReportCollection.ClearReportCollection();
            ParamentrsCollection.ClearItem();
            DealsCollection.dealsCollection.Clear();
        }

        public static void ClearParamAndDeals()
        {
            ParamentrsCollection.ClearItem();
            DealsCollection.dealsCollection.Clear();
        }
    }
}
