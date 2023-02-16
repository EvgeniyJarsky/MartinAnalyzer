using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report_BL.Controller.Func
{
    public static class Dates
    {
        public static string HowManyTimesBetween(string startDate_, string endDate_)
        {
            bool tryParseStartDate = DateTime.TryParse(startDate_, out DateTime startDate);
            bool tryParseEndDate = DateTime.TryParse(endDate_, out DateTime endDate);

            // Если парсинг лубой даты неуспешен  - возвращаем пустую строку
            if (!tryParseStartDate || !tryParseEndDate)
                return String.Empty;
            // Если дата начала больше чем дата конца
            if (startDate > endDate)
                return String.Empty;
            
            double days = Math.Round(Math.Abs((endDate - startDate).TotalDays), MidpointRounding.ToNegativeInfinity);

            //Дата без целых дней
            var sds =  startDate.Date;
            DateTime dateWithOutDays = startDate.AddDays(days);
            double hours = Math.Round(Math.Abs((endDate - dateWithOutDays).TotalHours), MidpointRounding.ToNegativeInfinity);

            DateTime dateWithOutHours = dateWithOutDays.AddHours(hours);
            double minutes = (endDate - dateWithOutHours).Minutes;

            

            string rez = $"{days} Дней | {hours} Часов | {minutes} Минут";

            return rez.ToString();
        }
    }
}
