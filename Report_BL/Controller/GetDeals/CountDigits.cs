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
        public static int Count(float price)
        {
            int digits = 0;
            // пришлось добавить еще один знак ',' так как при получении значении цены 118
            // Split говорит что 1 элемента нет
            //! Можно попробовать Try Catch!!!!!!!!!!!!!!!!!
            digits = (price.ToString() + ',').Split(',')[1].Length;
            return digits;
        }
    }
}
