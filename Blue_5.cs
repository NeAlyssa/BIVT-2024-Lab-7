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
            private bool _placeS;

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
                _placeS = false;
            }

            // методы
            public void SetPlace(int place)
            {
                if (!_placeS)
                {
                    _place = place;
                    _placeS = true;
                }
                else { Console.WriteLine("Место можно установить только один раз"); }
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
                    if (_sportsmen == null || _sportsmen.Length == 0 || _sportsmen[0] == null) return 0;
                    int[] scr = { 0, 5, 4, 3, 2, 1 };
                    return _sportsmen.Sum(sm => (sm != null && sm.Place >= 1 && sm.Place <= 5) ? scr[sm.Place] : 0);
                }
            }
            public int TopPlace
            {
                get
                {
                    if (_sportsmen == null || _sportsmen[0] == null) return 18;
                    int tP = _sportsmen[0].Place;
                    for (int i = 1; i < _counter; i++)
                    {
                        if (_sportsmen[i].Place < tP) { tP = _sportsmen[i].Place; }
                    }
                    return tP;
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
            public void Add(Sportsman nsportsmen)
            {
                if (_sportsmen == null || nsportsmen == null || _sportsmen.Length == 0 || _counter >= _sportsmen.Length) return;
                _sportsmen[_counter] = nsportsmen;
                _counter++;
            }
            public void Add(Sportsman[] n_sportsmen)
            {
                if (_sportsmen == null || n_sportsmen == null || n_sportsmen.Length == 0 || _sportsmen.Length == 0 || _counter >= _sportsmen.Length) return;
                int i = 0;
                while (_counter < _sportsmen.Length && i < n_sportsmen.Length)
                {
                    if (n_sportsmen[i] == null) continue;
                    _sportsmen[_counter] = n_sportsmen[i];
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
                        if (teams[j] == null) { (teams[j], teams[j + 1]) = (teams[j + 1], teams[j]); }
                        else if (teams[j + 1] == null) { continue; }
                        else if (teams[j].SummaryScore < teams[j + 1].SummaryScore) { (teams[j], teams[j + 1]) = (teams[j + 1], teams[j]); }
                        else if (teams[j + 1].SummaryScore == teams[j].SummaryScore && teams[j + 1].TopPlace < teams[j].TopPlace) { (teams[j], teams[j + 1]) = (teams[j + 1], teams[j]); }
                    }
                }
            }

            protected abstract double GetTeamStrength();
            public static Team GetChampion(Team[] teams)
            {
                if (teams == null) return null;
                double maxind;
                Team athlete = teams[0];
                if (teams[0] == null) { maxind = 0; }
                else { maxind = teams[0].GetTeamStrength(); }
                foreach (Team team in teams)
                {
                    if (team == null) { continue; }
                    double pw = team.GetTeamStrength();
                    if (pw > maxind)
                    {
                        athlete = team;
                        maxind = pw;
                    }
                }
                return athlete;
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
                double res = 0;
                foreach (Sportsman man in Sportsmen) { res += man.Place; }
                res /= 2;
                res = 100.0 / res;
                return res;
            }
        }
        public class WomanTeam : Team
        {
            // конструктор
            public WomanTeam(string name) : base(name) { }
            protected override double GetTeamStrength()
            {
                double res = 0, sm = 0, prz = 1;
                foreach (Sportsman woman in Sportsmen)
                {
                    sm += woman.Place;
                    prz *= woman.Place;
                }
                res = 100 * sm * Sportsmen.Length / prz;
                return res;
            }
        }
    }
}
