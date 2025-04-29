using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Lab_7
{
    public class Blue_2
    {
        public struct Participant
        {
            private string _name;
            private string _surname;
            private int[,] _marks;
            private int _c;

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new int[2, 5];
                _c = 0;
            }

            public string Name => _name;
            public string Surname => _surname;
            public int[,] Marks
            {
                get
                {
                    if (_marks == null) return null;
                    int[,] newMarks = new int[_marks.GetLength(0), _marks.GetLength(1)];
                    for (int i = 0; i < _marks.GetLength(0); i++)
                    {
                        for (int j = 0; j < _marks.GetLength(1); j++)
                        {
                            newMarks[i, j] = _marks[i, j];
                        }
                    }
                    return newMarks;
                }
            }

            public int TotalScore
            {
                get
                {
                    if (_marks == null) return 0;
                    int total = 0;
                    for (int i = 0; i < _marks.GetLength(0); i++)
                    {
                        for (int j = 0; j < _marks.GetLength(1); j++)
                        {
                            total += _marks[i, j];
                        }
                    }
                    return total;
                }
            }

            public void Jump(int[] result)
            {
                if (_c > 1 || _marks == null || result == null || result.Length == 0 || _marks.GetLength(0) == 0) return;

                for (int i = 0; i < 5; i++)
                {
                    _marks[_c, i] = result[i];
                }
                _c++;
            }

            public static void Sort(Participant[] array)
            {
                if (array == null || array.Length == 0) return;


                for(int i = 0; i < array.Length; i ++)
                {
                    for(int j = 0;j <  array.Length-1-i; j ++)
                    {
                        if (array[j].TotalScore < array[j + 1].TotalScore) (array[j], array[j + 1]) = (array[j + 1], array[j]);
                    }
                }


                //Array.Sort(array, (x, y) => y.TotalScore.CompareTo(x.TotalScore));
            }

            public void Print()
            {
                Console.WriteLine($"мя: {Name}, фамилия: {Surname}, балл: {TotalScore}");
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

                Participant[] la = new Participant[_participants.Length + 1];

                for (int i = 0; i < _participants.Length; i++)
                {
                    la[i] = _participants[i];
                }

                la[_participants.Length] = participant;
                _participants = la;
            }

            public void Add(params Participant[] participants)
            {
                if (participants == null || participants.Length == 0 || _participants == null) return;
                foreach (Participant participant in participants)
                {
                    Add(participant);
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
                    if (this.Participants.Length < 3) return null;
                    if (this.Participants == null) return null;


                    double[] prizes = new double[3];
                    prizes[0] = this.Bank * 0.5; // 50% на 1 место
                    prizes[1] = this.Bank * 0.3; // 30% на 2 место
                    prizes[2] = this.Bank * 0.2; // 20% на 3 место
                    return prizes;
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
                    if (this.Participants.Length < 3) return null;
                    if (this.Participants == null) return null;
                    

                    int count = this.Participants.Length / 2;

                    if(count > 10)
                    {
                        count = 10;
                    }
                    double N = 20.0 / count;
                    double[] prizes = new double[count];


                    for(int i = 0;i < count;i++)
                    {
                        prizes[i] = this.Bank * N / 100;
                    }
                    prizes[0] += this.Bank * 0.4; // 40% на 1 место
                    prizes[1] += this.Bank * 0.25; // 25% на 2 место
                    prizes[2] += this.Bank * 0.15; // 15% на 3 место

                    return prizes;
                }
            }
        }
    }
}
