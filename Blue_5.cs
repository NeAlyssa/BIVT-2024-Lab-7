using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    public class Blue_5
    {
        // класс
        public class Sportsman
        {
            // приватные поля
            private string _name;
            private string _surname;
            private int _place;

            // свойства
            public string Name => _name;
            public string Surname => _surname;
            public int Place => _place;

            // конструктор 
            public Sportsman(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _place = 0;
            }

            // методы
            public void SetPlace(int place)
            {
                if (_place != 0) return;
                _place = place;
            }
            public void Print()
            {
                Console.WriteLine($"{_name} {_surname} {_place}");
                Console.WriteLine();
            }
        }

        // абстрактный класс
        public abstract class Team
        {
            // поля
            private string _name;
            private Sportsman[] _sportsmen;
            private int _counter;

            // свойства 
            public string Name { get { return _name; } }
            public Sportsman[] Sportsmen
            {
                get { return _sportsmen; }
            }
            public int SummaryScore
            {
                get
                {
                    if (_sportsmen == null || _sportsmen.Length == 0) return 0;
                    int sm = 0;
                    for (int i = 0; i < _sportsmen.Length; i++)
                    {
                        if (_sportsmen[i].Place == 1) sm += 5;
                        else if (_sportsmen[i].Place == 2) sm += 4;
                        else if (_sportsmen[i].Place == 3) sm += 3;
                        else if (_sportsmen[i].Place == 4) sm += 2;
                        else if (_sportsmen[i].Place == 5) sm += 1;
                    }
                    return sm;
                }
            }
            public int TopPlace
            {
                get
                {
                    if (_sportsmen == null) return 0;
                    int min_sp = 18;
                    foreach (Sportsman sportsman in _sportsmen)
                    {
                        if (sportsman.Place != 0 && sportsman.Place < min_sp) { min_sp = sportsman.Place; }
                    }
                    return min_sp;
                }
            }

            // конструктор 
            public Team(string name)
            {
                _name = name;
                _sportsmen = new Sportsman[6];
                _counter = 0;
            }

            // методы
            public void Add(Sportsman sportsman)
            {
                if (_sportsmen == null || sportsman == null || _sportsmen.Length == 0 || _counter >= _sportsmen.Length) return;
                _sportsmen[_counter] = sportsman;
                _counter++;
            }
            public void Add(Sportsman[] sportsman)
            {
                if (_sportsmen == null || sportsman == null || sportsman.Length == 0 || _sportsmen.Length == 0 || _counter >= _sportsmen.Length) return;
                int i = 0;
                while (_counter < _sportsmen.Length && i < sportsman.Length)
                {
                    if (sportsman[i] == null) continue;
                    _sportsmen[_counter] = sportsman[i];
                    _counter++;
                    i++;
                }
            }
            public static void Sort(Team[] teams)
            {
                if (teams == null || teams.Length <= 1) return;
                for (int i = 0; i < teams.Length; i++)
                {
                    for (int j = 0; j < teams.Length - i - 1; j++)
                    {
                        if (teams[j + 1].SummaryScore > teams[j].SummaryScore) { (teams[j], teams[j + 1]) = (teams[j + 1], teams[j]); }
                        else if (teams[j].SummaryScore == teams[j + 1].SummaryScore && teams[j].TopPlace > teams[j + 1].TopPlace) { (teams[j], teams[j + 1]) = (teams[j + 1], teams[j]); }
                    }
                }
            }

            protected abstract double GetTeamStrength();
            public static Team GetChampion(Team[] teams)
            {
                if (teams == null || teams.Length == 0) return null;
                double maxt = 0;
                if (teams[0] != null) { maxt = teams[0].GetTeamStrength(); }
                int maxind = 0;
                for (int i = 0; i < teams.Length; i++)
                {
                    if (teams[i] != null && teams[i].GetTeamStrength() > maxt)
                    {
                        maxt = teams[i].GetTeamStrength();
                        maxind = i;
                    }
                }
                return teams[maxind];
            }
            public void Print()
            {
                Console.WriteLine($"{_name}, {SummaryScore}, {TopPlace}");
                foreach (var sportsman in _sportsmen) { sportsman.Print(); }
                Console.WriteLine();
            }
        }

        // классы-наследники
        public class ManTeam : Team
        {
            // конструктор
            public ManTeam(string name) : base(name) { }
            protected override double GetTeamStrength()
            {
                double plases = 0;
                int counter = 0;
                for (int i = 0; i < this.Sportsmen.Length; i++)
                {
                    if (this.Sportsmen[i] != null)
                    {
                        plases += this.Sportsmen[i].Place;
                        counter++;
                    }
                }
                double middle = plases / counter;
                return 100 / middle;
            }
        }
        public class WomanTeam : Team
        {
            // конструктор
            public WomanTeam(string name) : base(name) { }
            protected override double GetTeamStrength()
            {
                double plases1 = 0;
                int counter = 0;
                double plases2 = 1;
                for (int i = 0; i < this.Sportsmen.Length; i++)
                {
                    if (this.Sportsmen[i] != null)
                    {
                        plases1 += this.Sportsmen[i].Place;
                        plases2 *= this.Sportsmen[i].Place;
                        counter++;
                    }
                }
                return 100 * plases1 * counter / plases2;
            }
        }
    }
}
