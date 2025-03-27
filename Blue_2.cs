using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    public class Blue_2
    {
        //структура
        public struct Participant
        {
            //приватные поля
            private string _name;
            private string _surname;
            private int[,] _marks;
            private int _counter;

            //свойства только для чтения
            public string Name => _name;
            public string Surname => _surname;
            public int[,] Marks
            {
                get
                {
                    if (_marks == null) return null;
                    int[,] New = new int[2, 5];
                    for (int i = 0; i < New.GetLength(0); i++)
                    {
                        for (int j = 0; j < New.GetLength(1); j++)
                        {
                            New[i, j] = _marks[i, j];
                        }
                    }
                    return New;
                }
            }

            public int TotalScore
            {
                get
                {
                    if (_marks == null) return 0;
                    int score = 0;
                    for (int i = 0; i < _marks.GetLength(0); i++)
                    {
                        for (int j = 0; j < _marks.GetLength(1); j++)
                        {
                            score += _marks[i, j];
                        }
                    }
                    return score;
                }
            }

            //конструктор (метод заполняющий поля)
            public Participant(string Name, string Surname)
            {
                _name = Name;
                _surname = Surname;
                _marks = new int[2, 5];
                _counter = 0;
            }

            //метод
            public void Jump(int[] result)
            {
                if (result == null || _marks == null) return;
                for (int j = 0; j < 5; j++)
                {
                    _marks[_counter, j] = result[j];
                }
                _counter++;
            }

            public static void Sort(Participant[] array)
            {
                if (array == null) return;
                for (int i = 1, j = 2; i < array.Length;)
                {
                    if (i == 0 || array[i].TotalScore <= array[i - 1].TotalScore)
                    {
                        i = j;
                        j++;
                    }
                    else
                    {
                        Participant a = array[i];
                        array[i] = array[i - 1];
                        array[i - 1] = a;
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
            //приватные поля
            private string _name;
            private int _bank;
            private Participant[] _participants;
            
            //свойства только для чтения
            public string Name => _name;
            public int Bank => _bank;
            public Participant[] Participants => _participants;
            public abstract double[] Prize
            {
                get;
            }

            //конструктор
            public WaterJump(string Name, int Bank)
            {
                _name = Name;
                _bank = Bank;
                _participants = new Participant[0];
            }

            //методы
            //добавляем участника
            public void Add(Participant participant)
            {
                if (_participants == null) return;
                Participant[] New = new Participant[_participants.Length + 1];
                for (int i = 0; i < _participants.Length; i++)
                {
                    New[i] = _participants[i];
                }
                New[_participants.Length] = participant;
                _participants = New;
            }

            //добавляем участников
            public void Add(Participant[] participants)
            {
                if (participants == null || participants.Length == 0|| _participants==null) return;
                foreach (var participant in participants)
                {
                    Add(participant);
                }

            }
        }
        //классы наследники
        public class WaterJump3m : WaterJump
        {
            public WaterJump3m(string Name, int Bank) : base(Name, Bank) { }
            public override double[] Prize
            {
                get
                {
                    if (this.Participants == null || this.Participants.Length < 3)
                    {
                        return null;
                    }
                    // Распределение призов
                    double[] p = new double[3] { Bank * 0.5, Bank * 0.3, Bank * 0.2 };
                    return p;
                }
            }
        }

        public class WaterJump5m : WaterJump
        {
            public WaterJump5m(string Name, int Bank) : base(Name, Bank) { }
            public override double[] Prize
            {
                get
                {
                    if (Participants == null || Participants.Length < 3)
                    {
                        return null;
                    }
                    double[] p = new double[] { Bank * 0.4, Bank * 0.25, Bank * 0.15 };

                    int count = Participants.Length / 2;
                    if (count < 3) count = 3;
                    if (count > 10) count = 10;
                    //ищем остаток
                    double left = Bank * 0.20;
                    double ToEach = left / count;
                    double[] New = new double[count];
                    for (int i = 0; i < p.Length; i++)
                    {
                        New[i] = p[i];
                    }
                    for (int i = 0; i < count; i++)
                    {
                        New[i] += ToEach;
                    }
                    return New;
                }
            }
        }
    }
}
