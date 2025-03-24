using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab_7
{
    public class Blue_2
    {
        public struct Participant
        {
            //поля
            private string _name;
            private string _surname;
            private int[,] _marks;
            private int _count;
            //публичные свойства
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
                if (result == null) return;
                if (_marks == null) return;
                for (int i = 0; i < 5; i++)
                {
                    _marks[_count, i] = result[i];
                }
                _count++;
            }
            public static void Sort(Participant[] array) // p1.Sort(array) Participant.Sort(array)
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
                Participant[] array = new Participant[_participants.Length + 1];
                Array.Copy(_participants, array, _participants.Length);
                array[_participants.Length] = participant;
                _participants = array;
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
                    if (Participants == null || Participants.Length < 3) return default(double[]);
                    double[] prise = new double[3] { 0.5 * Bank, 0.3 * Bank, 0.2 * Bank };
                    return prise;
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
                   
                    int n = Participants.Length, k;
                    k = n / 2;
                    double[] prize = new double[k];

                    if (k <= 10)
                    {
                        for (int i = 3; i < k; i++)
                            prize[i] = 0.01 * Bank * (20/k);
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