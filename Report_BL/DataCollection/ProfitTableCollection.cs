using Report_BL.ReportModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report_BL.DataCollection
{
    public static class ProfitTableCollection
    {
        public static ObservableCollection<ProfitTable> profitTable = new ObservableCollection<ProfitTable>();
    }
}
