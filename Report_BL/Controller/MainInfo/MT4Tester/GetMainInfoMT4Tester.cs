using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report_BL.Controller
{
    public class MainInfoMT4Tester
    {
        /// <summary>
        /// Собираем главную информацию из отчета
        /// </summary>
        /// <param name="filePath">Путь к файлу</param>
        /// <returns> масссив параметров string  rez[9]</returns>
        
        public static string[] Get(string filePath)
        {
            #region Поля
            /// <summary>
            /// Имя робота
            /// </summary>
            string expertName = String.Empty;

            /// <summary>
            /// Символ
            /// </summary>
            string curencyName = String.Empty;
            /// <summary>
            /// Таймфрейм
            /// </summary>
            string timeFrame = String.Empty;

            /// <summary>
            /// Период тестирования
            /// </summary>
            string testPeriod = String.Empty;

            /// <summary>
            /// Начальный депозит
            /// </summary>
            string startDeposit = String.Empty;

            /// <summary>
            /// Суммарная прибыль
            /// </summary>
            string profit = String.Empty;

            /// <summary>
            /// Максимальная просадка
            /// </summary>
            string drawDown = String.Empty;

            /// <summary>
            /// Тип отчета - определен сразу
            /// </summary>
            string reportType = "MT4Tester";

            /// <summary>
            /// Меджик равен 0 т.к. в отчете торги одной парой
            /// </summary>
            string magic = "0";
            #endregion
            
            try
            {
                using (StreamReader? sr = new StreamReader(filePath))
                {
                    int count = 0;// счетчик что бы отловить 2-ю строку
                    string? line;
                    // Read and display lines from the file until the end of
                    // the file is reached.
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (curencyName == "")
                        {
                            if (line.Contains("<tr align=left><td colspan=2>"))
                            {
                                try
                                {
                                    curencyName = line.Remove(0, 54).Split('(')[0].Trim();
                                    continue;
                                }
                                catch (Exception)
                                {
                                    curencyName = "Read Error";
                                    continue;
                                }
                            }
                        }
                        if (timeFrame == "")
                        {
                            if (line.Contains("<tr align=left><td colspan=2>"))
                            {
                                // "<tr align=left><td colspan=2>������</td><td colspan=4>15 ����� (M15)  2012.01.03 01:00 - 2020.02.20 01:45</td></tr>"
                                try
                                {
                                    timeFrame = line.Split('(')[1].Split(')')[0];
                                    testPeriod = line.Split('(')[1].Split(')')[1].Trim().Split('<')[0];
                                    continue;
                                }
                                catch (Exception)
                                {
                                    testPeriod = "Read Error";
                                    timeFrame = "Read Error";
                                    continue;
                                }
                            }
                        }
                        //Чтобы не проверять до конца файла, если нашни expertName то строчку "<title>Strategy Tester:" не ищем
                        if (expertName == "")
                        {
                            if (line.Contains("<title>Strategy Tester:"))
                            {
                                // определили название эксперта
                                try
                                {
                                    expertName = line.Remove(0, 28).Split('<')[0];
                                    continue;
                                }
                                catch
                                {
                                    expertName = "Read Error";
                                    continue;
                                }
                            }
                        }
                        if (startDeposit == "")
                        {
                            if (line.Contains("</td><td></td><td align=right></td><td>"))
                            {
                                count += 1;
                                if (count == 2)
                                {
                                    // line = sr.ReadLine();
                                    // line = sr.ReadLine();
                                    try
                                    {
                                        startDeposit = line.Split('>')[4].Split('<')[0];
                                        continue;
                                    }
                                    catch
                                    {
                                        startDeposit = "Read Error";
                                        continue;
                                    }
                                }

                            }
                        }
                        if (profit == "")
                        {
                            if (count == 2)
                            {
                                try
                                {
                                    profit = line.Split('>')[4].Split('<')[0];
                                    continue;
                                }
                                catch
                                {
                                    profit = "Read Error";
                                    continue;
                                }
                            }
                        }
                        // todo надо упростить - использовать прочтение след. строки
                        if (drawDown == "")
                        {
                            if (count == 2)
                            {
                                count += 1;
                                continue;
                            }
                            if (count == 3)
                            {
                                try
                                {
                                    drawDown = line.Split('>')[8].Split('<')[0];
                                    break;
                                }
                                catch
                                {
                                    drawDown = "Read Error";
                                    break;
                                }

                            }

                        }
                    }
                }
            }
            catch (Exception e)
            {
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            string[] rez = new string[9];
            rez[0] = expertName;
            rez[1] = curencyName;
            rez[2] = timeFrame;
            rez[3] = testPeriod;
            rez[4] = startDeposit;
            rez[5] = profit;
            rez[6] = drawDown;
            rez[7] = reportType;
            rez[8] = magic;

            return rez;
        }
    }
}
