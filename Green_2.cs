using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lab_7
{
    public class Green_2
    {
        public class Human 
        {
            private string _name; 
            private string _surname; 
            public string Name 
            {
                get { return _name; }
            }
            public string Surname 
            {
                get { return _surname; }
            }
            public Human(string name, string surname) 
            {
                _name = name;
                _surname = surname;
            }
            public void Print()
            {
                Console.WriteLine("{0,-12} {1,-12}", Name, Surname);
            }
        }
        public class Student : Human 
        {
            private int[] _marks; 
            private static int _excellentAmount; 
            private bool _isCountedAsExcellent;
            public Student(string name, string surname) : base(name, surname)
            {
                _marks = new int[4];
                for (int i = 0; i < _marks.Length; i++)
                {
                    _marks[i] = 0;
                }
                _isCountedAsExcellent = false;
            }
            public static int ExcellentAmount 
            {
                get { return _excellentAmount; }
            }
            public int[] Marks 
            {
                get {return _marks != null ? (int[])_marks.Clone() : null;}
            }
            public double AvgMark 
            {
                get
                {
                    if (_marks.Length == 0 || _marks == null)
                    {
                        return 0;
                    }
                    double s = 0;
                    for (int i = 0; i < _marks.Length; i++)
                    {
                        s += _marks[i];
                    }
                    return s / _marks.Length;
                }
            }
            public bool IsExcellent 
            {
                get
                {
                    if (_marks.Length == 0 || _marks == null)
                    {
                        return false;
                    }
                    for (int i = 0; i < _marks.Length; i++)
                    {
                        if (_marks[i] < 4)
                        {
                            return false;
                        }
                    }
                    return true;
                }
            }
            public void Exam(int mrk) 
            {
                if (_marks == null || _marks.Length == 0)
                {
                    return;
                }
                for (int i = 0; i < _marks.Length; i++)
                {
                    if (_marks[i] == 0)
                    {
                        _marks[i] = mrk;
                        break;
                    }
                }
                if (_marks != null && _marks.All(m => m != 0) && _marks.All(m => m >= 4) && !_isCountedAsExcellent)
                {
                    _excellentAmount++;
                    _isCountedAsExcellent = true;
                }
            }
            public static void SortByAvgMark(Student[] arr)
            {
                if (arr.Length == 0 || arr == null)
                    return;
                bool swapped = true;
                while (swapped)
                {
                    swapped = false;
                    for (int i = 0; i < arr.Length - 1; i++)
                    {
                        if (arr[i].AvgMark < arr[i + 1].AvgMark)
                        {
                            (arr[i], arr[i + 1]) = (arr[i + 1], arr[i]);
                            swapped = true;
                        }
                    }
                }
            }
            new public void Print() 
            {
                Console.WriteLine("{0,-12} {1,-12} {2,-12:F2} {3,-12}", Name, Surname, AvgMark, IsExcellent);
            }
        }
    }



}