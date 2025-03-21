using System;
using System.Collections.Generic;
using System.Linq;
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
                    if (_marks == null || _marks.Length == 0) return null;
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
                if (result == null || _marks == null || result.Length < 5) return;
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
                Console.WriteLine($"Name: {_name}, Surname: {_surname}, Total score: {TotalScore}");
            }
        }

        public abstract class WaterJump
        {
            private string _name;
            private int _bank;
            private Participant[] _participants;
            private int currentIndex;


            public string Name => _name;
            public int Bank => _bank;
            public Participant[] Participants => _participants;
            

            public abstract double[] Prize { get; }


            public WaterJump(string name, int bank)
            {
                _name = name;
                _bank = bank;
                _participants = new Participant[0];
                currentIndex = 0;
            }
        
            public void Add(Participant participants)
            {
                if (_participants == null || currentIndex >= _participants.Length) return;
                _participants[currentIndex++] = participants;
            }


            public void Add(Participant[] participants)
            {
                if (participants == null) return;
                foreach (Participant p in participants)
                {
                    Add(p);
                }
            } 


            public void Print()
            {
                Console.WriteLine($"{_name}: bank = {_bank}");

                double[] money = this.Prize;
                if (money == null)
                {
                    Console.WriteLine("no money");
                    return;
                }
                int k = 1;
                foreach (double p in money)
                    Console.WriteLine($"{k++, 2} plase - {p}");
            }
        }


        public class WaterJump3m : WaterJump
        {
            public WaterJump3m(string _name, int bank) : base(_name, bank) { }

            public override double[] Prize
            {
                get
                {
                    double[] money = new double[3];

                    money[0] = (double)(0.5 * this.Bank);
                    money[1] = (double)(0.3 * this.Bank);
                    money[2] = (double)(0.2 * this.Bank);

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
                    if (this.Participants == null || this.Participants.Length < 3) return null;

                    int middle = this.Participants.Length / 2;
                    int numParticipants = 0;
                    if (middle <= 10) numParticipants = middle;
                    else if (middle > 10) numParticipants = 10;

                    double N = 20 / numParticipants;
                    double[] money = new double[numParticipants]; 
                    if (this.Participants.Length > 3)
                    {
                        for (int i = 0; i < numParticipants; i++)
                        {
                            money[i] = (double)(N / 100 * this.Bank);
                        }
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

