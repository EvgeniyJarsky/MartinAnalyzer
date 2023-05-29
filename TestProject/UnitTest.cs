using Report_BL.Controller.Func;
using Report_BL.Controller.MainInfo.MT4Tester;
using Report_BL.Func;
using Xunit.Sdk;

namespace TestProject
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void TestMethod()
        {
            string startDate = "02.01.2020 1:12:00";
            string endDate = "02.01.2020 13:31:00";
            string expected = "0 Дней | 12 Часов | 19 Минут";

            string actual = Dates.HowManyTimesBetween(startDate, endDate);

            Assert.AreEqual(expected, actual);

            startDate = "02.01.2020 3:22:00";
            endDate = "06.01.2020 13:14:00";
            expected = "4 Дней | 9 Часов | 52 Минут";// 3 ���� | 9 ����� | 52 �����

            actual = Dates.HowManyTimesBetween(startDate, endDate);

            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void FirstInfoMT4Tester()
        {
            string filePath = "F:\\!Coding\\C#\\MartinAnalyzer\\TestProject\\ReportExamples\\EURJPY_ENG.htm";

            var info = new Report_BL.ReportModel.FirstInfo(
                    filePath,
                    "MT4History",
                    new Dictionary<string, List<int>>(),
                    DateTime.MinValue,
                    DateTime.MinValue,
                    0);
            info.RobotName = "(EA) - Setka v1.43-ADX-IMP-R295-SR10";
            info.TimeFrame = "M1";
            info.StartDate = DateTime.Parse("02.01.2020 0:06:00");
            info.StartDeposit = 10000;
            info.EndDate = DateTime.Parse("13.01.2021 23:59:00");
            info.Profit = 4922.56f;
            info.DrawDown = 3669.94f;
            info.StartDeposit = 10000;

            var rezult = GetFirstInfoMT4.GetSymbolDateMagic(filePath);

            Assert.AreEqual(info.RobotName, rezult.RobotName);
            Assert.AreEqual(info.TimeFrame, rezult.TimeFrame);
            Assert.AreEqual(info.StartDate, rezult.StartDate);
            Assert.AreEqual(info.StartDeposit, rezult.StartDeposit);
            Assert.AreEqual(info.Profit, rezult.Profit);
            Assert.AreEqual(info.DrawDown, rezult.DrawDown);
            Assert.AreEqual(info.StartDeposit, rezult.StartDeposit);

        }

        [TestMethod]
        public void MainInfoMT4Tester()
        {
            string filePath = "F:\\!Coding\\C#\\MartinAnalyzer\\TestProject\\ReportExamples\\EURJPY_ENG.htm";
            string[] rez = new string[9]
            {
                "(EA) - Setka v1.43-ADX-IMP-R295-SR10",
                "EURJPY",
                "M1",
                "2020.01.02 00:06 - 2021.01.13 23:59",
                "10000.00",
                "4922.56",
                "3669.94 (28.32%)",
                "MT4Tester",
                "0"
            };

            var mainInfo = Report_BL.Controller.MainInfoMT4Tester.Get(filePath);

            Assert.AreEqual(rez[0], mainInfo[0]);
            Assert.AreEqual(rez[1], mainInfo[1]);
            Assert.AreEqual(rez[2], mainInfo[2]);
            Assert.AreEqual(rez[3], mainInfo[3]);
            Assert.AreEqual(rez[4], mainInfo[4]);
            Assert.AreEqual(rez[5], mainInfo[5]);
            Assert.AreEqual(rez[6], mainInfo[6]);
            Assert.AreEqual(rez[7], mainInfo[7]);
            Assert.AreEqual(rez[8], mainInfo[8]);

        }
    }
}