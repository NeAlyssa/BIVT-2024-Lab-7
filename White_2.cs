namespace Lab_7 {
    public class White_2 {
        public class Participant {

            // Поля
            private string _name;
            private string _surname;
            private double _firstJump;
            private double _secondJump;

            // Статические поля
            private static readonly int _normative;

            // Свойства
            public string Name => _name;
            public string Surname => _surname;
            public double FirstJump => _firstJump;
            public double SecondJump => _secondJump;
            public double BestJump => _firstJump >= _secondJump ? _firstJump : _secondJump;
            public bool IsPassed => BestJump >= _normative;

            // Конструктор
            public Participant(string name, string surname) {
                _name = name;
                _surname = surname;
                _firstJump = 0;
                _secondJump = 0;
            }

            public Participant(string name, string surname, double firstJump, double secondJump) {
                _name = name;
                _surname = surname;
                _firstJump = firstJump;
                _secondJump = secondJump;
            }

            // Статический конструктор
            static Participant() {
                _normative = 3;
            }

            // Методы
            public static void Sort(Participant[] array) {
                if (array == null) {
                    Console.WriteLine("Array can't be null");
                    return;
                }
                if (array.Length == 0) {
                    Console.WriteLine("Array must not be empty");
                    return;
                }

                var newArray = array.OrderByDescending(p => p.BestJump).ToArray();
                Array.Copy(newArray, array, newArray.Length);
            }

            public void Jump(double result) {
                if (result < 0) {
                    Console.WriteLine("Result can't be negative");
                    return;
                }

                if (_firstJump == 0) {
                    _firstJump = result;
                }
                else if (_secondJump == 0) {
                    _secondJump = result;
                }
            }

            public void Print() {
                Console.WriteLine("Name: " + Name + "\tSurname: " + Surname + "\tBestJump: " + BestJump);
            }

            public static Participant[]? GetPassed(Participant[] participants) {
                if (participants == null) {
                    Console.WriteLine("Array can't be null");
                    return null;
                }
                if (participants.Length == 0) {
                    Console.WriteLine("Array must not be empty");
                    return null;
                }
                return participants.Where(p => p.IsPassed).ToArray();
            }
        }
    }
}