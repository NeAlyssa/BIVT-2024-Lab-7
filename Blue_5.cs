using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    public class Blue_5
    {
        public class Sportsman
        {
            private string _name;
            private string _surname;
            private int _place;

            public string Name => _name;
            public string Surname => _surname;
            public int Place => _place;

            public Sportsman(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _place = 0;
            }

            public void SetPlace(int place) //устанавливает место спортсмена в таблице
            {
                if (place > 0 && _place==0)
                {
                    _place = place;
                }
            }
            public void Print()
            {
                Console.WriteLine(_name + " " + _surname + " " + _place);
            }
        }

        public abstract class Team
        {
            private string _name;
            private Sportsman[] _sportsmen;
            private int _cnt;

            public string Name => _name;
            public Sportsman[] Sportsmen => _sportsmen;


            public int SummaryScore
            {
                get
                {
                    if (_sportsmen == null || _sportsmen.Length == 0) return 0;
                    int score = 0;
                    foreach (Sportsman sportsman in _sportsmen)
                    {
                        if (sportsman==null) continue;
                        if (sportsman.Place > 0 && sportsman.Place <= 5)
                        {
                            score += 6 - sportsman.Place;
                        }
                        else score = 0;
                        //switch (sportsman.Place)
                        //{
                        //    case 1: score += 5; break;
                        //    case 2: score += 4; break;
                        //    case 3: score += 3; break;
                        //    case 4: score += 2; break;
                        //    case 5: score += 1; break;
                        //}
                    }
                    return score;
                }
            }

            public int TopPlace
            {
                get
                {
                    if (_sportsmen == null || _sportsmen.Length == 0) return 0;
                    int topplace = int.MaxValue;
                    foreach (Sportsman sportsman in _sportsmen)
                    {
                        if (sportsman.Place > 0 && sportsman.Place < topplace && sportsman!=null)
                        {
                            topplace = sportsman.Place;
                        }
                    }
                    if (topplace == int.MaxValue) return 18;
                    return topplace;
                }
            }

            //construct
            public Team(string name)
            {
                _name = name;
                _sportsmen = new Sportsman[6];
                _cnt = 0;
            }

            public void Add(Sportsman sportsman)
            {
                if (_sportsmen == null|| _cnt >= _sportsmen.Length || sportsman == null) return ;
                _sportsmen[_cnt] = sportsman;
                _cnt++;
            }

            public void Add(Sportsman[] newSportsmen)
            {
                if (_sportsmen == null || newSportsmen==null || _cnt>=_sportsmen.Length) return;
                foreach (Sportsman sportsman in newSportsmen)
                {
                    if(sportsman==null) continue;
                    Add(sportsman);
                }
            }

            public static void Sort(Team[] teams)
            {
                if (teams == null || teams.Length == 0) return;
                for (int i = 0; i < teams.Length-1; i++)
                {
                    for (int j = 0; j < teams.Length - i - 1; j++)
                    {
                        if (teams[j].SummaryScore < teams[j + 1].SummaryScore)
                        {
                            (teams[j], teams[j + 1]) = (teams[j + 1], teams[j]);
                        }
                        else if (teams[j].SummaryScore == teams[j + 1].SummaryScore)
                        {
                            if (teams[j].TopPlace > teams[j + 1].TopPlace) (teams[j], teams[j + 1]) = (teams[j + 1], teams[j]);
                        }
                    }
                }
            }

            protected abstract double GetTeamStrength();

            public static Team GetChampion(Team[] teams)
            {
                if (teams == null || teams.Length == 0) return null;

                Team champion = null; 
                double maxStrength = double.MinValue; 

                foreach (var team in teams)
                {
                    if (team == null) continue; 

                    double strength = team.GetTeamStrength(); 
                    if (strength > maxStrength)
                    {
                        champion = team; 
                        maxStrength = strength; 
                    }
                }

                return champion; 
            }
            public void Print()
            {
                Console.WriteLine(_name);
                for (int i = 0; i < _cnt; i++)
                    _sportsmen[i].Print();
                Console.WriteLine();
            }
        }

        public class ManTeam : Team
        {
            public ManTeam(string name) : base(name) { }
            protected override double GetTeamStrength()
            {
                int sum = 0, cnt=0;

                foreach(Sportsman sportsman in this.Sportsmen)
                {
                    if (sportsman == null) continue;
                    sum += sportsman.Place;
                    cnt++;
                }
                return (100 / (sum / cnt));

            }
        }

        public class WomanTeam : Team
        {
            public WomanTeam(string name) : base(name) { }
            protected override double GetTeamStrength()
            {
                int sum = 0, cnt=0,product=1;
                foreach(Sportsman sportsman in this.Sportsmen)
                {
                    if (sportsman == null) continue;
                    sum += sportsman.Place;
                    product *= sportsman.Place;
                    cnt++;
                }
                return (100 * (sum * cnt / product));
            }
        }
    }
}
