using System.Linq;
using System.Collections.ObjectModel;


using Report_BL.ReportModel;


namespace Report_BL.Controller.GetDeals.HistoryMT4
{
    // Парсим сделки
    public static class GetDealsHistoryMT4
    {
        public static void GetDaels(NewReport report)
        {
            string[] keywordsMT4 = { ">buy<", ">sell<"};
            int digits = 0;
            int countOrder = 0;
            double balance = 0;
            OrderStruct.Order order = new OrderStruct.Order();

            var dealList = new ObservableCollection<Deal>();
            

            using(StreamReader sr = new StreamReader(report.FilePath))
            {
                string? line;
                while((line = sr.ReadLine()) != null)
                {
                    if(line.Contains("Open Trades:")) break;

                    if(line.Contains("buy") || line.Contains("sell"))
                    {
                        if(line.Contains(report.Symbol.ToLower()))
                        {
                            string lineToParse = line;
                            string? newLine = sr.ReadLine();
                            if(newLine.Contains(report.Magic.ToString()))
                            {
                                ParseHistoryMT4Line.ParseRez(line, ref order);
                                if(order.lot != 0) // распрсили строку успешно
                                {
                                    countOrder++;

                                    var newOrder = new Report_BL.ReportModel.Deal();

                                    newOrder.Number = countOrder;
                                    newOrder.Symbol = order.symbol;
                                    newOrder.Buy_Sell = order.sell_buy;
                                    newOrder.Date = order.openDate;
                                    newOrder.Price = order.openPrice;
                                    newOrder.Lot = order.lot;
                                    newOrder.Profit = 0;
                                    newOrder.Direct = "open";
                                    newOrder.Balance = (float)balance;

                                    // Report_BL.DataCollection.DealsCollection.dealsCollection.Add(newOrder);
                                    dealList.Add(newOrder);

                                    newOrder = new Report_BL.ReportModel.Deal();

                                    newOrder.Number = countOrder;
                                    newOrder.Symbol = order.symbol;
                                    newOrder.Buy_Sell = order.sell_buy;
                                    newOrder.Date = order.closeDate;
                                    newOrder.Price = order.closePrice;
                                    newOrder.Lot = order.lot;
                                    newOrder.Profit = order.profit;
                                    newOrder.Direct = "close";
                                    newOrder.Balance = (float)(balance + order.profit);

                                    // Report_BL.DataCollection.DealsCollection.dealsCollection.Add(newOrder);
                                    dealList.Add(newOrder);


                                    order = new OrderStruct.Order();

                                }

                            }
                        }
                    }
                }
            }

            // TODO! Сортировка пузырьком переделать на более быструю
            for(int i =0; i<dealList.Count(); i++)
            {
                for(int j=i+1; j<dealList.Count(); j++)
                {
                    if(dealList[i].Date > dealList[j].Date)
                    {
                        var temp = dealList[i];
                        dealList[i] = dealList[j];
                        dealList[j] = temp;
                    }
                }
            }
            foreach(var ord in dealList)
                Report_BL.DataCollection.DealsCollection.dealsCollection.Add(ord);

        }
    }

}