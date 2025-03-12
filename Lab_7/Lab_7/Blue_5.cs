using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Lab_7
{
    public class Blue_5
    {
        public class Sportsman
        {
            //поля
            private string _name;
            private string _surname;
            private int _place;

            //свойства
            public string Name => _name;
            public string Surname => _surname;
            public int Place => _place;

            //конструкторы
            public Sportsman(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _place = 0;
            }
            //методы
            public void SetPlace(int place)
            {
                if (_place > 0 || place < 1) return; //если место уже есть или передано некорректное место
                _place = place;
            }
            public void Print()
            {
                Console.WriteLine($"{_name} {_surname} is on the {_place} place");
            }
        }//class Sportsman

        public abstract class Team
        {
            //поля
            private string _name;
            private Sportsman[] _sportsmen; // 6
            private int _sportsmenNum;

            //свойства
            public string Name => _name;
            public Sportsman[] Sportsmen => _sportsmen;
            //protected int SportsmenNum => _sportsmenNum;
            
            public int SummaryScore
            {
                get
                {
                    if (_sportsmen == null) return 0;
                    if (_sportsmen[0] == null) return 0;

                    int sum = 0;
                    for (int k = 0; k < _sportsmenNum; k++)
                    {
                        int score = 6 - _sportsmen[k].Place;
                        if (score < 0 || score > 5) score = 0;
                        sum += score;
                    }
                    return sum;
                }
            }
            public int TopPlace
            {
                get
                {
                    if (_sportsmen == null) return 0;
                    if (_sportsmen[0] == null) return 0;

                    int top = _sportsmen[0].Place;
                    for (int k = 1; k < _sportsmenNum; k++)
                    {
                        if (_sportsmen[k].Place < top)
                            top = _sportsmen[k].Place;
                    }
                    return top;
                }
            }

            //конструкторы
            public Team(string name)
            {
                _name = name;
                _sportsmen = new Sportsman[6];
                _sportsmenNum = 0;
            }

            //методы
            public void Add(Sportsman sportsman)
            {
                if (sportsman == null || _sportsmen == null || _sportsmenNum >= _sportsmen.Length) return;
                
                _sportsmen[_sportsmenNum] = sportsman;
                _sportsmenNum++;
            }
            public void Add(Sportsman[] sportsmen)
            {
                if (_sportsmen == null || sportsmen == null || sportsmen.Length == 0 || _sportsmenNum >= _sportsmen.Length)
                    return;
                
                int k = 0;
                while (_sportsmenNum < _sportsmen.Length && k < sportsmen.Length)
                {
                    if (sportsmen[k] != null)
                    {
                        _sportsmen[_sportsmenNum] = sportsmen[k];
                        _sportsmenNum++;
                    }
                    k++;
                }
            }
            private static void Swap(ref Team[] teams, int i, int j)
            {
                Team tmp = teams[i];
                teams[i] = teams[j];
                teams[j] = tmp;
            }
            public static void Sort(Team[] teams)
            {
                if (teams == null || teams.Length == 0) return;

                for (int i = 0; i < teams.Length - 1; i++)
                {
                    for (int j = 0; j < teams.Length - i - 1; j++)
                    {
                        if (teams[j] == null)
                            Swap(ref teams, j, j + 1);
                        else if (teams[j + 1] == null)
                            continue;
                        else if (teams[j + 1].SummaryScore > teams[j].SummaryScore)
                            Swap(ref teams, j, j + 1);
                        else if (teams[j + 1].SummaryScore == teams[j].SummaryScore &&
                            teams[j + 1].TopPlace < teams[j].TopPlace)
                            Swap(ref teams, j, j + 1);
                    }
                }
            }
            protected abstract double GetTeamStrength();
            public static Team GetChampion(Team[] teams)
            {
                if (teams == null || teams.Length == 0) return null;

                double maxStrength;
                if (teams[0] == null) maxStrength = 0;
                else maxStrength = teams[0].GetTeamStrength();
                int maxIndex = 0;
                for(int n = 1; n < teams.Length; n++)
                {
                    if (teams[n] == null) continue;
                    if (teams[n].GetTeamStrength() > maxStrength)
                    {
                        maxStrength = teams[n].GetTeamStrength();
                        maxIndex = n;
                    }
                }
                return teams[maxIndex];
            }
            public void Print()
            {
                double strength = Math.Round(GetTeamStrength(), 2);
                Console.WriteLine($"{_name}'s strength = {strength}");
                for (int k = 0; k < _sportsmenNum; k++)
                    _sportsmen[k].Print();
                Console.WriteLine();
            }

        }// abstract class Team

        public class ManTeam : Team
        {
            public ManTeam(string name) : base(name) { }
            protected override double GetTeamStrength()
            {
                double totalPlaces = 0;
                int num = 0;
                foreach(Sportsman sportsman in this.Sportsmen)
                {
                    if(sportsman == null) continue;
                    totalPlaces += sportsman.Place;
                    num++;
                }
                double averagePlases = totalPlaces / num;
                return 100 / averagePlases;
            }
        } //  class ManTeam : Team

        public class WomanTeam : Team
        {
            public WomanTeam(string name) : base(name) { }
            protected override double GetTeamStrength()
            {
                double sumPlaces = 0;
                int multPlaces = 1;
                int num = 0;
                foreach (Sportsman sportsman in this.Sportsmen)
                {
                    if (sportsman == null) continue;
                    multPlaces *= sportsman.Place;
                    sumPlaces += sportsman.Place;
                    num++;
                }
                return 100 * sumPlaces * num / multPlaces;
            }
        } //  class WomanTeam : Team

    }
}
