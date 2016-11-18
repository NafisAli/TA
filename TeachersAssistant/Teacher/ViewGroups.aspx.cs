using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TeachersAssistant.Teacher
{
    public partial class ViewGroups : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["UserType"] == null) || (Session["UserType"].ToString() != "teacher"))
            {
                Response.Redirect("~/Login.aspx");
            }

            LoadGroupDetails();
        }

        protected void ButtonBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Teacher/Dashboard.aspx");
        }

        protected void LoadGroupDetails()
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            string sql = "select * from Groups where TeacherID='"+Session["TeacherId"].ToString()+"'";
            //sql = "Select * from StudentInformation where TeacherId='"+Session["TeacherId"].ToString()+"'";
            SqlDataAdapter AdapterGroupInformation = new SqlDataAdapter(sql, conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(AdapterGroupInformation);
            DataSet ds = new DataSet();
            AdapterGroupInformation.Fill(ds, "GroupInformation");

            GridViewGroupDetails.DataSource = ds.Tables["GroupInformation"];
            GridViewGroupDetails.DataBind();
        }
    }
}