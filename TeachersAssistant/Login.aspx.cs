using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TeachersAssistant
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ButtonLogin_Click(object sender, EventArgs e)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            SqlConnection Conn = new SqlConnection(ConnectionString);

            string Query = "select * from UserInfo where ID = '" + TextBoxId.Text + "' and Password = '" + TextBoxPass.Text + "'";
            SqlCommand Cmd = new SqlCommand(Query, Conn);

            Conn.Open();
            SqlDataReader reader = Cmd.ExecuteReader();

            if (reader.Read())
            {
                string db_username = reader["ID"].ToString();
                string db_password = reader["Password"].ToString();
                string db_usertype = reader["Type"].ToString();

                if (db_username == TextBoxId.Text && db_password == TextBoxPass.Text)
                {
                    Session["CurrentUser"] = db_username;
                    Conn.Close();
                    //LabelMessage.Text = db_username + " " + db_password + " " + db_usertype;
                    LabelMessage.Visible = false;

                    Session["UserId"] = db_username;
                    Session["UserType"] = db_usertype;

                    if (db_usertype == "admin")
                    {
                        Response.Redirect("~/Admin.aspx");
                    }
                    else if(db_usertype == "teacher")
                    {
                        Response.Redirect("~/Teacher/Dashboard.aspx");
                    }
                    else
                    {
                        Response.Redirect("~/Student/Home.aspx");
                    }
                }
                else
                {
                    LabelMessage.Text = "Invalid Username or Password";
                    LabelMessage.Visible = true;
                }
            }
            else
            {
                LabelMessage.Text = "Invalid Username or Password";
                LabelMessage.Visible = true;
            }

            Conn.Close();
        }
    }
}