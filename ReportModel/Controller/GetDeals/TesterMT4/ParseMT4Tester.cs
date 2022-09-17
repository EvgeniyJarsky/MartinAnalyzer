using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report_BL.Controller.GetDeals.TesterMT4
{
    public static class ParseMT4Tester
    {
        /// <summary>
        /// Парсим строку с информацияей по сделке
        /// </summary>
        /// <param name="line">Cтрока из отчета которую надо парсить</param>
        /// <param name="symbol">Символ</param>
        /// <returns>массив string[Number|Symbol|Date|Buy_Sell|Direct|Lot|Price|Profit|Balance]
        /// </returns>
        public static string[] ParseDealsMT4Tester(string line, string symbol)
        {
            string[] result = new string[9];

            string openDate = line.Split('>')[4].Split('<')[0]; // open date
            string sell_buy = line.Split('>')[6].Split('<')[0]; // sell/buy
            string orderNumber = line.Split('>')[8].Split('<')[0];// order number
            string lot = line.Split('>')[10].Split('<')[0]; // lot
            string price = line.Split('>')[12].Split('<')[0]; // open/close price
            string sL = line.Split('>')[14].Split('<')[0]; // s/l
            string tP = line.Split('>')[16].Split('<')[0]; // t/p
            string profit = line.Split('>')[18].Split('<')[0]; // profit
            string balance = line.Split('>')[20].Split('<')[0]; // balance

            string direct = "close";
            if (sell_buy == "sell" || sell_buy == "buy")
            {
                direct = "open";
            }
            result[0] = orderNumber;
            result[1] = symbol;
            result[2] = openDate;
            result[3] = sell_buy;
            result[4] = direct;
            result[5] = lot;
            result[6] = price;
            result[7] = profit;
            result[8] = balance;

            return result;
        }
    }
}
