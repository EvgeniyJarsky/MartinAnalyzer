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
            int digits = 0; //количество знаков после запятой.
            int countOrder = 0;
            double balance = 0;
            OrderStruct.Order order = new OrderStruct.Order();

            var dealList = new ObservableCollection<Deal>();

            // Читаем файл
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

                            if(IsMagicCorrect(line, report.Magic))
                            {
                                ParseHistoryMT4Line.ParseRez(line, ref order);
                                if(order.lot != 0) // распрсили строку успешно
                                {
                                    #region Определяем максимальное количество знаков после запятой
                                    int currentDigit = Report_BL.Controller.GetDeals.CountDigits.Count(order.openPrice);
                                    digits = Math.Max(digits, currentDigit);
                                    #endregion
                                    
                                    countOrder++;

                                    #region Запоминаем открытие позиции
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
    
                                        dealList.Add(newOrder);
                                    #endregion

                                    #region Запоминаем закрытие позиции
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
    
                                        dealList.Add(newOrder);
                                    #endregion

                                    // Обнуляем информацию
                                    order = new OrderStruct.Order();

                                }

                            }
                        }
                        report.Digits = digits;
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

            // Ситуация когда одним временем закрывается сетка из одного ордера и сразу же 
            // открывается новая сетка - необходимо чтобы сначала шел ордера закрытия а потом уже 
            // шел ордер отрытия новой сетки

            ObservableCollection<Deal>? dealList1 = new ObservableCollection<Deal>();

            var deal = new Deal();
            deal = null;
            
            // Одновременно определим максимальный номер ордера
            // что бы потом исправить нумерацию ордеров - сделать по порядку
            int orderNumber = 0;

            for(int i =0; i<dealList.Count(); i++)
            {

                orderNumber = Math.Max(orderNumber, dealList[i].Number);

                // Если это первый ордет => пропускаем
                if(deal == null)
                {
                    deal = new Deal();
                    deal = dealList[i];

                    dealList1.Add(deal);

                    continue;
                }
                else
                {
                    deal = dealList[i-1];
                }

                if(deal.Date == dealList[i].Date)
                {
                    if(deal.Direct == "open" && dealList[i].Direct == "close")
                    {
                        dealList1.Remove(deal);
                        dealList1.Add(dealList[i]);
                        dealList1.Add(deal);
                    }
                    else
                    {
                        //dealList1.Add(deal);
                        dealList1.Add(dealList[i]);
                    }
                }
                else
                {
                    //dealList1.Add(deal);
                    dealList1.Add(dealList[i]);
                }
                
            }


            #region Выстраиваем нумерацию ордеров
                foreach (var ord in dealList1)
                {
                    ord.Number = orderNumber - ord.Number + 1;    
                }
            #endregion

            foreach(var ord in dealList1)
                Report_BL.DataCollection.DealsCollection.dealsCollection.Add(ord);
        }
        /*
        Проверка что в строке line сщдержится magic
        Можно попробовать line.Contains(magic)!?
        */
        private static bool IsMagicCorrect(string line, int magic)
        {
            int parseMadgic = 0;;
            bool rezult = false;
            try
            {
                parseMadgic = Convert.ToInt32(line.Split('#')[1].Split(' ')[0]);
            }
            catch(FormatException ex)
            {
                parseMadgic = Convert.ToInt32(line.Split('#')[2].Split(' ')[0]);
            }
            finally
            {
                if(parseMadgic == magic)
                    rezult = true;
            }
            return rezult;
        }
    }

}