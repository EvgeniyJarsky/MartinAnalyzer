using Report_BL.ReportModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
                Number   = param[0],
                Symbol   = param[1],
                Date     = param[2],
                Buy_Sell = param[3],
                Direct   = param[4],
                Lot      = param[5],
                Price    = param[6],
                Profit   = param[7],
                Balance  = param[8]
            });
        }
    }
}
