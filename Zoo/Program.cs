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
        private Zoo _zoo;

        public Terminal()
        {
            _zoo = new Zoo();
        }

        public void Work()
        {
            bool isWorking = true;

            Console.ForegroundColor = ConsoleColor.Yellow;

            while (isWorking)
            {
                Console.Clear();

                _zoo.ShowInfo();

                Console.Write("\nВведите номер вольера: ");

                if (TryGetNumber(out int index) && index > 0 && index <= _zoo.Enclosures.Count)
                    GoToEnclosure(--index);
                else
                    ShowErrorMessage();
            }
        }

        private bool TryGetNumber(out int index)
        {
            if (int.TryParse(Console.ReadLine(), out int number))
            {
                index = number;

                return true;
            }
            else
            {
                index = 0;

                return false;
            }

        }

        private void ShowErrorMessage()
        {
            Console.WriteLine("Вы ввели недопустимую команду");
            Console.ReadKey();
        }

        private void GoToEnclosure(int numberOfEnclosure)
        {
            Console.Clear();

            _zoo.Enclosures[numberOfEnclosure].ShowInfo();

            Console.ReadKey();
        }
    }

    class Zoo
    {
        private List<Enclosure> _enclosures = new List<Enclosure>();
        private List<Animal> _animals = new List<Animal>()
        {
            { new Duck("Утка", "Кря") },
            { new Bear("Медведь", "Гррр") },
            { new Tiger("Тигр", "Рррар") },
            { new Elefant("Слон", "Трууу") }
        };

        public Zoo()
        {
            int min = 3;
            int max = 11;

            foreach (Animal animal in _animals)
            {
                int numberOfAnimals = HolyRandome.GetNumber(min, max);

                _enclosures.Add(new Enclosure(animal, numberOfAnimals));
            }
        }

        public IReadOnlyList<IEnclosure> Enclosures =>
            _enclosures;

        public void ShowInfo()
        {
            int counter = 1;

            foreach (Enclosure enclosure in _enclosures)
                Console.WriteLine($"{counter++}) - вольер {enclosure.Label.Name}");
        }
    }

    class Enclosure : IEnclosure
    {
        private Label _label;
        private List<Animal> _animals = new List<Animal>();

        public Enclosure(Animal animal, int numberOfAnimals)
        {
            _label = new Label(animal.Name);

            for (int i = 0; i < numberOfAnimals; i++)
                _animals.Add(animal.Clone());
        }

        public ILable Label =>
            _label;

        public void ShowInfo()
        {
            Console.WriteLine($"В данном вольере содержатся {Label.Name}({_animals.Count})");
            Console.WriteLine($"{Label.Name} делают <<{_animals[0].Sound}>>\n");

            ShowAnimals();
        }

        private void ShowAnimals()
        {
            for (int i = 0; i < _animals.Count; i++)
            {
                int number = i + 1;


                Console.WriteLine(_animals[i].GetInfo(number));
            }
        }
    }

    class Label : ILable
    {
        public Label(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }

    interface IEnclosure
    {
        void ShowInfo();
    }

    interface ILable
    {
        string Name { get; }
    }

    abstract class Animal
    {
        protected bool IsMale;

        public Animal(string name, string sound)
        {
            Name = name;
            Sound = sound;

            SetRandomSex();
        }

        protected string Sex =>
            IsMale ? "самец" : "самка";
        public string Sound { get; protected set; }
        public string Name { get; protected set; }

        public string GetInfo(int number) =>
            $"{Name}{number} - пол: {Sex}";

        public abstract Animal Clone();

        protected void SetRandomSex()
        {
            int index = HolyRandome.GetNumber(2);

            IsMale = Convert.ToBoolean(index);
        }
    }

    class Duck : Animal
    {
        public Duck(string name, string sound) : base(name, sound) { }

        public override Animal Clone() =>
            new Duck(Name, Sound);
    }

    class Bear : Animal
    {
        public Bear(string name, string sound) : base(name, sound) { }

        public override Animal Clone() =>
            new Bear(Name, Sound);
    }

    class Tiger : Animal
    {
        public Tiger(string name, string sound) : base(name, sound) { }

        public override Animal Clone() =>
            new Tiger(Name, Sound);
    }

    class Elefant : Animal
    {
        public Elefant(string name, string sound) : base(name, sound) { }

        public override Animal Clone() =>
             new Elefant(Name, Sound);
    }

    class HolyRandome
    {
        private static Random _random = new Random();

        public static int GetNumber(int minValue, int maxValue) =>
                    _random.Next(minValue, maxValue);

        public static int GetNumber(int maxValue) =>
                   _random.Next(maxValue);
    }
}
