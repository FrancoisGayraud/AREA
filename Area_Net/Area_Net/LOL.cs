using Google.Apis.Gmail.v1;
using Newtonsoft.Json;
using RiotSharp;
using RiotSharp.StaticDataEndpoint;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace Area_Net
{
    public class LOL
    { 
        public string summonerName { get; set; }
        public string userId { get; set; }
        public string From { get; set; }
        public string To { get; set; }

        public LOL(string from, string to, string id)
        {
            userId = id;
            From = from;
            To = to;
        }

        public void useLolApi()
        {
            var url = @"https://euw1.api.riotgames.com/lol/summoner/v3/summoners/by-name/" + summonerName + "?api_key=RGAPI-c2f9a714-35cf-43a3-b74b-02be9a4b942e";
            string JsonResponse;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                JsonResponse = reader.ReadToEnd();
            }
            JsonForLol Response = JsonConvert.DeserializeObject<JsonForLol>(JsonResponse);
            GMail gmail = new GMail();
            string[] Scopes = { GmailService.Scope.GmailSend };
            gmail.GmailMain(userId, Scopes);
            gmail.SendIt(From, To, "Your league data", "Your id : " + Response.id + "\nYour summoner level: " + Response.summonerLevel + "\nYour account ID : " + Response.accountId);
        }
    }


    public class JsonForLol
    {
        public int id { get; set; }
        public int accountId { get; set; }
        public string name { get; set; }
        public int profileIconId { get; set; }
        public long revisionDate { get; set; }
        public int summonerLevel { get; set; }
    }
}