using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TeachersAssistant.Student
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
                Students s = (Students)Session["Student"];

                this.LabelStudentID.Text = s.Id;
                this.LabelStudentName.Text = s.Name;
                this.LabelStudentGender.Text = s.Gender;
                this.LabelStudentEmail.Text = s.Email;
                this.LabelStudentCgpa.Text = s.Cgpa.ToString();
                this.LabelStudentCreditsCompleted.Text = s.CreditsCompleted.ToString();
                this.LabelStudentSemester.Text = s.Semester.ToString();
            }   
        }
    }
}