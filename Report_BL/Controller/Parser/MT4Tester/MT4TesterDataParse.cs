using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report_BL.Controller.Parser.MT4Tester
{
    public static class MT4TesterDataParse
    {
        /// <summary>
        /// Парсим строку с датами начала и конца тестирования.
        /// И заносим их в массив data
        /// </summary>
        /// <param name="line">Строка для парсинга периода тестирования</param>
        /// <returns>
        /// Массив: 0-й элемент это дата начала тестирования
        /// 1-й элемент это дата конца тестирования. В случае неуспешного
        /// парсинга возвращаем null и пишем лог
        /// </returns>
        public static DateTime[] DateParse(string line)
        {
            DateTime[] date = new DateTime[2];

            try
            {
                date[0] = DateTime.Parse(line.Split('(')[1].Split(')')[1].Trim().Split('<')[0].Split('-')[0]);
                date[1] = DateTime.Parse(line.Split('(')[1].Split(')')[1].Trim().Split('<')[0].Split('-')[1]);
                return date;
            }
            catch
            {
                //Logging.Logging.WriteLog("Ошибка чтения периода тестирования отчета Tester MT4");
                //Logging.Logging.WriteLog(line);
                //Logging.Logging.WriteLog(" ");

                return null;
            }
        }
    }
}
