using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report_BL.Controller.GetDeals
{
    public static class CountDigits
    {
        /// <summary>
        /// Определяем количество знаков после запятой
        /// </summary>
        /// <param name="price"> цена 1,45896</param>
        /// <returns> int 4 or 5 or 3</returns>
        public static int Count(string price)
        {
            int digits = 0;
            digits = price.Split('.')[1].Length;
            return digits;
        }
    }
}
