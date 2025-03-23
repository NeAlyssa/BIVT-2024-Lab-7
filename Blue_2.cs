using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lab_7.Blue_1;
using static Lab_7.Blue_2;

namespace Lab_7
{
    public class Blue_2
    {
        public abstract class WaterJump
        {
            private string _name;
            private int _bank;
            private Participant[] _participants;

            public string Name => _name;
            public int Bank => _bank;
            public Participant[] Participants => _participants;

            //вопрос почему без ссылки 
            //public int[] Participants
            //{
            //    get
            //    {
            //        if (_participants == null) return default(int[]);
            //        var newArray = new int[_participants.Length];
            //        Array.Copy(_participants, newArray, newArray.Length);
            //        _participants = newArray;
            //        return _participants;
            //    }
            //}
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
                Participant[] newArray = new Participant[_participants.Length +1];
                Array.Copy(_participants, newArray, _participants.Length);
                newArray[_participants.Length] = participant;
                _participants = newArray;
            }
            public void Add(Participant[] participant)
            {
                if (_participants == null || participant == null) return;
                foreach(Participant player in participant)
                {
                    Add(player);
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
                    if (Participants.Length < 3 || Participants == null) return default(double[]);
                    double first = Bank * 0.5;
                    double second = Bank * 0.3;
                    double third = Bank * 0.2;
                    return new double[] { first, second, third };
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
                    if (Participants.Length < 3 || Participants == null) return default(double[]);
                    int count = 0;
                    if (Participants.Length / 2 <= 10) count = Participants.Length / 2;
                    else count = 10;
                    double[] aboveAvarage = new double[count];
                    double N = 20.0 / aboveAvarage.Length;
                    for (int i = 0; i < aboveAvarage.Length / 2; i++)
                    {
                        if (i == 0) aboveAvarage[i] = Bank * 0.4;
                        if (i == 1) aboveAvarage[i] = Bank * 0.25;
                        if (i == 2) aboveAvarage[i] = Bank * 0.15;
                        aboveAvarage[i] = Bank * (N / 100);
                    }
                    return aboveAvarage;
                }
            }
        }


        public struct Participant
        {
            private string _name;
            private string _surname;
            private int[,] _marks;

            public string Name => _name;
            public string Surname => _surname;
            public int[,] Marks
            {
                get
                {
                    if (_marks == null) return default(int[,]);
                    var newArray = new int[2, 5];
                    //заполнение матрицы из-под оценок
                    for (int i = 0; i < 2; i++)
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            newArray[i, j] = _marks[i, j];
                        }
                    }
                    return newArray;
                }
            }
            public int TotalScore
            {
                get
                {
                    if (_marks == null) return 0; //zero прягнул чел упал лицом в грязь
                    int totalScore = 0;
                    //2 прыжка по пять оценок судей
                    for (int i = 0; i < 2; i++)
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            totalScore += _marks[i, j];
                        }
                    }
                    return totalScore;
                }
            }

            //конструктор(тоже является методом)
            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new int[2, 5]; //не null тк из условия ограниченое число
            }
            public void Jump(int[] result) //заполнение оценками массива _marks
            {
                //true if its filled 
                bool isFirstJumpFilled = _marks[0, 0] != 0 || _marks[0, 1] != 0
                    || _marks[0, 2] != 0 || _marks[0, 3] != 0 || _marks[0, 4] != 0;
                //if not true we will fill
                if (!isFirstJumpFilled)
                {
                    for (int j = 0; j < 5; j++)
                        _marks[0, j] = result[j];
                }
                //заполнен ли второй прыжок
                else if (!(_marks[1, 0] != 0 || _marks[1, 1] != 0 || _marks[1, 2] != 0 || _marks[1, 3] != 0 || _marks[1, 4] != 0))
                {
                    for (int j = 0; j < 5; j++)
                        _marks[1, j] = result[j];
                }
            }
            public static void Sort(Participant[] array) //54321
            {
                if (array == null) return;
                for (int i = 0; i < array.Length; i++)
                {
                    for (int j = 1; j < array.Length - i; j++)
                    {
                        //method
                        if (array[j - 1].TotalScore < array[j].TotalScore)
                        {
                            var temp = array[j - 1];
                            array[j - 1] = array[j];
                            array[j] = temp;

                        }
                    }
                }
            }
            public void Print()
            {
                //Интерполированная строка
                Console.WriteLine($"{Name}\t{Surname}");
                for (int i = 0; i < _marks.GetLength(0); i++)
                {
                    for (int j = 0; j < _marks.GetLength(1); j++)
                    {
                        Console.Write(_marks[i, j] + " ");
                    }
                    Console.WriteLine();
                }
            }




        }
    }
}
