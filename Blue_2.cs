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
            //polya
            private string _name; //private для полей, которые должны быть скрыты от внешнего доступа (инкапсуляция)
            private string _surname;
            private int[,] _marks;
            private int _cnt;

            //svoystva
            public string Name => _name;
            public string Surname => _surname;
            public int[,] Marks //копия массива оценок каждого спортсмена для чтения
            {
                get //для чтения значения свойства
                {
                    if (_marks == null) return null;
                    int[,] copyMarks = new int[2, 5];
                    for (int i = 0; i < 2; i++)
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            copyMarks[i, j] = _marks[i, j];
                        }
                    }
                    return copyMarks;
                }
            }
            public int TotalScore //сумма всех оценок спортсмена
            {
                get
                {
                    if (_marks == null) return 0;
                    int s = 0;
                    for (int i = 0; i < 2; i++)
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            s += _marks[i, j];
                        }
                    }
                    return s;
                }
            }
            //construct
            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new int[2, 5]; //инициализация нулями
                _cnt = 0;
            }
            //method
            public void Jump(int[] result) //заполняет результат прыжка оценками, массив - прыжок
            {
                if (result == null || _marks == null) return;
                if (_cnt == 0)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        _marks[0, j] = result[j];
                    }
                    _cnt++;
                }
                else if (_cnt == 1)
                {
                    for (int j=0;j<5; j++)
                    {
                        _marks[1,j] = result[j];
                    }
                    _cnt++;
                }
            }

            public static void Sort(Participant[] array) //пузырьком <3 суммарного результата спортсмена по местам
            {
                if (array == null || array.Length == 0) return;
                for (int i = 0; i < array.Length -1; i++)
                {
                    for (int j = 0; j < array.Length - i - 1; j++)
                    {
                        if (array[j].TotalScore < array[j + 1].TotalScore)
                        {
                            Participant temp = array[j];
                            array[j] = array[j + 1];
                            array[j + 1] = temp;
                        }
                    }
                }
            }
            //vyvod

            public void Print()
            {
                Console.WriteLine(_name + " " + _surname);
                for (int i = 0; i < _marks.GetLength(0); i++)
                {
                    for (int j = 0; j < _marks.GetLength(1); j++)
                    {
                        Console.WriteLine(_marks[i, j]);
                    }
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
            private int _cnt;


            public string Name => _name;
            public int Bank => _bank;
            public Participant[] Participants => _participants;

            public abstract double[] Prize { get; }

            public WaterJump(string name, int bank)
            {
                _name = name;
                _bank = bank;
                _participants = new Participant[0];
                _cnt = 0;
            }

            public void Add(Participant participant) //добавление объекта типа Тим в массив тимс
            {
                if (_participants == null) return;

                Participant[] newArr=new Participant[_participants.Length+1];
                Array.Copy(_participants, newArr, _participants.Length);
                newArr[newArr.Length-1] = participant;
                _participants = newArr;
            }
            public void Add(Participant[] participants) //добавление массива объектов типа Тим в массив тимс
            {
                if (_participants == null || participants == null || participants.Length == 0) return;
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
                    if (this.Participants.Length < 3 || this.Participants == null) return null;
                    double[] prizes = new double[3];
                    prizes[0] = (double)this.Bank * 0.5;
                    prizes[1] = (double)this.Bank * 0.3;
                    prizes[2] = (double)this.Bank * 0.2;
                    return prizes;
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
                    if (this.Participants.Length < 3 || this.Participants == null) return null;
                    int mid = this.Participants.Length / 2;
                    var topParticipants = new double[mid];
                    Array.Copy(this.Participants, mid, topParticipants, 0, mid);
                    double N = 20.0 / Math.Min(topParticipants.Length, 10);
                    double[] prizes = new double[mid];
                    for (int i = 3; i < mid - 1; i++)
                    {
                        prizes[i] += (double)Bank * (N / 100);
                    }
                    prizes[0] = (double)Bank * 0.4;
                    prizes[1] = (double)Bank * 0.25;
                    prizes[2] = (double)Bank * 0.15;
                    return prizes;
                }
            }
        }
    }
}
