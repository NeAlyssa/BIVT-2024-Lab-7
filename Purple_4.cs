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
            private double _timeRun;
            private bool _flag;

            public string Name => _name;
            public string Surname => _surname;
            public double Time => _timeRun;

            public Sportsman(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _timeRun = 0;
                _flag = false;
            }

            public void Run(double time)
            {
                if (_flag == true) return;
                _timeRun = time;
                _flag = true;
            }

            public void Print()
            {
            }
            public static void Sort(Sportsman[] array)
            {
                if (array == null) return;
                Array.Sort(array, (a, b) =>
                {
                    double x = a.Time - b.Time;
                    if (x < 0) return -1;
                    else if (x > 0) return 1;
                    else return 0;
                });
            }
        }
        public class Group
        {
            private string _title;
            private Sportsman[] _sportsmen;

            public string Name => _title;
            public Sportsman[] Sportsmen => _sportsmen;
             

            public Group(string title)
            {
                _title = title;
                _sportsmen = new Sportsman[0];
            }
            public Group(Group grop_2)
            {
                _title = grop_2.Name;
                if (grop_2.Sportsmen != null)
                {
                    _sportsmen = new Sportsman[grop_2.Sportsmen.Length];
                    Array.Copy(grop_2.Sportsmen, _sportsmen, grop_2.Sportsmen.Length);
                }
                else
                {
                    _sportsmen = new Sportsman[0];
                }
            }

            public void Add(Sportsman s)
            {
                if (_sportsmen == null) return;
                Sportsman[] new_sport = new Sportsman[_sportsmen.Length + 1];
                Array.Copy(_sportsmen, new_sport, _sportsmen.Length);
                new_sport[new_sport.Length - 1] = s;
                _sportsmen = new_sport;
            }

            public void Add(Sportsman[] s)
            {
                if (_sportsmen == null || s == null) return;
                Sportsman[] new_sport = new Sportsman[_sportsmen.Length + s.Length];
                Array.Copy(_sportsmen, new_sport, _sportsmen.Length);
                Array.ConstrainedCopy(s, 0, new_sport, _sportsmen.Length, s.Length);
                _sportsmen = new_sport;


            }

            public void Add(Group g)
            {
                if (_sportsmen == null) return;
                Add(g.Sportsmen);
            }

            public void Sort()
            {
                if (_sportsmen == null) return;
                Array.Sort(_sportsmen, (a, b) =>
                {
                    double x = a.Time - b.Time;
                    if (x > 0) return 1;
                    else if (x < 0) return -1;
                    else return 0;
                });
            }

            public static Group Merge(Group group1, Group group2)
            {
                Group g = new Group("Финалисты");
                var first = group1.Sportsmen;
                var second = group2.Sportsmen;
                if (group1.Sportsmen == null) first = new Sportsman[0];
                if (group2.Sportsmen == null) second = new Sportsman[0];
                g._sportsmen = new Sportsman[first.Length + second.Length];
                for (int i = 0, j = 0; i < first.Length || j < second.Length;)
                {
                    if (i < first.Length && j < second.Length)
                    {
                        if (first[i].Time <= second[j].Time)
                        {
                            g._sportsmen[i + j] = first[i++];
                        }
                        else
                        {
                            g._sportsmen[i + j] = second[j++];
                        }
                    }
                    else
                    { 
                        if (i < first.Length)
                        {
                            Array.ConstrainedCopy(first, i, g._sportsmen, i + j, first.Length - i);
                            i = first.Length;
                        }
                        if (j < second.Length)
                        {
                            Array.ConstrainedCopy(second, j, g._sportsmen, i + j, second.Length - j);
                            j = second.Length;
                        }
                    }
                }
                return g;
            }
            public void Print()
            {
            }
            
            public void Split(out Sportsman[] men, out Sportsman[] women)
            {
                if (_sportsmen == null) 
                {
                    women = null;
                    men = null;
                    return;
                }
                
                men = _sportsmen.Where(x => (x is SkiMan)).ToArray();
                women = _sportsmen.Where(x => (x is SkiWoman)).ToArray();

            }
            public void Shuffle()
            {
                if (_sportsmen == null) return;
                Sort();
                Split(out Sportsman[] men, out Sportsman[] women);
                if (men == null || women == null || men.Length == 0 || women.Length == 0) return;
                if (men.Length != 0 && women.Length != 0 && men[0].Time > women[0].Time)
                {
                    var neww = women;
                    women = men;
                    men = neww;
                }
                int index_total = 0;
                int i;
                for (i = 0; i < men.Length && i < women.Length; i++) 
                {
                    _sportsmen[index_total] = men[i];
                    index_total++;
                    _sportsmen[index_total] = women[i];
                    index_total++;
                }
                for(int j = i; j < men.Length; j++)
                {
                    _sportsmen[index_total] = men[j];
                    index_total++;
                }
                for (int j = i; j < women.Length; j++)
                {
                    _sportsmen[index_total] = women[j];
                    index_total++;
                }
            }
        }
        public class SkiMan : Sportsman
        {
            public SkiMan(string name, string surname) : base(name, surname) { }
            public SkiMan(string name, string surname, double time) : base(name, surname) 
            {
                Run(time);
            }

        }
        public class SkiWoman : Sportsman
        {
            public SkiWoman(string name, string surname) : base(name, surname) { }
            public SkiWoman(string name, string surname, double time) : base(name, surname)
            {
                Run(time);
            }
        }
    }
}
