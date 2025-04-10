using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    public class Blue_5
    {
        public class Sportsman
        {
            // поля
            private string _name;
            private string _surname;
            private int _place;

            // свойства
            public string Name { get { return _name; } }
            public string Surname { get { return _surname; } }
            public int Place { get { return _place; } }

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
                if (_place != 0 ) return;
                _place = place;
            }

            public void Print()
            {
                Console.WriteLine($"{_name} {_surname}: {_place}");
            }
        }

        public abstract class Team
        {
            // поля
            private string _name;
            private Sportsman[] _sportsmen;
            private int _sportsmenPlace;

            // свойства
            public string Name { get { return _name; } }
            public Sportsman[] Sportsmen { get { return _sportsmen; } }
            public int SummaryScore
            {
                get
                {
                    if (_sportsmen == null || _sportsmen.Length == 0) return 0;

                    int sum = 0;
                    for (int i = 0; i < _sportsmen.Length; i++)
                    {
                        switch (_sportsmen[i].Place)
                        {
                            case 1: sum += 5; break;
                            case 2: sum += 4; break;
                            case 3: sum += 3; break;
                            case 4: sum += 2; break;
                            case 5: sum += 1; break;
                            default: break;
                        }
                    }
                    return sum;
                }
            }
            public int TopPlace
            {
                get
                {
                    if (_sportsmen == null) return 0;

                    int top = 18;
                    for (int i = 0; i < _sportsmen.Length; i++)
                    {
                        if (top > _sportsmen[i].Place && _sportsmen[i].Place != 0)
                            top = _sportsmen[i].Place;
                    }

                    return top;
                }
            }

            // конструктор
            public Team(string name)
            {
                _name = name;
                _sportsmen = new Sportsman[6];
                _sportsmenPlace = 0;
            }

            // методы
            public void Add(Sportsman sportsman)
            {
                if (_sportsmen == null)
                    _sportsmen = new Sportsman[6];

                if (_sportsmenPlace < 6)
                    _sportsmen[_sportsmenPlace++] = sportsman;
            }

            public void Add(Sportsman[] sportsmen)
            {
                if (_sportsmen == null || sportsmen == null || sportsmen.Length == 0 || _sportsmenPlace >= _sportsmen.Length) return;

                foreach (var sportsman in sportsmen)
                    Add(sportsman);
                
            }

            // void Remove(int count) 
            public void Remove(int count)
            {
                if (_sportsmen == null) return;
                int n = 0;
                foreach (var i in _sportsmen)
                    n++;

                if (count > n || count == 0) return;
                Sportsman[] new_team = new Sportsman[n - count];
                for (int i = 0; i < n - count; i++)
                {
                    new_team[i] = _sportsmen[i];
                }
                _sportsmen = new_team;

            }

            public static void Sort(Team[] teams)
            {
                if (teams == null || teams.Length == 0) return;

                for (int i = 0; i < teams.Length - 1; i++)
                    for (int j = 0; j < teams.Length - i - 1; j++)
                    {
                        if (teams[j].SummaryScore < teams[j + 1].SummaryScore)
                            (teams[j], teams[j + 1]) = (teams[j + 1], teams[j]);

                        else if (teams[j + 1].SummaryScore == teams[j].SummaryScore && teams[j + 1].TopPlace < teams[j].TopPlace)
                            (teams[j], teams[j + 1]) = (teams[j + 1], teams[j]);

                    }
            }

            protected abstract double GetTeamStrength();
            public static Team GetChampion(Team[] teams)
            {
                if (teams == null || teams.Length == 0) return null;

                double max;
                int index = 0;

                if (teams[0] == null) max = 0;
                else max = teams[0].GetTeamStrength();

                for (int i = 0; i < teams.Length; i++)
                {
                    if (teams[i] == null) continue;

                    if (max < teams[i].GetTeamStrength())
                    {
                        max = teams[i].GetTeamStrength();
                        index = i;
                    }
                }
                return teams[index];
            }

            public void Print()
            {
                
                Console.WriteLine($"Команда: {_name}");
                Console.WriteLine($"Суммарный балл: {SummaryScore}");
                Console.WriteLine($"Наивысшее место: {TopPlace}");
                Console.WriteLine("Спортсмены:");

                if (_sportsmen != null && _sportsmen.Length > 0)
                {
                    for (int i = 0; i < _sportsmen.Length; i++)
                    {
                        _sportsmen[i].Print();
                    }
                }
                else
                {
                    Console.WriteLine("Нет данных о спортсменах.");
                }
                Console.WriteLine();

            }
        }

        public class ManTeam : Team
        {
            public ManTeam(string name) : base(name) { }

            protected override double GetTeamStrength() 
            {
                double places = 0;
                int cnt = 0;

                foreach (var sportsman in Sportsmen)
                {
                    if (sportsman == null) continue;

                    places += sportsman.Place;
                    cnt++;
                }

                if (cnt <= 0) return 0;

                double average = places / (double)cnt;
                return 100.0 / average;
            }
        }

        public class WomanTeam : Team
        {
            public WomanTeam(string name) : base(name) { }

            protected override double GetTeamStrength()
            {
                double sum = 0;
                int cnt = 0, mult = 1;

                foreach (var sportsman in Sportsmen)
                {
                    if (sportsman == null) continue;

                    sum += sportsman.Place;
                    mult *= sportsman.Place;
                    cnt++;
                }

                if (mult == 0) return 0;

                double res = (100 * sum * cnt) / mult;
                return res;
            }
        }
    }
}
