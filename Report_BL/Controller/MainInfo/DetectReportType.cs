using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report_BL.Controller.MainInfo
{
    /// <summary>
    /// Определяем тип выбранного файла
    /// </summary>
    public static class DetectReportType
    {
        /// <summary>
        /// получаем тип выбранного файла по расширеню файла
        /// </summary>
        /// <param name="filePath">Путь к файлу.</param>
        /// <returns> string MyFXBook || MT5 || MT4Tester || MT4History || UnKnownFile </returns>
        public static string GetReportType(string filePath)
        {
            string fileExtension = Path.GetExtension(filePath);

            if (fileExtension == ".csv")
                return "MyFXBook";
            if (fileExtension == ".html")
                return "MT5";
            // Определим какой это отчет: Tester или History
            if (fileExtension == ".htm")
            {
                foreach(string line in File.ReadLines(filePath))
                {
                    if(line.Contains("Strategy Tester Report"))
                        return "MT4Tester";
                    if (line.Contains("Closed Transactions:"))
                        return "MT4History";
                }
            }
                return "UnKnownFile";
        }
        
        public static int GetBuildMT4(string filePath)
        {
            using (StreamReader sr = new StreamReader(filePath))
            {
                string? line = String.Empty;
                while ((line = sr.ReadLine()) != null)
                {
                    if (!line.Contains("Build"))
                    {
                        continue;
                    }
                    // TODO  нужна обработка исключений
                    string rezString = line.Split('"')[3].Split(' ')[1];
                    //   Console.WriteLine(line.Split("("));
                    int rez = Int16.Parse(rezString);
                    return rez;
                }
                return 0;
            }
        }
    }
}
