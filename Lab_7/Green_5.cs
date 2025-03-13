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
        public struct Student // Публичная структура студента.
        {
            private string _name; // Его имя.
            private string _surname; // Его фамилия.
            private int[] _marks; // Его оценки. 
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
                get { return _marks; }
            }
            public Student(string name, string surname)  // Публичный конструктор, принимающий имя и фамилию, инициализирует массив оценок по нулям.
            {
                _name = name;
                _surname = surname;
                _marks = new int[5];
                for (int i = 0; i < _marks.Length; i++)
                {
                    _marks[i] = 0;
                }
            }
            public double AvgMark // Свойство, возвращающее средний балл.
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
            public void Exam(int mark) // Студент одноразово проходит экзамен и получает оценку за него.
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
            public void Print() // Публичный метод для вывода информации о необходимых полях структуры.
            {
                Console.WriteLine("{0,-12} {1,-12} {2,-12:F2} {3, -12}", Name, Surname, AvgMark, string.Join(", ", Marks));
            }
        }
        public class Group // Публичная структура группы.
        {
            private string _name; // Название.
            private Green_5.Student[] _students; // Студенты в ней.
            private int _studentCount; // Их количество.
            public string Name // Свойство имени.
            {
                get { return _name; }
            }
            public Green_5.Student[] Students // Свойство студентов.
            {
                get { return _students != null ? (Student[])_students.Clone() : null; }
            }
            public virtual double AvgMark // Высчитываем средний балл в группе.
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
            public Group(string name) // Конкструктор группы.
            {
                _name = name;
                _students = new Green_5.Student[0];
                _studentCount = 0;
            }
            public void Add(Green_5.Student student) // Метод добавления одного студента.
            {
                if (_students == null)
                {
                    _students = new Green_5.Student[0];
                }
                Array.Resize(ref _students, _studentCount + 1);
                _students[_studentCount] = student;
                _studentCount++;
            }
            public void Add(Green_5.Student[] newStudents) // Метод добавления нескольких студентов.
            {
                if (newStudents == null)
                {
                    return;
                }
                if (_students == null)
                {
                    _students = new Green_5.Student[0];
                }
                int newLength = _studentCount + newStudents.Length;
                Array.Resize(ref _students, newLength);
                for (int i = 0; i < newStudents.Length; i++)
                {
                    _students[_studentCount + i] = newStudents[i];
                }
                _studentCount = newLength;
            }
            public static void SortByAvgMark(Group[] array) // Сортировка.
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
            public void Print() // Публичный метод для вывода информации о необходимых полях структуры.
            {
                Console.WriteLine("{0,-12} {1,-15:F2}", Name, AvgMark);
            }
        }
        public class EliteGroup : Group // Публичный класс элиты.
        {
            private string _name; // Название элитной группы.
            public EliteGroup(string name) : base(name)
            {
                _name = name;
            }
            public override double AvgMark // Среднее взвешненное значение.
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
                                    default:
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
        public class SpecialGroup : Group // Спецгруппа.
        {
            private string _name; // Её название.
            public SpecialGroup(string name) : base(name)
            {
                _name = name;
            }
            public override double AvgMark // Среднее взвешненное значение.
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
                                    default:
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
