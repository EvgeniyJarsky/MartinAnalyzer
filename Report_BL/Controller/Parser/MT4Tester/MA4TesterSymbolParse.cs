using System;

namespace Report_BL.Controller.Parser.MT4Tester
{
    /// <summary>
    /// Парсим имя символа из отчета МТ4
    /// </summary>
    public static class MA4TesterSymbolParse
    {
        /// <summary>
        /// Парсим имя символа
        /// </summary>
        /// <param name="line">Строка из файла для парсинга</param>
        /// <returns>Возвращает либо имя символа либо Null</returns>
        public static string? SymbolParse(string line)
        {
            try
            {
                return line.Remove(0, 54).Split('(')[0].Trim();
            }
            catch(Exception)
            {
                //Logging.Logging.WriteLog("Ошибка чтения символа отчета Tester MT4");
                //Logging.Logging.WriteLog(line);
                //Logging.Logging.WriteLog(" ");

                return null;
            }
            
        }
    }
}
