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
        }
        
       
            public abstract class Team
        {
            private string _name;
            private int _count;
            private Sportsman[] _sportsmen;

            public string Name => _name;
            public Sportsman[] Sportsmen
            {
                get
                {
                    if (_sportsmen == null) return null;
                    return _sportsmen;
                }
            }

            //свойство
            public int SummaryScore
            {
                get
                {
                    if (_sportsmen == null) return 0;

                    int sum = 0;
                    for (int i = 0; i < _count; i++)
                    {
                        if (_sportsmen[i].Place <= 5 && _sportsmen[i].Place != 0)
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
                if (_sportsmen == null || _sportsmen.Length < count) return;
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
               
                return 100/(mid/f);
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
                if (place > 0)
                {
                    _place = place;
                }
                else return;
            }

            public void Print()
            {
                Console.WriteLine($"Спортсмен: {Name} {Surname}, Место: {Place}");
            }
        }

        public abstract class Team
        {
            private string _name;
            private Sportsman[] _sportsmen;
            private int _count;

            public string Name => _name;
            public Sportsman[] Sportsmen
            {
                get
                {
                    if (_sportsmen == null) return null;
                    return _sportsmen;
                }
            }

            public int SummaryScore
            {
                get
                {
                    if (Sportsmen == null || Sportsmen.Length == 0)
                        return 0;

                    int totalScore = 0;

                    foreach (var sportsman in Sportsmen)
                    {
                        if (sportsman != null)
                        {
                            switch (sportsman.Place)
                            {
                                case 1: totalScore += 5; break;
                                case 2: totalScore += 4; break;
                                case 3: totalScore += 3; break;
                                case 4: totalScore += 2; break;
                                case 5: totalScore += 1; break;
                                default: break;
                            }
                        }
                    }
                    return totalScore;
                }
            }

            public int TopPlace
            {
                get
                {
                    if (Sportsmen == null || Sportsmen.Length == 0)
                        return 0;

                    int topPlace = int.MaxValue;
                    foreach (var sportsman in Sportsmen)
                    {
                        if (sportsman != null && sportsman.Place < topPlace && sportsman.Place != 0)
                        {
                            topPlace = sportsman.Place;
                        }
                    }
                    return topPlace == int.MaxValue ? 18 : topPlace;
                }
            }

            protected Team(string name)
            {
                _name = name;
                _sportsmen = new Sportsman[6];
                _count = 0;
            }

            public void Add(Sportsman sportsman)
            {
                if (_count < 6 && sportsman != null)
                {
                    if (_sportsmen == null)
                    {
                        _sportsmen = new Sportsman[6];
                    }
                    _sportsmen[_count] = sportsman;
                    _count++;
                }
            }

            public void Add(Sportsman[] sportsmen)
            {
                foreach (var sportsman in sportsmen)
                {
                    Add(sportsman);
                }
            }

            public static void Sort(Team[] teams)
            {
                if (teams == null || teams.Length == 0) return;
                for (int i = 0; i < teams.Length; i++)
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
                if (teams == null || teams.Length == 0)
                    return null;

                Team champion = teams[0];
                double maxStrength = champion.GetTeamStrength();

                for (int i = 1; i < teams.Length; i++)
                {
                    double currentStrength = teams[i].GetTeamStrength();
                    if (currentStrength > maxStrength)
                    {
                        maxStrength = currentStrength;
                        champion = teams[i];
                    }
                }

                return champion;
            }
            public void Print()
            {
                Console.WriteLine($"Команда: {Name}");
                Console.WriteLine($"Суммарный балл: {SummaryScore}");
                Console.WriteLine($"Наивысшее место: {TopPlace}");
                Console.WriteLine("Спортсмены:");

                if (Sportsmen != null && Sportsmen.Length > 0)
                {
                    foreach (var sportsman in Sportsmen)
                    {
                        sportsman?.Print();
                    }
                }
                else
                {
                    Console.WriteLine("Нет данных о спортсменах.");
                }
            }

        }

        public class ManTeam : Team
        {
            public ManTeam(string name) : base(name) { }

            protected override double GetTeamStrength()
            {
                if (Sportsmen == null || Sportsmen.Length == 0)
                    return 0;

                double sumPlaces = 0;
                int count = 0;

                foreach (var sportsman in Sportsmen)
                {
                    if (sportsman != null && sportsman.Place != 0)
                    {
                        sumPlaces += sportsman.Place;
                        count++;
                    }
                }

                if (count == 0)
                    return 0;

                return 100 / (sumPlaces / count);
            }
        }

        public class WomanTeam : Team
        {
            public WomanTeam(string name) : base(name) { }

            protected override double GetTeamStrength()
            {
                if (Sportsmen == null || Sportsmen.Length == 0)
                    return 0;

                double sumPlaces = 0;
                double productPlaces = 1;
                int count = 0;

                foreach (var sportsman in Sportsmen)
                {
                    if (sportsman?.Place != 0)
                    {
                        sumPlaces += sportsman.Place;
                        productPlaces *= sportsman.Place;
                        count++;
                    }
                }

                if (count == 0)
                    return 0;

                return 100 * sumPlaces * count / productPlaces;
            }
        }
    }



