using System;
using System.Collections.Generic;
using System.Linq;

namespace Main
{
    public class Student : IStudent
    {
        public string ID { get; private set; }
        public string Name { get; private set; }
        private string password;
        public List<Course> Courses { get; private set; }

        public Student(string id, string name, string password)
        {
            ID = id;
            Name = name;
            this.password = password;
            Courses = new List<Course>();
        }

        public bool Authenticate(string password) => this.password == password;

        public void UpdatePassword(string newPassword) => this.password = newPassword;

        public void UpdateName(string newName) => Name = newName;

        public void UpdateID(string newID) => ID = newID;
    }
}