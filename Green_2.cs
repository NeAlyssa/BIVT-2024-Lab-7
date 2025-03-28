using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace Lab_7
{
    public class Green_2
    {
        public class Human
        {
            private string name1;
            private string surname1;


            public string Name => name1;
            public string Surname => surname1;


            public Human(string name, string surname)
            {
                name1 = name;
                surname1 = surname;
            }

            public void Print()
            {
                Console.WriteLine($" {Name} {Surname}");
            }
        }

        public class Student : Human
        {
            private static int otl= 0;
            private int[] marks1;


            public int[] Marks
            {
                get
                {
                    if (marks1 == null) return null;
                    int[] arrays = new int[marks1.Length];
                    Array.Copy(marks1, arrays, marks1.Length);
                    return arrays;
                }
            }
            public static int ExcellentAmount => otl;

            public double AvgMark
            {
                get
                {
                    
                    if (marks1 == null) return 0;
                    int cnt = 0;
                    double sum = 0;

                    foreach (int mark in marks1)
                    {
                        if (mark != 0)
                        {
                            sum += mark;
                            cnt++;
                        }
                    }
                    if (cnt == 0)
                        return 0;

                    return sum / cnt;
                }
            }
            public bool IsExcellent
            {
                get
                {
                    if (marks1 == null)
                        return false;
                    for (int i = 0; i < marks1.Length; i++)
                    {
                        if (marks1[i] < 4)
                        {
                            return false;
                        }
                    }
                    return true;
                }
            }
            public Student(string surname, string name) : base(surname, name)
            {
                marks1 = new int[4];
            }

            public void Exam(int mark)
            {
                if (mark < 2 || mark > 5)
                {
                    Console.WriteLine("неправильные   оценки");
                    return;
                }

                for (int i = 0; i < marks1.Length; i++)
                {
                    if (marks1[i] == 0)
                    {
                        marks1[i] = mark;
                        if (IsExcellent)
                        {
                            otl++;
                        }
                        return;
                    }
                }
            }

            public static void SortByAvgMark(Student[] array)
            {
                if (array == null)
                {
                    return;
                }
                int n = array.Length;
                for (int i = 0; i < n - 1; i++)
                {
                    for (int j = 0; j < n - i - 1; j++)
                    {
                        if (array[j].AvgMark < array[j + 1].AvgMark)
                        {
                            Student vrem = array[j];
                            array[j] = array[j + 1];
                            array[1 + j] = vrem;
                        }
                    }
                }
            }

            public new void Print()
            {
                {
                    base.Print();
                    Console.WriteLine($"КТо?: {Name} {Surname}");
                    Console.WriteLine(string.Join(", ", Marks));
                    Console.WriteLine($"СР Балл: {AvgMark:F2}");
                    Console.WriteLine(IsExcellent ? "Отличник" : "Не отличник");
                    Console.WriteLine();
                }
            }
        }
    }
}
