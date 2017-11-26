using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Timers;

namespace APIManager
{
    public partial class APIManager : ServiceBase
    {
        public APIManager()
        {
            InitializeComponent();
        }
        protected override void OnStart(string[] args)
        {
            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = 60000;
            aTimer.Enabled = true;
            aTimer.Start();
        }
        protected override void OnStop()
        {
        }
        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("I'm in OnTimedEvent !!! Going to get Actions Array");
            List<Action> Actions = GetActions();
            foreach (Action Action in Actions)
            {
                System.Diagnostics.Debug.WriteLine(Action.Name + Action.ActionAPI + Action.TriggerAPI + Action.ActionData + Action.TriggerData);
            }
        }
        private static List<Action> GetActions()
        {
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT ActionName, ActionAPI, TriggerAPI, ActionData, TriggerData FROM Actions", con))
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
                                    Name = reader.GetString(0),
                                    ActionAPI = reader.GetString(1),
                                    TriggerAPI = reader.GetString(2),
                                    ActionData = reader.GetString(3),
                                    TriggerData = reader.GetString(4)
                                }
                            );
                        }
                        return (list);
                    }
                }
            }
        }
    }
    public class Action
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ActionAPI { get; set; }
        public string TriggerAPI { get; set; }
        public string ActionData { get; set; }
        public string TriggerData { get; set; }
    }
}
