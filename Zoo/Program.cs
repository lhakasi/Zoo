using System;
using System.Collections.Generic;

namespace Zoo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Terminal terminal = new Terminal();

            terminal.Work();
        }
    }

    class Terminal
    {
        Zoo zoo = new Zoo();

        public void Work()
        {
            const string GoToEnclosure1Command = "1";
            const string GoToEnclosure2Command = "2";
            const string GoToEnclosure3Command = "3";
            const string GoToEnclosure4Command = "4";
            const string ExitCommand = "5";

            bool isWork = true;

            while (isWork)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Добро пожаловать в зоопарк!");
                Console.WriteLine(new string('=', 40));

                Console.WriteLine($"{GoToEnclosure1Command} - подойти к вольеру 1");
                Console.WriteLine($"{GoToEnclosure2Command} - подойти к вольеру 2");
                Console.WriteLine($"{GoToEnclosure3Command} - подойти к вольеру 3");
                Console.WriteLine($"{GoToEnclosure4Command} - подойти к вольеру 4");
                Console.WriteLine($"{ExitCommand} - закрыть программу");

                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case GoToEnclosure1Command:
                        GoToEnclosure1();
                        break;

                    case GoToEnclosure2Command:
                        break;

                    case GoToEnclosure3Command:
                        break;

                    case GoToEnclosure4Command:
                        break;

                    case ExitCommand:
                        isWork = false;
                        break;

                    default:
                        Console.WriteLine("Вы ввели недопустимую команду");
                        break;
                }

                Console.Clear();
            }
        }

        private void GoToEnclosure1()
        {
            string animalsInEnclosure1 = "Утки";

            zoo.ShowInfo(animalsInEnclosure1);
        }
    }

    class Zoo
    {
        private Dictionary<string, Enclosure> _enclosures = new Dictionary<string, Enclosure>()
        {
            {"Утки", new Enclosure() }
        };

        public void ShowInfo(string key)
        {
            _enclosures[key].ShowInfo();

            Console.ReadKey();
        }
    }

    class Enclosure
    {
        private List<Animal> _animals = new List<Animal>()
        {
            new Duck("Утка", "Кря", true),
            new Duck("Утка", "Кря", false),
            new Duck("Утка", "Кря", true)
        };

        public void ShowInfo()
        {
            foreach (Animal animal in _animals)
            {
                animal.ShowInfo();
            }
        }

    }

    abstract class Animal
    {
        protected static int _id;
        protected string _name;

        protected bool _isMale;

        public Animal(string name, string sound, bool isMale)
        {
            ++_id;
            _name = name + _id;
            Sound = sound;
            _isMale = isMale;
        }

        protected string Sex => _isMale ? "самец" : "самка";
        public string Sound { get; protected set; }

        public void ShowInfo()
        {
            Console.WriteLine($"{_name} - пол: {Sex}");
        }
    }

    class Duck : Animal
    {
        public Duck(string name, string sound, bool isMale) : base(name, sound, isMale) { }
    }
}
