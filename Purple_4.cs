using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using static Lab_6.Purple_4;

namespace Lab_7
{
    public class Purple_4
    {
        public class Sportsman
        {
            private string _name;
            private string _surname;
            private double _time;

            public string Name => _name;
            public string Surname => _surname;
            public double Time => _time;


            public Sportsman(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _time = 0;
            }

            public void Run(double time)
            { 
                _time = time;
            }
            
            public void Print()
            {
                Console.WriteLine(_name + " " + _surname + " " + _time);
            }

            public static void Sort(Sportsman[] array)
            {
                if (array == null) return;
                for (int i = 0; i < array.Length; i++)
                {
                    for (int j = 0; j < array.Length - 1; j++)
                    {
                        if (array[j].Time > array[j + 1].Time)
                        {
                            Sportsman l = array[j];
                            array[j] = array[j + 1];
                            array[j + 1] = l;
                        }
                    }
                }
            }
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
            public SkiWoman(string name, string surname) : base(name, surname) { }
            public SkiWoman(string name, string surname, double time) : base(name, surname)
            {
                Run(time);
            }
        }
        public class Group
        { 
            private string _name;
            private Sportsman[] _sportsmen;

            public string Name => _name;
            public Sportsman[] Sportsmen => _sportsmen;

            public Group(string name)
            { 
                _name = name;
                _sportsmen = new Sportsman[0];
            }
            public Group(Group group)
            {
                _name = group._name;
                _sportsmen = new Sportsman[group._sportsmen.Length];
                for (int i = 0; i < group._sportsmen.Length; i++)
                {
                    _sportsmen[i] = group._sportsmen[i];
                }
            }
            public void Add(Sportsman s)
            {
                Sportsman[] sp = new Sportsman[_sportsmen.Length + 1];
                int i = 0;
                for (; i < _sportsmen.Length; i++)
                {
                    sp[i] = _sportsmen[i];
                }
                sp[i] = s;
                _sportsmen = sp;
            }
            public void Add(Sportsman[] s)
            {
                if (s == null) return;
                if (_sportsmen == null || s.Length == 0) return;
                Sportsman[] sp = new Sportsman[_sportsmen.Length + s.Length];
                int i = 0;
                for (; i < _sportsmen.Length; i++)
                {
                    sp[i] = _sportsmen[i];
                }
                for (int j = 0; j < s.Length; j++, i++)
                {
                    sp[i] = s[j];
                }
                _sportsmen = sp;
            }
            public void Add(Group g)
            {
                Sportsman[] sp = new Sportsman[_sportsmen.Length + g._sportsmen.Length];
                int i = 0;
                for (; i < _sportsmen.Length; i++)
                {
                    sp[i] = _sportsmen[i];
                }
                for (int j = 0; j < g._sportsmen.Length; j++, i++)
                {
                    sp[i] = g._sportsmen[j];
                }
                _sportsmen = sp;
            }
            public void Sort()
            {
                for (int i = 0; i < _sportsmen.Length; i++)
                {
                    for (int j = 0; j < _sportsmen.Length - 1; j++)
                    {
                        if (_sportsmen[j].Time > _sportsmen[j + 1].Time)
                        {
                            Sportsman l = _sportsmen[j];
                            _sportsmen[j] = _sportsmen[j + 1];
                            _sportsmen[j + 1] = l;
                        }
                    }
                }
            }
            public static Group Merge(Group group1, Group group2)
            { 
                Group g = new Group("Финилисты");
                for (int i = 0, j = 0; i < group1._sportsmen.Length || j < group2._sportsmen.Length;)
                {
                    if (!(i < group1._sportsmen.Length))
                    {
                        g.Add(group2._sportsmen[j]);
                        j++;
                    }
                    else if (!(j < group2._sportsmen.Length))
                    {
                        g.Add(group1._sportsmen[i]);
                        i++;
                    }
                    else
                    {
                        if (group1._sportsmen[i].Time < group2._sportsmen[j].Time)
                        {
                            g.Add(group1._sportsmen[i]);
                            i++;
                        }
                        else
                        {
                            g.Add(group2._sportsmen[j]);
                            j++;
                        }
                    }
                }
                return g;
            }

            public void Split(out Sportsman[] men, out Sportsman[] women)
            {
                men = null;
                women = null;
                if (_sportsmen == null) return;
                int m = 0, w = 0;
                for (int i = 0; i < _sportsmen.Length; i++)
                {
                    if (_sportsmen[i] is SkiMan)
                    {
                        m += 1;
                    }
                    else
                    {
                        w += 1;
                    }
                }
                men = new Sportsman[m];
                women = new Sportsman[w];
                m = 0;
                w = 0;
                for (int i = 0; i < _sportsmen.Length; i++)
                {
                    if (_sportsmen[i] is SkiMan)
                    {
                        men[m] = _sportsmen[i];
                        m += 1;
                    }
                    else
                    {
                        women[w] = _sportsmen[i];
                        w += 1;
                    }
                }
            }
            public void Shuffle()
            {
                if (_sportsmen == null) return;
                Sportsman[] men;
                Sportsman[] women;
                Sort();
                Split(out men, out women);
                if (men == null || women == null) return;
                int koef;
                if (_sportsmen[0] is SkiMan) koef = 0;
                else
                {
                    koef = 1;
                }

                Sportsman[] S = new Sportsman[men.Length + women.Length];
                for (int i = 0, j = 0, k = 0; k < men.Length + women.Length; k++)
                {
                    if (k % 2 == koef)
                    {
                        if (i < men.Length)
                        {
                            S[k] = men[i];
                            i++;
                        }
                        else
                        {
                            S[k] = women[j];
                            j++;
                        }
                    }
                    else
                    {
                        if (i < women.Length)
                        {
                            S[k] = women[j];
                            j++;
                        }
                        else
                        {
                            S[k] = men[i];
                            i++;
                        }
                    }
                }
                _sportsmen = S;
            }
            public void Print()
            {
                foreach (Sportsman p in _sportsmen)
                    p.Print();
            }
        }
    }
}
