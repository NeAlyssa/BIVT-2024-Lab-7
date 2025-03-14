using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lab_6
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
            private int f;
            public static void Sorted(Sportsman[] array)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    for (int j = 0; j < array.Length - 1 - i; j++)
                    {
                        if (array[j].Time > array[j + 1].Time)
                        {
                            var temp = array[j];
                            array[j] = array[j + 1];
                            array[j + 1] = temp;
                        }
                    }
                }
            }
            public static void Sort(Sportsman[] array)
            {
                if (array == null) return;
                for (int i = 0; i < array.Length; i++)
                {
                    if (array[i] == null) return;
                    for (int j = 0; j < array.Length - 1 - i; j++)
                    {
                        if (array[j].Time > array[j + 1].Time)
                        {
                            var temp = array[j];
                            array[j] = array[j + 1];
                            array[j + 1] = temp;
                        }
                    }
                }
            }
            public Sportsman(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _time = 0;
                f = 0;
            }
            public void Run(double time)
            {
                if (f == 1) return;
                _time = time;
                f = 1;
            }
            public void Print()
            {
                Console.WriteLine($"{Name} {Surname}: {Time}");
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
                _name = group.Name;
                _sportsmen = group.Sportsmen;
            }
            public void Add(Sportsman sportsman)
            {
                var s = Sportsmen;
                if (s == null) return;
                _sportsmen = new Sportsman[s.Length + 1];
                Array.Copy(s, _sportsmen, s.Length);
                _sportsmen[s.Length] = sportsman;
            }
            public void Add(Sportsman[] sportsmen)
            {
                if (sportsmen == null) return;
                var s = Sportsmen;
                if (s == null) return;
                _sportsmen = new Sportsman[s.Length + sportsmen.Length];
                Array.Copy(s, _sportsmen, s.Length);
                Array.Copy(sportsmen, 0, _sportsmen, s.Length, sportsmen.Length);
            }
            public void Add(Group group)
            {
                if (group.Sportsmen == null) return;
                var s = Sportsmen;
                if (s == null) return;
                _sportsmen = new Sportsman[_sportsmen.Length + group.Sportsmen.Length];
                Array.Copy(s, _sportsmen, s.Length);
                Array.Copy(group.Sportsmen, 0, _sportsmen, s.Length, group.Sportsmen.Length);
            }
            public void Sort()
            {
                if (_sportsmen == null) return;
                for (int i = 0; i < Sportsmen.Length; i++)
                {
                    for (int j = 0; j < Sportsmen.Length - 1 - i; j++)
                    {
                        if (_sportsmen[j].Time > _sportsmen[j + 1].Time)
                        {
                            var temp = _sportsmen[j];
                            _sportsmen[j] = _sportsmen[j + 1];
                            _sportsmen[j + 1] = temp;
                        }
                    }
                }
            }
            public static Group Merge(Group group1, Group group2)
            {
                Group groupmerge = new Group("Ans");
                if (group1.Sportsmen == null && group2.Sportsmen == null) return group1;
                if (group2.Sportsmen == null) return group2;
                if (group2.Sportsmen == null) return group1;
                groupmerge._sportsmen = new Sportsman[group1.Sportsmen.Length + group2.Sportsmen.Length];
                int i1 = 0, i2 = 0, k = 0;
                while (group1.Sportsmen.Length > i1 && group2.Sportsmen.Length > i2)
                {
                    if (group1.Sportsmen[i1].Time < group2.Sportsmen[i2].Time)
                    {
                        groupmerge._sportsmen[k] = group1.Sportsmen[i1];
                        i1++; k++;
                    }
                    else
                    {
                        groupmerge._sportsmen[k] = group2.Sportsmen[i2];
                        i2++; k++;
                    }
                }
                while (group1.Sportsmen.Length > i1)
                {
                    groupmerge._sportsmen[k] = group1.Sportsmen[i1];
                    i1++; k++;
                }
                while (group2.Sportsmen.Length > i2)
                {
                    groupmerge._sportsmen[k] = group2.Sportsmen[i2];
                    i2++; k++;
                }
                return groupmerge;
            }
            public void Print()
            {
                foreach (var sportsmen in _sportsmen)
                {
                    Console.WriteLine($"{sportsmen.Name} {sportsmen.Surname}: {sportsmen.Time}");
                }
            }
            public void Split(out Sportsman[] men, out Sportsman[] women)
            {
                if (_sportsmen == null || _sportsmen.Length == 0)
                {
                    men = null;
                    women = null;
                    return;
                }
                Sportsman[] men0 = new Sportsman[0];
                Sportsman[] women0 = new Sportsman[0];
                for (int i = 0; i< _sportsmen.Length; i++)
                {
                    if (_sportsmen[i] is SkiMan)
                    {
                        Array.Resize(ref men0, men0.Length + 1);
                        men0[men0.Length - 1] = _sportsmen[i];
                    }
                    if (_sportsmen[i] is SkiWoman)
                    {
                        Array.Resize(ref women0, women0.Length + 1);
                        women0[women0.Length - 1] = _sportsmen[i];
                    }
                }
                men = men0;
                women = women0;
            }
            public void Shuffle()
            {
                if (_sportsmen == null) return;
                double timemax = _sportsmen[0].Time;
                int imax = 0;
                for (int i = 0; i < _sportsmen.Length-1;i++)
                {
                    if (_sportsmen[i].Time > timemax)
                    {
                        timemax = _sportsmen[i].Time;
                        imax = i;
                    }
                }
                Sportsman temp = _sportsmen[0];
                _sportsmen[0] = _sportsmen[imax];
                _sportsmen[imax] = temp;
                for (int i = 1; i< _sportsmen.Length-1; i++)
                {
                    for (int j = i+1; j < _sportsmen.Length; j++)
                    {
                        if (_sportsmen[i].Time < _sportsmen[j].Time && _sportsmen[j] is SkiMan != _sportsmen[i-1] is SkiMan)
                        {
                            temp = _sportsmen[i];
                            _sportsmen[i] = _sportsmen[j];
                            _sportsmen[j] = temp;
                        }
                    }
                }
            }
        }
        public class SkiMan : Sportsman
        {
            public SkiMan(string name, string surname) : base(name, surname) { }
            public SkiMan(string name, string surname, double time) :base(name, surname)
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
