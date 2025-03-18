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
            //поля
            private string _name;
            private string _surname;
            protected int[] _marks;
            protected int _skipped;

            //свойства
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
                    return (double)Sum(_marks) / _marks.Length; // Средняя оценка
                }
            }

            //конструкторы
            public Student(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new int[0];
                _skipped = 0;
            }

            //защищённый конструктор копирования
            protected Student(Student student)
            {
                _name = student._name;
                _surname = student._surname;
                _marks = (int[])student._marks.Clone(); // Клонируем массив
                _skipped = student._skipped;
            }

            public class Undergraduate : Student //класс-наследник
            {
                //конструкторы
                public Undergraduate(string name, string surname) : base(name, surname) { } 

                public Undergraduate(Student student) : base(student) { }

                ///методы
                public void WorkOff(int mark) //метод отработки пропусков
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
                            if (_marks[i] == 2) // Ищем двойку и заменяем на новую оценку
                            {
                                _marks[i] = mark;
                                return;
                            }
                        }
                    }
                }

                public new void Print()
                {
                    Console.WriteLine($"Студент: {Name} {Surname}, Средняя оценка: {AvgMark:F2}, Пропуски: {Skipped}");
                }
            }
                //методы
                public void Lesson(int mark)
                {
                    if (mark == 0)
                    {
                        _skipped++; // Увеличиваем количество пропусков
                    }
                    else
                    {
                        if (_marks == null ) return;
                        int[] newMarks = new int[_marks.Length + 1];
                        for (int i = 0; i < _marks.Length; i++)
                        {
                            newMarks[i] = _marks[i];
                        }

                        // Добавляем новый результат в конец массива
                        newMarks[newMarks.Length - 1] = mark;
                        _marks = newMarks;
                    }
                }

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
                            if (array[j].Skipped < array[j + 1].Skipped) // Сортировка по убыванию пропусков
                            {
                                (array[j], array[j + 1]) = (array[j + 1], array[j]);
                                sw = true;
                            }
                        }
                        if (!sw) break; // Если перестановок не было, массив уже отсортирован
                    }
                }

                public void Print()
                {
                    Console.WriteLine($"Имя: {_name}, Фамилия: {_surname}, Средняя оценка: {AvgMark:F2}, Пропуски: {_skipped}");
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


