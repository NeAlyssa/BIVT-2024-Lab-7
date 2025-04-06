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
        public struct Student
        {
            private string _name;
            private string _surname;
            private int[] _marks;
            public string Name
            {
                get { return _name; }
            }
            public string Surname
            {
                get { return _surname; }
            }
            public int[] Marks
            {
                get { return _marks; }
            }
            public Student(string n, string sn)
            {
                _name = n;
                _surname = sn;
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
            public void Exam(int mrk)
            {
                if (_marks.Length == 0 || _marks == null)
                {
                    return;
                }
                int ind = 0;
                bool foundZero = false;

                while (ind < _marks.Length && !foundZero)
                {
                    if (_marks[ind] == 0)
                    {
                        _marks[ind] = mrk;
                        foundZero = true;
                    }
                    ind++;
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
            private Green_5.Student[] _students;
            private int _studentCount;
            public string Name
            {
                get { return _name; }
            }
            public Green_5.Student[] Students
            {
                get { return _students; }
            }
            public virtual double AvgMark
            {
                get
                {
                    if (_studentCount == 0 || _students == null)
                    {
                        return 0;
                    }
                    double sum = 0d;
                    int count = 0;
                    int i = 0;
                    while (i < _studentCount)
                    {
                        var marks = _students[i].Marks;
                        int j = 0;
                        while (j < marks.Length)
                        {
                            sum += marks[j];
                            count++;
                            j++;
                        }
                        i++;
                    }
                    return count > 0 ? sum / count : 0d;
                }
            }
            public Group(string name)
            {
                _name = name;
                _students = new Green_5.Student[0];
                _studentCount = 0;
            }
            public void Add(Green_5.Student student)
            {
                if (_students == null)
                {
                    _students = new Green_5.Student[0];
                }
                Array.Resize(ref _students, _studentCount + 1);
                _students[_studentCount] = student;
                _studentCount++;
            }
            public void Add(Green_5.Student[] ns)
            {
                if (ns == null)
                {
                    return;
                }
                if (_students == null)
                {
                    _students = new Green_5.Student[0];
                }
                int nl = _studentCount + ns.Length;
                Array.Resize(ref _students, nl);
                for (int i = 0; i < ns.Length; i++)
                {
                    _students[_studentCount + i] = ns[i];
                }
                _studentCount = nl;
            }
            public static void SortByAvgMark(Group[] arr)
            {
                bool hasSwaps;
                int sortRange = arr.Length;
                while (sortRange > 1)
                {
                    hasSwaps = false;
                    int currentIndex = 0;
                    while (currentIndex < sortRange - 1)
                    {
                        if (arr[currentIndex].AvgMark < arr[currentIndex + 1].AvgMark)
                        {
                            (arr[currentIndex + 1], arr[currentIndex]) =
                                (arr[currentIndex], arr[currentIndex + 1]);
                            hasSwaps = true;
                        }
                        currentIndex++;
                    }
                    sortRange--;
                    if (!hasSwaps) break;
                }
            }
            public void Print()
            {
                Console.WriteLine("{0,-12} {1,-15:F2}", Name, AvgMark);
            }
        }
        public class EliteGroup : Group
        {
            private string _name;
            public EliteGroup(string name) : base(name)
            {
                _name = name;
            }
            public override double AvgMark
            {
                get
                {
                    if (Students.Length == 0 || Students == null)
                    {
                        return 0;
                    }
                    double sumweight = 0;
                    double weight = 0;
                    foreach (Student student in Students)
                    {
                        double stuws = 0;
                        double stuw = 0;
                        foreach (int mrk in student.Marks)
                        {
                            if (mrk != 0)
                            {
                                switch (mrk)
                                {
                                    case 5:
                                        stuws += mrk * 1;
                                        stuw += 1;
                                        break;
                                    case 4:
                                        stuws += mrk * 1.5;
                                        stuw += 1.5;
                                        break;
                                    case 3:
                                        stuws += mrk * 2;
                                        stuw += 2;
                                        break;
                                    case 2:
                                        stuws += mrk * 2.5;
                                        stuw += 2.5;
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                        if (stuw > 0)
                        {
                            sumweight += stuws;
                            weight += stuw;
                        }
                    }
                    return weight == 0 ? 0 : sumweight / weight;
                }
            }
        }
        public class SpecialGroup : Group
        {
            private string _name;
            public SpecialGroup(string name) : base(name)
            {
                _name = name;
            }
            public override double AvgMark
            {
                get
                {
                    if (Students.Length == 0 || Students == null)
                    {
                        return 0;
                    }
                    double weights = 0;
                    double weight = 0;
                    foreach (Student student in Students)
                    {
                        double stuws = 0;
                        double stuw = 0;
                        foreach (int mrk in student.Marks)
                        {
                            if (mrk != 0)
                            {
                                switch (mrk)
                                {
                                    case 5:
                                        stuws += mrk * 1;
                                        stuw += 1;
                                        break;
                                    case 4:
                                        stuws += mrk * 0.75;
                                        stuw += 0.75;
                                        break;
                                    case 3:
                                        stuws += mrk * 0.5;
                                        stuw += 0.5;
                                        break;
                                    case 2:
                                        stuws += mrk * 0.25;
                                        stuw += 0.25;
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                        if (stuw > 0)
                        {
                            weights += stuws;
                            weight += stuw;
                        }
                    }
                    return weight == 0 ? 0 : weights / weight;
                }
            }
        }
    }



}
