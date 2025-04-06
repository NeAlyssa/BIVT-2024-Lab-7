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
            private bool _placeset;

            public string Name => _name;
            public string Surname => _surname;
            public int Place => _place;

            public Sportsman(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _place = 0;
                _placeset = false;
            }

            public void SetPlace(int place) //устанавливает место спортсмена в таблице
            {
                if (_placeset==false)
                {
                    _place = place;
                    _placeset = true;
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
                    if (_sportsmen == null || _sportsmen.Length==0 || _sportsmen[0]==null) return 0;
                    int score = 0;
                    for (int i=0; i<_sportsmen.Length;i++)
                    {
                        if (_sportsmen[i] == null) continue;
                        if (_sportsmen[i].Place == 1) score += 5;
                        else if (_sportsmen[i].Place == 2) score += 4;
                        else if (_sportsmen[i].Place == 3) score += 3;
                        else if (_sportsmen[i].Place == 4) score += 2;
                        else if (_sportsmen[i].Place == 5) score += 1;
                    }
                    return score;
                }
            }

            public int TopPlace
            {
                get
                {
                    if (_sportsmen == null || _sportsmen[0] == null) return 18;
                    int topplace = _sportsmen[0].Place;
                    for (int i=1;i<_cnt;i++)
                    {
                        if (_sportsmen[i].Place<topplace)
                        {
                            topplace = _sportsmen[i].Place;
                        }
                    }
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
                if (_sportsmen == null || _cnt==6 || sportsman == null) return ;
                _sportsmen[_cnt] = sportsman;
                _cnt++;
            }

            public void Add(Sportsman[] newSportsmen)
            {
                if (_sportsmen == null || newSportsmen==null) return;
                foreach (Sportsman sportsman in newSportsmen)
                {
                    Add(sportsman);
                }
            }

            public static void Sort(Team[] teams)
            {
                if (teams == null || teams.Length <= 1) return;
                for (int i = 0; i < teams.Length; i++)
                {
                    for (int j = 0; j < teams.Length - i - 1; j++)
                    {
                        if (teams[j] == null)
                        {
                            (teams[j], teams[j + 1]) = (teams[j + 1], teams[j]);
                        }
                        else if (teams[j + 1] == null) continue;
                        else if (teams[j].SummaryScore < teams[j + 1].SummaryScore)
                        {
                            (teams[j], teams[j + 1]) = (teams[j + 1], teams[j]);
                        }
                        else if (teams[j].SummaryScore == teams[j + 1].SummaryScore && teams[j].TopPlace > teams[j + 1].TopPlace)
                        {
                            (teams[j], teams[j + 1]) = (teams[j + 1], teams[j]);
                        } 
                    }
                }
            }

            protected abstract double GetTeamStrength();

            public static Team GetChampion(Team[] teams)
            {
                if (teams == null) return null;

                Team champion = teams[0]; 
                double maxStrength;
                if (champion == null) maxStrength = 0;
                else maxStrength = teams[0].GetTeamStrength();
                foreach (Team team in teams)
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
                double sum = 0;
                foreach(Sportsman sportsman in Sportsmen)
                {
                    sum += sportsman.Place;
                }
                return (100 / (sum / 2));

            }
        }

        public class WomanTeam : Team
        {
            public WomanTeam(string name) : base(name) { }
            protected override double GetTeamStrength()
            {
                double sum = 0, product=1;
                foreach(Sportsman sportsman in Sportsmen)
                {
                    sum += sportsman.Place;
                    product *= sportsman.Place;
                }
                return (100 * sum * Sportsmen.Length / product);
            }
        }
    }
}
