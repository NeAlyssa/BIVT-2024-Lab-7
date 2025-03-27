using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lab_7
{
    public class Green_5
    {
        public class Student
        {
            private string _name;
            private string _surname;
            private int[] _marks;
            public string Name { get { return _name; } }
            public string Surname { get { return _surname; } }
            public int[] Marks { get { return _marks; } }
            public Student(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new int[5];
                for (int i = 0; i < _marks.Length; i++)
                {
                    _marks[i] = 0;
                }
            }
            public double AvgMark
            {
                get
                {
                    if (_marks == null || _marks.Length == 0)
                    {
                        return 0;
                    }
                    double sum = 0;
                    for (int i = 0; i < _marks.Length; i++)
                    {
                        sum += _marks[i];
                    }
                    return sum / _marks.Length;
                }
            }
            public void Exam(int mark)
            {
                if (_marks == null || _marks.Length == 0)
                {
                    return;
                }
                for (int i = 0; i < _marks.Length; i++)
                {
                    if (_marks[i] == 0)
                    {
                        _marks[i] = mark;
                        break;
                    }
                }
            }
            public void Print()
            {
                Console.WriteLine("{0,-12} {1,-12} {2,-12:F2} {3, -12}", Name, Surname, AvgMark, string.Join(", ", Marks));
            }
        }
        public class Group
        {
            private string _name;
            private Student[] _students;
            private int _studentCount;
            public string Name { get { return _name; } }
            public Student[] Students { get { return _students; } }
            public virtual double AvgMark
            {
                get
                {
                    if (_students == null || _studentCount == 0)
                    {
                        return 0;
                    }
                    double totalSum = 0;
                    int totalCount = 0;
                    for (int i = 0; i < _studentCount; i++)
                    {
                        foreach (double mark in _students[i].Marks)
                        {
                            totalSum += mark;
                            totalCount++;
                        }
                    }
                    return totalCount == 0 ? 0 : totalSum / totalCount;
                }
            }
            public Group(string name)
            {
                _name = name;
                _students = new Student[0];
                _studentCount = 0;
            }
            public void Add(Student student)
            {
                if (_students == null)
                {
                    _students = new Student[0];
                }
                Array.Resize(ref _students, _studentCount + 1);
                _students[_studentCount] = student;
                _studentCount++;
            }
            public void Add(Student[] newStudents)
            {
                if (newStudents == null)
                {
                    return;
                }
                if (_students == null)
                {
                    _students = new Student[0];
                }
                int newLength = _studentCount + newStudents.Length;
                Array.Resize(ref _students, newLength);
                for (int i = 0; i < newStudents.Length; i++)
                {
                    _students[_studentCount + i] = newStudents[i];
                }
                _studentCount = newLength;
            }
            public static void SortByAvgMark(Group[] array)
            {
                if (array == null || array.Length == 0)
                {
                    return;
                }
                bool swapped;
                do
                {
                    swapped = false;
                    for (int i = 0; i < array.Length - 1; i++)
                    {
                        if (array[i].AvgMark < array[i + 1].AvgMark)
                        {
                            Group temp = array[i];
                            array[i] = array[i + 1];
                            array[i + 1] = temp;
                            swapped = true;
                        }
                    }
                } while (swapped);
            }
            public void Print()
            {
                Console.WriteLine("{0,-12} {1,-15:F2}", Name, AvgMark);
            }
        }
        public class EliteGroup : Group
        {
            public EliteGroup(string name) : base(name) { }
            public override double AvgMark
            {
                get
                {
                    if (Students == null || Students.Length == 0)
                    {
                        return 0;
                    }
                    double totalWeightedSum = 0;
                    double totalWeight = 0;
                    foreach (Student student in Students)
                    {
                        double studentWeightedSum = 0;
                        double studentWeightTotal = 0;
                        foreach (int mark in student.Marks)
                        {
                            if (mark != 0)
                            {
                                switch (mark)
                                {
                                    case 5:
                                        studentWeightedSum += mark * 1;
                                        studentWeightTotal += 1;
                                        break;
                                    case 4:
                                        studentWeightedSum += mark * 1.5;
                                        studentWeightTotal += 1.5;
                                        break;
                                    case 3:
                                        studentWeightedSum += mark * 2;
                                        studentWeightTotal += 2;
                                        break;
                                    case 2:
                                        studentWeightedSum += mark * 2.5;
                                        studentWeightTotal += 2.5;
                                        break;
                                }
                            }
                        }
                        if (studentWeightTotal > 0)
                        {
                            totalWeightedSum += studentWeightedSum;
                            totalWeight += studentWeightTotal;
                        }
                    }
                    return totalWeight == 0 ? 0 : totalWeightedSum / totalWeight;
                }
            }
        }
        public class SpecialGroup : Group
        {
            public SpecialGroup(string name) : base(name) { }
            public override double AvgMark
            {
                get
                {
                    if (Students == null || Students.Length == 0)
                    {
                        return 0;
                    }
                    double totalWeightedSum = 0;
                    double totalWeight = 0;
                    foreach (Student student in Students)
                    {
                        double studentWeightedSum = 0;
                        double studentWeightTotal = 0;
                        foreach (int mark in student.Marks)
                        {
                            if (mark != 0)
                            {
                                switch (mark)
                                {
                                    case 5:
                                        studentWeightedSum += mark * 1;
                                        studentWeightTotal += 1;
                                        break;
                                    case 4:
                                        studentWeightedSum += mark * 0.75;
                                        studentWeightTotal += 0.75;
                                        break;
                                    case 3:
                                        studentWeightedSum += mark * 0.5;
                                        studentWeightTotal += 0.5;
                                        break;
                                    case 2:
                                        studentWeightedSum += mark * 0.25;
                                        studentWeightTotal += 0.25;
                                        break;
                                }
                            }
                        }
                        if (studentWeightTotal > 0)
                        {
                            totalWeightedSum += studentWeightedSum;
                            totalWeight += studentWeightTotal;
                        }
                    }
                    return totalWeight == 0 ? 0 : totalWeightedSum / totalWeight;
                }
            }
        }
    }
}
