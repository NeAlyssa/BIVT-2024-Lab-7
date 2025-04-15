using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static Lab_7.Purple_4;

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
                if (_time == 0) _time = time;
            }

            public static void Sort(Sportsman[] array)
            {
                if (array == null) return;
                for (int i = 0; i < array.Length; i++)
                {
                    for (int j = 0; j < array.Length - i - 1; j++)
                    {
                        if (array[j].Time > array[j + 1].Time)
                        {
                            var p = array[j];
                            array[j] = array[j + 1];
                            array[j + 1] = p;
                        }
                    }
                }
            }

            public void Print()
            {
                Console.WriteLine(Name + " " + Surname + " " + Time);
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


        public class Group
        {
            private string _name;
            private Sportsman[] _sportsman;

            public string Name => _name;
            public Sportsman[] Sportsmen => _sportsman;

            public Group(string name)
            {
                _name = name;
                _sportsman = new Sportsman[0];
            }

            public Group(Group group)
            {
                _name = group._name;
                var NewArray = new Sportsman[group._sportsman.Length];
                Array.Copy(group._sportsman, NewArray, group._sportsman.Length);
                _sportsman = NewArray;
            }

            public void Add(Sportsman sportsman)
            {
                var NewArray = new Sportsman[_sportsman.Length + 1];
                Array.Copy(_sportsman, NewArray, _sportsman.Length);
                NewArray[_sportsman.Length] = sportsman;
                _sportsman = NewArray;
            }

            public void Add(Sportsman[] array)
            {
                if (array == null) return;
                foreach (var s in array)
                {
                    Add(s);
                }
            }

            public void Add(Group group)
            {
                Add(group._sportsman);
            }

            public void Sort()
            {
                if (_sportsman == null) return;
                for (int i = 0; i < _sportsman.Length; i++)
                {
                    for (int j = 0; j < _sportsman.Length - i - 1; j++)
                    {
                        if (_sportsman[j].Time > _sportsman[j + 1].Time)
                        {
                            var p = _sportsman[j];
                            _sportsman[j] = _sportsman[j + 1];
                            _sportsman[j + 1] = p;
                        }
                    }
                }
            }

            public static Group Merge(Group group1, Group group2)
            {
                Group gr = new Group("Финалисты");
                int i = 0, j = 0;
                while (i < group1._sportsman.Length && j < group2._sportsman.Length)
                {
                    if (group1._sportsman[i].Time <= group2._sportsman[j].Time)
                        gr.Add(group1._sportsman[i++]);
                    else
                        gr.Add(group2._sportsman[j++]);
                }
                while (i < group1._sportsman.Length)
                    gr.Add(group1._sportsman[i++]);
                while (j < group2._sportsman.Length)
                    gr.Add(group2._sportsman[j++]);
                return gr;
            }

            public void Split(out Sportsman[] men, out Sportsman[] women)
            {
                men = new Sportsman[0];
                women = new Sportsman[0];
                if (_sportsman == null) return;
                foreach (var r in _sportsman)
                {
                    if (r is SkiMan)
                    {
                        Array.Resize(ref men, men.Length + 1);
                        men[men.Length - 1] = r;
                    }
                    else
                    {
                        Array.Resize(ref women, women.Length + 1);
                        women[women.Length - 1] = r;
                    }
                }
            }

            public void Shuffle()
            {
                if (_sportsman == null) return;
                Split(out Sportsman[] men, out Sportsman[] women);
                Sportsman.Sort(men);
                Sportsman.Sort(women);
                int i = 0, j = 0, k = 0;
                if (men[0].Time <= women[0].Time)
                {
                    while (i < men.Length && j < women.Length)
                    {
                        _sportsman[k++] = men[i++];
                        _sportsman[k++] = women[j++];
                    }
                }
                else
                {
                    while (i < men.Length && j < women.Length)
                    {
                        _sportsman[k++] = women[j++];
                        _sportsman[k++] = men[i++];
                    }
                }
                while (i < men.Length) _sportsman[k++] = men[i++];
                while (j < women.Length) _sportsman[k++] = women[j++];
            }

            public void Print()
            {
                foreach (Sportsman s in _sportsman)
                {
                    s.Print();
                }
            }
        }
    }
}