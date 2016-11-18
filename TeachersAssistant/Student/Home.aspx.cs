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
    public partial class Profile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["UserType"] == null) || (Session["UserType"].ToString() != "student"))
            {
                Response.Redirect("~/Login.aspx");
            }
            else
            {
                string ConnectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                SqlConnection Conn = new SqlConnection(ConnectionString);

                string Query = "select * from Student where StudentId = '" + Session["UserId"].ToString() + "'";
                SqlCommand Cmd = new SqlCommand(Query, Conn);

                Conn.Open();
                SqlDataReader reader = Cmd.ExecuteReader();

                if (reader.Read())
                {
                    Students s = new Students(reader["StudentId"].ToString(), reader["Name"].ToString(), reader["Gender"].ToString(), reader["Email"].ToString(), Convert.ToDouble(reader["Cgpa"]), Convert.ToInt32(reader["CreditsCompleted"]), Convert.ToInt32(reader["Semester"]));
                    Session["Student"] = s;

                    LabelStudentName.Text = s.Name;
                }
            }
        }

        protected void ButtonStudentProfile_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Student/Profile.aspx");
        }

        protected void ButtonLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            
            Response.Redirect("~/Login.aspx");
        }
    }
}