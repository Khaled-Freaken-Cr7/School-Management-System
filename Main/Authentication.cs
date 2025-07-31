using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main
{
    public class Authentication : IAuthentication
    {
        private readonly IStudentRepo repo;
        public Authentication(IStudentRepo repo)
        {
            this.repo = repo;
        }
        public bool login(string id,string password)
        {
            var student = repo.GetID(id);
            return student != null && student.Authenticate(password);
        }
    }
    public class AuthenticationService
    {
        private readonly IAuthentication strategy;

        public AuthenticationService(IAuthentication strategy)
        {
            this.strategy = strategy;
        }

        public bool Login(string id, string password) => strategy.login(id, password);
    }
}