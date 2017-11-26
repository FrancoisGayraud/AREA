using System;
using System.Collections.Generic;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.IO;
using System.Threading;
using System.Net.Mail;

namespace Area_Net
{
    class GMail
    {
        private static string ApplicationName = "Gmail API .NET Quickstart";
        private  UserCredential credential;
        public void GmailMain(string UserID, string[] Scopes)
        {
            System.Diagnostics.Debug.WriteLine("user id when in gmaiMain : " + UserID);
            using (var stream =
                new FileStream("./gmail_client_secret.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = System.Environment.GetFolderPath(
                    System.Environment.SpecialFolder.Personal);
                credPath = Path.Combine(credPath, ".credentials/gmail-dotnet-quickstart-" + UserID + ".json");
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }
        }
        public void SendIt(string From, string To, string Subject, string Body)
        {
            System.Diagnostics.Debug.WriteLine("TRYING TO SEND IT. FROM IS --" + From + "-- AND TO IS --" + To + "--");
            var msg = new AE.Net.Mail.MailMessage
            {
                Subject = Subject,
                Body = Body,
                From = new MailAddress(From)
            };
            msg.To.Add(new MailAddress(To));
            msg.ReplyTo.Add(msg.From);
            var msgStr = new StringWriter();
            msg.Save(msgStr);
            var gmail = new GmailService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
            var result = gmail.Users.Messages.Send(new Message
            {
                Raw = Base64UrlEncode(msgStr.ToString())
            }, "me").Execute();
            Console.WriteLine("Message ID {0} sent.", result.Id);
        }

        private static string Base64UrlEncode(string input)
        {
            var inputBytes = System.Text.Encoding.UTF8.GetBytes(input);
            return Convert.ToBase64String(inputBytes)
              .Replace('+', '-')
              .Replace('/', '_')
              .Replace("=", "");
        }
        public void ReadLabels()
        {
            var service = new GmailService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            UsersResource.LabelsResource.ListRequest request = service.Users.Labels.List("me");

            IList<Google.Apis.Gmail.v1.Data.Label> labels = request.Execute().Labels;
            System.Diagnostics.Debug.WriteLine("Labels:");
            if (labels != null && labels.Count > 0)
            {
                foreach (var labelItem in labels)
                {
                    System.Diagnostics.Debug.WriteLine(labelItem.Name);
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("No labels found.");
            }
        }
    }
}