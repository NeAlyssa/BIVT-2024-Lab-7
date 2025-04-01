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
        public class Student 
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
                _restored = false;
                _examCount = 0;
                _isExpelled = false;
            }
            public string Name 
            {
                get { return _name; }
            }
            public string Surname 
            {
                get { return _surname; }
            }
            public int ID 
            {
                get { return _id; }
            }
            public int[] Marks 
            {
                get { return _marks != null ? (int[])_marks.Clone() : null; }
            }
            public double AvgMark 
            {
                get
                {
                    if (_examCount == 0 || _marks == null)
                    {
                        return 0;
                    }
                    double s = 0;
                    for (int i = 0; i < _examCount; i++)
                    {
                        s += _marks[i];
                    }
                    return s / _examCount;
                }
            }
            public bool IsExpelled 
            {
                get
                {
                    if (_restored)
                    {
                        return false;
                    }
                    if (_examCount == 0)
                    {
                        return false;
                    }
                    for (int i = 0; i < _examCount; i++)
                    {
                        if (_marks[i] < 3)
                        {
                            return true;
                        }
                    }

                    {
                        return false;
                    }
                }
            }
            public void Exam(int mrk) 
            {
                if (_marks.Length == 0 || _marks == null)
                {
                    return;
                }
                if (_examCount >= 3)
                {
                    return;
                }
                if (_isExpelled && !_restored)
                {
                    return;
                }
                if (_restored)
                {
                    _isExpelled = false;
                    _restored = false;
                    
                }
                _marks[_examCount] = mrk;
                _examCount++;
                if (mrk < 3)
                {
                    _isExpelled = true;
                }
            }
            public void Restore()
            {
                _isExpelled = false;
                _restored = true;
                
            }
            public static void SortByAvgMark(Student[] arr)
            {
                if (arr.Length == 0 || arr == null)
                {
                    return;
                }
                bool swapped;
                do
                {
                    swapped = false;
                    for (int i = 0; i < arr.Length - 1; i++)
                    {
                        if (arr[i].AvgMark < arr[i + 1].AvgMark)
                        {
                            Student t = arr[i];
                            arr[i] = arr[i+1];
                            arr[i+1] = t;
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
            public static void Sort(Student[] std) 
            {
                bool swapped;
                do
                {
                    swapped = false;
                    for (int i = 0; i < std.Length - 1; i++)
                    {
                        if (std[i].ID > std[i+1].ID)
                        {
                            Student temp = std[i];
                            std[i] = std[i+1];
                            std[i+1] = temp;
                            swapped = true;
                        }
                    }
                } while (swapped);
            }
            public static Student[] Expel(ref Student[] sdt)
            {
                int k = 0;
                for (int i = 0; i < sdt.Length; i++)
                {
                    if (sdt[i].IsExpelled == true)
                    {
                        k++;
                    }
                }
                int ind = 0;
                Student[] exsdt = new Student[k];
                for (int i = 0; i < sdt.Length; i++)
                {
                    if (sdt[i].IsExpelled == true)
                    {
                        exsdt[ind++] = sdt[i];
                    }
                }
                sdt = sdt.Where(x => !(x.IsExpelled)).ToArray();
                return exsdt;
            }
            public static void Restore(ref Student[] students, Student restored) 
            {
                bool studentExists = false;
                int checkIndex = 0;
                while (checkIndex < students.Length && !studentExists)
                {
                    if (students[checkIndex].ID == restored.ID)
                    {
                        studentExists = true;
                    }
                    checkIndex++;
                }
                if (!studentExists && restored.IsExpelled && restored.ID >= 1)
                {
                    int targetPosition = restored.ID - 1;
                    if (targetPosition > students.Length)
                    {
                        targetPosition = students.Length;
                    }
                    Student[] updatedStudents = new Student[students.Length + 1];
                    int copyIndex = 0;
                    while (copyIndex < targetPosition)
                    {
                        updatedStudents[copyIndex] = students[copyIndex];
                        copyIndex++;
                    }
                    updatedStudents[targetPosition] = restored;
                    while (copyIndex < students.Length)
                    {
                        updatedStudents[copyIndex + 1] = students[copyIndex];
                        copyIndex++;
                    }
                    students = updatedStudents;
                }
            }
        }
    }
}