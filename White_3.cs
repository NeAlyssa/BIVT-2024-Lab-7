namespace Lab_7 {
    public class White_3 {
        public class Student {
            // Поля
            private string _name;
            private string _surname;
            protected int[] _marks;
            protected int _skips;

            // Свойства
            public string Name => _name;
            public string Surname => _surname;
            public int Skipped => _skips;
            public double AvgMark => (_marks != null && _marks.Length > 0) ? (double)_marks.Sum()/_marks.Length : 0;

            // Конструктор
            public Student(string name, string surname) {
                _name = name;
                _surname = surname;
                _marks = new int[0];
                _skips = 0;
            }

            // Защищенный конструктор
            protected Student(Student student) {
                _name = student._name;
                _surname = student._surname;
                _marks = new int[student._marks.Length];
                Array.Copy(student._marks, _marks, student._marks.Length);
                _skips = student._skips;
            }

            // Методы
            public void Lesson(int mark) {
                if (mark < 0) {
                    Console.WriteLine("Mark can't be negative");
                    return;
                } else if (mark == 0) {
                    _skips += 1;
                } else {
                    if (_marks == null) {
                        Console.WriteLine("Array of marks wasn't initialized");
                        return;
                    }

                    var newArray = new int[_marks.Length + 1];
                    Array.Copy(_marks, newArray, _marks.Length);
                    newArray[_marks.Length] = mark;
                    _marks = newArray;
                }
            }

            public static void SortBySkipped(Student[] array) {
                if (array == null) {
                    Console.WriteLine("Array can't be null");
                    return;
                }
                if (array.Length == 0) {
                    Console.WriteLine("Array must not be empty");
                    return;
                }

                var newArray = array.OrderByDescending(p => p.Skipped).ToArray();
                Array.Copy(newArray, array, newArray.Length);
            }

            public virtual void Print() {
               Console.WriteLine("Name: " + Name + "\tSurname: " + Surname + "\tAverage mark: " + AvgMark + "\t Skipped: " + Skipped); 
            }
        }

        public class Undergraduate : Student {
            public Undergraduate(string name, string surname) : base(name, surname) {
            }

            public Undergraduate(Student student) : base(student) {
            }

            public void WorkOff(int mark) {
                if (_skips > 0) {
                    _skips -= 1;
                    Lesson(mark);
                }
                else {
                    for (int i = 0; i < _marks.Length; ++i) {
                        if (_marks[i] == 2) {
                            _marks[i] = mark;
                            return;
                        }
                    }
                }
            }

            public override void Print() {
               Console.WriteLine("Undergraduate's name: " + Name + "\tSurname: " + Surname + "\tAverage mark: " + AvgMark + "\t Skipped: " + Skipped); 
            }
        }
    }
}