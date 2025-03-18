using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    public class Blue_2 
    {
        public struct Participant // структура
        {
            private string _name; // поле с именем
            private string _surname; // поле с фамилией
            private int[,] _marks; // oценки
            private bool[] _jumped; // массив для проверки

            // свойства
            public string Name => _name;
            public string Surname => _surname; // сокращение get
            public int[,] Marks
            {
                get
                {
                    if (_marks == null) return null;
                    int[,] copmarks = new int[2, 5];
                    for (int i = 0; i < 2; i++)
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            copmarks[i, j] = _marks[i, j];
                        }
                    }
                    return copmarks;
                }
            }
            public int TotalScore
            {
                get
                {
                    if (_marks == null) return 0;

                    int total = 0;
                    for (int i = 0; i < 2; i++)
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            total += _marks[i, j];
                        }
                    }
                    return total;
                }
            }

            // конструктор
            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new int[2, 5];
                _jumped = new bool[2]; // По два прыжка
            }

            // методы 
            public void Jump(int[] result) // заполняет результат очередного прыжка оценками судей
            {
                if (result == null) return;
                if (_marks == null) return;

                int nojump = Array.FindIndex(_jumped, jump => !jump); // находим первый неоценённый прыжок

                
                if (nojump== -1)
                {
                    return; // все прыжки уже оценены
                }
                for (int i = 0; i < 5; i++)
                {
                    _marks[nojump, i] = result[i];
                }

                _jumped[nojump] = true; // прыжок оценённый
            }

            public static void Sort(Participant[] array) // по убыванию суммарного результата спортсмена
            {
                if (array == null || array.Length == 0) return;
                // делаем сортировку

                for (int i = 0; i < array.Length - 1; i++)
                {
                    for (int j = 0; j < array.Length - i - 1; j++)
                    {
                        if (array[j + 1].TotalScore > array[j].TotalScore) // обращаемся к элементу массива и получаем значение этого элемента в свойвстве TotalScore
                        {
                            (array[j + 1], array[j]) = (array[j], array[j + 1]);
                        }
                    }
                }
            }

            // метод принт
            public void Print()
            {
                Console.WriteLine($"{Name} {Surname} балл: {TotalScore}");
            }
        }

        public abstract class WaterJump
        {
            private string _name;
            private int _bank;
            private Participant[] _participants;
            private int _participantCount;

            public string Name => _name;
            public int Bank => _bank;
            public Participant[] Participants => _participants;
            public abstract double[] Prize { get; }

            public WaterJump(string name, int bank)
            {
                _name = name;
                _bank = bank;
                _participants = new Participant[0];
                _participantCount = 0;
            }

            public void Add(Participant participant) // одна команда в группу
            {
                if (_participants == null) return;

                if (_participantCount < _participants.Length)
                {
                    _participants[_participantCount] = participant;
                    _participantCount++;
                }
            }
            public void Add(Participant[] participant) // несколько
            {
                if (_participants == null || _participants.Length == 0 || participant == null) return;

                foreach (var team in _participants)
                {
                    Add(team);
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
                    double[] prizes = new double[3];
                    if (Participants.Length < 3 || Participants == null) return prizes;

                    prizes[0] = (double)this.Bank * 0.5; // Первое место
                    prizes[1] = (double)this.Bank * 0.3; // Второе место
                    prizes[2] = (double)this.Bank * 0.2; // Третье место

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
                    if (Participants.Length < 3 || Participants == null) return null;

                    var top = new double[this.Participants.Length / 2];

                    Array.Copy(Participants, this.Participants.Length / 2, top, 0, this.Participants.Length / 2);

                    double N = 20.0 / Math.Min(top.Length, 10);
                    double[] prizes = new double[this.Participants.Length / 2];

                    for (int i = 3; i < this.Participants.Length / 2 - 1; i++)
                    {
                        prizes[i] += (double)this.Bank * (N / 100);
                    }

                    prizes[0] += (double)this.Bank * 0.4; // 40%
                    prizes[1] += (double)this.Bank * 0.25; // 25%
                    prizes[2] += (double)this.Bank * 0.15; // 15%

                    return prizes;
                }
            }
        }
    }
}
