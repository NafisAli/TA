using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TeachersAssistant.Teacher
{
    public partial class CreateGroup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["UserType"] == null) || (Session["UserType"].ToString() != "teacher"))
            {
                Response.Redirect("~/Login.aspx");
            }

            LoadStudentCards();
            ExistingIdCheckInitialize();
        }

        protected void LoadStudentCards()
        {
            if (Session["StudentComparisonList"] != null)
            {
                PanelCard.Controls.Clear();

                List<Students> StudentList = (List<Students>)Session["StudentComparisonList"];

                int x = 0;

                Table t = new Table();
                TableRow tr = new TableRow();

                foreach (var obj in StudentList)
                {
                    //WebUserControlStudentCard StudentCard = new WebUserControlStudentCard();
                    WebUserControlStudentCard StudentCard = (WebUserControlStudentCard)LoadControl("~/Controls/WebUserControlStudentCard.ascx");
                    TableCell tc = new TableCell();

                    StudentCard.Card = obj;
                    //StudentCard.Visible = true;
                    StudentCard.ID = x.ToString();

                    tc.Controls.Add(StudentCard);
                    //PanelCard.Controls.Add(StudentCard);
                    x++;
                    //Button b = new Button();
                    //b.Text = obj.Name;
                    //PanelCard.Controls.Add(b);
                    tr.Controls.Add(tc);
                }

                t.Controls.Add(tr);
                PanelCard.Controls.Add(t);
            }
        }

        protected void ButtonCreate_Click(object sender, EventArgs e)
        {
            DataSet ds = (DataSet)Session["EXISTINGGROUPS"];

            if (ds.Tables["GroupInformation"].Rows.Contains(TextBoxGroupId.Text) == false)
            {
                string connStr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                SqlConnection conn = new SqlConnection(connStr);

                string sql = "Insert into Groups values(" + TextBoxGroupId.Text + ",'" + TextBoxGroupName.Text + "','" + TextBoxGroupDescription.Text + "','" + Session["TeacherId"].ToString() + "')";
                SqlCommand Cmd = new SqlCommand(sql, conn);

                conn.Open();
                Cmd.ExecuteNonQuery();
                List<Students> StudentList = (List<Students>)Session["StudentComparisonList"];
                foreach (var obj in StudentList)
                {
                    sql = "Insert into StudentGroup values(" + TextBoxGroupId.Text + ", '" + obj.Id + "')";
                    Cmd = new SqlCommand(sql, conn);
                    Cmd.ExecuteNonQuery();
                }

                conn.Close();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "MessageGroupIdExists", "alert('ID already in use. Please provide a different ID')", true);
            }
        }

        protected void ButtonCancelGroup_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Teacher/Dashboard.aspx");
        }

        protected void ExistingIdCheckInitialize()
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            string sql = "select * from Groups where TeacherID='" + Session["TeacherId"].ToString() + "'";
            //sql = "Select * from StudentInformation where TeacherId='"+Session["TeacherId"].ToString()+"'";
            SqlDataAdapter AdapterGroupInformation = new SqlDataAdapter(sql, conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(AdapterGroupInformation);
            DataSet ds = new DataSet();
            AdapterGroupInformation.Fill(ds, "GroupInformation");
            ds.Tables["GroupInformation"].PrimaryKey = new DataColumn[] { ds.Tables["GroupInformation"].Columns["GroupID"] };

            Session["EXISTINGGROUPS"] = ds;
        }
    }
}