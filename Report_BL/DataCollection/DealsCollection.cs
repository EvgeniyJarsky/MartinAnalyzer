using Report_BL.ReportModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report_BL.DataCollection
{
    /// <summary>
    /// Коллекции для сбора итоговых данных
    /// </summary>
    public static class DealsCollection
    {
        /// <summary>
        /// Коллекция объектов "Сделка" с основными переметрами - время открытия/закрытия, тип ордера, лот и т.д.
        /// </summary>
        public static ObservableCollection<Deal> dealsCollection = new ObservableCollection<Deal>();
        
        public static void AddNewItem(string[] param)
        {
            dealsCollection.Add(new Deal()
            {
                Number = int.Parse(param[0]),
                Symbol = param[1],
                Date = DateTime.Parse(param[2]),
                Buy_Sell = param[3],
                Direct = param[4],
                Lot = double.Parse(param[5].Replace(',', '.'), CultureInfo.InvariantCulture),
                Price = double.Parse(param[6].Replace(',', '.'), CultureInfo.InvariantCulture),
                Profit = param[7],
                Balance = param[8]
            });
        }
    }
}
