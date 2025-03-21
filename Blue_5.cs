using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    public class Blue_5
    {
        public abstract class Sportsman
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
                if (_place > 0) return;
                _place = place;
            }

            public void Print()
            {
                Console.WriteLine($"Name: {_name}, Surname: {_surname}, Place: {_place}");
            }


        }

      

        public abstract class Team
        {
            private string _name;
            private Sportsman[] _sportsmen;
            private int _curInd;
            public string Name => _name;
            public Sportsman[] Sportsmen => _sportsmen;
            public int SummaryScore
            {
                get
                {
                    if (_sportsmen == null) return 0;
                    int sum = 0;
                    for (int i = 0; i < _sportsmen.Length; i++)
                    {
                        switch (_sportsmen[i].Place)
                        {
                            case 1:
                                sum += 5;
                                break;
                            case 2:
                                sum += 4;
                                break;
                            case 3:
                                sum += 3;
                                break;
                            case 4:
                                sum += 2;
                                break;
                            case 5:
                                sum += 1;
                                break;
                            default:
                                sum += 0;
                                break;

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
                    int min = 18;

                    foreach (Sportsman sportsman in _sportsmen)
                    {
                        if (sportsman.Place < min && sportsman.Place != 0)
                            min = sportsman.Place;

                    }
                    return min;
                }
            }
            public Team(string name)
            {
                _name = name;
                _sportsmen = new Sportsman[6];
                _curInd = 0;
            }
            public void Add(Sportsman sportsman)
            {
                if (_sportsmen == null || _curInd == 6) return;
                _sportsmen[_curInd] = sportsman;
                _curInd++;

            }
            public void Add(Sportsman[] sportsmen)
            {
                if (sportsmen == null) return;
                foreach (var sportsman in sportsmen)
                    Add(sportsman);
            }

            public static void Sort(Team[] teams)
            {
                if (teams == null || teams.Length <= 1) return;

                for (int i = 0; i < teams.Length; i++)
                {
                    for (int j = 0; j < teams.Length - i - 1; j++)
                    {
                        if (teams[j].SummaryScore < teams[j + 1].SummaryScore)
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

            public void Print()
            {
                Console.WriteLine($"Name: {_name}, Summary score: {SummaryScore}, Top Place: {TopPlace}");
                foreach (var sportsman in _sportsmen)
                {
                    sportsman.Print();
                }
                Console.WriteLine();

            }

            protected abstract double GetTeamStrength();
            

            public static Team GetChampion(Team[] teams)
            {
                if (teams == null)
                    return null;

                Team winner = null;
                double maxx= double.MinValue;
                foreach (Team team in teams)
                {
                    if (team != null)
                    { 
                        double strength = team.GetTeamStrength();
                        if (strength > maxx)
                        {
                            maxx = strength;
                            winner = team;
                        }
                    }

                }
                return winner;
            }

        }


        public class ManTeam : Team
        {
            public ManTeam(string name) : base(name) { }
            protected override double GetTeamStrength()
            {
                double midPlace = 0;
                foreach (Sportsman sportsman in Sportsmen)
                    midPlace += sportsman.Place;

                midPlace /= 2;
                return 100.0 / midPlace;
            }
        }

        public class WomanTeam : Team
        {
            public WomanTeam(string name) : base(name) { }
            protected override double GetTeamStrength()
            {
                double sumPlace = 0;
                double multPlace = 1;
                foreach (Sportsman sportsman in Sportsmen)
                {
                    sumPlace += sportsman.Place;
                    multPlace *= sportsman.Place;
                }
                return 100.0 * sumPlace * Sportsmen.Length / multPlace;
            }

        }
    }
}
