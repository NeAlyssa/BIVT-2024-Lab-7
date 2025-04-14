using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
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
            private int _count;

            public string Name => _name;
            public string Surname => _surname;

            public int[,] Marks
            {
                get
                {
                    if (_marks == null) return null;
                    int[,] marks = new int[_marks.GetLength(0), _marks.GetLength(1)];
                    for (int i = 0; i < _marks.GetLength(0); i++)
                    {
                        for (int j = 0; j < _marks.GetLength(1); j++)
                        {
                            marks[i, j] = _marks[i, j];
                        }
                    }
                    return marks;

                }
            }

            public int TotalScore
            {
                get
                {
                    if (_marks == null) return 0;
                    int total_score = 0;
                    for (int i = 0; i < 2; i++)
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            total_score += _marks[i, j];
                        }
                    }
                    return total_score;
                }
            }

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new int[,] { { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 } };
                _count = 0;
            }

            public void Jump(int[] result)
            {
                if (_marks == null || _marks.GetLength(0) == 0 || result == null || result.Length == 0 || _count > 1) return;
                for (int i = 0; i < 5; i++)
                {
                    _marks[_count, i] = result[i];
                }
                _count++;
            }

            public static void Sort(Participant[] array)
            {
                if (array == null || array.Length == 0) return;
                //for (int i = 0; i < array.Length; i++)
                //{
                //    for (int j = 0; j < array.Length - i - 1; j++)
                //    {
                //        if (array[j].TotalScore < array[j + 1].TotalScore)
                //        {
                //            (array[j], array[j + 1]) = (array[j + 1], array[j]);
                //        }
                //    }
                //}

                for (int i = 1; i < array.Length;)
                {
                    if (i == 0 || array[i].TotalScore <= array[i - 1].TotalScore)
                        i++;
                    else
                    {
                        (array[i], array[i - 1]) = (array[i - 1], array[i]);
                        i--;
                    }
                }
            }

            public void Print()
            {
                Console.WriteLine($"{_name} {_surname}: {TotalScore} TotalScore");
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

            public abstract double[] Prize
            {
                get;
            }

            public WaterJump(string name, int bank)
            {
                _name = name;
                _bank = bank;
                _participants = new Participant[0];
            }
            public void Add(Participant participant)
            {
                if (_participants == null) return;
                Participant[] p = new Participant[_participants.Length + 1];
                for (int i = 0; i < _participants.Length; i++)
                {
                    p[i] = _participants[i];
                }
                p[_participants.Length] = participant;
                _participants = p;
            }
            public void Add(Participant[] participants)
            {
  
                if (participants == null || _participants == null) return;
                foreach (Participant p in participants)
                {
                    Add(p);
                }
            }
        }

        public class WaterJump3m : WaterJump
        {
            public WaterJump3m(string _name, int bank) : base(_name, bank) { }
            public override double[] Prize
            {
                get
                {
                    if (this.Participants.Length < 3 || this.Participants == null) return null;
                    double[] prize = new double[3];
                    prize[0] = (double) this.Bank * 0.5;
                    prize[1] = (double) this.Bank * 0.3;
                    prize[2] = (double) this.Bank * 0.2;

                    return prize;
                }
            }
        }

        public class WaterJump5m : WaterJump
        {
            public WaterJump5m(string _name, int bank) : base(_name, bank) { }
            public override double[] Prize
            {
                get
                {
                    if (this.Participants.Length < 3 || this.Participants == null) return null;
                    int count = this.Participants.Length / 2;
                    if (count > 10)
                    {
                        count = 10;
                    }
                    double[] prize = new double[count];
                    double N = 20 / (double) count;
                    for (int i = 0; i < count; i++)
                    {
                        prize[i] = Math.Round((double)this.Bank * N/100, 5);
                    }
                    prize[0] += (double) this.Bank * 0.4;
                    prize[1] += (double) this.Bank * 0.25;
                    prize[2] += (double) this.Bank * 0.15;
                    return prize;
                }
            }

        }
    }
}
