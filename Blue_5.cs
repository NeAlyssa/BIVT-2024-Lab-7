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
                if (_place == 0 && place > 0)
                {
                    _place = place;
                }
            }

            public void Print()
            {
                Console.WriteLine($"{_name} {_surname} - {_place}");
            }
        } // class Sportsman
        
       
        public abstract class Team
        {
            private string _name;
            private int _count;
            private Sportsman[] _sportsmen;

            public string Name => _name;
            public Sportsman[] Sportsmen => _sportsmen;
           

            //свойство
            public int SummaryScore
            {
                get
                {
                    if (_sportsmen == null) return 0;

                    int sum = 0;
                    for (int i = 0; i < _count; i++)
                    {
                        if (_sportsmen[i] != null && _sportsmen[i].Place <= 5)
                        {
                            sum += 6 - _sportsmen[i].Place;
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

                    int topPlace = 18;
                    for (int i = 0; i < _count; i++)
                    {
                        if (_sportsmen[i].Place < topPlace && _sportsmen[i].Place != 0)
                        {
                            topPlace = _sportsmen[i].Place;
                        }
                    }
                    return topPlace;
                }
            }

            public Team(string name)
            {
                _name = name;
                _sportsmen = new Sportsman[6];
                _count = 0;
            }

            public void Add(Sportsman sportsman)
            {
                if (_sportsmen == null || _count >= _sportsmen.Length) return;

                _sportsmen[_count++] = sportsman;
            }

            public void Add(Sportsman[] sportsmen)
            {
                if (_sportsmen == null || sportsmen == null) return;

                foreach (Sportsman sportsman in sportsmen)
                {
                    Add(sportsman);
                }
            }

            // method void Remove(int count)
            public void Remove(int count)
            {
                if (_sportsmen == null ) return;
                var newarray = new Sportsman[_sportsmen.Length - count];
                for (int i = 0; i < _sportsmen.Length - count; i++)
                {
                    newarray[i] = _sportsmen[i];
                }

                _sportsmen = newarray;
            }

            public static void Sort(Team[] teams)
            {
                if (teams == null || teams.Length == 0)
                    return;

                for (int i = 0; i < teams.Length - 1; i++)
                {
                    for (int j = 0; j < teams.Length - 1 - i; j++)
                    {
                        if (teams[j].SummaryScore < teams[j + 1].SummaryScore)
                        {
                            Team temp = teams[j];
                            teams[j] = teams[j + 1];
                            teams[j + 1] = temp;
                        }
                        else if (teams[j].SummaryScore == teams[j + 1].SummaryScore)
                        {
                            if (teams[j].TopPlace > teams[j + 1].TopPlace)
                            {
                                Team temp = teams[j];
                                teams[j] = teams[j + 1];
                                teams[j + 1] = temp;
                            }
                        }
                    }
                }
            }


            public void Print()
            {
                Console.WriteLine($"{_name}: ");
                foreach (Sportsman sportsman in _sportsmen)
                {
                    sportsman.Print();
                }
                Console.WriteLine("");
            }
            protected abstract double GetTeamStrength();
            public static Team GetChampion(Team[] teams)
            {
                if (teams == null) return null;
                Team champion = teams[0];
                double max = double.MinValue;
                
                for (int i = 0; i < teams.Length; i++)
                {
                    double str = teams[i].GetTeamStrength();
                    if (str > max)
                    {
                        max = str;
                        champion = teams[i];
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
                if (Sportsmen == null || Sportsmen.Length == 0)  return 0;
                double mid;
                mid = 0;
                int f = 0;
                foreach (Sportsman sportsman in Sportsmen)
                {
                    mid += sportsman.Place; f++;
                }
                double sr = mid / f;
                return 100/sr;
            }
        }


       
        public class WomanTeam : Team
            {
                public WomanTeam(string name) : base(name) { }
                protected override double GetTeamStrength()
                {
                    double sum = 0, mark = 1, otv;
                    foreach (Sportsman sportsman in Sportsmen)
                    {
                        
                    sum = sum + sportsman.Place;
                    mark = mark * sportsman.Place;
                    }
                otv = 100.0 * sum * Sportsmen.Length / mark; 
                    return otv;
                }
            }

        }
}


       
   

