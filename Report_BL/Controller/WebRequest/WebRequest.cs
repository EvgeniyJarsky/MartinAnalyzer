using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace Report_BL.Controller.MyWebRequest_
{
    /*
        var request = new GetRequest("https://scripts.tlap.com/quotes.php?q=AUDCAD");
        request.Run();
        var rez = request.Response;
    */
    public class GetRequest
    {
        HttpWebRequest _request;
        string _address;

        public string Response { get; set; }

        public GetRequest(string address)
        {
            _address = address;
        }

        
        public void Run()
        {
            _request = (HttpWebRequest)WebRequest.Create(_address);
            
            _request.Method = "GET";

            try
            {
                HttpWebResponse response = (HttpWebResponse)_request.GetResponse();
                var stream = response.GetResponseStream();
                if (stream != null) Response = new StreamReader(stream).ReadToEnd();
            }
            catch(Exception ex)
            {
            }

            
        }
    }
}