using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main
{
    public class CourseEnrollmentService
    {
        public void Enroll(Student student, Course course)
        {
            if (!student.Courses.Any(c => c.CourseID == course.CourseID))
            {
                student.Courses.Add(course);
            }
        }

        public void RemoveCourse(Student student, string courseId)
        {
            var course = student.Courses.FirstOrDefault(c => c.CourseID == courseId);
            if (course != null)
                student.Courses.Remove(course);
        }

        public void ClearAllCourses(Student student) => student.Courses.Clear();
    }
}
