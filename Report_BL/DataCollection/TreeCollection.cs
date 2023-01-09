using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report_BL.DataCollection
{
    /// <summary>
    /// Коллекция биндиться с деревом сеток
    /// </summary>
    public static class TreeCollection
    {
        public static ObservableCollection<Report_BL.ReportModel.TreeViewClass> grid =
            new ObservableCollection<Report_BL.ReportModel.TreeViewClass>();
    }
}
