using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main
{
    public class Course
    {
        public string CourseID { get; set; }
        public string CourseName { get; set; }
        public int creditHours;
        public Course(string ID,string name,int credit)
        {
            CourseID = ID;
            CourseName = name;
            creditHours = credit;
        }
    }
}