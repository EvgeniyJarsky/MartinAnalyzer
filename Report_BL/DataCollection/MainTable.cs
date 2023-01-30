using System.Collections.ObjectModel;


namespace Report_BL.DataCollection
{
    public static class MainTable
    {
        public static ObservableCollection<Report_BL.ReportModel.MainTable> mainTable =
            new ObservableCollection<Report_BL.ReportModel.MainTable>();

        // Возвращаем из коллекции Главной таблицы строку с заданным кол-вом колен в сетке
        // Если такой строки нет - создаем новую и возвращаем ее
        public static Report_BL.ReportModel.MainTable GetRow(int gridCount)
        {
            foreach(var item in mainTable)
            {
                if(item.countOrders == gridCount)
                {
                    return item;
                }
            }
            var newRow = new Report_BL.ReportModel.MainTable();
            newRow.countOrders = gridCount;
            mainTable.Add(newRow);

            return newRow;
        }
    }
}