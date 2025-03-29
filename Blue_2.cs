using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    public class Blue_2
    {

        public struct Participant
        {
            // поля
            private string _name;
            private string _surname;
            private int[,] _marks;
            private int _cnt;

            // свойства
            public string Name => _name;
            public string Surname => _surname;
            public int[,] Marks
            {
                get
                {
                    if (_marks == null) return null;
                    int[,] copy = new int[_marks.GetLength(0), _marks.GetLength(1)];
                    for (int i = 0; i < _marks.GetLength(0); i++)
                        for (int j = 0; j < _marks.GetLength(1); j++)
                            copy[i, j] = _marks[i, j];
                    return copy;
                }
            }
            // свойство 
            public int TotalScore
            {
                get
                {

                    if (_marks == null) return 0;
                    int sum = 0;
                    for (int i = 0; i < _marks.GetLength(0); i++)
                        for (int j = 0; j < _marks.GetLength(1); j++)
                            sum += _marks[i, j];
                    return sum;
                }
            }
            // конструктор 
            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new int[2, 5];
                _cnt = 0;
            }

            // метод
            public void Jump(int[] result)
            {
                if (result == null || _marks == null || result.Length == 0) return;

                if (_cnt != 0 && _cnt != 1) return;
                for (int j = 0; j < _marks.GetLength(1); j++)
                    _marks[_cnt, j] = result[j];

                _cnt++;

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
             
                for (int i = 0; i < _marks.GetLength(0); i++)
                {
                    for (int j = 0; j < _marks.GetLength(1); j++)          
                        Console.WriteLine(_marks[i, j]);
                    
                    Console.WriteLine();
                }
                Console.WriteLine(TotalScore);
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

            public void Add(Participant participants)
            {
                if (_participants == null) return;
                Array.Resize(ref _participants, _participants.Length + 1);
                _participants[_participants.Length - 1] = participants;

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
                    if (this.Participants == null || this.Participants.Length < 3)
                        return null;

                    double[] money = new double[3];
                    money[0] = 0.5 * this.Bank;
                    money[1] = 0.3 * this.Bank;
                    money[2] = 0.2 * this.Bank;

                    return money;
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
                    if (this.Participants == null || this.Participants.Length < 3)
                        return null;

                    int mid = this.Participants.Length / 2;
                    double[] money = new double[Math.Min(mid, 10)];
                    double N = 20.0 / Math.Min(mid, 10);
                    money[0] += 0.4 * this.Bank;
                    money[1] += 0.25 * this.Bank;
                    money[2] += 0.15 * this.Bank;
                    for (int i = 0; i < money.Length; i++)
                    {
                        money[i] += N * this.Bank / 100;
                    }
                    return money;
                }

            }
        }


    }
}

