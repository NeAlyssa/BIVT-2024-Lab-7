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
                        for (int j = 0; j < _marks.GetLength(1); j++)
                            r[i, j] = _marks[i, j];
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

                for (int i = 1; i < array.Length; i++)
                {
                    for (int j = i; j > 0 && array[j].TotalScore > array[j - 1].TotalScore; j--)
                    {
                        var temp = array[j];
                        array[j] = array[j - 1];
                        array[j - 1] = temp;
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
                            Console.Write($"{_marks[i, j]} ");
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

            public Participant[] Participants => _participants;

            public abstract double[] Prize { get; }

            public WaterJump(string name, int bank)
            {
                _name = name;
                _bank = bank;
                _participants = new Participant[0];
                _count = 0;
            }

            public void Add(Participant participant)
            {
                
                if (_participants == null) return;
                Participant[] newarr = new Participant[_participants.Length + 1];
                for (int i = 0; i < _participants.Length; i++)
                {
                   newarr[i] = _participants[i];
                }
                newarr[_participants.Length] = participant;
                _participants = newarr;
            }

            public void Add(Participant[] participants)
            {
                if (participants == null || participants.Length == 0) return;

                foreach (var p in participants)
                {
                    Add(p);
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
                    if (this.Participants == null) return null;
                    if (Participants.Length < 3) return null;
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
                    var participants = Participants;
                    if (participants.Length < 3) return null;

                    double[] basePrize = { 0.4 * Bank, 0.25 * Bank, 0.15 * Bank };

                    int count = participants.Length / 2;
                    if (count < 3) count = 3;
                    if (count > 10) count = 10;

                    double bonusPool = 0.20 * Bank;
                    double bonusPerPerson = bonusPool / count;

                    double[] finalPrizes = new double[count];
                    for (int i = 0; i < 3; i++)
                        finalPrizes[i] = basePrize[i];

                    for (int i = 0; i < count; i++)
                        finalPrizes[i] += bonusPerPerson;

                    return finalPrizes;
                }
            }
        }
    }
}
