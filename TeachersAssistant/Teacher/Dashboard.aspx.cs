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
    public partial class Dashboard : System.Web.UI.Page
    {
        //private string sql;
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["UserType"] == null) || (Session["UserType"].ToString() != "teacher"))
            {
                Response.Redirect("~/Login.aspx");
            }

            //Response.Write("loaded from db");
            Session["TeacherId"] = Session["UserId"];
            Session["SearchQuery"] = "Select * from StudentInformation where TeacherId='" + Session["TeacherId"].ToString() + "'";
            
            LoadStudentCards();
            
            if(Session["SearchResult"] != null)
            {
                DataTable dt = (DataTable)Session["SearchResult"];

                GridViewStudentSearch.DataSource = dt;
                GridViewStudentSearch.DataBind();
            }
            else
            {
                LoadFromDatabaseForStudentSearch();
            }

            if (!IsPostBack)
            {
                LoadTeacherCourseFromDatabase();
                LoadSemesterFromDatabase();
                //QueryBuilder();
                //LoadStudentCards();
                Session["SortingOrder"] = "ASC";
            }
            
        }

        protected void LoadFromDatabaseForStudentSearch()
        {
            string connStr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            string sql = Session["SearchQuery"].ToString();
            //sql = "Select * from StudentInformation where TeacherId='"+Session["TeacherId"].ToString()+"'";
            SqlDataAdapter AdapterStudentInformation = new SqlDataAdapter(sql, conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(AdapterStudentInformation);
            DataSet ds = new DataSet();
            AdapterStudentInformation.Fill(ds, "StudentInformation");

            GridViewStudentSearch.DataSource = ds.Tables["StudentInformation"];
            GridViewStudentSearch.DataBind();

            Cache["DATASETSTUDENTSEARCH"] = ds;
            Cache["AdapterStudentInformation"] = AdapterStudentInformation;
        }

        protected void LoadTeacherCourseFromDatabase()
        {
            string connStr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);

            string sql = "select tc.TeacherId, c.Name as CourseName from TeacherCourse tc, Course c where tc.CourseId = c.CourseID and TeacherId = '" + Session["TeacherId"].ToString() + "'";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "CourseList");
                        
            DropDownListCourseList.DataSource = ds.Tables["CourseList"];
            DropDownListCourseList.DataTextField = "CourseName";
            DropDownListCourseList.DataValueField = "CourseName";
            DropDownListCourseList.DataBind();

            DropDownListCourseList.Items.Add(new ListItem("---None---", "NULL", true));
            DropDownListCourseList.SelectedIndex = DropDownListCourseList.Items.IndexOf(new ListItem("---None---", "NULL", true));

        }

        protected void LoadSemesterFromDatabase()
        {
            string connStr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);

            string sql = "select Name as SemesterName from Semester";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "SemesterList");
            DropDownListSemesterList.DataSource = ds.Tables["SemesterList"];
            DropDownListSemesterList.DataTextField = "SemesterName";
            DropDownListSemesterList.DataValueField = "SemesterName";
            DropDownListSemesterList.DataBind();

            DropDownListSemesterList.Items.Add(new ListItem("---None---", "NULL", true));
            DropDownListSemesterList.SelectedIndex = DropDownListCourseList.Items.IndexOf(new ListItem("---None---", "NULL", true));
        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            //QueryBuilder();
            string sql = QueryBuilder();
            //string sql = Session["SearchQuery"].ToString();
            Response.Write(sql);
            //string sql = Session["SearchQuery"].ToString() + " and Grade > 3.75";
            //sql += " and Grade > 3.75";
            string connStr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            SqlDataAdapter AdapterStudentInformation = new SqlDataAdapter(sql, conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(AdapterStudentInformation);
            DataSet ds = new DataSet();
            AdapterStudentInformation.Fill(ds, "StudentInformation");

            GridViewStudentSearch.DataSource = ds.Tables["StudentInformation"];
            GridViewStudentSearch.DataBind();

            Session["SearchResult"] = ds.Tables["StudentInformation"];
        }        

        protected void GridViewStudentSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Response.Write(GridViewStudentSearch.SelectedDataKey.Value
            string sql = "select * from Student where StudentId = '" + GridViewStudentSearch.SelectedDataKey.Value + "'";

            string connStr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand Cmd = new SqlCommand(sql, conn);

            conn.Open();
            SqlDataReader reader = Cmd.ExecuteReader();

            Students s = new Students();

            if (reader.Read())
            {
                s = new Students(reader["StudentId"].ToString(), reader["Name"].ToString(), reader["Gender"].ToString(), reader["Email"].ToString(), Convert.ToDouble(reader["Cgpa"]), Convert.ToInt32(reader["CreditsCompleted"]), Convert.ToInt32(reader["Semester"]));
            }

            if (Session["StudentComparisonList"] == null)
            {                
                List<Students> StudentList = new List<Students>();
                StudentList.Add(s);
                Session["StudentComparisonList"] = StudentList;
            }
            else
            {
                List<Students> StudentList = (List<Students>) Session["StudentComparisonList"];
                
                if(StudentList.Count < 3)
                {
                    Students Existing = StudentList.Find(x => x.Id == s.Id);
                    if (Existing != null)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlreadyExist", "alert('Student already in comparison list.')", true);
                    }
                    else
                    {
                        StudentList.Add(s);
                    }

                    Session["StudentComparisonList"] = StudentList;
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "MessageExceedThree", "alert('Cannot compare more than 3 students.')", true);
                }
            }

            LoadStudentCards();                   
        }

        protected void ButtonReset_Click(object sender, EventArgs e)
        {
            Session["SearchQuery"] = "Select * from StudentInformation where TeacherId='" + Session["TeacherId"].ToString() + "'";
            Session["SearchResult"] = null;

            DropDownListSemesterList.SelectedIndex = DropDownListCourseList.Items.IndexOf(new ListItem("---None---", "NULL", true));
            DropDownListCourseList.SelectedIndex = DropDownListCourseList.Items.IndexOf(new ListItem("---None---", "NULL", true));
            TextBoxSearchByCgpa.Text = "";
            TextBoxSearchByAbsence.Text = "";

            LoadFromDatabaseForStudentSearch();
        }

        protected string QueryBuilder()
        {
            string Query = Session["SearchQuery"].ToString();

            if(DropDownListCourseList.SelectedValue != "NULL")
            {
                Query += " and CourseName = '"+DropDownListCourseList.SelectedValue+"'";
            }

            if(DropDownListSemesterList.SelectedValue != "NULL")
            {
                Query += " and Semester = '" + DropDownListSemesterList.SelectedValue + "'";
            }

            if(TextBoxSearchByCgpa.Text != "")
            {
                if(RadioButtonCgpaGreaterThan.Checked == true)
                {
                    Query += " and Cgpa > " + Convert.ToDouble(TextBoxSearchByCgpa.Text).ToString();
                }

                if (RadioButtonCgpaLessThan.Checked == true)
                {
                    Query += " and Cgpa < " + Convert.ToDouble(TextBoxSearchByCgpa.Text).ToString();
                }
            }

            if (TextBoxSearchByAbsence.Text != "")
            {
                if (RadioButtonAbsenceGreaterThan.Checked == true)
                {
                    Query += " and ClassMissed > " + Convert.ToInt32(TextBoxSearchByAbsence.Text);
                }

                if (RadioButtonAbsenceLessThan.Checked == true)
                {
                    Query += " and ClassMissed < " + Convert.ToInt32(TextBoxSearchByAbsence.Text);
                }
            }

            //Session["SearchQuery"] = Query;
            return Query;
        }

        protected void GridViewStudentSearch_Sorting(object sender, GridViewSortEventArgs e)
        {
            string SortingDerection = SetSortOrder();

            if (Session["SearchResult"] != null)
            {
                //DataSet ds = (DataSet)Cache["DATASETSTUDENTSEARCH"];
                DataTable dt = (DataTable)Session["SearchResult"];
                //DataTable dt  = (DataTable)GridViewStudentSearch.DataSource;
                //DataTable dt = ds.Tables["StudentInformation"];

                dt.DefaultView.Sort = e.SortExpression + " " + SortingDerection;

                GridViewStudentSearch.DataSource = dt;
                GridViewStudentSearch.DataBind();
            }
            else
            {
                DataSet ds = (DataSet)Cache["DATASETSTUDENTSEARCH"];
                DataTable dt = (DataTable)ds.Tables["StudentInformation"];

                dt.DefaultView.Sort = e.SortExpression + " " + SortingDerection;

                GridViewStudentSearch.DataSource = dt;
                GridViewStudentSearch.DataBind();
            }
        }

        protected string SetSortOrder()
        {
            if(Session["SortingOrder"].ToString() == "ASC")
            {
                Session["SortingOrder"] = "DESC";
                return "DESC";
            }
            else
            {
                Session["SortingOrder"] = "ASC";
                return "ASC";
            }
        }

        protected void LoadStudentCards()
        {
            if(Session["StudentComparisonList"] != null)
            {
                PanelCard.Controls.Clear();

                List<Students> StudentList = (List<Students>) Session["StudentComparisonList"];

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
                    StudentCard.ID = "card" + x.ToString();

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

        protected void ButtonResetCompare_Click(object sender, EventArgs e)
        {
            Session["StudentComparisonList"] = null;
            PanelCard.Controls.Clear();
        }

        protected void ButtonCreateGroup_Click(object sender, EventArgs e)
        {
            if (Session["StudentComparisonList"] != null)
            {
                Response.Redirect("~/Teacher/CreateGroup.aspx");
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlreadyExist", "alert('No student selected.')", true);
            }       
        }

        protected void ButtonViewGroups_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Teacher/ViewGroups.aspx");
        }

        protected void ButtonTeacherSignout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();

            Response.Redirect("~/Login.aspx");
        }
    }
}