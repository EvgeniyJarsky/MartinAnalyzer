namespace Report_BL.Func
{
    public static class TwoDates
    {
        public static string HowManyTimesBetween(string startDate_, string endDate_)
        {
            bool tryParseStartDate = DateTime.TryParse(startDate_, out DateTime startDate);
            bool tryParseEndDate = DateTime.TryParse(endDate_, out DateTime endDate);
        
            // Если парсинг лубой даты неуспешен  - возвращаем пустую строку
            if(!tryParseStartDate || !tryParseEndDate)
                return "Parsing error";       
            // Если дата начала больше чем дата конца
            if(startDate > endDate)
                return "error date";

            double days = Math.Round(Math.Abs((endDate - startDate).TotalDays), MidpointRounding.ToNegativeInfinity);

            //Дата без целых дней
            DateTime dateWithOutDays =  startDate.AddDays(days);
            double hours = Math.Round(Math.Abs((endDate - dateWithOutDays).TotalHours), 0);

            DateTime dateWithOutHours = dateWithOutDays.AddHours(hours);
            double minutes = (endDate - dateWithOutHours).Minutes;

            string rez = $"{days} Дней | {hours} Часов | {minutes} Минут";

             return rez.ToString();
        }
    }
}
