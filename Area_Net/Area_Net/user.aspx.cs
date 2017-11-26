using Microsoft.AspNet.Identity;
using System;
using System.Web;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using Google.Apis.Gmail.v1;
using System.Collections.Generic;
using System.Linq;

namespace Area_Net
{
    public partial class user : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (User.Identity.IsAuthenticated)
                {
                    StatusText.Text = string.Format("Welcome {0}", User.Identity.GetUserName());
                }
                else
                {
                    Response.Redirect("~/home.aspx");
                }
            }
            DisplayActions();
        }
        protected void DisplayActions()
        {
            //Populating a DataTable from database.
            DataTable dt = this.GetData("SELECT Id, ActionName, ActionAPI, TriggerAPI, ActionData, TriggerData FROM Actions WHERE UserId = \'" + User.Identity.GetUserId() + "\'");

            //Building an HTML string.
            StringBuilder html = new StringBuilder();

            //Table start.
            html.Append("<table border = '1'>");

            //Building the Header row.
            html.Append("<tr>");
            foreach (DataColumn column in dt.Columns)
            {
                html.Append("<th>");
                html.Append(column.ColumnName);
                html.Append("</th>");
            }
            html.Append("</tr>");

            //Building the Data rows.
            foreach (DataRow row in dt.Rows)
            {
                html.Append("<tr>");
                foreach (DataColumn column in dt.Columns)
                {
                    html.Append("<td>");
                    html.Append(row[column.ColumnName]);
                    html.Append("</td>");
                }
                html.Append("</tr>");
            }

            //Table end.
            html.Append("</table>");

            //Append the HTML string to Placeholder.
            ActionsPlaceHolder.Controls.Add(new Literal { Text = html.ToString() });
        }
        private DataTable GetData(string SelectString)
        {
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(SelectString))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            con.Close();
                            return dt;
                        }
                    }
                }
            }
        }
        public void AddAction(object sender, EventArgs e)
        {
            string ActionNameTxt = Request.Form["ActionName"];
            string ActionAPI = Request.Form["ActionAPI"];
            string TriggerAPI = Request.Form["TriggerAPI"];
            string ActionData = Request.Form["ActionData"];
            string TriggerData = Request.Form["TriggerData"];
            string UserId = User.Identity.GetUserId();
            CheckGMailAuthorizations(ActionAPI, TriggerAPI);
            System.Diagnostics.Debug.WriteLine(ActionNameTxt + ActionAPI + TriggerAPI + ActionData + TriggerData + UserId);
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                string sql = "INSERT INTO Actions (UserId, ActionName, ActionAPI, TriggerAPI, ActionData, TriggerData) VALUES(@UserId, @ActionName, @ActionAPI, @TriggerAPI, @ActionData, @TriggerData)";
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@ActionName", ActionNameTxt);
                    cmd.Parameters.AddWithValue("@ActionAPI", ActionAPI);
                    cmd.Parameters.AddWithValue("@TriggerAPI", TriggerAPI);
                    cmd.Parameters.AddWithValue("@ActionData", ActionData);
                    cmd.Parameters.AddWithValue("@TriggerData", TriggerData);
                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        System.Diagnostics.Debug.WriteLine("SQL ERROR: " + ex.Message);
                    }
                    con.Close();
                    Response.Redirect("~/user.aspx");
                }
            }
        }
        public void RemoveAction(object sender, EventArgs e)
        {
            string ActionID = Request.Form["todelete"];
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "DELETE FROM Actions WHERE UserID = \'" + User.Identity.GetUserId() + "\' AND Id = " + ActionID;
                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        System.Diagnostics.Debug.WriteLine("SQL ERROR: " + ex.Message);
                    }
                    con.Close();
                    Response.Redirect("~/user.aspx");
                }
            }
        }
        protected void SignOut(object sender, EventArgs e)
        {
            var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
            authenticationManager.SignOut();
            Response.Redirect("~/home.aspx");
        }
        protected void CheckGMailAuthorizations(string ActionAPI, string TriggerAPI)
        {
            List<string> Scopes = new List<string>();
            GMail gmail = new GMail();
            if (TriggerAPI == "GMail")
            {
                Scopes.Add(GmailService.Scope.GmailReadonly);
            }
            if (ActionAPI == "GMail")
            {
                Scopes.Add(GmailService.Scope.GmailSend);
            }
            if (Scopes.Any())
                gmail.GmailMain(User.Identity.GetUserId(), Scopes.ToArray());
        }
    }
}