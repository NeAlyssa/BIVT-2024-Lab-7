using System;
using System.Collections.Generic;
using System.Data;
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
                    if (_marks == null) return default(int[,]);
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
                if (array == null || array.Length <= 1) return;
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

            public string Name => _name;
            public int Bank => _bank;
            public Participant[] Participants => _participants;
            public abstract double[] Prize { get; }

            public WaterJump(string name, int bank)
            {
                _name = name;
                _bank = bank;
                _participants = new Participant[0];
            }

            public void Add(Participant participant) // одна команда в группу
            {
                if (_participants == null) return;

                Participant[] participant2 = new Participant[_participants.Length + 1];
                for (int i = 0; i < _participants.Length; i++)
                {
                    participant2[i] = _participants[i];
                }
                participant2[_participants.Length] = participant;
                _participants = participant2;
            }
            public void Add(Participant[] participants) // несколько
            {
                if (_participants == null || _participants.Length == 0 || participants == null) return;

                foreach (Participant team in participants)
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
                    if (this.Participants.Length < 3 || this.Participants == null) return default(double[]);

                    prizes[0] = this.Bank * 0.5; // Первое место
                    prizes[1] = this.Bank * 0.3; // Второе место
                    prizes[2] = this.Bank * 0.2; // Третье место

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
                    if (this.Participants.Length < 3 || this.Participants == null) return default(double[]);

                    int n;
                    double[] prizes;
                    if (Participants.Length / 2 < 10)
                    {
                        prizes = new double[Participants.Length / 2];
                        n = Participants.Length / 2;
                    }
                    else
                    {
                        prizes = new double[10];
                        n = 10;
                    }

                    double N = 20.0 / n;

                    for (int i = 0; i < n; i++)
                    {
                        prizes[i] = this.Bank * (N / 100);
                    }

                    prizes[0] += this.Bank * 0.4; // 40%
                    prizes[1] += this.Bank * 0.25; // 25%
                    prizes[2] += this.Bank * 0.15; // 15%

                    return prizes;
                }
            }
        }
    }
}
