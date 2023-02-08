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
        
        public static void AddNewItem(Report_BL.Controller.GetDeals.TesterMT4.ParseMT4Tester.Deal deal)
        {
            dealsCollection.Add(new Deal()
            {
                Number = Convert.ToInt32(deal.orderNumber),
                Symbol = deal.symbol,
                Date = deal.dateAndTimeOfDeal,
                Buy_Sell = deal.sell_buy,
                Direct = deal.direct,
                Lot = deal.lot,
                Price = deal.price,
                // Lot = double.Parse(param[5].Replace(',', '.'), CultureInfo.InvariantCulture),
                // Price = double.Parse(param[6].Replace(',', '.'), CultureInfo.InvariantCulture),
                //! убрать преобразование в стринг
                Profit = deal.profit.ToString(),
                Balance = deal.balance.ToString()
            });
        }
    }
}
