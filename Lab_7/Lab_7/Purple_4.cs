using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
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
            private int f;
            private static void Sorted(Sportsman[] array)
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
            public Sportsman[] Sportsmen
            {
                get
                {
                    if (_sportsmen == null) return null;
                    var newArray = new Sportsman[_sportsmen.Length];
                    Array.Copy(_sportsmen, newArray, _sportsmen.Length);
                    return newArray;
                }
            }
            public Group(string name)
            {
                _name = name;
                _sportsmen = new Sportsman[0];
            }
            public Group(Group group)
            {
                _name = group.Name;
                if (group.Sportsmen == null)
                    _sportsmen = null;
                else {
                    _sportsmen = new Sportsman[group.Sportsmen.Length];
                    Array.Copy(group.Sportsmen, _sportsmen, group.Sportsmen.Length);
                }
            }
            public void Add(Sportsman sportsman)
            {
                if (_sportsmen == null || sportsman == null) return;
                Array.Resize(ref _sportsmen, _sportsmen.Length + 1);
                _sportsmen[_sportsmen.Length - 1] = sportsman;
            }
            public void Add(Sportsman[] sportsmen)
            {
                if (_sportsmen == null|| sportsmen == null) return;
                int sportsmenlen = _sportsmen.Length;
                Array.Resize(ref _sportsmen, sportsmenlen + sportsmen.Length);
                Array.Copy(sportsmen, 0, _sportsmen, sportsmenlen, sportsmen.Length);
            }
            public void Add(Group group)
            {
                Add(group.Sportsmen);
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
                if (group1.Sportsmen == null) return group2;
                if (group2.Sportsmen == null) return group1;
                groupmerge._sportsmen = new Sportsman[group1.Sportsmen.Length + group2.Sportsmen.Length];
                int i1 = 0, i2 = 0, k = 0;
                group1.Sort();
                group2.Sort();
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
                Sort();
                Print();
                Sportsman[] men = new Sportsman[0];
                Sportsman[] women = new Sportsman[0];
                Split(out men,out women);
                int i = 0, j = 0, k = 0;
                if (_sportsmen[0] is SkiMan)
                    i++;
                else j++;
                while (i <men.Length && j < women.Length)
                {
                    if (_sportsmen[k++] is SkiMan)
                    {
                        _sportsmen[k] = women[j++];
                    }
                    else
                    {
                        _sportsmen[k] = men[i++];
                    }
                }
                while (i < men.Length)
                {
                    k++;
                    _sportsmen[k] = men[i++];
                }
                while(j < women.Length)
                {
                    k++;
                    _sportsmen[k] = women[j++];
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
