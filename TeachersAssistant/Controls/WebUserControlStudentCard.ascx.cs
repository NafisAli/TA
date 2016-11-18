using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TeachersAssistant
{
    public partial class WebUserControlStudentCard : System.Web.UI.UserControl
    {
        private Students CardId;

        public Students Card
        {
            get { return this.CardId; }
            set { this.CardId = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            LabelStudentName.Text = CardId.Name;
            LabelStudentId.Text = CardId.Id;
            LabelStudentGender.Text = CardId.Gender;
            LabelStudentEmail.Text = CardId.Email;
            LabelStudentCgpa.Text += CardId.Cgpa.ToString();
            LabelStudentSemester.Text += CardId.Semester.ToString();
            LabelCreditsCompleted.Text += CardId.CreditsCompleted.ToString();

            ImageStudent.Height = 50;
            ImageStudent.Width = 50;

            if (CardId.Gender == "Female")
            {
                ImageStudent.ImageUrl = "~/Images/femaleDefault.png";
            }
            else
            {
                ImageStudent.ImageUrl = "~/Images/maleDefault.png";
            }
        }
        
        //protected void ButtonRemoveSelected_Click(object sender, EventArgs e)
        //{
        //    List<Students> StudentList = (List<Students>)Session["StudentComparisonList"];
        //    //StudentList.RemoveAt(StudentList.IndexOf(CardId));
        //    var item = StudentList.First(x => x.Id == CardId.Id);
        //    StudentList.Remove(item);
        //    Session["StudentComparisonList"] = StudentList;
        //}
    }
}