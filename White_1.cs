namespace Lab_7 {
    public class White_1 {
        public class Participant {

            // Поля
            private string _surname;
            private string _club;
            private double _firstJump;
            private double _secondJump;

            // Статические поля
            private static int _normative;
            private static int _jumpers;
            private static int _disqualified;
        
            // Свойства
            public string Surname => _surname;
            public string Club => _club;
            public double FirstJump => _firstJump;
            public double SecondJump => _secondJump;
            public double JumpSum => _firstJump + _secondJump;

            //Статические свойства
            public static int Jumpers => _jumpers;
            public static int Disqualified => _disqualified;

            // Конструктор
            public Participant(string surname, string club) {
                _surname = surname;
                _club = club;
                _firstJump = 0;
                _secondJump = 0;
                _jumpers += 1;
            }

            // Статический конструктор
            static Participant() {
                _normative = 5;
                _jumpers = 0;
                _disqualified = 0;
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

                var newArray = array.OrderByDescending(p => p.JumpSum).ToArray();
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
                Console.WriteLine("Surname: " + Surname + "\tClub: " + Club + "\tJumpSum: " + JumpSum);
            }

            // Статические методы
            public static void Disqualify(ref Participant[] participants) {
                if (participants == null) {
                    Console.WriteLine("Array can't be null");
                    return;
                }
                if (participants.Length == 0) {
                    Console.WriteLine("Array must not be empty");
                    return;
                }

                var qualified = participants.Where(
                    p => (p.FirstJump >= _normative && p.SecondJump >= _normative)
                    ).ToArray();
                _disqualified += participants.Length - qualified.Length;
                participants = qualified;
            }
        }
    }
}