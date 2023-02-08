using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Report_BL.Controller.GetDeals.TesterMT4
{
    public static class ParseMT4Tester
    {
        public struct Deal
        {
            public int orderNumber;
            public string symbol;
            public DateTime dateAndTimeOfDeal;
            public string sell_buy;
            public string direct;
            public float lot;
            public float price;
            public float profit;
            public float balance;

            public Deal
                (int orderNumber,
                string symbol,
                 DateTime dateAndTimeOfDeal,
                 string sell_buy,
                 string direct,
                 float lot,
                 float price,
                 float profit,
                 float balance
                 )
            {
                this.orderNumber = orderNumber;
                this.symbol = symbol;
                this.dateAndTimeOfDeal = dateAndTimeOfDeal;
                this.sell_buy = sell_buy;
                this.direct = direct;
                this.lot = lot;
                this.price = price;
                this.profit = profit;
                this.balance = balance;

            }
        }
        
        /// <summary>
        /// Парсим строку с информацияей по сделке
        /// </summary>
        /// <param name="line">Cтрока из отчета которую надо парсить</param>
        /// <param name="symbol">Символ</param>
        /// <returns>массив string[Number|Symbol|Date|Buy_Sell|Direct|Lot|Price|Profit|Balance]
        /// </returns>
        public static Deal ParseDealsMT4Tester(string line, string symbol)
        {
            string dateTime_str     = line.Split('>')[4].Split('<')[0]; // open date
            string sell_buy_str     = line.Split('>')[6].Split('<')[0]; // sell/buy
            string orderNumber_str = line.Split('>')[8].Split('<')[0];// order number
            string lot_str          = line.Split('>')[10].Split('<')[0]; // lot
            string price_str       = line.Split('>')[12].Split('<')[0]; // open/close price
            string sL_str           = line.Split('>')[14].Split('<')[0]; // s/l
            string tP_str           = line.Split('>')[16].Split('<')[0]; // t/p
            string profit_str       = line.Split('>')[18].Split('<')[0]; // profit
            string balance_str      = line.Split('>')[20].Split('<')[0]; // balance

            string direct_str = "close";
            if (sell_buy_str == "sell" || sell_buy_str == "buy")
            {
                direct_str = "open";
            }

            int number     = int.Parse(orderNumber_str);
            string sym     = symbol;
            DateTime dt    = DateTime.Parse(dateTime_str);
            string sell_buy     = sell_buy_str;
            //
            float lot     = float.Parse(lot_str.Replace(',','.'), CultureInfo.InvariantCulture);
            float price   = float.Parse(price_str.Replace(',','.'), CultureInfo.InvariantCulture);
            
            if(profit_str == "") profit_str = "0";
                float profit  = float.Parse(profit_str.Replace(',','.'), CultureInfo.InvariantCulture);
            if(balance_str == "") balance_str = "0";
                float balance = float.Parse(balance_str.Replace(',','.'), CultureInfo.InvariantCulture);

            Deal newDeal = new Deal{
                orderNumber       = number,
                symbol            = symbol,
                dateAndTimeOfDeal = DateTime.Parse(dateTime_str),
                sell_buy          = sell_buy,
                direct            = direct_str,
                lot               = lot,
                price             = price,
                profit            = profit,
                balance           = balance
            };

            return newDeal;
        }
    }
}
