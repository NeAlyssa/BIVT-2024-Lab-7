using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    public class White_3
    {
        public class Student
        {
            private readonly string _surname;
            private readonly string _name;
            protected int[] _marks;
            protected int _skipped;

            public string Surname => _surname;
            public string Name => _name;
            public int Skipped => _skipped;
            public double AvgMark => _marks == null || _marks.Length == 0 ? 0 : (_marks.Sum() / Convert.ToDouble(_marks.Length));

            public Student(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new int[0];
                _skipped = 0;
            }
            protected Student(Student student)
            {
                _name = student.Name;
                _surname = student.Surname;
                _marks = new int[student._marks.Length];
                Array.Copy(student._marks, _marks, student._marks.Length);
                _skipped = student.Skipped;
            }
            public void Lesson(int mark)
            {
                if (_marks == null) _marks = new int[0];
                if (mark == 0) _skipped++;
                else
                {
                    int[] temp = new int[_marks.Length + 1];
                    for (int i = 0; i < _marks.Length; i++)
                    {
                        temp[i] = _marks[i];
                    }
                    temp[temp.Length - 1] = mark;
                    _marks = temp;
                }
            }
            public static void SortBySkipped(Student[] array)
            {
                int length = array.Length;
                for (int i = 0; i < length - 1; i++)
                {
                    for (int j = 0; j < length - i - 1; j++)
                    {
                        if (array[j].Skipped < array[j + 1].Skipped)
                        {
                            Student temp = array[j];
                            array[j] = array[j + 1];
                            array[j + 1] = temp;
                        }
                    }
                }
            }
            public virtual void Print()
            {
                Console.WriteLine($"{Surname} {Name} средний балл: {Math.Round(AvgMark, 2)}, кол-во пропущенных занятий: {Skipped}");
            }
        }
        public class Undergraduate : Student
        {
            public Undergraduate(string name, string surname) : base(name, surname) { }

            public Undergraduate(Student student) : base(student) { }

            public void WorkOff(int mark)
            {
                if (_skipped == 0)
                {
                    for(int i = 0; i < _marks.Length; i++)
                    {
                        if(_marks[i] == 2)
                        {
                            _marks[i] = mark;
                            return;
                        }
                    }
                }
                else
                {
                    _skipped -= 1;
                    Lesson(mark);
                }
            }

            public override void Print()
            {
                Console.WriteLine($"{Surname} {Name} средний балл: {Math.Round(AvgMark, 2)}, кол-во пропущенных занятий: {Skipped}");
            }
        }
    }
}
