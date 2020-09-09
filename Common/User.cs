using System;

namespace Common
{
    public class User
    {
        public string Name { get; set; }

        public User()
        {
            Console.WriteLine("Введите ваше имя");
            do
            {
                Name = Console.ReadLine();
            }
            while (Name.Length == 0);
        }
        
        public User(string name)
        {
            Name = name;
        }
    }
}
