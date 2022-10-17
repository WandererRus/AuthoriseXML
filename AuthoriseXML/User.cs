using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthoriseXML
{
    internal class User
    {
        string _name;
        string _password;
        DateTime _birthDate;
        public string Login { get => _name; set => _name = value; }
        public string Password { get => _password; set => _password = value; }
        public DateTime DateOfBirth { get => _birthDate; set => _birthDate = value; }
        public User(string name, string password, DateTime dateOfBirth)
        {
            Login = name;
            Password = password;
            DateOfBirth = dateOfBirth;
        }
    }
}
