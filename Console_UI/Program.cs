using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Console_UI
{
    internal class Program
    {
        delegate void FirstAndSecond();


        static void MainMethod(FirstAndSecond func)
        {
            Console.WriteLine("Open db");
            func();
            Console.WriteLine("Close db");
        }

        static void Main(string[] args)
        {
            #region Пути к файлам
            // Путь к файлу отчета МТ4 тестер
            string filePath = "F:\\!Coding\\C#\\MartinAnalyzer\\ReportExamples\\EURJPY_ENG.htm";

            //Путь к файлу МТ4 History
            //string filePath = "F:\\!Coding\\C#\\MartinAnalyzer\\ReportExamples\\MT4_History.htm";
            #endregion

            #region Пишем лог
            //Logging.Logging.WriteLog("massege");
            #endregion

            #region Работа с делегатами
            void WriteMessage1() => Console.WriteLine("Delegate Start");

            FirstAndSecond delegateMy;
            delegateMy = WriteMessage1;

            MainMethod(delegateMy);

            #endregion

            #region Прообразование строки 0.01 в double
            string number = "0.01";
            double rezult = double.Parse(number.Replace(',', '.'), CultureInfo.InvariantCulture);
            Console.WriteLine(rezult);
            #endregion

            Console.ReadLine();
        }
    }
}
