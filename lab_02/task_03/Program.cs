using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task_03
{
    public sealed class Authenticator
    {
        private static readonly object _lock = new object();
        private static Authenticator _instance;

        public string CurrentUser { get; private set; }

        private Authenticator()
        {
            CurrentUser = "Гість";
        }

        public static Authenticator GetInstance()
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new Authenticator();
                    }
                }
            }
            return _instance;
        }

        public void Login(string username)
        {
            CurrentUser = username;
        }

        public void Logout()
        {
            CurrentUser = "Гість";
        }

        public void DisplayUser()
        {
            Console.WriteLine("Поточний користувач: " + CurrentUser);
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            var auth1 = Authenticator.GetInstance();
            var auth2 = Authenticator.GetInstance();

            auth1.DisplayUser();
            auth1.Login("admin");
            auth2.DisplayUser();


            Console.WriteLine("auth1 == auth2: " + (auth1 == auth2));

            auth2.Logout();
            auth1.DisplayUser(); 
        }
    }
}
