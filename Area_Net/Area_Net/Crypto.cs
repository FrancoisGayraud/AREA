using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace Crypto
{
    class CryptoAPI
    {
        public string Price { get; set; }
        public int Market { get; set; }
        public void CryptoMain(string currency)
        {
            string JsonResponse = string.Empty;
            string url = @"https://min-api.cryptocompare.com/data/pricemultifull?fsyms=" + currency + "&tsyms=USD";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                JsonResponse = reader.ReadToEnd();
            }
            JsonClass Response = JsonConvert.DeserializeObject<JsonClass>(JsonResponse);
            if (currency.Equals("BTC"))
            {
                Price = Response.RAW.BTC.USD.PRICE.ToString();
            }
            else if (currency.Equals("ETH"))
            {
                Price = Response.RAW.ETH.USD.PRICE.ToString();
            }
            else if (currency.Equals("LSK"))
            {
                Price = Response.RAW.LSK.USD.PRICE.ToString();
            }
            else if (currency.Equals("DSH"))
            {
                Price = Response.RAW.DSH.USD.PRICE.ToString();
            }

        }
        public bool IsTriggered(string TriggerData)
        {
            TriggerData = TriggerData.Trim();
            string[] tokens = TriggerData.Split(' ');
            if (tokens.Length != 3)
                return false;   
            CryptoMain(tokens[0]);
            int TriggerPrice = int.Parse(tokens[2]);
            int CurrentPrice = int.Parse(Regex.Match(Price, @"\d+").Value);
            if (tokens[0] == "BTC" || tokens[0] == "LSK" || tokens[0] == "ETH" || tokens[0] == "DSH")
            {
                if (tokens[1] == "<" && (CurrentPrice <= TriggerPrice))
                    return true;
                else if (tokens[1] == ">" && (CurrentPrice >= TriggerPrice))
                    return true;
                else
                    return false;
            }
            else
                return false;
        }
    }
    public class USD
    {
        public string TYPE { get; set; }
        public string MARKET { get; set; }
        public string FROMSYMBOL { get; set; }
        public string TOSYMBOL { get; set; }
        public string FLAGS { get; set; }
        public double PRICE { get; set; }
        public int LASTUPDATE { get; set; }
        public double LASTVOLUME { get; set; }
        public double LASTVOLUMETO { get; set; }
        public string LASTTRADEID { get; set; }
        public double VOLUMEDAY { get; set; }
        public double VOLUMEDAYTO { get; set; }
        public double VOLUME24HOUR { get; set; }
        public double VOLUME24HOURTO { get; set; }
        public double OPENDAY { get; set; }
        public double HIGHDAY { get; set; }
        public double LOWDAY { get; set; }
        public double OPEN24HOUR { get; set; }
        public double HIGH24HOUR { get; set; }
        public double LOW24HOUR { get; set; }
        public string LASTMARKET { get; set; }
        public double CHANGE24HOUR { get; set; }
        public double CHANGEPCT24HOUR { get; set; }
        public double CHANGEDAY { get; set; }
        public double CHANGEPCTDAY { get; set; }
        public float SUPPLY { get; set; }
        public double MKTCAP { get; set; }
        public double TOTALVOLUME24H { get; set; }
        public double TOTALVOLUME24HTO { get; set; }
    }

    public class BTC
    {
        public USD USD { get; set; }
    }

    public class ETH
    {
        public USD USD { get; set; }
    }

    public class LSK
    {   
        public USD USD { get; set; }
    }

    public class DSH
    {
        public USD USD { get; set; }
    }

    public class RAW
    {
        public BTC BTC { get; set; }
        public ETH ETH { get; set; }
        public LSK LSK { get; set; }
        public DSH DSH { get; set; }
    }

    public class USD2
    {
        public string FROMSYMBOL { get; set; }
        public string TOSYMBOL { get; set; }
        public string MARKET { get; set; }
        public string PRICE { get; set; }
        public string LASTUPDATE { get; set; }
        public string LASTVOLUME { get; set; }
        public string LASTVOLUMETO { get; set; }
        public string LASTTRADEID { get; set; }
        public string VOLUMEDAY { get; set; }
        public string VOLUMEDAYTO { get; set; }
        public string VOLUME24HOUR { get; set; }
        public string VOLUME24HOURTO { get; set; }
        public string OPENDAY { get; set; }
        public string HIGHDAY { get; set; }
        public string LOWDAY { get; set; }
        public string OPEN24HOUR { get; set; }
        public string HIGH24HOUR { get; set; }
        public string LOW24HOUR { get; set; }
        public string LASTMARKET { get; set; }
        public string CHANGE24HOUR { get; set; }
        public string CHANGEPCT24HOUR { get; set; }
        public string CHANGEDAY { get; set; }
        public string CHANGEPCTDAY { get; set; }
        public string SUPPLY { get; set; }
        public string MKTCAP { get; set; }
        public string TOTALVOLUME24H { get; set; }
        public string TOTALVOLUME24HTO { get; set; }
    }

    public class BTC2
    {
        public USD2 USD { get; set; }
    }

    public class DISPLAY
    {
        public BTC2 BTC { get; set; }
        public ETH2 ETH { get; set; }
        public LSK2 LSK { get; set; }
        public DSH2 DSH2 { get; set; }
    }

    public class JsonClass
    {
        public RAW RAW { get; set; }
        public DISPLAY DISPLAY { get; set; }
    }

    public class DSH2
    {
        public USD2 USD { get; set; }
    }

    public class ETH2
    {
        public USD2 USD { get; set; }
    }

    public class LSK2
    {
        public USD2 USD { get; set; }
    }

}