using System;
using System.Collections.Generic;
using System.Linq;

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
            private static int _Id = 1;
            private int _idStudent;

            public string Name => _name;
            public string Surname => _surname;
            public int[] Marks
            {
                get
                {
                    if (_marks == null)
                    {
                        return new int[0];
                    }
                    int[] arr = new int[_marks.Length];
                    Array.Copy(_marks, arr, _marks.Length);
                    return arr;
                }
            }
            public bool IsExpelled => _isExpelled;
            public int ID => _idStudent;

            public double AvgMark
            {
                get
                {
                    if (_marks == null || _marks.Length == 0)
                    {
                        return 0;
                    }

                    double sum = 0;
                    int k = 0;

                    foreach (int mark in _marks)
                    {
                        if (mark != 0)
                        {
                            sum += mark;
                            k++;
                        }
                    }
                    return k == 0 ? 0 : sum / k;
                }
            }
            static Student()
            {
                _Id = 1;
            }

            public Student(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new int[3];
                _isExpelled = false;
                _idStudent = _Id++;
            }
            public void Restore()
            {
                if (_isExpelled)
                {
                    _isExpelled = false;
                    Console.WriteLine($"{Name} {Surname} не отчислен");
                }
                else
                {
                    Console.WriteLine();
                }
            }

            public void Exam(int mark)
            {
                if (_isExpelled)
                {
                    Console.WriteLine($"{Name} {Surname} отчислен");
                    return;
                }

                if (mark < 2 || mark > 5)
                {
                    Console.WriteLine();
                    return;
                }

                for (int i = 0; i < _marks.Length; i++)
                {
                    if (_marks[i] == 0)
                    {
                        _marks[i] = mark;
                        if (mark == 2)
                        {
                            _isExpelled = true;
                        }
                        return;
                    }
                }
                Console.WriteLine("оценки уже выставлены.");
            }

            public static void SortByAvgMark(Student[] array)
            {
                if (array == null || array.Length == 0)
                    return;

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

            public void Print()
            {
                Console.WriteLine($"ID: {ID}, Студент: {Name} {Surname}, Средний балл: {AvgMark:F2}, Отчислен: {(IsExpelled ? "Да" : "Нет")}");
            }
        }

        public class Commission
        {
            public static void Sort(Student[] students)
            {
                if (students == null || students.Length == 0)
                    return;

                for (int i = 0; i < students.Length - 1; i++)
                {
                    for (int j = 0; j < students.Length - 1 - i; j++)
                    {
                        if (students[j].ID > students[j + 1].ID)
                        {
                            Student temp = students[j];
                            students[j] = students[j + 1];
                            students[j + 1] = temp;
                        }
                    }
                }
            }

            public static Student[] Expel(ref Student[] students)
            {

                int k = 0;
                for (int i = 0; i < students.Length; i++)
                {
                    if (students[i].IsExpelled == true)
                    {
                        k++;
                    }
                }
                int j = 0;
                Student[] array = new Student[k];
                for (int i = 0; i < students.Length; i++)
                {
                    if (students[i].IsExpelled == true)
                    {
                        array[j++] = students[i];
                    }
                }
                students = students.Where(x => !(x.IsExpelled)).ToArray();
                return array;
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
                    int f = Math.Min(restored.ID - 1, students.Length);
                    Student[] new_Students = new Student[students.Length + 1];

                    for (int i = 0; i < f; i++)
                    {
                        new_Students[i] = students[i];
                    }
                    new_Students[f] = restored;
                    for (int i = f; i < students.Length; i++)
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