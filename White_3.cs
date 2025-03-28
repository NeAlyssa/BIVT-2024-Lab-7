using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lab_7
{
    public class White_3
    {
        public class Student
        {
            // Поля
            private string _name;
            private string _surname;
            protected int[] _marks;  
            protected int _skipped;  

            // Свойства
            public string Name => _name;
            public string Surname => _surname;
            public int Skipped => _skipped;
            public double AvgMark
            {
                get
                {
                    if (_marks == null || _marks.Length == 0)
                    {
                        return 0;
                    }
                    return (double)Sum(_marks) / _marks.Length;
                }
            }

            //конструктор
            public Student(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new int[0];
                _skipped = 0;
            }

            //защищенный конструктор
            protected Student(Student student)
            {
                _name = student._name;
                _surname = student._surname;
                _marks = (int[])student._marks.Clone();
                _skipped = student._skipped;
            }

            // Метод посещения урока
            public void Lesson(int mark)
            {
                if (mark == 0)
                {
                    _skipped++;
                }
                else
                {
                    if (_marks == null) return;
                    int[] newMarks = new int[_marks.Length + 1];
                    for (int i = 0; i < _marks.Length; i++)
                    {
                        newMarks[i] = _marks[i];
                    }
                    newMarks[newMarks.Length - 1] = mark;
                    _marks = newMarks;
                }
            }

            // Сортировка по количеству пропусков
            public static void SortBySkipped(Student[] array)
            {
                if (array == null || array.Length == 0) return;
                int n = array.Length;
                bool sw;

                for (int i = 0; i < n - 1; i++)
                {
                    sw = false;
                    for (int j = 0; j < n - 1 - i; j++)
                    {
                        if (array[j].Skipped < array[j + 1].Skipped)
                        {
                            (array[j], array[j + 1]) = (array[j + 1], array[j]);
                            sw = true;
                        }
                    }
                    if (!sw) break;
                }
            }

            public void Print()
            {
                Console.WriteLine($"Имя: {_name}, Фамилия: {_surname}, Средняя оценка: {AvgMark:F2}, Пропуски: {_skipped}");
            }

            // Подсчет суммы оценок
            private int Sum(int[] array)
            {
                int s = 0;
                if (array == null || array.Length == 0) return 0;
                for (int i = 0; i < array.Length; i++)
                {
                    s += array[i];
                }
                return s;
            }
        }

        // класс насследник
        public class Undergraduate : Student
        {
            // конструктор
            public Undergraduate(string name, string surname) : base(name, surname) { }

            //конструктор
            public Undergraduate(Student student) : base(student) { }

            //метод WorkOff: возможность "отработать" пропуск
            public void WorkOff(int mark)
            {
                if (_skipped > 0)
                {
                    _skipped--;
                    Lesson(mark);
                }
                else
                {
                    for (int i = 0; i < _marks.Length; i++)
                    {
                        if (_marks[i] == 2)  // Ищем двойку и заменяем
                        {
                            _marks[i] = mark;
                            return;
                        }
                    }
                }
            }

            //метод Print()
            public void Print()
            {
                Console.WriteLine($"(Undergraduate) Имя: {Name}, Фамилия: {Surname}, Средняя оценка: {AvgMark:F2}, Пропуски: {Skipped}");
            }

            private int Sum(int[] array)
                {
                    int s = 0;
                    if (array.Length == 0) return 0;
                    if (array==null) return 0;
                    for (int i = 0; i < array.Length; i++)
                    {
                        s += array[i];
                    }
                    return s;
                }

        }

    }

}


