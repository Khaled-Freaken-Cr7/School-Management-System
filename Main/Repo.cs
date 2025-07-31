using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main
{
    public class StudentRepo : IStudentRepo
    {
        private readonly Dictionary<string, Student> students = new();

        public void Add(Student student)
        {
            if (!students.ContainsKey(student.ID))
            {
                students[student.ID] = student;
            }
        }
        public Student GetID(string id) => students.ContainsKey(id) ? students[id] : null;
        public void Delete(string id)
        {
            if(students.ContainsKey(id))
            {
                students.Remove(id);
            }
        }
        public IEnumerable<Student> GetAll() => students.Values;
    }
}