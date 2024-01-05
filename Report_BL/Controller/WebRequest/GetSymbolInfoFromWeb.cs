using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace Report_BL.Controller.WebRequest_
{
    public class CurrentSymbolPriceFromWeb
    {
        // Тип цены ask/bid
        private string priceType = "ask";
        // Имя символа.
        private string symbolName = String.Empty;
        public string PriceType { get; set; }

        public string SymbolName {private get{return symbolName;} set {symbolName = value;}}

        // Формируем веб ссылку для запроса информации о symbol
        private string WebPath()
        {
            const string webPath = "https://scripts.tlap.com/quotes.php?q=";
            string sf = webPath + this.symbolName;
            return webPath + this.symbolName;
        }

        // Web запрос информации о symbol
        private string StringJson()
        {
            var request = new Report_BL.Controller.MyWebRequest_.GetRequest(this.WebPath());
            request.Run();
            return request.Response.ToString();
        }

        // Парсим json строку и выбираем строку с ценой типа ask или bid (по умолчанию ask = priceType)
        public string Get()
        {
            //string result = "[{\"symbol\":\"AUDCAD\",\"digits\":4,\"ask\":0.8957,\"bid\":0.8949,\"change\":-0.0001,\"lasttime\":1704405642,\"change24h\":-0.0036}]";
            using JsonDocument doc = JsonDocument.Parse(this.StringJson());
            JsonElement root = doc.RootElement;

            return root[0].GetProperty(this.priceType).ToString();
        }
    }
}
