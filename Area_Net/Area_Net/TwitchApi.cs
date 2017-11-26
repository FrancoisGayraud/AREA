using TwitchLib;
using TwitchLib.Models.Client;
using TwitchLib.Events.Client;
using TwitchLib.Extensions.Client;
using System;
using System.Threading;
using System.Threading.Tasks;
using Area_Net;
using Google.Apis.Gmail.v1;

namespace Twitch
{
    public class TwitchApi
    {
        TwitchClient client;
        ConnectionCredentials credentials;
        public string account { get; set; }
        public string gmailAddressFrom { get; set; }
        public string gmailAddressTo { get; set; }
        public string userId { get; set; }
        public TwitchApi(string acc, string from, string to, string id)
        {
            account = acc;
            gmailAddressFrom = from;
            gmailAddressTo = to;
            userId = id;
            credentials = new ConnectionCredentials("MeneurRouge", "4mey2kbwotbajhrdhgteeqyklwko48");
            client = new TwitchClient(credentials, account);
            client.OnMessageReceived += onMessageReceived;
            client.Connect();
        }

        private void onMessageReceived(object sender, OnMessageReceivedArgs e)
        {
            if (e.ChatMessage.Message.Contains("rules"))
            {
                GMail gmail = new GMail();
                string[] Scopes = { GmailService.Scope.GmailSend };
                gmail.GmailMain(userId, Scopes);
                gmail.SendIt(gmailAddressFrom, gmailAddressTo, "Twitch chat rules", "This are the rules in " + account + " channel: 1. don't be stupid 2. enjoy");
            }
            else
             client.SendMessage($"Hi there {e.ChatMessage.Username}! Write rules to have rules on your Gmail");
        }

        private void onWhisperReceived(object sender, OnWhisperReceivedArgs e)
        {
            client.SendWhisper(e.WhisperMessage.Username, "yo mec");
        }

        public void initTwitchLoop()
        {
            try
            {
                while (true)
                {
                    System.Threading.Thread.Sleep(1000);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}