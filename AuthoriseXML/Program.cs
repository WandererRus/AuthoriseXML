using System.Xml;
using System.Xml.Linq;

namespace AuthoriseXML
{
    internal class Program
    {
        static bool AuthoriseFailed = true; 
        static void Main(string[] args)
        {
            if (ConsoleCommand.UserStart() == "auth")
            {
                while (AuthoriseFailed)
                {
                    ConsoleCommand.MustAuth();
                    AuthoriseFailed = Authorize.GetUserAuthorize(ConsoleCommand.Auth());
                    if (AuthoriseFailed) ConsoleCommand.FailedAuth();
                }
                if (!AuthoriseFailed) ConsoleCommand.SucessAuth();
            }
            else
            {
                string login;
                do
                {
                    Console.WriteLine("Введите желаемый логин");
                    login = Console.ReadLine();
                }
                while (Authorize.ExistUser(login));
                Console.WriteLine("Введите желаемый пароль");
                string password = Console.ReadLine();
                Console.WriteLine("Введите дату рождения в формате 2000.12.28");
                string[] date = Console.ReadLine().Split('.');
                DateTime dateOfBirth = new DateTime(Int32.Parse(date[0]), Int32.Parse(date[1]), Int32.Parse(date[2]));
                Authorize.SaveUserData(login,password,dateOfBirth);
            }
        }
    }

    class User 
    {
        string _name;
        string _password;
        DateTime _birthDate;
        public string Login { get => _name; set  =>  _name = value; }
        public string Password { get => _password; set => _password = value; }
        public DateTime DateOfBirth { get => _birthDate; set => _birthDate = value; }
        public User(string name, string password, DateTime dateOfBirth) 
        {
            Login = name;
            Password = password;
            DateOfBirth = dateOfBirth;
        }
    }

    class Authorize 
    {
        public static bool GetUserAuthorize( User user) 
        {
            bool tmp = true;
            FileInfo file = new FileInfo("users.xml");
            if (file.Exists)
            {
                XmlDocument xdoc = new XmlDocument();
                xdoc.Load("users.xml");                
                foreach (XmlElement element in xdoc.GetElementsByTagName("user"))
                {
                    if (element.GetElementsByTagName("login")[0].InnerText == user.Login &&
                        element.GetElementsByTagName("password")[0].InnerText == user.Password)
                        tmp = false;
                   
                }
                if (ConsoleCommand.AuthTryCount == 3)
                {
                    tmp = false;
                    ConsoleCommand.AuthTryCount = 0;
                    ConsoleCommand.UserStart();
                }
                return tmp;

            }
            else
            {
                return tmp;
            }
            
        }
        public static bool ExistUser(string login)
        {
            FileInfo file = new FileInfo("users.xml");
            if (file.Exists)
            {
                XmlDocument xdoc = new XmlDocument();
                xdoc.Load("users.xml");
                bool findUser = false;
                foreach (XmlElement element in xdoc.GetElementsByTagName("user"))
                {
                    if (element.GetElementsByTagName("login")[0].InnerText == login)
                        findUser = true;
                }
                return findUser;
            }
            else 
            {
                return false;
            }
            
        }
        public static void SaveUserData(string log, string pas, DateTime date)
        {
            List<User> users = new List<User>();
            XElement root;
            XmlDocument xdoc;
            FileInfo file = new FileInfo("users.xml");
            if (!file.Exists)
            {
                FileStream fs = file.Create();
                fs.Close();
                users.Add(new User(log, pas, date));                
            }
            else
            {
                xdoc = new XmlDocument();
                xdoc.Load("users.xml");
                foreach (XmlElement element in xdoc.GetElementsByTagName("user"))
                {
                    users.Add(new User(
                        element.GetElementsByTagName("login")[0].InnerText,
                        element.GetElementsByTagName("password")[0].InnerText,
                        Convert.ToDateTime(element.GetElementsByTagName("dateofbirth")[0].InnerText)));
                    users.Add(new User(log, pas, date));
                }
                xdoc.RemoveAll();
            }            
            root = new XElement("users");
            foreach (User user in users)
            {
                XElement element = new XElement("user");
                element.Add(
                    new XElement("login", user.Login),
                    new XElement("password", user.Password),
                    new XElement("dateofbirth", user.DateOfBirth));
               root.Add(element);
            }
            root.Save("users.xml");            
        }

    }

    class ConsoleCommand 
    {
        public static int AuthTryCount = 0;
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
            User user = new User(login, password,DateTime.Now);
            return user;
        }
        public static void SucessAuth()
        {
            Console.WriteLine("Вы успешно авторизовались.");
        }
        public static void FailedAuth()
        {
            Console.WriteLine("Вы не смогли авторизоваться.");
            AuthTryCount++;
        }
        public static string UserStart() 
        {
            Console.WriteLine("Для авторизации наберите auth, для регистрации наберите register");
            string answer = "";
            switch (Console.ReadLine())
            {
                case "auth": answer = "auth"; break;
                case "register": answer = "register"; break;
                default: Console.WriteLine("Введенная вами команда не распознана."); UserStart(); break;
            }
            return answer;
        }
    }    
}
