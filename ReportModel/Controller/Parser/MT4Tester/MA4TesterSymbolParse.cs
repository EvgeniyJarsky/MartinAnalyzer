using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report_BL.Controller.Parser.MT4Tester
{
    /// <summary>
    /// Парсим имя символа из отчета МТ4
    /// </summary>
    public static class MA4TesterSymbolParse
    {
        /// <summary>
        /// Ключеая строка которая должна содержаться в
        /// строке отчета - там где есть имя символа
        /// </summary>
        const string keyLine = "<tr align=left><td colspan=2>";

        /// <summary>
        /// Парсим имя символа
        /// </summary>
        /// <param name="line">Строка из файла для парсинга</param>
        /// <returns>Возвращает либо имя символа либо Error</returns>
        public static string SymbolParse(string line)
        {
            string symbol = String.Empty;

            if(line.Contains(keyLine))
            {
                try
                {
                    symbol = line.Remove(0, 54).Split('(')[0].Trim();
                }
                catch(Exception e)
                {
                    symbol = String.Empty;
                    Console.WriteLine("Ошибка парсинга символа MT4Tester");
                    Console.WriteLine(e);
                }
            }
            return symbol;
        }
    }
}
