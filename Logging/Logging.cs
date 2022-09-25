using System;
using System.IO;

namespace Logging
{
    public static class Logging
    {
        /// <summary>
        /// Записываем лог в файл \\Log\\log.txt
        /// При необходимости создаем нужный каталог и файл
        /// в корневой папке
        /// </summary>
        /// <param name="massege">Текст сообщения.</param>
        public static void WriteLog(string massege)
        {
            // curDir = "F:\\!Coding\\C#\\MartinAnalyzer\\Console_UI\\bin\\Debug"    
            string curDir = Directory.GetCurrentDirectory();
            string dirPath = curDir + "\\Log";
            string filePath = dirPath + "\\log.txt";
            if(!Directory.Exists(filePath))
                Directory.CreateDirectory(dirPath);
            using(StreamWriter sw = File.AppendText(filePath))
                sw.WriteLine($"{DateTime.Now} {massege}");
        }
    }
}
