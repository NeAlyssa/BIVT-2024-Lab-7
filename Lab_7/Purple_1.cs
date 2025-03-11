using System;

namespace Lab_7
{
    public class Purple_1
    {
        public class Participant //TODO check if the change to class broke anything
        {
            private string _name;
            private string _surname;
            private double[] _coefs;
            private int[,] _marks;

            private int _amount_of_jumps;


            public string Name => _name;
            public string Surname => _surname;
            public double[] Coefs
            {
                get
                {
                    if (_coefs == null) return default(double[]);

                    var newArray = new double[_coefs.Length];
                    Array.Copy(_coefs, newArray, _coefs.Length);
                    return newArray;
                }
            }
            public int[,] Marks
            {
                get
                {
                    if (_marks == null) return default(int[,]);

                    var newMatrix = new int[_marks.GetLength(0), _marks.GetLength(1)];
                    Array.Copy(_marks, newMatrix, _marks.Length);
                    return newMatrix;
                }
            }
            public double TotalScore
            {
                get
                {
                    if (_marks == null || _coefs == null) return 0;

                    double totalScore = 0;
                    for (int i = 0; i < _marks.GetLength(0); i++)
                    {
                        int[] newArr = new int[7];
                        for (int j = 0; j < _marks.GetLength(1); j++) newArr[j] = _marks[i, j];
                        Array.Sort(newArr);

                        double sum = 0;

                        for (int k = 1; k < newArr.Length - 1; k++) sum += newArr[k];

                        sum *= _coefs[i];
                        totalScore += sum;
                    }
                    return totalScore;
                }
            }



            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _coefs = new double[] { 2.5, 2.5, 2.5, 2.5 };
                _marks = new int[4, 7];
                _amount_of_jumps = 0;
            }


            public void SetCriterias(double[] coefs)
            {
                if (coefs == null || _coefs == null || coefs.Length != 4) return;
                Array.Copy(coefs, _coefs, coefs.Length);
            }

            public void Jump(int[] marks)
            {
                if (_amount_of_jumps > 4 || marks == null || _marks == null || marks.Length != 7) return; //needed or not?
                for (int j = 0; j < _marks.GetLength(1); j++)
                {
                    _marks[_amount_of_jumps, j] = marks[j];
                }
                _amount_of_jumps++;
            }

            public static void Sort(Participant[] array)
            {
                if (array == null) return;
                for (int i = 0; i < array.Length; i++)
                {
                    Participant key = array[i];
                    int j = i - 1;

                    while (j >= 0 && array[j].TotalScore < key.TotalScore)
                    {
                        array[j + 1] = array[j];
                        j = j - 1;
                    }
                    array[j + 1] = key;
                }
            }

            public void Print()
            {
                Console.WriteLine(_name + " " + _surname);
                Console.Write("Coefs: ");
                foreach (double var in _coefs)
                {
                    Console.Write(var + "  ");
                }
                Console.WriteLine();
                Console.WriteLine("Marks:");
                for (int i = 0; i < _marks.GetLength(0); i++)
                {
                    for (int j = 0; j < _marks.GetLength(1); j++)
                    {
                        Console.Write(_marks[i, j] + " ");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
        }

        public class Judge
        {
            private string _name;
            private int[] _marks;

            private int indexInMarks;

            public string Name => _name;

            public Judge(string name, int[] marks)
            {
                _name = name;
                _marks = (int[])marks.Clone(); //Because reference type
                indexInMarks = 0;
            }

            public int CreateMark()
            {
                if (indexInMarks >= _marks.Length) indexInMarks = 0; //should work?
                return _marks[indexInMarks++];
            }

            public void Print()
            {
                Console.WriteLine(Name);
                foreach (var num in _marks)
                {
                    Console.Write(num + " ");
                }
                Console.WriteLine();
            }

        }

        public class Competition
        {
            private Participant[] _participants;
            private Judge[] _judges;

            public Participant[] Participants => _participants; //Because incapsulated.....or is it?
            public Judge[] Judges => _judges;

            public Competition(Judge[] judges)
            {
                _judges = (Judge[])judges.Clone(); //I have no idea whether this works, how it does if yes or if its needed at all
                _participants = new Participant[0];
            }

            public void Evaluate(Participant jumper)
            {
                if (jumper == null) return;

                int[][] marks = new int[][]
                {
                    new int[_judges.Length], new int[_judges.Length], new int[_judges.Length], new int[_judges.Length]
                };
                for (int jump = 0; jump < 4; jump++)
                {
                    for (int judge = 0; judge < _judges.Length; judge++)
                    {
                        marks[jump][judge] = _judges[judge].CreateMark();
                    }
                }
                for (int jump = 0; jump < 4; jump++)
                {
                    jumper.Jump(marks[jump]);
                }
            }

            public void Add(Participant jumper)
            {
                if (jumper == null) return;
                Array.Resize(ref _participants, _participants.Length + 1);
                Evaluate(jumper);
                _participants[_participants.Length - 1] = jumper;
            }

            public void Add(Participant[] jumpers)
            {
                foreach (var jumper in jumpers) Add(jumper);
            }

            public void Sort()
            {
                if (_participants == null) return;
                for (int i = 0; i < _participants.Length; i++)
                {
                    Participant key = _participants[i];
                    int j = i - 1;

                    while (j >= 0 && _participants[j].TotalScore < key.TotalScore)
                    {
                        _participants[j + 1] = _participants[j];
                        j = j - 1;
                    }
                    _participants[j + 1] = key;
                }
            }
        }
    }
}
