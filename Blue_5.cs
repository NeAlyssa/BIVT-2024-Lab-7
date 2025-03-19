using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Lab_6
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
            public string Name { get { return _name; } }
            public string Surname { get { return _surname; } }
            public int Place { get { return _place; } }
            //конструктор 
            public Sportsman(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _place = 0;
            }
            //методы
            public void SetPlace(int place)
            {
                if (_place != 0) return;
                _place = place;
            }
            public void Print()
            {
                Console.WriteLine($"{_name} {_surname} {_place}");
            }
        }
        public abstract class Team
        {
            //поля
            private string _name;
            private Sportsman[] _sportsmen;
            private int _count;
            //свойства 
            public string Name { get { return _name; } }
            public Sportsman[] Sportsmen
            {
                get { return _sportsmen; }
            }
            public int SummaryScore
            {
                get
                {
                    if (_sportsmen == null || _sportsmen.Length == 0) return 0;
                    int sum = 0;
                    for (int i = 0; i < _sportsmen.Length; i++)
                    {
                        if (_sportsmen[i].Place == 1) sum += 5;
                        else if (_sportsmen[i].Place == 2) sum += 4;
                        else if (_sportsmen[i].Place == 3) sum += 3;
                        else if (_sportsmen[i].Place == 4) sum += 2;
                        else if (_sportsmen[i].Place == 5) sum += 1;
                    }
                    return sum;
                }
            }
            public int TopPlace
            {
                get
                {
                    if (_sportsmen == null || _sportsmen.Length == 0) return 18;
                    int maxPlace = 18;
                    for (int i = 0; i < _sportsmen.Length; i++)
                    {
                        if (_sportsmen[i].Place > 0 && _sportsmen[i].Place < maxPlace) maxPlace = _sportsmen[i].Place;
                    }
                    return maxPlace;
                }
            }

            //конструктор 
            public Team(string name)
            {
                _name = name;
                _sportsmen = new Sportsman[6];
                _count = 0;
            }
            //методы
            public void Add(Sportsman sportsman)
            {
                if (_sportsmen == null || sportsman == null || _sportsmen.Length == 0 || _count >= _sportsmen.Length) return;
                _sportsmen[_count] = sportsman;
                _count++;
            }
            public void Add(Sportsman[] sportsman)
            {
                if (_sportsmen == null || sportsman == null || sportsman.Length == 0 || _sportsmen.Length == 0 || _count >= _sportsmen.Length) return;
                int i = 0;
                while (_count < _sportsmen.Length && i < sportsman.Length)
                {
                    if (sportsman[i] == null) continue;
                    _sportsmen[_count] = sportsman[i];
                    _count++;
                    i++;
                }
            }
            public static void Sort(Team[] teams)
            {
                if (teams == null || teams.Length == 0) return;
                for (int i = 1, j = 2; i < teams.Length;)
                {
                    if (i == 0 || teams[i - 1].SummaryScore > teams[i].SummaryScore)
                    {
                        i = j;
                        j++;
                    }
                    else if (teams[i - 1].SummaryScore == teams[i].SummaryScore && teams[i - 1].TopPlace <= teams[i].TopPlace)
                    {
                        i = j;
                        j++;
                    }
                    else
                    {
                        Team temp = teams[i];
                        teams[i] = teams[i - 1];
                        teams[i - 1] = temp;
                        i--;
                    }
                }
                //на всякий случай возможно стоит проверить, что каждый элемент teams не null, иначе наверное его надо в конец 
            }
            protected abstract double GetTeamStrength();
            public static Team GetChampion(Team[] teams)
            {
                if (teams == null || teams.Length == 0) return null; //если массив с командами не содержит команд, то и команду с максимальным результатом мы вывести не сможем
                double maxTeam = 0;
                if (teams[0] != null) maxTeam = teams[0].GetTeamStrength();
                int maxIndex = 0;
                for (int i = 0; i < teams.Length; i++)
                {
                    if (teams[i] != null && teams[i].GetTeamStrength() > maxTeam)
                    {
                        maxTeam = teams[i].GetTeamStrength();
                        maxIndex = i;
                    }
                }
                return teams[maxIndex];
            }
            public void Print()
            {
                for (int i = 0; i < _sportsmen.Length; i++)
                {
                    Console.WriteLine($"{_name} {SummaryScore} {TopPlace}");
                }
            }
        }
        public class ManTeam : Team
        {
            //конструктор
            public ManTeam(string name) : base(name) { }
            protected override double GetTeamStrength()
            {
                double plases = 0;
                int count = 0;
                for (int i = 0; i < this.Sportsmen.Length; i++)
                {
                    if (this.Sportsmen[i] != null)
                    {
                        plases += this.Sportsmen[i].Place;
                        count++;
                    }
                }
                double mid = plases / count;
                return 100 / mid;
            }
        }
        public class WomanTeam : Team
        {
            //конструктор
            public WomanTeam(string name) : base(name) { }
            protected override double GetTeamStrength()
            {
                double plases = 0;
                int count = 0;
                double plases1 = 1;
                for (int i = 0; i < this.Sportsmen.Length; i++)
                {
                    if (this.Sportsmen[i] != null)
                    {
                        plases += this.Sportsmen[i].Place;
                        plases1 *= this.Sportsmen[i].Place;
                        count++;
                    }
                }
                return 100 * plases * count / plases1;
            }
        }
    }
}
