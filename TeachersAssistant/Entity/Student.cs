using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeachersAssistant
{
    public class Students
    {
        private string _Id;
        private string _Name;
        private string _Gender;
        private string _Email;
        private double _Cgpa;
        private int _CreditsCompleted;
        private int _Semester;

        public Students()
        {

        }

        public Students(string _Id, string _Name, string _Gender, string _Email, double _Cgpa, int _CreditsCompleted, int _Semester)
        {
            this._Id = _Id;
            this._Name = _Name;
            this._Gender = _Gender;
            this._Email = _Email;
            this._Cgpa = _Cgpa;
            this._CreditsCompleted = _CreditsCompleted;
            this._Semester = _Semester;
        }

        public string Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        public string Gender
        {
            get { return _Gender; }
            set { _Gender = value; }
        }

        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        public double Cgpa
        {
            get { return _Cgpa; }
            set { _Cgpa = value; }
        }

        public int CreditsCompleted
        {
            get { return _CreditsCompleted; }
            set { _CreditsCompleted = value; }
        }

        public int Semester
        {
            get { return _Semester; }
            set { _Semester = value; }
        }
    }
}
