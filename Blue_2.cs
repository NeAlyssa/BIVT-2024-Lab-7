using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    public class Blue_2
    {
        public struct Participant
        {
            private string _name;
            private string _surname;
            private int[,] _marks;

            public string Name => _name;
            public string Surname => _surname;
            public int[,] Marks
            {
                get
                {
                    if (_marks == null) return null;
                    int[,] copy = new int[_marks.GetLength(0), _marks.GetLength(1)];
                    for (int i = 0; i < _marks.GetLength(0); i++)
                    {
                        for (int j = 0; j < _marks.GetLength(1); j++)
                        {
                            copy[i, j] = _marks[i, j];
                        }
                    }
                    return copy;
                }
            }


            public int TotalScore
            {
                get
                {
                    if (_marks == null) return 0;
                    int score = 0;
                    for (int i = 0; i < _marks.GetLength(0); i++)
                    {
                        for (int j = 0; j < _marks.GetLength(1); j++)
                        {
                            score += _marks[i, j];
                        }
                    }
                    return score;
                }
            }


            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new int[2, 5];
            }


            public void Jump(int[] result)
            {
                if (_marks == null || result == null || _marks.GetLength(0) < 2 || _marks.GetLength(1) < 2) return;
                if (_marks[0, 0] == 0 && _marks[0, 1] == 0 && _marks[0, 2] == 0 && _marks[0, 3] == 0 && _marks[0, 4] == 0)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        _marks[0, i] = result[i];
                    }
                }
                else if (_marks[1, 0] == 0 && _marks[1, 1] == 0 && _marks[1, 2] == 0 && _marks[1, 3] == 0 && _marks[1, 4] == 0)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        _marks[1, i] = result[i];
                    }
                }
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
                Console.WriteLine($"{_name}   {_surname}   {TotalScore}");
            }

        }

        public abstract class WaterJump
        {
            private string _name;
            private int _bank;
            private Participant[] _participants;


            public string Name => _name;
            public int Bank => _bank;
            public Participant[] Participants => _participants;

            public abstract double[] Prize { get; }


            public WaterJump(string name, int bank)
            {
                _name = name;
                _bank = bank;
                _participants = new Participant[0];
            }

            public void Add(Participant participant)
            {
                if (_participants == null) return;
                Array.Resize(ref _participants, _participants.Length + 1);
                _participants[_participants.Length - 1] = participant;

            }

            public void Add(Participant[] participants)
            {
                if (participants == null || _participants == null) return;
                foreach (Participant participant in participants)
                {
                    Add(participant);
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
                    if (Participants == null || Participants.Length < 3) return null;
                    double[] prize = new double[3];
                    prize[0] = Bank * 0.5;
                    prize[1] = Bank * 0.3;
                    prize[2] = Bank * 0.2;
                    return prize;
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
                    if (Participants == null || Participants.Length < 3) return null;
                    double[] prize = new double[Math.Min(Participants.Length/2, 10)];
                    double N = 20.0 / Math.Min(Participants.Length / 2, 10);
                    prize[0] = Bank * 0.3;
                    prize[1] = Bank * 0.25;
                    prize[2] = Bank * 0.15;
                    for (int i = 0; i < prize.Length; i++)
                    {
                        prize[i] += N / 100 * Bank;
                    }
                    return prize;
                }
            }
        }
    }
}
