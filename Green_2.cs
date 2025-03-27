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
            public string Name => _name;
            public string Surname => _surname;
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
            private int[] _marks = new int[4];
            private static int _excellentAmount;
            private bool _isCountedAsExcellent;
            public Student(string name, string surname) : base(name, surname) { }
            public static int ExcellentAmount => _excellentAmount;
            public int[] Marks => (int[])_marks.Clone();
            public double AvgMark => _marks.Length == 0 ? 0 : _marks.Average();
            public bool IsExcellent => _marks.All(m => m >= 4);
            public void Exam(int mark)
            {
                for (int i = 0; i < _marks.Length; i++)
                {
                    if (_marks[i] == 0)
                    {
                        _marks[i] = mark;
                        break;
                    }
                }
                if (_marks.All(m => m >= 4) && !_isCountedAsExcellent)
                {
                    _excellentAmount++;
                    _isCountedAsExcellent = true;
                }
            }
            public static void SortByAvgMark(Student[] array)
            {
                Array.Sort(array, (s1, s2) => s2.AvgMark.CompareTo(s1.AvgMark));
            }
            public new void Print()
            {
                Console.WriteLine("{0,-12} {1,-12} {2,-12:F2} {3,-12}", Name, Surname, AvgMark, IsExcellent);
            }
        }
    }
}
