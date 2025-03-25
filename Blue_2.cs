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
            //свойства
            public string Name => _name;
            public string Surname => _surname;

            public int[,] Marks
            {
                get
                {
                    if (_marks == null) return default(int[,]);
                    int[,] newMarks = new int[2, 5];
                    for (int i = 0; i < newMarks.GetLength(0); i++)
                    {
                        for (int j = 0; j < newMarks.GetLength(1); j++)
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
                    int count = 0;
                    for (int i = 0; i < _marks.GetLength(0); i++)
                    {
                        for (int j = 0; j < _marks.GetLength(1); j++)
                        {
                            count += _marks[i, j];
                        }
                    }
                    return count;
                }
            }
            //конструктор
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
                for (int j = 0; j < 5; j++)
                {
                    _marks[_count, j] = result[j];
                }
                _count++;
            }
            public static void Sort(Participant[] array)
            {
                if (array == null) return;
                for (int i = 0; i < array.Length; i++)
                {
                    for (int j = 0; j < array.Length - i - 1; j++)
                    {
                        if (array[j].TotalScore < array[j + 1].TotalScore)
                        {
                            var temp = array[j];
                            array[j] = array[j + 1];
                            array[j + 1] = temp;
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
        //abstract
        public abstract class WaterJump
        {
            private string _name;
            private int _bank;
            private Participant[] _participants;
            public string Name => _name;
            public int Bank => _bank;
            public Participant[] Participants => _participants;
            public abstract double[] Prize {  get; } //служит "шаблоном" для производных классов, которые обязаны предоставить свою реализацию этого свойства, поэтому ничего тут не прописываем
            public WaterJump(string name, int bank)
            {
                _name = name;
                _bank = bank;
                _participants = new Participant[0];
            }
            public void Add(Participant participant)
            {
                if (_participants == null) return;
                Participant[] New = new Participant[_participants.Length + 1];
                Array.Copy(_participants, New, _participants.Length);
                New[_participants.Length] = participant;
                _participants = New;
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
            public override double[]Prize
            {
                get
                {
                    if (Participants == null || Participants.Length < 3) return default(double[]);
                    int count = Participants.Length;
                    int halfCount = count / 2;
                    double[] prize = new double[Math.Min(halfCount, 10)];
                    double N = 20.0 / Math.Min(halfCount, 10);
                    for (int i = 0; i < prize.Length; i++)
                    {
                        prize[i] += 0.01 * N * Bank;
                    }
                    prize[0] += 0.4 * Bank;
                    prize[1] += 0.25 * Bank;
                    prize[2] += 0.15 * Bank;
                    return prize;
                }
            }
        }
    }
}
