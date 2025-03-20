using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

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
            public string Name { get { return _name; } }
            public string Surname { get { return _surname; } }
            public int[,] Marks
            {
                get
                {
                    if (_marks == null) return null;
                    int[,] copy = new int[2, 5];
                    for (int i = 0; i < 2; i++)
                    {
                        for (int j = 0; j < 5; j++)
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
                    int count = 0;
                    for (int i = 0; i < 2; i++)
                    {
                        for (int j = 0; j < 5; j++)
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
            //методы
            public void Jump(int[] result)
            {
                if (result == null || _marks == null || result.Length == 0) return;
                if (_count != 0 || _count != 1) return;
                for (int j = 0; j < _marks.GetLength(1); j++)
                {
                    _marks[_count, j] = result[j];
                }
                _count++;
            }
            public static void Sort(Participant[] array)
            {
                if (array == null || array.Length == 0) return;
                for (int i = 1, j = 2; i < array.Length;)
                {
                    if (i == 0 || array[i - 1].TotalScore >= array[i].TotalScore)
                    {
                        i = j;
                        j++;
                    }
                    else
                    {
                        Participant temp = array[i];
                        array[i] = array[i - 1];
                        array[i - 1] = temp;
                        i--;
                    }
                }
            }
            public void Print()
            {
                Console.WriteLine($"{_name} {_surname} {TotalScore}");
            }
        }
        public abstract class WaterJump
        {
            //поля 
            private string _name;
            private int _bank;
            private Participant[] _participants;
            //свойства 
            public string Name => _name;
            public int Bank => _bank;
            public Participant[] Participants => _participants;
            public abstract double[] Prize { get; } //возвращает суммы для участников, занявших призовые места
            //конструктор 
            public WaterJump (string name, int bank)
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
                if (_participants == null || participants == null) return;
                int n = participants.Length;
                int m = _participants.Length;
                Array.Resize(ref _participants, m + n);
                for (int i = 0; i < n; i++)
                {
                    _participants[m + i] = participants[i];
                }
            }
        }
        public class WaterJump3m : WaterJump
        {
            //свойства 
            public WaterJump3m(string _name, int bank) : base(_name, bank) { }
            public override double[] Prize
            {
                get
                {
                    if (this.Participants == null || this.Participants.Length < 3) return null;
                    double[] prize = new double[3];
                    prize[0] = 0.5 * this.Bank;
                    prize[1] = 0.3 * this.Bank;
                    prize[2] = 0.2 * this.Bank;
                    return prize;
                }
            }

        }
        public class WaterJump5m : WaterJump
        {
            //свойства
            public WaterJump5m(string _name, int bank) : base(_name, bank) { }
            public override double[] Prize
            {
                get
                {
                    if (this.Participants == null || this.Participants.Length < 3) return null;
                    int n = this.Participants.Length;
                    if (n / 2 <= 10) 
                    { 
                        double[] prize = new double[n / 2];
                        int N = 20 / (n / 2);
                        prize[0] += 0.4 * this.Bank;
                        prize[1] += 0.25 * this.Bank;
                        prize[2] += 0.15 * this.Bank;
                        for (int i = 0; i < n / 2; i++)
                        {
                            prize[i] += N * this.Bank / 100;
                        }
                        return prize;
                    }
                    else 
                    { 
                        double[] prize = new double[10];
                        int N = 10;
                        prize[0] += 0.4 * this.Bank;
                        prize[1] += 0.25 * this.Bank;
                        prize[2] += 0.15 * this.Bank;
                        for (int i = 0; i < 10; i++)
                        {
                            prize[i] += N * this.Bank / 100;
                        }
                        return prize;
                    }
                }
            }
        }
    }
}
