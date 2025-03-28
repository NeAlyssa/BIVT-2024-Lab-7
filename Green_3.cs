using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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
            private int _cnt_ex;
            private int _idStudent;
            private static int _id = 1;


            public double AvgMark
            {
                get
                {
                    if (_marks == null || _marks.Length == 0) return 0;

                    double obsh = 0;
                    int cnt = 0;
                    foreach (int mark in _marks)
                    {
                        if (mark != 0)
                        {
                            obsh += mark;
                            cnt++;
                        }
                    }
                    if (cnt == 0) return 0;
                    return obsh / cnt;
                }
            }

            public bool IsExpelled => _isExpelled;
            public string Name => _name;
            public string Surname => _surname;
            public int[] Marks
            {
                get
                {
                    if (_marks == null) return null;
                    int[] arrays = new int[_marks.Length];
                    Array.Copy(_marks, arrays, _marks.Length);
                    return arrays;
                }
            }
            public int ID => _idStudent;
            

            public void Restore()
            {
                if (_isExpelled)
                {
                    _isExpelled = false;


                    Console.WriteLine($"{Name} {Surname} учится ");
                }
                else
                {
                    Console.WriteLine();
                }
            }
            static Student()
            {
                _id = 1;
            }
            public Student(string name, string surname)
            {
                _name= name;
                _surname = surname;
                _marks = new int[3] { 0, 0, 0 };
                _isExpelled = false;
                _cnt_ex = 0;
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
                    for (int j = 0; j < n - 1 - i; j++)
                    {
                        if (array[j].AvgMark < array[j + 1].AvgMark)
                        {
                            Student temp = array[j];
                            array[j] = array[j + 1];
                            array[j + 1] = temp;
                        }
                    }
                }
            }

            public void Exam(int mark)
            {
                if (_marks == null) return;


                if (_isExpelled) return;

                if (_cnt_ex >= 3) return;
                if (mark >= 2 && mark <= 5)
                {
                    _marks[_cnt_ex] = mark;
                    _cnt_ex++;
                }
                else
                {
                    _marks[_cnt_ex] = mark;
                    _cnt_ex++;
                    _isExpelled = true;
                }
                if (mark <= 2) _isExpelled = true;
            }



            public void Print()
            {
                Console.WriteLine("----------------------------------------");
                Console.WriteLine($"Номер студ билета: {ID}");
                Console.WriteLine($"Студент: {Name} {Surname}");

                Console.WriteLine($"Оценки: {string.Join(", ", Marks)}");

                Console.WriteLine($"СР Балл: {AvgMark:F2}");

                Console.WriteLine($"Отчислен: {(IsExpelled ? "+" : "-")}");
                Console.WriteLine("----------------------------------------");

            }
        }
        public class Commission
        {


            public static Student[] Expel(ref Student[] students)
            {

                
                int k = 0;
                for (int i = 0; i < students.Length; i++)
                {
                    if (students[i].IsExpelled)
                    {
                        k++;
                    }
                }


                Student[] arrayn = new Student[k];


                Student[] arrayf = new Student[students.Length - k];

                
                int oti = 0;
                int nori = 0;
                for (int i = 0; i < students.Length; i++)
                {
                    if (students[i].IsExpelled)
                    {
                        arrayn[oti++] = students[i]; 
                    }
                    else
                    {
                        arrayf[nori++] = students[i];
                    }
                }

                
                students = arrayf;

                
                return arrayn;
            }
            public static void Sort(Student[] students)
            {
                if (students == null || students.Length == 0)
                    return;

                for (int i = 0; i < students.Length - 1; i++)
                {
                    for (int j = 0; j < students.Length - i - 1; j++)
                    {
                        if (students[j].ID > students[j + 1].ID)
                        {
                            Student t = students[j];
                            students[j] = students[j + 1];
                            students[j + 1] = t;
                        }
                    }
                }
            }
            public static void Restore(ref Student[] students, Student restored)
            {
                foreach (Student student in students)
                {
                    if (student.ID == restored.ID)
                    {
                        return;
                    }
                }

                if (restored.IsExpelled && restored.ID >= 1)
                {
                    int ar = Math.Min(restored.ID - 1, students.Length);
                    Student[] new_Students = new Student[students.Length + 1];


                    for (int i = 0; i < ar; i++)
                    {
                        new_Students[i] = students[i];
                    }


                    new_Students[ar] = restored;
                    for (int i = ar; i < students.Length; i++)
                    {
                        new_Students[i + 1] = students[i];
                    }
                    students = new_Students;
                }
                else
                {
                    return;
                }
            }
        }
    }
}
