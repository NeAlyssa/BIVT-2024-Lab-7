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
        public class Student // Публичный класс студента.
        {
            private string _name; // Его имя.
            private string _surname; // Его фамилия.
            private int[] _marks; // Его оценки.
            private bool _isExpelled; // Флаг отчисления, выставляемый при неудачном экзамене.
            private int _examCount; // Кол-во сданных экзаменов.
            private int _id; // Настоящий номер его студ. билета.
            private static int _nextId; // Статичный айдишник для продвижения.
            private bool _restored; // Статус камбека.
            static Student() // Статичный конструктор специально для айдишника.
            {
                _nextId = 1;
            }
            public Student(string name, string surname) // Публичный конструктор студента.
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
            public string Name // Свойство имени.
            {
                get { return _name; }
            }
            public string Surname // Свойство фамилии.
            {
                get { return _surname; }
            }
            public int[] Marks // Свойство оценок.
            {
                get { return _marks != null ? (int[])_marks.Clone() : null; }
            }
            public int ID // Свойство айдишника.
            {
                get { return _id; }
            }
            public double AvgMark // Свойство средней оценки.
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
            public bool IsExpelled // Свойство статуса отчисления.
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
            public void Exam(int mark) // Экзамен.
            {
                if (_marks == null || _marks.Length == 0)
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
                    _restored = false;
                    _isExpelled = false;
                }
                _marks[_examCount] = mark;
                _examCount++;
                if (mark < 3)
                {
                    _isExpelled = true;
                }
            }
            public void Restore() // Изменяем с "отчисленного" на "не отчисленного".
            {
                _restored = true;
                _isExpelled = false;
            }
            public static void SortByAvgMark(Student[] array) // Метод сортировки по средней оценке.
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
                            Student temp = array[i];
                            array[i] = array[i + 1];
                            array[i + 1] = temp;
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
        public class Commission // Публичный класс комиссии.
        {
            public static void Sort(Student[] students) // Сортировка.
            {
                bool swapped;
                do
                {
                    swapped = false;
                    for (int i = 0; i < students.Length - 1; i++)
                    {
                        if (students[i].ID > students[i + 1].ID)
                        {
                            Student temp = students[i];
                            students[i] = students[i + 1];
                            students[i + 1] = temp;
                            swapped = true;
                        }
                    }
                } while (swapped);
            }
            public static Student[] Expel(ref Student[] students) // Отчисляем студентов (жаль что не всех).
            {
                int count = 0;
                for (int i = 0; i < students.Length; i++)
                {
                    if (students[i].IsExpelled == true)
                    {
                        count++;
                    }
                }
                int index = 0;
                Student[] ExStudents = new Student[count];
                for (int i = 0; i < students.Length; i++)
                {
                    if (students[i].IsExpelled == true)
                    {
                        ExStudents[index++] = students[i];
                    }
                }
                students = students.Where(x => !(x.IsExpelled)).ToArray();
                return ExStudents;
            }
            public static void Restore(ref Student[] students, Student restored) // Зачисляем студента.
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
                    int insertPos = Math.Min(restored.ID - 1, students.Length);
                    Student[] newStudents = new Student[students.Length + 1];
                    for (int i = 0; i < insertPos; i++)
                    {
                        newStudents[i] = students[i];
                    }
                    newStudents[insertPos] = restored;
                    for (int i = insertPos; i < students.Length; i++)
                    {
                        newStudents[i + 1] = students[i];
                    }
                    students = newStudents;
                }
                else
                {
                    return;
                }
            }
        }
    }
}
