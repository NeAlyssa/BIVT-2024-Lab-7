using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    public class Purple_4
    {
        public class Sportsman
        {
            private string _name;
            private string _surname;
            private double _time;
            private bool _timeSet;

            public string Name { get { return _name; } }
            public string Surname { get { return _surname; } }
            public double Time { get { return _time; } }

            public Sportsman(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _time = 0;
                _timeSet = false;
            }

            public void Run(double time)
            {
                if (!_timeSet)
                {
                    _time = time;
                    _timeSet = true;
                }
            }

            public static void Sort(Sportsman[] array)
            {
                if (array == null) return;
                
                var newArr = array
                    .OrderBy(sportsman => sportsman?.Time ?? double.MaxValue)
                    .ToArray();
                Array.Copy(newArr, array, newArr.Length);
            }
            public void Print() { }
        }

        public class SkiMan : Sportsman
        {
            public SkiMan(string name, string surname) : base(name, surname) {}

            public SkiMan(string name, string surname, double time) : base(name, surname)
            {
                Run(time);
            }
        }

        public class SkiWoman : Sportsman
        {
            public SkiWoman(string name, string surname) : base(name, surname) {}

            public SkiWoman(string name, string surname, double time) : base(name, surname)
            {
                Run(time);
            }
        }



        public struct Group
        {
            private string _name;
            private Sportsman[] _sportsmen;

            public string Name { get { return _name; } }
            public Sportsman[] Sportsmen
            {
                get
                {
                    if (_sportsmen == null) { return null; }
                    return _sportsmen;
                }
            }

            public Group(string name)
            {
                _name = name;
                _sportsmen = new Sportsman[0];
            }
            public Group(Group other)
            {
                _name = other.Name;
                if (other.Sportsmen != null)
                {
                    _sportsmen = new Sportsman[other.Sportsmen.Length];
                    Array.Copy(other.Sportsmen, _sportsmen, _sportsmen.Length);
                } else
                {
                    _sportsmen = new Sportsman[0];
                }
            }

            public void Add(Sportsman sportsman)
            {
                if (_sportsmen == null) { return; }
                Sportsman[] newArray = new Sportsman[_sportsmen.Length + 1];
                Array.Copy(_sportsmen, newArray, _sportsmen.Length);
                newArray[_sportsmen.Length] = sportsman;
                _sportsmen = newArray;
            }

            public void Add(Sportsman[] sportsmen)
            {
                if (sportsmen == null || _sportsmen == null) { return; }
                Sportsman[] newArray = new Sportsman[_sportsmen.Length + sportsmen.Length];
                Array.Copy(_sportsmen, newArray, _sportsmen.Length);
                Array.ConstrainedCopy(sportsmen, 0, newArray, _sportsmen.Length, sportsmen.Length);
                _sportsmen = newArray;
            }

            public void Add(Group other)
            {
                if(_sportsmen == null || other.Sportsmen == null) { return; }
                Add(other.Sportsmen);
            }
            
            public void Sort()
            {
                if (_sportsmen == null) { return; }
                _sportsmen = _sportsmen
                    .OrderBy(sportsman => sportsman?.Time ?? double.MaxValue)
                    .ToArray();
            }

            public static Group Merge(Group group1, Group group2)
            {
                Group merged = new Group("Финалисты");
                Sportsman[] gr1, gr2;
                if(group1.Sportsmen == null) { gr1 = new Sportsman[0]; }
                else {  gr1 = group1.Sportsmen; }
                if(group2.Sportsmen == null) { gr2 = new Sportsman[0]; }
                else { gr2 = group2.Sportsmen; }
                merged._sportsmen = new Sportsman[gr1.Length + gr2.Length];
                for (int i = 0, j = 0; i < gr1.Length || j < gr2.Length;)
                {
                    if (i < gr1.Length && gr1[i] == null) { ++i; continue;  }
                    if (j < gr2.Length && gr2[j] == null) { ++j; continue; }
                    if(i < gr1.Length && j < gr2.Length)
                    {
                        if (gr1[i].Time <= gr2[j].Time)
                        {
                            merged._sportsmen[i + j] = gr1[i++];
                        } else
                        {
                            merged._sportsmen[i + j] = gr2[j++];
                        }
                    } else
                    {
                        if(i < gr1.Length)
                        {
                            Array.ConstrainedCopy(gr1, i, merged._sportsmen, i + j, gr1.Length - i);
                            i = gr1.Length;
                        }
                        if(j < gr2.Length)
                        {
                            Array.ConstrainedCopy(gr2, j, merged._sportsmen, i + j, gr2.Length - j);
                            j = gr2.Length;
                        }
                    }
                }
                return merged;
            }

            private void Count(out int count_men, out int count_women)
            {
                count_men = 0; count_women = 0;
                if (_sportsmen == null) return;
                foreach(Sportsman sportsman in _sportsmen)
                {
                    if (sportsman is SkiMan)
                    {
                        count_men++;
                    } else if(sportsman is SkiWoman)
                    {
                        count_women++;
                    }
                }
            }
            public void Split(out Sportsman[] men, out Sportsman[] women)
            {
                Count(out int count_men, out int count_women);
                men = new Sportsman[count_men]; women = new Sportsman[count_women];
                if (_sportsmen == null) return;
                int i = 0, j = 0;
                foreach (Sportsman sportsman in _sportsmen) {
                    if (sportsman is SkiMan) { men[i++] = sportsman; }
                    else if(sportsman is SkiWoman) { women[j++] = sportsman; }
                }
            }

            public void Shuffle()
            {
                if(_sportsmen == null) return;
                Sort();
                Split(out Sportsman[] men, out Sportsman[] women);
                if(men.Length == 0 || women.Length == 0) return;
                bool started_with_woman = men[0].Time > women[0].Time;
                for(int i = 0; i < Math.Min(women.Length, men.Length); ++i)
                {
                    _sportsmen[started_with_woman ? 2 * i + 1 : 2 * i] = men[i];
                    _sportsmen[started_with_woman ? 2 * i : 2 * i + 1] = women[i];
                }
                int ind = Math.Min(women.Length, men.Length);
                for(int i = 2 * ind; i < _sportsmen.Length; ++i)
                {
                    if(ind >= women.Length)
                    {
                        _sportsmen[i] = men[ind++]; 
                    } else
                    {
                        _sportsmen[i] = women[ind++];
                    }
                }
            }

            public void Print()
            {

            }
        }
    }
}
