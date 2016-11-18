using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TeachersAssistant
{
    public partial class Admin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if((Session["UserType"] == null) || (Session["UserType"].ToString() != "admin"))
            {
                Response.Redirect("~/Login.aspx");
            }
        }

        private void LoadFromDatabaseForTeacher()
        {
            string connStr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);

            string sql = "select u.ID, u.Password, t.Name, t.Email from UserInfo u, Teacher t where u.ID = t.TeacherID";            
            SqlDataAdapter AdapterUserTeacher = new SqlDataAdapter(sql, conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(AdapterUserTeacher);
            DataSet ds = new DataSet();
            AdapterUserTeacher.Fill(ds, "UserTeacher");

            sql = "select * from UserInfo";
            SqlDataAdapter AdapterUserInfo = new SqlDataAdapter(sql, conn);
            builder = new SqlCommandBuilder(AdapterUserInfo);
            AdapterUserInfo.Fill(ds, "UserInfo");

            sql = "select * from Teacher";
            SqlDataAdapter AdapterTeacher = new SqlDataAdapter(sql, conn);
            builder = new SqlCommandBuilder(AdapterTeacher);
            AdapterTeacher.Fill(ds, "Teacher");

            ds.Tables["UserTeacher"].PrimaryKey = new DataColumn[] { ds.Tables["UserTeacher"].Columns["ID"] };
            ds.Tables["UserInfo"].PrimaryKey = new DataColumn[] { ds.Tables["UserInfo"].Columns["ID"] };
            ds.Tables["Teacher"].PrimaryKey = new DataColumn[] { ds.Tables["Teacher"].Columns["TeacherID"] };

            GridViewTeachers.DataSource = ds.Tables["UserTeacher"];
            GridViewTeachers.DataBind();

            Cache["DATASETTEACHER"] = ds;
            Cache["AdapterUserTeacher"] = AdapterUserTeacher;
            Cache["AdapterUserInfo"] = AdapterUserInfo;
            Cache["AdapterTeacher"] = AdapterTeacher;
        }

        private void LoadDataFromCacheForTeacher()
        {
            DataSet ds = (DataSet)Cache["DATASETTEACHER"];
            GridViewTeachers.DataSource = ds.Tables["UserTeacher"];
            GridViewTeachers.DataBind();
        }

        private void LoadFromDatabaseForStudent()
        {
            string connStr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);

            string sql = "select u.ID, u.Password, s.Name, s.Gender, s.Email, s.Cgpa, s.CreditsCompleted, s.Semester from UserInfo u, Student s where u.ID = s.StudentId";
            SqlDataAdapter AdapterUserStudent = new SqlDataAdapter(sql, conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(AdapterUserStudent);
            DataSet ds = new DataSet();
            AdapterUserStudent.Fill(ds, "UserStudent");

            sql = "select * from UserInfo";
            SqlDataAdapter AdapterUserInfo = new SqlDataAdapter(sql, conn);
            builder = new SqlCommandBuilder(AdapterUserInfo);
            AdapterUserInfo.Fill(ds, "UserInfo");

            sql = "select * from Student";
            SqlDataAdapter AdapterStudent = new SqlDataAdapter(sql, conn);
            builder = new SqlCommandBuilder(AdapterStudent);
            AdapterStudent.Fill(ds, "Student");

            ds.Tables["UserStudent"].PrimaryKey = new DataColumn[] { ds.Tables["UserStudent"].Columns["ID"] };
            ds.Tables["UserInfo"].PrimaryKey = new DataColumn[] { ds.Tables["UserInfo"].Columns["ID"] };
            ds.Tables["Student"].PrimaryKey = new DataColumn[] { ds.Tables["Student"].Columns["StudentId"] };

            GridViewStudents.DataSource = ds.Tables["UserStudent"];
            GridViewStudents.DataBind();

            Cache["DATASETSTUDENT"] = ds;
            Cache["AdapterUserStudent"] = AdapterUserStudent;
            Cache["AdapterUserInfo"] = AdapterUserInfo;
            Cache["AdapterStudent"] = AdapterStudent;
        }

        private void LoadDataFromCacheForStudent()
        {
            DataSet ds = (DataSet)Cache["DATASETSTUDENT"];
            GridViewStudents.DataSource = ds.Tables["UserStudent"];
            GridViewStudents.DataBind();
        }

        protected void ButtonViewTeacher_Click(object sender, EventArgs e)
        {            
            if (Cache["DATASETTEACHER"] == null)
            {
                Response.Write("loaded from db");
                this.LoadFromDatabaseForTeacher();                
            }
            else
            {
                Response.Write("loaded from cc teacher");
                this.LoadDataFromCacheForTeacher();
            }

            MultiViewAdmin.ActiveViewIndex = 1;
        }

        protected void GridViewTeachers_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewTeachers.EditIndex = e.NewEditIndex;
            this.LoadDataFromCacheForTeacher();
        }

        protected void GridViewTeachers_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DataSet ds = (DataSet)Cache["DATASETTEACHER"];

            DataRow dr = ds.Tables["UserTeacher"].Rows.Find(e.Keys["ID"]);
            dr.Delete();

            dr = ds.Tables["Teacher"].Rows.Find(e.Keys["ID"]);
            dr.Delete();

            dr = ds.Tables["UserInfo"].Rows.Find(e.Keys["ID"]);
            dr.Delete();

            Cache["DATASETTEACHER"] = ds;
            this.LoadDataFromCacheForTeacher();
        }        

        protected void GridViewTeachers_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewTeachers.EditIndex = -1;
            this.LoadDataFromCacheForTeacher();
        }

        protected void GridViewTeachers_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            DataSet ds = (DataSet)Cache["DATASETTEACHER"];

            DataRow dr = ds.Tables["UserTeacher"].Rows.Find(e.Keys["ID"]);
            dr["Password"] = e.NewValues["Password"];
            dr["Name"] = e.NewValues["Name"];
            dr["Email"] = e.NewValues["Email"];

            dr = ds.Tables["UserInfo"].Rows.Find(e.Keys["ID"]);
            dr["Password"] = e.NewValues["Password"];

            dr = ds.Tables["Teacher"].Rows.Find(e.Keys["ID"]);
            dr["Name"] = e.NewValues["Name"];
            dr["Email"] = e.NewValues["Email"];

            Cache["DATASETTEACHER"] = ds;
            GridViewTeachers.EditIndex = -1;
            this.LoadDataFromCacheForTeacher();
        }

        protected void ButtonConfirmViewTeacher_Click(object sender, EventArgs e)
        {
            DataSet ds = (DataSet)Cache["DATASETTEACHER"];

            SqlDataAdapter AdapterUserInfo = (SqlDataAdapter)Cache["AdapterUserInfo"];
            SqlDataAdapter AdapterTeacher = (SqlDataAdapter)Cache["AdapterTeacher"];

            AdapterTeacher.Update(ds.Tables["Teacher"]);
            AdapterUserInfo.Update(ds.Tables["UserInfo"]);           
           
            LabelMessage.Text = "Changes saved permanently";            
        }

        protected void ButtonUndoViewTeacher_Click(object sender, EventArgs e)
        {
            DataSet ds = (DataSet)Cache["DATASETTEACHER"];

            if (ds.HasChanges())
            {
                ds.RejectChanges();
                LabelMessage.Text = "Undo successfull";
                this.LoadDataFromCacheForTeacher();
            }
            else
            {
                LabelMessage.Text = "Nothing to be undone";
            }
        }

        protected void ButtonViewStudent_Click(object sender, EventArgs e)
        {
            if (Cache["DATASETSTUDENT"] == null)
            {
                Response.Write("loaded from db");
                this.LoadFromDatabaseForStudent();
            }
            else
            {
                Response.Write("loaded from cc student");
                this.LoadDataFromCacheForStudent();
            }

            MultiViewAdmin.ActiveViewIndex = 3;
        }

        protected void GridViewStudents_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewStudents.EditIndex = e.NewEditIndex;
            this.LoadDataFromCacheForStudent();
        }

        protected void GridViewStudents_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            DataSet ds = (DataSet)Cache["DATASETSTUDENT"];

            DataRow dr = ds.Tables["UserStudent"].Rows.Find(e.Keys["ID"]);
            dr["Password"] = e.NewValues["Password"];
            dr["Name"] = e.NewValues["Name"];
            dr["Email"] = e.NewValues["Email"];
            dr["Gender"] = e.NewValues["Gender"];
            dr["Cgpa"] = e.NewValues["Cgpa"];
            dr["CreditsCompleted"] = e.NewValues["CreditsCompleted"];
            dr["Semester"] = e.NewValues["Semester"];


            dr = ds.Tables["UserInfo"].Rows.Find(e.Keys["ID"]);
            dr["Password"] = e.NewValues["Password"];

            dr = ds.Tables["Student"].Rows.Find(e.Keys["ID"]);
            dr["Name"] = e.NewValues["Name"];
            dr["Email"] = e.NewValues["Email"];
            dr["Gender"] = e.NewValues["Gender"];
            dr["Cgpa"] = e.NewValues["Cgpa"];
            dr["CreditsCompleted"] = e.NewValues["CreditsCompleted"];
            dr["Semester"] = e.NewValues["Semester"];

            Cache["DATASETSTUDENT"] = ds;
            GridViewStudents.EditIndex = -1;
            this.LoadDataFromCacheForStudent();
        }

        protected void GridViewStudents_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DataSet ds = (DataSet)Cache["DATASETSTUDENT"];

            DataRow dr = ds.Tables["UserStudent"].Rows.Find(e.Keys["ID"]);
            dr.Delete();

            dr = ds.Tables["Student"].Rows.Find(e.Keys["ID"]);
            dr.Delete();

            dr = ds.Tables["UserInfo"].Rows.Find(e.Keys["ID"]);
            dr.Delete();

            Cache["DATASETSTUDENT"] = ds;
            this.LoadDataFromCacheForStudent();
        }

        protected void GridViewStudents_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewStudents.EditIndex = -1;
            this.LoadDataFromCacheForStudent();
        }

        protected void ButtonConfirmViewStudent_Click(object sender, EventArgs e)
        {
            DataSet ds = (DataSet)Cache["DATASETSTUDENT"];

            SqlDataAdapter AdapterUserInfo = (SqlDataAdapter)Cache["AdapterUserInfo"];
            SqlDataAdapter AdapterStudent= (SqlDataAdapter)Cache["AdapterStudent"];

            AdapterStudent.Update(ds.Tables["Student"]);
            AdapterUserInfo.Update(ds.Tables["UserInfo"]);

            LabelMessage.Text = "Changes saved permanently";
        }

        protected void ButtonUndoViewStudent_Click(object sender, EventArgs e)
        {
            DataSet ds = (DataSet)Cache["DATASETSTUDENT"];

            if (ds.HasChanges())
            {
                ds.RejectChanges();
                LabelMessage.Text = "Undo successfull";
                this.LoadDataFromCacheForStudent();
            }
            else
            {
                LabelMessage.Text = "Nothing to be undone";
            }
        }

        protected void ButtonAddTeacher_Click(object sender, EventArgs e)
        {
            if (Cache["DATASETTEACHER"] == null)
            {
                Response.Write("loaded from db");
                this.LoadFromDatabaseForTeacher();
            }
            else
            {
                Response.Write("loaded from cc teacher");
                this.LoadDataFromCacheForTeacher();
            }

            MultiViewAdmin.ActiveViewIndex = 0;
        }

        protected void ButtonAddNewTeacher_Click(object sender, EventArgs e)
        {
            DataSet ds = (DataSet)Cache["DATASETTEACHER"];

            if (ds.Tables["UserInfo"].Rows.Contains(TextBoxTeacherId.Text) == false)
            {
                DataRow dr = ds.Tables["UserInfo"].NewRow();
                dr["ID"] = TextBoxTeacherId.Text;
                dr["Password"] = TextBoxTeacherPassword.Text;
                dr["Type"] = "teacher";
                ds.Tables["UserInfo"].Rows.Add(dr);

                dr = ds.Tables["Teacher"].NewRow();
                dr["TeacherID"] = TextBoxTeacherId.Text;
                dr["Name"] = TextBoxTeacherName.Text;
                dr["Email"] = TextBoxTeacherEmail.Text;
                ds.Tables["Teacher"].Rows.Add(dr);

                SqlDataAdapter AdapterUserInfo = (SqlDataAdapter)Cache["AdapterUserInfo"];
                SqlDataAdapter AdapterTeacher = (SqlDataAdapter)Cache["AdapterTeacher"];

                AdapterUserInfo.Update(ds.Tables["UserInfo"]);
                AdapterTeacher.Update(ds.Tables["Teacher"]);

                TextBoxTeacherId.Text = "";
                TextBoxTeacherPassword.Text = "";
                TextBoxTeacherName.Text = "";
                TextBoxTeacherEmail.Text = "";

                LabelMessage.Text = "New teacher added successfully.";
                LabelMessage.Visible = true;

                this.LoadFromDatabaseForTeacher();
                MultiViewAdmin.ActiveViewIndex = 1;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "MessageTeacherIdExists", "alert('ID already in use. Please provide a different ID')", true);
            }         
        }

        protected void ButtonAddStudent_Click(object sender, EventArgs e)
        {
            if (Cache["DATASETSTUDENT"] == null)
            {
                Response.Write("loaded from db");
                this.LoadFromDatabaseForStudent();
            }
            else
            {
                Response.Write("loaded from cc teacher");
                this.LoadDataFromCacheForStudent();
            }
            MultiViewAdmin.ActiveViewIndex = 2;
        }

        protected void ButtonAddNewStudent_Click(object sender, EventArgs e)
        {
            DataSet ds = (DataSet)Cache["DATASETSTUDENT"];

            if (ds.Tables["UserInfo"].Rows.Contains(TextBoxStudentID.Text) == false)
            {
                DataRow dr = ds.Tables["UserInfo"].NewRow();
                dr["ID"] = TextBoxStudentID.Text;
                dr["Password"] = TextBoxStudentPassword.Text;
                dr["Type"] = "student";
                ds.Tables["UserInfo"].Rows.Add(dr);

                dr = ds.Tables["Student"].NewRow();
                dr["StudentId"] = TextBoxStudentID.Text;
                dr["Name"] = TextBoxStudentName.Text;
                dr["Email"] = TextBoxStudentEmail.Text;

                if (RadioButtonMale.Checked)
                {
                    dr["Gender"] = "Male";
                    RadioButtonMale.Checked = false;
                }
                else
                {
                    dr["Gender"] = "Female";
                    RadioButtonFemale.Checked = false;
                }

                dr["Cgpa"] = "0.0";
                dr["CreditsCompleted"] = "0";
                dr["Semester"] = "1";

                ds.Tables["Student"].Rows.Add(dr);

                SqlDataAdapter AdapterUserInfo = (SqlDataAdapter)Cache["AdapterUserInfo"];
                SqlDataAdapter AdapterTeacher = (SqlDataAdapter)Cache["AdapterStudent"];

                AdapterUserInfo.Update(ds.Tables["UserInfo"]);
                AdapterTeacher.Update(ds.Tables["Student"]);

                TextBoxStudentID.Text = "";
                TextBoxStudentPassword.Text = "";
                TextBoxStudentName.Text = "";
                TextBoxStudentEmail.Text = "";


                LabelMessage.Text = "New Student added successfully.";
                LabelMessage.Visible = true;

                this.LoadFromDatabaseForStudent();
                MultiViewAdmin.ActiveViewIndex = 3;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "MessageStudentIdExists", "alert('ID already in use. Please provide a different ID')", true);
            }
        }

        protected void ButtonSignOut_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();

            Response.Redirect("~/Login.aspx");
        }
    }
}