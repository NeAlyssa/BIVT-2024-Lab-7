using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lab_7
{
    public class Green_3
    {
        public class Student .
        {
            private string _name; 
            private string _surname; 
            private int[] _marks;
            private bool _isExpelled;
            private int _examCount; 
            private int _id; 
            private static int _nextId; 
            private bool _restored;
            static Student() 
            {
                _nextId = 1;
            }
            public Student(string name, string surname)
            {
                _id = _nextId++;
                _name = name;
                _surname = surname;
                _marks = new int[3];
                for (int i = 0; i < _marks.Length; i++)
                {
                    _marks[i] = 0;
                }
                _isExpelled = false;
                _examCount = 0;
                _restored = false;
            }
            public string Name => _name; 
            public string Surname => _surname; 
            public int[] Marks => (int[])_marks.Clone(); 
            public int ID => _id; 
            public double AvgMark 
            {
                get
                {
                    if (_marks == null || _examCount == 0)
                    {
                        return 0;
                    }
                    double sum = 0;
                    for (int i = 0; i < _examCount; i++)
                    {
                        sum += _marks[i];
                    }
                    return sum / _examCount;
                }
            }
            public bool IsExpelled 
            {
                get
                {
                    if (_restored) return false;
                    if (_examCount == 0) return false;
                    return _marks.Take(_examCount).Any(m => m < 3);
                }
            }
            public void Exam(int mark) 
            {
                if (_examCount >= 3 || (_isExpelled && !_restored))
                {
                    return;
                }
                if (_restored)
                {
                    _restored = false;
                    _isExpelled = false;
                }
                _marks[_examCount++] = mark;

                if (mark < 3)
                {
                    _isExpelled = true;
                }
            }
            public void Restore() 
            {
                _restored = true;
                _isExpelled = false;
            }
            public static void SortByAvgMark(Student[] array) 
            {
                if (array == null || array.Length == 0) return;

                bool swapped;
                do
                {
                    swapped = false;
                    for (int i = 0; i < array.Length - 1; i++)
                    {
                        if (array[i].AvgMark < array[i + 1].AvgMark)
                        {
                            (array[i], array[i + 1]) = (array[i + 1], array[i]);
                            swapped = true;
                        }
                    }
                } while (swapped);
            }
            public void Print()
            {
                Console.WriteLine("{0,-12} {1,-10} {2,-15:F2} {3,-10}", Name, Surname, AvgMark, IsExpelled);
            }
        }
        public class Commission 
        {
            public static void Sort(Student[] students)
            {
                bool swapped;
                do
                {
                    swapped = false;
                    for (int i = 0; i < students.Length - 1; i++)
                    {
                        if (students[i].ID > students[i + 1].ID)
                        {
                            (students[i], students[i + 1]) = (students[i + 1], students[i]);
                            swapped = true;
                        }
                    }
                } while (swapped);
            }
            public static Student[] Expel(ref Student[] students) 
            {
                Student[] expelled = students.Where(s => s.IsExpelled).ToArray();
                students = students.Where(s => !s.IsExpelled).ToArray();
                return expelled;
            }
            public static void Restore(ref Student[] students, Student restored) 
            {
                if (students.Any(s => s.ID == restored.ID)) return;
                if (restored.IsExpelled && restored.ID >= 1)
                {
                    int insertPos = Math.Min(restored.ID - 1, students.Length);
                    students = students.Take(insertPos)
                                       .Append(restored)
                                       .Concat(students.Skip(insertPos))
                                       .ToArray();
                }
            }
        }
    }
}
