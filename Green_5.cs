using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lab_7
{
    public class Green_5
    {
        public struct Student
        {
            private string name1;
            private string surname1;
            private int[] marks1;

            public string Name => name1;
            public string Surname => surname1;
            public int[] Marks
            {
                get
                {
                    if (marks1 == null)
                    {
                        return null;
                    }
                    int[] arrays = new int[marks1.Length];
                    Array.Copy(marks1, arrays, marks1.Length);
                    return arrays;
                }
            }
            public double AvgMark
            {
                get
                {
                    if (marks1 == null || marks1.Length == 0)
                    {
                        return 0;
                    }
                    double summa = 0;
                    for (int i = 0; i < marks1.Length; i++)
                    {
                        summa += marks1[i];
                    }
                    return summa / marks1.Length;
                }
            }

            public Student(string name, string surname)
            {
                name1 = name;
                surname1 = surname;
                marks1 = new int[5];
            }

            public void Exam(int mark)
            {
                if (mark < 2 || mark > 5)
                {
                    return;
                }
                if (marks1 == null)
                {
                    return;
                }


                for (int i = 0; i < marks1.Length; i++)
                {
                    if (marks1[i] == 0)
                    {
                        marks1[i] = mark;
                        break;
                    }
                }

                Console.WriteLine("экзамены написаны");
            }

            public void Print()
            {
                Console.WriteLine($"{Name} {Surname}");
                Console.WriteLine($"балл ср арифм: {AvgMark:F2}");
            }
        }

        public class Group
        {
            private string name1;
            private Student[] students1;
            private int cnt1;

            public string Name => name1;
            public Student[] Students => students1;

            public virtual double AvgMark
            {
                get
                {
                    if (students1 == null || cnt1 == 0)
                    {
                        return 0;
                    }

                    int t = 0;
                    double sr = 0;

                    for (int i = 0; i < cnt1; i++)
                    {
                        if (students1[i].AvgMark > 0)
                        {
                            sr += students1[i].AvgMark;
                            t++;
                        }
                    }
                    return t == 0 ? 0 : sr / t;
                }
            }

            public Group(string name)
            {
                name1 = name;
                students1 = new Student[0];
                cnt1 = 0;
            }

            public void Add(Student student)
            {
                if (students1 == null)
                    students1 = new Student[0];
                Array.Resize(ref students1, cnt1 + 1);
                students1[cnt1] = student;
                cnt1++;
            }

            public void Add(Student[] Students)
            {
                if (Students == null)
                {
                    return;
                }
                if (students1 == null)
                {
                    students1 = new Student[0];
                }

                int new_length = cnt1 + Students.Length;

                Array.Resize(ref students1, new_length);
                for (int i = 0; i < Students.Length; i++)
                {
                    students1[cnt1 + i] = Students[i];
                }
                cnt1 = new_length;
            }

            public static void SortByAvgMark(Group[] groups)
            {
                if (groups == null || groups.Length == 0)
                    return;
                bool n;
                do
                {
                    n = false;
                    for (int i = 0; i < groups.Length - 1; i++)
                    {
                        if (groups[i].AvgMark < groups[i + 1].AvgMark)
                        {
                            Group t = groups[i];
                            groups[i] = groups[i + 1];
                            groups[i + 1] = t;
                            n = true;
                        }
                    }
                } while (n);
            }

            public void Print()
            {
                Console.WriteLine($"{Name} :Балл: {AvgMark:F2}");

                foreach (var student in students1)
                {
                    student.Print();
                }
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
                    if (Students == null || Students.Length == 0)
                    {
                        return 0;
                    }


                    double summa = 0;
                    double count = 0;

                    foreach (var student in Students)
                    {
                        if (student.Marks.Length > 0)
                        {
                            double weighet_sum = 0;
                            foreach (var mark in student.Marks)
                            {
                                switch (mark)
                                {
                                    case 4:
                                        weighet_sum += 1.5;break;
                                    case 3:
                                        weighet_sum += 2;break;
                                    case 2:
                                        weighet_sum += 2.5;break;
                                    case 5:
                                        weighet_sum += 1;break;

                                    
                                }
                            }
                            summa += weighet_sum / student.Marks.Length;
                            count++;
                        }
                    }
                    return count == 0 ? 0 : summa / count;
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
                    if (Students == null || Students.Length == 0)
                    {
                        return 0;
                    }

                    int count = 0;
                    double summa = 0;

                    foreach (var student in Students)
                    {
                        if (student.Marks.Length > 0)
                        {
                            double weighet_sum = 0;
                            foreach (var mark in student.Marks)
                            {
                                switch (mark)
                                {
                                    case 4:
                                        weighet_sum += 0.75;break;
                                    case 3:
                                        weighet_sum += 0.5;break;
                                    case 2:
                                        weighet_sum += 0.25;break;
                                    case 5:
                                        weighet_sum += 1;break;
                                }
                            }
                            summa += weighet_sum / student.Marks.Length;
                            count++;
                        }
                    }
                    return count == 0 ? 0 : summa / count;
                }
            }
        }
    }

}
