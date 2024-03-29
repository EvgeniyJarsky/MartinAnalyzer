namespace Report_BL.Controller.GetDeals.HistoryMT4
{
    public static class ParseHistoryMT4Line
    {
        public static void ParseRez(string line, ref OrderStruct.Order orderStruct)
        {
            try
            {
                var parseRez = line.Split('>');

                if(DateTime.TryParse(parseRez[4].Split('<')[0], out DateTime dt))
                {
                    orderStruct.openDate   = DateTime.Parse(parseRez[4].Split('<')[0]);
                    orderStruct.sell_buy   = parseRez[6].Split('<')[0];
                    orderStruct.lot        = float.Parse(parseRez[8].Split('<')[0].Replace('.',','));
                    orderStruct.symbol     = parseRez[10].Split('<')[0].ToUpper();
                    orderStruct.openPrice  = float.Parse(parseRez[12].Split('<')[0].Replace('.',','));
                    orderStruct.closeDate  = DateTime.Parse(parseRez[18].Split('<')[0]);
                    orderStruct.closePrice = float.Parse(parseRez[20].Split('<')[0].Replace('.',','));
                    orderStruct.comission  = float.Parse(parseRez[22].Split('<')[0].Replace('.',','));
                    orderStruct.taxes      = float.Parse(parseRez[24].Split('<')[0].Replace('.',','));
                    orderStruct.swap       = float.Parse(parseRez[26].Split('<')[0].Replace('.',','));
                    orderStruct.profit     = float.Parse(parseRez[28].Split('<')[0].Replace('.',','));
                }
                else
                {
                    orderStruct.openDate   = DateTime.Parse(parseRez[5].Split('<')[0]);
                    orderStruct.sell_buy   = parseRez[7].Split('<')[0];
                    orderStruct.lot        = float.Parse(parseRez[9].Split('<')[0].Replace('.',','));
                    orderStruct.symbol     = parseRez[11].Split('<')[0].ToUpper();
                    orderStruct.openPrice  = float.Parse(parseRez[13].Split('<')[0].Replace('.',','));
                    orderStruct.closeDate  = DateTime.Parse(parseRez[19].Split('<')[0]);
                    orderStruct.closePrice = float.Parse(parseRez[21].Split('<')[0].Replace('.',','));
                    orderStruct.comission  = float.Parse(parseRez[23].Split('<')[0].Replace('.',','));
                    orderStruct.taxes      = float.Parse(parseRez[25].Split('<')[0].Replace('.',','));
                    orderStruct.swap       = float.Parse(parseRez[27].Split('<')[0].Replace('.',','));
                    orderStruct.profit     = float.Parse(parseRez[29].Split('<')[0].Replace('.',','));
                }

                orderStruct.profit     = orderStruct.profit + orderStruct.comission + orderStruct.taxes + orderStruct.swap;
            }
            catch (FormatException ex)
            {
                // int fg=0;
            }
        }
    }
}