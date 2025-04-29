using System;
using System.Collections.Generic;
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

            public void SetPlace(int place)
            {
                if (place <= 0 || _place > 0) return;

                _place = place;
            }

            public void Print()
            {
                Console.WriteLine($"{_name} {_surname} {_place}");
            }
        }

        public abstract class Team
        {
            private string _name;
            private Sportsman[] _sportsmen;


            private int cout;

            public string Name => _name;
            public Sportsman[] Sportsmen => _sportsmen;
            public int SummaryScore
            {
                get
                {
                    if (_sportsmen == null) return 0;
                    int score = 0;
                    for (int i = 0; i < _sportsmen.Length; i++)
                    {
                        if (_sportsmen[i] == null) continue;
                        switch (_sportsmen[i].Place)
                        {
                            case 1: score += 5; break;
                            case 2: score += 4; break;
                            case 3: score += 3; break;
                            case 4: score += 2; break;
                            case 5: score += 1; break;
                            default: break;
                        }
                    }
                    return score;
                }
            }
            public int TopPlace
            {
                get
                {
                    if (_sportsmen == null) return 18;
                    int topPlace = 18;
                    foreach (Sportsman s in _sportsmen)
                    {
                        if(s == null) continue;


                        if (s.Place < topPlace && s.Place > 0)
                        {
                            topPlace = s.Place;
                        }
                    }

                    return topPlace;
                }
            }

            public Team(string name)
            {
                _name = name;
                _sportsmen = new Sportsman[6];
                cout = 0;
            }

            public void Add(Sportsman sportsman)
            {
                if (_sportsmen == null) return;

                _sportsmen[cout++] = sportsman;
            }

            public void Add(Sportsman[] newSportsmen)
            {
                if (_sportsmen == null || _sportsmen.Length == 0) return;
                int c = 0;


                while(c < newSportsmen.Length && cout < _sportsmen.Length)
                {
                    _sportsmen[cout++] = newSportsmen[c++];
                }
            }
            public static void Sort(Team[] teams)
            {
                if (teams.Length == 0) return;
                if (teams == null) return;

                for (int i = 0; i < teams.Length - 1; i++)
                {
                    for (int j = 0; j < teams.Length - i - 1; j++)
                    {
                        if (teams[j].SummaryScore < teams[j + 1].SummaryScore)
                        {

                            (teams[j], teams[j + 1]) = (teams[j + 1], teams[j]);
                        }
                        else if (teams[j].SummaryScore == teams[j + 1].SummaryScore)
                        {
                            if (teams[j].TopPlace > teams[j + 1].TopPlace)
                            {
                                (teams[j], teams[j + 1]) = (teams[j + 1], teams[j]);

                            }
                        }
                    }

                }
            }
            public void Print()
            {
                for (int i = 0; i < _sportsmen.Length; i++)
                {
                    Console.WriteLine($"{_name}: {SummaryScore}, {TopPlace}");
                }
            }
            protected abstract double GetTeamStrength();

            public static Team GetChampion(Team[] teams)
            {
                if (teams == null || teams.Length == 0) return null;

                Team champion = null;
                double maxstrength = double.MinValue;

                foreach (var team in teams)
                {
                    if (team != null)
                    {
                        double strength = team.GetTeamStrength();
                        if (strength > maxstrength)
                        {
                            maxstrength = strength;
                            champion = team;
                        }
                    }
                }

                return champion;
            }

            
           

            
        }

     
        public class ManTeam : Team
        {
            public ManTeam(string name) : base(name) { }

            protected override double GetTeamStrength()
            {
                double average = 0;
                int ount = 0;

                foreach (var sportsman in Sportsmen)
                {
                    if (sportsman != null )
                    {
                        average += sportsman.Place;
                        ount++;
                    }
                }

                if (ount == 0) return 0;
                average /= ount;

                return 100 / average;
            }
        }

      
        public class WomanTeam : Team
        {
            public WomanTeam(string name) : base(name) { }

            protected override double GetTeamStrength()
            {
                double sumPlaces = 0;
                double p = 1;
                int ount = 0;

                foreach (var sportsman in Sportsmen)
                {
                    if (sportsman != null)
                    {
                        sumPlaces += sportsman.Place;
                        p *= sportsman.Place;
                       ount++;
                    }
                }

                return sumPlaces*ount*100/p;


            }
            
        }
    }
}
