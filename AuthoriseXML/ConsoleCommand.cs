using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthoriseXML
{
    internal class ConsoleCommand
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
            Console.WriteLine("Введите пароль:");
            string password = Console.ReadLine();
            User user = new User(login, password, DateTime.Now);
            return user;
        }
        public static void SucessAuth()
        {
            Console.WriteLine("Вы успешно авторизовались.");
            Authorize.UserInSystem = true;
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

        public static void InformationAfterAuth() 
        {
            Console.WriteLine("Возможные команды: \n quizlist \n startquiz {quiz name} \n quizresults \n " +
                "topquiz {quiz name} \n changesettings \n exit ");
        }
    }
}
