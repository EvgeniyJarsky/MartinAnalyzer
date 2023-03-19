namespace Report_BL.Controller.GetDeals.HistoryMT4
{
    public class OrderStruct
    {
        public struct Order
        {
            public DateTime openDate;
            public string sell_buy;
            public float lot;
            public float openPrice;
            public string symbol;
            public float _openPrice;
            public DateTime closeDate;
            public float closePrice;
            public float comission;
            public float taxes;
            public float swap;
            public float profit;
        }
    }
}
