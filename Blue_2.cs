using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Lab_7.Blue_2;
using static Lab_7.Blue_5;

namespace Lab_7
{
    public class Blue_2
    {
        public struct Participant
        {
            private string _name;
            private string _surname;
            private int[,] _marks;
            private int _index;

            public string Name => _name;
            public string Surname => _surname;
            public int[,] Marks
            {
                get
                {
                    if (_marks == null || _marks.GetLength(0) == 0 || _marks.GetLength(1) == 0) return null;
                    int rows = _marks.GetLength(0);
                    int cols = _marks.GetLength(1);
                    int[,] copy = new int[rows, cols];
                    for (int i = 0; i < rows; i++)
                    {
                        for (int j = 0; j < cols; j++)
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
                    if (_marks == null || _marks.GetLength(0) == 0 || _marks.GetLength(1) == 0) return 0;
                    int sum = 0;
                    for (int i = 0; i < _marks.GetLength(0); i++)
                    {
                        for (int j = 0; j < _marks.GetLength(1); j++)
                        {
                            sum += _marks[i, j];
                        }
                    }
                    return sum;

                }
            }

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new int[2, 5];
                _index = 0;
            }

            public void Jump(int[] result)
            {
                if (_marks == null || _marks.GetLength(0) == 0 || _marks.GetLength(1) == 0 || result == null || result.Length == 0 || _index > 1) return;
                if (_index == 0)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        _marks[0, i] = result[i];
                    }
                    _index++;
                }
                else if (_index == 1)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        _marks[1, i] = result[i];
                    }
                    _index++;
                }
                //                for (int i = 0; i < result.Length; i++)
                //{
                //_marks[_index, i] = result[i];
                //}
                //_index++;
            }
            public static void Sort(Participant[] array)
            {
                if (array == null || array.Length == 0) return;
                for (int i = 1, k = 2; i < array.Length;)
                {
                    if (i == 0 || array[i - 1].TotalScore >= array[i].TotalScore)
                    {
                        i = k;
                        k++;
                    }
                    else
                    {
                        Participant t = array[i];
                        array[i] = array[i - 1];
                        array[i - 1] = t;
                        i--;
                    }
                }
                //if (array == null || array.Length == 0) return;
                //for (int i = 0; i < array.Length-1; i++)
                //{
                //    for (int j = 0; j < array.Length - i - 1; j++)
                //    {
                //        if (array[j].TotalScore < array[j + 1].TotalScore)
                //        {
                //            (array[j], array[j + 1]) = (array[j + 1], array[j]);
                //        }
                //    }
                //}
            }
            public void Print(Participant participant)
            {
                Console.WriteLine($"Name: {participant.Name}, Surname: {participant.Surname}, TotalScore: {participant.TotalScore}");
                for (int i = 0; i < participant.Marks.GetLength(1); i++) Console.Write($"{participant.Marks[0, i]} ");
                Console.WriteLine();
                Console.Write("Second Jump: ");
                for (int i = 0; i < participant.Marks.GetLength(1); i++) Console.Write($"{participant.Marks[1, i]} ");
                Console.WriteLine();
            }
        }

        public abstract class WaterJump
        {
            private string _name;
            private int _bank;
            private Participant[] _participants;
            private int _sportsmenind;

            public string Name => _name;
            public int Bank => _bank;
            public Participant[] Participants => _participants;

            public abstract double[] Prize { get; }

            public WaterJump(string name, int bank)
            {
                _name = name;
                _bank = bank;
                _participants = new Participant[0];
                _sportsmenind = 0;
            }

            public void Add(Participant part)
            {
                if (_participants == null ) return;
                //Participant[] temp = new Participant[_participants.Length + 1];
                //for (int i = 0; i < _participants.length; i++)
                //{
                //    temp[i] = _participants[i];
                //}
                //temp[_participants.length] = part;
                if ( _sportsmenind< _participants.Length)
                {
                    _participants[_sportsmenind] = part;
                    _sportsmenind++;

                }


            }

            public void Add(Participant[] part)
            {
                if (_participants == null|| part == null || part.Length==0 ) return;
                //int ind = 0;
                //Participant[] temp = new Participant[_participants.Length + 1];
                //for (int i = 0; i < _participants.Length; i++)
                //{
                //    temp[i] = _participants[i];
                //    ind++;
                //}
                //foreach (Participant p in part)
                //{
                //    temp[ind + 1] = p;
                //    ind++;
                //}
                //_participants = temp;
                foreach (Participant p in _participants)
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
                    if (this.Participants == null || this.Participants.Length < 3) return null;
                    double[] sum = new double[3];
                    sum[0] = 0.5 * this.Bank;
                    sum[1] = 0.3 * this.Bank;
                    sum[2] = 0.2 * this.Bank;
                    return sum;
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
                    if (this.Participants == null || this.Participants.Length < 3) return null;
                    
                    int m= this.Participants.Length/2;
                    if (m > 10) m = 10;
                    double[] sum = new double[m];
                    for(int i = 0; i < m; i++)
                    {
                        sum[i] = this.Bank * 0.2;
                    }
                    sum[0]= 0.4 * this.Bank;
                    sum[1]= 0.25* this.Bank;
                    sum[2]= 0.15*this.Bank;

                    return sum;
                }

            }

        }

    }
}
