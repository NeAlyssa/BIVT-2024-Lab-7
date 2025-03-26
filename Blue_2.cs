using System;

namespace Lab_7
{
    public class Blue_2
    {
        public struct Participant
        {
            private string _name;
            private string _surname;
            private int[,] _marks;
            private int _count;

            public string Name => _name;
            public string Surname => _surname;

            public int[,] Marks
            {
                get
                {
                    if (_marks == null) return null;
                    int[,] r = new int[_marks.GetLength(0), _marks.GetLength(1)];
                    for (int i = 0; i < _marks.GetLength(0); i++)
                    {
                        for (int j = 0; j < _marks.GetLength(1); j++)
                        {
                            r[i, j] = _marks[i, j];
                        }
                    }
                    return r;
                }
            }

            public int TotalScore
            {
                get
                {
                    if (_marks == null) return 0;
                    int sum = 0;
                    foreach (var mark in _marks)
                        sum += mark;
                    return sum;
                }
            }

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new int[2, 5];
                _count = 0;
            }

            public void Jump(int[] result)
            {
                if (result == null || _marks == null) return;
                for (int i = 0; i < 5; i++)
                {
                    _marks[_count, i] = result[i];
                }
                _count++;
            }

            public static void Sort(Participant[] array)
            {
                if (array == null || array.Length == 0) return;
                for (int i = 0; i < array.Length; i++)
                {
                    for (int j = 0; j < array.Length - i - 1; j++)
                    {
                        if (array[j].TotalScore < array[j + 1].TotalScore)
                        {
                            (array[j], array[j + 1]) = (array[j + 1], array[j]);
                        }
                    }
                }
            }

            public void Print()
            {
                Console.WriteLine($"{Name}\t{Surname}\t{TotalScore}");
                if (_marks != null)
                {
                    for (int i = 0; i < _marks.GetLength(0); i++)
                    {
                        for (int j = 0; j < _marks.GetLength(1); j++)
                        {
                            Console.Write($"{_marks[i, j]} ");
                        }
                        Console.WriteLine();
                    }
                }
            }
        }

        public abstract class WaterJump
        {
            private string _name;
            private int _bank;
            protected Participant[] _participants;
            private int _count;

            public string Name => _name;
            public int Bank => _bank;

            public Participant[] Participants
            {
                get
                {
                    Participant[] result = new Participant[_count];
                    Array.Copy(_participants, result, _count);
                    return result;
                }
            }

            public abstract double[] Prize { get; }

            public WaterJump(string name, int bank)
            {
                _name = name;
                _bank = bank;
                _participants = new Participant[10]; 
                _count = 0;
            }

            public void Add(Participant participant)
            {
                if (_count >= _participants.Length) 
                {
                    Participant[] newArray = new Participant[_participants.Length * 2];
                    Array.Copy(_participants, newArray, _participants.Length);
                    _participants = newArray;
                }

                _participants[_count] = participant;
                _count++;
            }

            public void Add(Participant[] participants)
            {
                if (participants == null) return;

                while (_count + participants.Length > _participants.Length)
                {
                    Participant[] newArray = new Participant[_participants.Length * 2];
                    Array.Copy(_participants, newArray, _participants.Length);
                    _participants = newArray;
                }

                foreach (var p in participants)
                {
                    _participants[_count] = p;
                    _count++;
                }
            }
        }

        public class WaterJump3m : WaterJump
        {
            public WaterJump3m(string name, int bank) : base(name, bank) { }

            public override double[] Prize
            {
                get
                {
                    if (Participants.Length < 3) return new double[0];
                    return new double[] { 0.5 * Bank, 0.3 * Bank, 0.2 * Bank };
                }
            }
        }

        public class WaterJump5m : WaterJump
        {
            public WaterJump5m(string name, int bank) : base(name, bank) { }

            public override double[] Prize
            {
                get
                {
                    if (Participants.Length < 3) return new double[0];

                    int n = Participants.Length, k = n / 2;
                    double[] prize = new double[k];

                    if (k <= 10)
                    {
                        for (int i = 3; i < k; i++)
                            prize[i] = 0.01 * Bank * (20 / k);
                    }
                    else
                    {
                        for (int i = 3; i < 10; i++)
                            prize[i] = 0.01 * Bank * 10;
                    }

                    prize[0] = 0.4 * Bank;
                    prize[1] = 0.25 * Bank;
                    prize[2] = 0.15 * Bank;

                    return prize;
                }
            }
        }
    }
}
