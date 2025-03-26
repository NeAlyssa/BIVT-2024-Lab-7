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
            }

            // метод
            public void Jump(int[] result)
            {
                if (result == null || _marks == null) return;
                int count1 = 0;
                int count2 = 0;
                for (int j = 0; j < 5; j++)
                {
                    count1 += _marks[0, j];
                    count2 += _marks[1, j];
                }
                if (count1 == 0)
                {
                    for (int j = 0; j < 5; j++)
                        _marks[0, j] = result[j];
                }
                else if (count2 == 0)
                {
                    for (int j = 0; j < 5; j++)
                        _marks[1, j] = result[j];
                }

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
                _participants = null;
              
            }

            public void Add(Participant participants)
            {
                if (_participants == null) return;
                Participant[] newParticipant = new Participant[_participants.Length + 1];
                Array.Copy(_participants, newParticipant, _participants.Length);
                newParticipant[newParticipant.Length - 1] = participants;
                _participants = newParticipant;

            }


            public void Add(Participant[] participants)
            {
                if (participants == null) return;
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
                    money[0] = 0.5 * (double)this.Bank;
                    money[1] = 0.3 * (double)this.Bank;
                    money[2] = 0.2 * (double)this.Bank;

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
                    if (this.Participants == null || this.Participants.Length < 3) return null;

                    int middle = this.Participants.Length / 2;
                    int n;
                    if (middle > 10) n = 10;                   
                    else n = middle;
                    
                    double N = 20.0 / (double)n;
                    double[] money = new double[n];
                    for (int i = 3; i < middle - 1; i++)
                    {
                        money[i] = Math.Round((double)this.Bank * (N / 100), 5);
                    }

                    money[0] += (double)(0.4 * this.Bank);
                    money[1] += (double)(0.25 * this.Bank);
                    money[2] += (double)(0.15 * this.Bank);

                    return money;

                }

            }
        }


    }
}

