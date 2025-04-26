namespace Lab_7 {
    public class White_4 {
        public class Human {
            protected string _name;
            protected string _surname;

            public string Name => _name;
            public string Surname => _surname;

            public Human(string name, string surname) {
                _name = name;
                _surname = surname;
            }

            public void Print() {
               Console.WriteLine("Human's name: " + Name + "\tSurname: " + Surname); 
            }
        }

        public class Participant : Human {
            // Поля
            private double[] _scores;
            private static int _count;

            // Свойства
            public double[]? Scores {
                get {
                    if (_scores == null) {
                        return default(double[]);
                    }

                    var newArray = new double[_scores.Length];
                    Array.Copy(_scores, newArray, _scores.Length);
                    return newArray;
                }
            }
            public double TotalScore => (_scores != null) ? _scores.Sum() : 0;
            public static int Count => _count;
            

            // Конструктор
            public Participant(string name, string surname) : base(name, surname) {
                _scores = new double[0];
                _count += 1;
            }

            static Participant() {
                _count = 0;
            }

            // Методы
            public void PlayMatch(double result) {
                const double eps = 0.0001;

                if (!(Math.Abs(result - 0) < eps || Math.Abs(result - 0.5) < eps || Math.Abs(result - 1) < eps)) {
                    Console.WriteLine("Result can be one of: 0, 0.5, 1");
                    return;
                }

                if (_scores == null) {
                    Console.WriteLine("Array of scores wasn't initialized");
                    return;
                }

                var newArray = new double[_scores.Length + 1];
                Array.Copy(_scores, newArray, _scores.Length);
                newArray[_scores.Length] = result;
                _scores = newArray;
            }

            public static void Sort(Participant[] array) {
                if (array == null) {
                    Console.WriteLine("Array can't be null");
                    return;
                }
                if (array.Length == 0) {
                    Console.WriteLine("Array must not be empty");
                    return;
                }

                var newArray = array.OrderByDescending(p => p.TotalScore).ToArray();
                Array.Copy(newArray, array, newArray.Length);
            }

            public new void Print() {
               Console.WriteLine("Name: " + Name + "\tSurname: " + Surname + "\tTotal score: " + TotalScore); 
            }

        }
    }
}