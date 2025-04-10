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
            // поля
            private string _name;
            private string _surname;
            private int[,] _marks;
            private int _jumpcnt;

            // свойства
            public string Name { get { return _name; } }
            public string Surname { get { return _surname; } }
            public int[,] Marks
            {
                get
                {
                    if (_marks == null || _marks.GetLength(0) == 0 || _marks.GetLength(1) == 0) return null;

                    int[,] copy = new int[_marks.GetLength(0), _marks.GetLength(1)];
                    for (int i = 0; i < _marks.GetLength(0); i++)
                        for (int j = 0; j < _marks.GetLength(1); j++)
                            copy[i, j] = _marks[i, j];

                    return copy;
                }
            }
            public int TotalScore
            {
                get
                {
                    if (_marks == null || _marks.GetLength(0) == 0 || _marks.GetLength(1) == 0) return 0;

                    int total = 0;
                    foreach (var mark in _marks)
                        total += mark;

                    return total;
                }
            }

            // конструктор
            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new int[2, 5];
                _jumpcnt = 0;
            }

            // методы
            public void Jump(int[] result)
            {
                if (result == null || result.Length == 0 || _marks == null || _marks.GetLength(0) == 0 || _marks.GetLength(1) == 0 || _jumpcnt >= 2) return;

                for (int j = 0; j < 5; j++)
                {
                    _marks[_jumpcnt, j] = result[j];
                }
                _jumpcnt++;
            }

            // сортировка по убыванию суммарного результата спортсмена
            public static void Sort(Participant[] array)
            {
                if (array == null || array.Length == 0) return;

                for (int i = 0; i < array.Length - 1; i++)
                    for (int j = 0; j < array.Length - i - 1; j++)
                        if (array[j].TotalScore < array[j + 1].TotalScore)
                        {
                            Participant temp = array[j + 1];
                            array[j + 1] = array[j];
                            array[j] = temp;
                        }

            }

            public void Print()
            {
                Console.WriteLine($"{_name} {_surname}: ");
                for (int i = 0; i < _marks.GetLength(0); i++)
                {
                    for (int j = 0; j < _marks.GetLength(1); j++)
                        Console.Write(_marks[i, j] + " ");
                    Console.WriteLine();
                    
                    
                }
                Console.WriteLine(TotalScore);
                Console.WriteLine();
            }

        } // end of struct Participant

        abstract public class WaterJump
        {
            private string _name;
            private int _bank;
            private Participant[] _participants;

            public string Name { get { return _name; } }
            public int Bank { get { return _bank; } }
            public Participant[] Participants
            {
                get
                {
                    Participant[] temp = new Participant[_participants.Length];
                    Array.Copy(_participants, temp, _participants.Length);

                    return temp;
                }
            }
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
                for (int i = 0; i < _participants.Length; i++) 
                    array[i] = _participants[i];

                array[_participants.Length] = participant;

                //Participant[] array = new Participant[_participants.Length + 1];
                //Array.Copy(_participants, array, _participants.Length);
                //array[array.Length - 1] = participant;

                _participants = array;
            }

            public void Add(Participant[] participants)
            {
                if (_participants == null || participants == null || participants.Length == 0) return;

                foreach (Participant participant in participants)
                {
                    Add(participant);
                }
            }
        } // end of abstract public class WaterJump

        public class WaterJump3m : WaterJump
        {
            public WaterJump3m(string name, int bank) : base(name, bank) { }

            public override double[] Prize
            {
                get
                {
                    if (Participants == null || Participants.Length < 3) return null;

                    double[] prizes = new double[3];

                    double bank = (double)Bank;
                    prizes[0] = bank * 0.5;
                    prizes[1] = bank * 0.3;
                    prizes[2] = bank * 0.2;

                    return prizes;
                }
            }

        }// end of class WaterJump3m

        public class WaterJump5m : WaterJump
        {
            public WaterJump5m(string name, int bank) : base(name, bank) {}

            public override double[] Prize
            {
                get
                {
                    if (Participants == null || Participants.Length < 3) return null;

                    int aboveMid = Participants.Length / 2;

                    int len;
                    if (aboveMid > 10)
                        len = 10;
                    else
                        len = aboveMid;

                    double N = 20.0 / (double)len;

                    double bank = (double)Bank;
                    double[] prizes = new double[len];

                    for (int i = 0; i < len; i++)
                    {
                        prizes[i] = N / 100 * bank;
                    }
                    prizes[0] += 0.4 * bank;
                    prizes[1] += 0.25 * bank;
                    prizes[2] += 0.15 * bank;

                    return prizes;
                }
            }
        } // end of class WaterJump5m

    }

}
