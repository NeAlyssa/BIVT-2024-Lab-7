using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    internal class Blue_5
    {
        //класс
        public class Sportsman
        {
            //приватные поля
            private string _name;
            private string _surname;
            private int _place;
            private bool _placeSet;

            //свойства для чтения
            public string Name => _name;
            public string Surname => _surname;
            public int Place => _place;

            //конструктор
            public Sportsman(string Name, string Surname)
            {
                _name = Name;
                _surname = Surname;
                _place = 0;
                _placeSet = false;
            }

            public void SetPlace(int place)
            {
                if (!_placeSet)
                {
                    _place = place;
                    _placeSet = true;
                }
            }

            public void Print()
            {
                Console.WriteLine($"{_name} {_surname} {_place}");
            }

        }
        //абстрактный класс
        public abstract class Team
        {
            //приватные поля
            private string _name;
            private Sportsman[] _sportsmen;
            private int _index;

            //свойства для чтения
            public string Name => _name;
            public Sportsman[] Sportsmen => _sportsmen;

            public int SummaryScore
            {
                get
                {
                    if (_sportsmen == null) return 0;
                    int score = 0;
                    for (int i = 0; i < 6; i++)
                    {
                        if (_sportsmen[i].Place <= 5 && _sportsmen[i].Place != 0)
                        {
                            score += 6 - _sportsmen[i].Place;
                        }
                    }
                    return score;
                }
            }

            public int TopPlace
            {
                get
                {
                    if (_sportsmen == null) return 0;
                    int Min = 18;
                    for (int i = 0; i < 6; i++)
                    {
                        if (Min > _sportsmen[i].Place && _sportsmen[i].Place != 0)
                        {
                            Min = _sportsmen[i].Place;
                        }
                    }
                    return Min;
                }
            }

            public Team(string Name)
            {
                _name = Name;
                _sportsmen = new Sportsman[6];
                _index = 0;
            }

            public void Add(Sportsman sportsman)
            {
                if (_sportsmen == null || _index == 6) return;
                if (_index < _sportsmen.Length)
                {
                    _sportsmen[_index++] = sportsman;
                }
            }

            public void Add(Sportsman[] sportsmen)
            {
                if (_sportsmen == null) return;
                foreach (var sportsman in sportsmen)
                {
                    Add(sportsman);
                }
            }

            public static void Sort(Team[] teams)
            {
                if (teams == null) return;
                for (int i = 0; i < teams.Length; i++)
                {
                    for (int j = 0; j < teams.Length - 1 - i; j++)
                    {
                        if (teams[j].SummaryScore < teams[j + 1].SummaryScore)
                        {
                            var a = teams[j];
                            teams[j] = teams[j + 1];
                            teams[j + 1] = a;
                        }
                        else if (teams[j].SummaryScore == teams[j + 1].SummaryScore && teams[j].TopPlace > teams[j + 1].TopPlace)
                        {
                            var a = teams[j];
                            teams[j] = teams[j + 1];
                            teams[j + 1] = a;
                        }
                    }
                }
            }
            protected abstract double GetTeamStrength();
            public static Team GetChampion(Team[] teams)
            {
                if (teams == null) return null;
                Team Champ=teams[0];
                double Max=Champ.GetTeamStrength();
                for(int i=0; i<teams.Length; i++)
                {
                    double str = teams[i].GetTeamStrength();
                    if (str > Max)
                    {
                        Max = str;
                        Champ = teams[i];
                    }
                }
                return Champ;
            }

            public void Print()
            {
                Console.WriteLine($" {Name} {SummaryScore} {TopPlace} ");
            }
        }

        public class ManTeam : Team
        {
            public ManTeam(string Name) : base(Name) { }
            protected override double GetTeamStrength()
            {
                if (Sportsmen==null || Sportsmen.Length==0) return 0;
                double sum = 0;
                int counter = 0;
                foreach(var sportsman in Sportsmen)
                {
                    if (sportsman.Place != 0)
                    {
                        sum += sportsman.Place;
                        counter++;
                    }
                }
                if (counter == 0) return 0;
                double sred=sum/counter;
                return 100 / sred;
            }
        }
        public class WomanTeam : Team
        {
            public WomanTeam(string Name) : base(Name) { }
            protected override double GetTeamStrength()
            {
                if (Sportsmen == null || Sportsmen.Length == 0) return 0;
                double sum = 0;
                int counter = 0;
                double proizv = 1;
                foreach (var sportsman in Sportsmen)
                {
                    if (sportsman.Place != 0)
                    {
                        sum += sportsman.Place;
                        counter++;
                        proizv*= sportsman.Place;
                    }
                }
                if (counter == 0) return 0;
                return 100 * sum* counter/proizv;
            }
        }
    }

}
