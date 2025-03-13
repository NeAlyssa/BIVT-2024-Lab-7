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
            //поля
            private string _name;
            private string _surname;
            private int[,] _marks; 
            private bool[] _isJumpMarked; 

            //свойства
            public string Name => _name;
            public string Surname => _surname;
            public int[,] Marks 
            {
                get
                {
                    if (_marks == null) return null;

                    int[,] copy = new int[2, 5];
                    for (int i = 0; i < 2; i++)
                    {
                        for (int j = 0; j < 5; j++)
                            copy[i, j] = _marks[i, j];
                    }
                    return copy;
                }
            }
            public int TotalScore 
            {
                get
                {
                    if (_marks == null) return 0;

                    int totalScore = 0;
                    for (int i = 0; i < 2; i++)
                    {
                        for (int j = 0; j < 5; j++)
                            totalScore += _marks[i, j];
                    }
                    //Console.WriteLine($"{_name} {_surname}'s total score is {totalScore}");
                    return totalScore;
                }
            }

            //конструкторы
            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new int[2, 5]; 
                _isJumpMarked = new bool[2];
            }

            //методы
            public void Jump(int[] result)
            {
                if (result == null) return;
                if (_marks == null || _isJumpMarked == null) return;

                int jumpNum = 0;
                while (_isJumpMarked[jumpNum] && jumpNum < _isJumpMarked.Length) // поиск неоценённого прыжка
                    jumpNum++;
                if (jumpNum >= _isJumpMarked.Length) // все прыжки оценены
                    return;

                for (int k = 0; k < 5; k++)
                    _marks[jumpNum, k] = result[k];
                _isJumpMarked[jumpNum] = true;
            }
            public static void Sort(Participant[] array)
            {
                if (array == null || array.Length == 0) return;

                for (int i = 0; i < array.Length - 1; i++)
                {
                    for (int j = 0; j < array.Length - i - 1; j++)
                    {
                        if (array[j + 1].TotalScore > array[j].TotalScore)
                        {
                            Participant tmp = array[j + 1];
                            array[j + 1] = array[j];
                            array[j] = tmp;
                        }
                    }
                }
            }
            public void Print()
            {
                Console.WriteLine($"{_name} {_surname}'s marks:");
                Console.Write("             First Jump   Second Jump   Total Score");
                for (int i = 0; i < _marks.GetLength(1); i++) 
                {
                    Console.WriteLine();
                    Console.Write($"judge №{i + 1} ");
                    for (int j = 0; j < _marks.GetLength(0); j++) 
                    {
                        Console.Write($"{_marks[j, i],12}"); 
                    }
                }
                int totalScore = TotalScore;
                Console.WriteLine($"{TotalScore,12}");
            }
        }//struct Participant

        public abstract class WaterJump
        {
            private string _name;
            private int _bank;
            private Participant[] _participants;

            public string Name => _name;
            public int Bank => _bank;
            public Participant[] Participants => _participants;
            public abstract double[] Prize { get; }
            //возвращает суммы для участников, занявших призовые места (вещественные числа) в порядке занятых мест

            public WaterJump(string name, int bank)
            {
                _name = name;
                _bank = bank;
                _participants = new Participant[0];
            }

            private void IncreaseCapasity(int newItemsNum) // увеличивает кол-во участников на newItemsNum
            {
                if (_participants == null || newItemsNum < 1) return;

                Participant[] tmp = new Participant[_participants.Length + newItemsNum];
                int k = 0;
                foreach (Participant participant in _participants)
                    tmp[k++] = participant;
                _participants = tmp;
            }
            public void Add(Participant participant)
            {
                if (_participants == null) return;

                IncreaseCapasity(1);
                _participants[_participants.Length - 1] = participant;
            }
            public void Add(Participant[] participants)
            {
                if (_participants == null || participants == null || participants.Length == 0) return;

                int oldLenth = _participants.Length;
                IncreaseCapasity(participants.Length);
                foreach(Participant participant in participants)
                {
                    _participants[oldLenth] = participant;
                    oldLenth++;
                }
            }

            internal void Print()
            {
                Console.WriteLine($"{_name}'s bank = {_bank}");
                double[] prizes = this.Prize;
                if (prizes == null)
                {
                    Console.WriteLine("no prizes: incomplete membership\n");
                    return;
                }
                Console.WriteLine("prizes:");
                int k = 1;
                foreach(double prize in prizes)
                    Console.WriteLine($"{k++, 2} plase - {prize}");
                Console.WriteLine();
            }
        } //class WaterJump

        public class WaterJump3m : WaterJump
        {
            public WaterJump3m(string _name, int bank) : base(_name, bank) { }

            public override double[] Prize
            {
                get
                {
                    if (this.Participants == null || this.Participants.Length < 3) return null;

                    double[] prizes = new double[3];
                    //double remainder = (double)this.Bank;
                    //remainder -= prizes[0]; 
                    //prizes[1] = 0.6 * remainder;
                    //remainder -= prizes[1];
                    //prizes[2] = remainder;
                    prizes[0] = 0.5 * (double)this.Bank;
                    prizes[1] = 0.3 * (double)this.Bank;
                    prizes[2] = 0.2 * (double)this.Bank;
                    return prizes;
                }
            }
        } // class WaterJump3m : WaterJump

        public class WaterJump5m : WaterJump
        {
            public WaterJump5m(string _name, int bank) : base(_name, bank) { }

            public override double[] Prize
            {
                get
                {
                    if (this.Participants == null || this.Participants.Length < 3) return null;

                    int numAboveTheMiddle = this.Participants.Length / 2;
                    int prizesNum;
                    if (numAboveTheMiddle > 10)
                        prizesNum = 10;
                    else
                        prizesNum= numAboveTheMiddle;
                    double[] prizes = new double[prizesNum];
                    double N = 20 / prizesNum;  
                    double degN = N / 100;

                    double reminder = (double)this.Bank;
                    for (int k = 0; k < prizesNum; k++)
                    {
                        prizes[k] = Math.Round(degN * (double)this.Bank, 5);
                        //reminder -= prizes[k];
                        //reminder = Math.Round(reminder, 5);
                    }
                    //prizes[0] += 0.5 * reminder;
                    //prizes[0] = Math.Round(prizes[0], 5);
                    //reminder -= 0.5 * reminder;
                    //reminder = Math.Round(reminder, 5);
                    //prizes[1] += 0.625 * reminder;
                    //prizes[1] = Math.Round(prizes[1], 5);
                    //reminder -= 0.625 * reminder;
                    //reminder = Math.Round(reminder, 5);
                    //prizes[2] += reminder;
                    //prizes[2] = Math.Round(prizes[2], 5);
                    prizes[0] += 0.4 * (double)this.Bank;
                    //prizes[0] = Math.Round(prizes[0], 5);
                    prizes[1] += 0.25 * (double)this.Bank;
                    //prizes[1] = Math.Round(prizes[1], 5);
                    prizes[2] += 0.15 * (double)this.Bank;
                    //prizes[2] = Math.Round(prizes[2], 5);
                    return prizes;
                }
            }
        } // class WaterJump5m : WaterJump
    }
}
