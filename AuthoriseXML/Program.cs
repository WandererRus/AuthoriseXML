using AuthoriseXML;
using System.Xml;
using System.Xml.Linq;

namespace AuthoriseXML
{
    internal class Program
    {
        static bool AuthoriseFailed = true; 
        static void Main(string[] args)
        {
            while (AuthoriseFailed)
            {
                ConsoleCommand.MustAuth();
                AuthoriseFailed = Authorize.GetUserAuthorize(ConsoleCommand.Auth());
                if (AuthoriseFailed) ConsoleCommand.FailedAuth();
            }
            if (!AuthoriseFailed) ConsoleCommand.SucessAuth();
        }
    }

    class User 
    {
        string _name;
        string _password;
        DateTime _birthDate = DateTime.Now;
        public string Login { get => _name; set  =>  _name = value; }
        public string Password { get => _password; set => _password = value; }
        public DateTime DateOfBirth { get => _birthDate; set => _birthDate = value; }
        public User(string name, string password, DateTime dateOfBirth) 
        {
            Login = name;
            Password = password;
            DateOfBirth = dateOfBirth;
        }
        public User(string name, string password)
        {
            Login = name;
            Password = password;
        }
    }

    class Authorize 
    {
        public static bool GetUserAuthorize( User user) 
        {
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load("users.xml");
            bool tmp = true;
            foreach (XmlElement element in xdoc.GetElementsByTagName("user"))
            {
                if(element.GetElementsByTagName("login")[0].InnerText == user.Login &&
                    element.GetElementsByTagName("password")[0].InnerText == user.Password)
                    tmp = false;
            }
            return tmp;
        }
        static void SetUserData(string log, string pas, DateTime date)
        {

        }

    }

    class ConsoleCommand 
    {
        public static void MustAuth() 
        {
            Console.WriteLine("Авторизуйтесь! Заполните поля логина и пароля.");
        }
        public static User Auth()
        {
            Console.WriteLine("Введите логин:");
            string login = Console.ReadLine();
            Console.WriteLine("Введите логин:");
            string password = Console.ReadLine();
            User user = new User(login, password);
            return user;
        }
        public static void SucessAuth()
        {
            Console.WriteLine("Вы успешно авторизовались.");
        }
        public static void FailedAuth()
        {
            Console.WriteLine("Вы не смогли авторизоваться.");
        }
    }    
}
