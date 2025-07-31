using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main
{
    public interface IStudentRepo
    {
        void Add(Student student);
        Student GetID(string ID);
        void Delete(string ID);
        IEnumerable<Student> GetAll();
    }
    public interface IAuthentication
    {
        bool login(string id, string password);
    }
    public interface IStudent
    {
        string ID { get; }
        string Name { get; }
        void UpdateName(string newName);
        void UpdateID(string newID);
    }
}