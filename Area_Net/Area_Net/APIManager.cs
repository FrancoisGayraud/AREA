using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading;
using System.Timers;
using Crypto;
using Google.Apis.Gmail.v1;
using Twitch;

namespace Area_Net
{
    public class APIManager
    {
        public static Boolean botRunning = false;
        public static void StartManaging()
        {
            Thread APIThread = new Thread(APIThreadWork);
            APIThread.Start();
        }
     
        public static void APIThreadWork()
        {
            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = 60000;
            aTimer.Enabled = true;  
        }
        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("I'm in OnTimedEvent !!! Going to get Actions Array");
            List<Action> Actions = GetActions();
            foreach (Action Action in Actions)
            {
                System.Diagnostics.Debug.WriteLine(Action.Name + Action.ActionAPI + Action.TriggerAPI + Action.ActionData + Action.TriggerData);
                HandleAction(Action);
            }
        }
        private static void HandleAction(Action Action)
        {
            bool Triggered = false;
            if (Action.TriggerAPI == "Crypto")
            {
                Crypto.CryptoAPI Crypto = new CryptoAPI();
                Triggered = Crypto.IsTriggered(Action.TriggerData);
                if (Triggered)
                    DoAction(Action);
            }
            else if (Action.TriggerAPI == "Twitch")
            {
                if (!botRunning)
                {
                    string[] tokens = Action.ActionData.Split(' ');
                    TwitchApi twitch = new TwitchApi(Action.TriggerData, tokens[0], tokens[1], Action.UserId);
                    Thread TwitchThread = new Thread(twitch.initTwitchLoop);
                    TwitchThread.Start();
                    DoAction(Action);
                    botRunning = true;
                }
            }
            else if (Action.TriggerAPI == "League")
            {
                int tmp;
                string[] tokens = Action.TriggerData.Split(' ');
                string[] data = Action.ActionData.Split(' ');
                LOL lol = new LOL(data[0], data[1], Action.UserId);
                lol.summonerName = tokens[0];
                lol.useLolApi();
            }
            else if (Action.TriggerAPI == "Reddit")
            {
                string[] tokens = Action.TriggerData.Split(' ');
                string[] data = Action.ActionData.Split(' ');
                Reddit reddit = new Reddit(data[0], data[1], Action.UserId);
                reddit.subName = tokens[0];
                reddit.initReddit();
            }
        }
        private static void DoAction(Action Action)
        {
            if (Action.ActionAPI == "GMail")
            {
                System.Diagnostics.Debug.WriteLine("AN ACTION WAS TRIGGERED. TRYING TO SEND A MAIL." + Action);
                GMail gmail = new GMail();
                string[] Scopes = { GmailService.Scope.GmailSend };
                gmail.GmailMain(Action.UserId, Scopes);
                string[] tokens = Action.ActionData.Split(' ');
                if (Action.TriggerAPI == "Crypto")
                    gmail.SendIt(tokens[0], tokens[1], ("Area triggered: " + Action.Name + " (" + Action.TriggerAPI + " : " + Action.TriggerData + ")"), ("Area triggered: " + Action.Name + " (" + Action.TriggerAPI + " : " + Action.TriggerData + ")"));
                else if (Action.TriggerAPI == "Twitch")
                    gmail.SendIt(tokens[0], tokens[1], ("Area triggered: " + Action.Name + " (" + Action.TriggerAPI + ")") ,"Area trigger: your bot is connected on " + Action.TriggerData + " channel");
            }
        }
        private static List<Action> GetActions()
        {
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT Id, UserId, ActionName, ActionAPI, TriggerAPI, ActionData, TriggerData FROM Actions", con))
                {
                    con.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        var list = new List<Action>();
                        while (reader.Read())
                        {
                            list.Add(
                                new Action
                                {
                                    Id = reader.GetInt32(0),
                                    UserId = reader.GetSqlString(1).ToString(),
                                    Name = reader.GetString(2),
                                    ActionAPI = reader.GetString(3),
                                    TriggerAPI = reader.GetString(4),
                                    ActionData = reader.GetString(5),
                                    TriggerData = reader.GetString(6),
                                }
                            );
                        }
                        return (list);
                    }
                }
            }
        }
    }
}