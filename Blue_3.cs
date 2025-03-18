using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    public class Blue_3
    {
        public class Participant 
        {
            private string _name; // поле с именем
            private string _surname; // поле с фамилией
            protected int[] _penaltyTimes; // массив для штрафеых

            // свойства
            public string Name => _name;
            public string Surname => _surname; // сокращение get
            public int[] Penalties
            {
                get
                {
                    if (_penaltyTimes == null) return null;
                    int[] coppenaltyTimes = new int[_penaltyTimes.Length];
                    for (int i = 0; i < _penaltyTimes.Length; i++)
                    {
                        coppenaltyTimes[i] = _penaltyTimes[i];
                    }
                    return coppenaltyTimes;
                }
            }
            public int Total // сумму всех штрафных минут спортсмена
            {
                get
                {
                    if (_penaltyTimes == null) return 0;

                    return _penaltyTimes.Sum();
                }
            }

            public virtual bool IsExpelled // свойство показывает исключен ли спортсмен из из списка кандидатов
            {
                get
                {
                    if (_penaltyTimes == null) return false;
                    for (int k = 0; k < _penaltyTimes.Length; k++)
                    {
                        if (_penaltyTimes[k] == 10)
                        {
                            return true;
                        }
                    }
                    return false;
                }
            }

            // конструктор
            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _penaltyTimes = new int[0];
            }

            // методы 

            public virtual void PlayMatch(int time) // добавляет в массив штрафов штрафное время в очередном матче
            {
                if (_penaltyTimes == null) return;

                int[] penaltyTimes2 = new int[_penaltyTimes.Length + 1];
                for(int i = 0; i < penaltyTimes2.Length - 1; i++)
                {
                    penaltyTimes2[i] = _penaltyTimes[i];
                }
                penaltyTimes2[penaltyTimes2.Length - 1] = time;
                _penaltyTimes = penaltyTimes2;
            }

            public static void Sort(Participant[] array)
            {
                if (array == null || array.Length == 0) return;
                // делаем сортировку

                for (int i = 0; i < array.Length; i++)
                {
                    for (int j = 0; j < array.Length - i - 1; j++)
                    {
                        if (array[j].Total > array[j + 1].Total) // обращаемся к элементу массива и получаем значение этого элемента в свойвстве TotalTime
                        {
                            (array[j], array[j + 1]) = (array[j + 1], array[j]);
                        }
                    }
                }
            }

            public void Print()
            {
                Console.WriteLine($"{Name} {Surname}: общий штраф: {Total} исключен: {IsExpelled}");
            }
        }
        public class BasketballPlayer : Participant
        {
            public BasketballPlayer(string name, string surname) : base(name, surname)
            {
                _penaltyTimes = new int[0];
            }
            public override bool IsExpelled
            {
                get
                {
                    if (_penaltyTimes == null) return false;
                    int n = 0;
                    for (int k = 0; k < _penaltyTimes.Length; k++)
                    {
                        if (_penaltyTimes[k] >= 5)
                        {
                            n++;
                        }
                    }
                    if (n > 0.1 * _penaltyTimes.Length || this.Total >= 2 * _penaltyTimes.Length)
                    {
                        return true;
                    }
                    return false;
                }

            }
            public override void PlayMatch(int n)
            {
                if (n < 0 || n > 5 || _penaltyTimes == null)
                {
                    return;
                }
                base.PlayMatch(n);
            }
        }
        public class HockeyPlayer : Participant
        {
            private int _player;
            private int _time;

            public HockeyPlayer(string name, string surname) : base(name, surname)
            {
                _penaltyTimes = new int[0];
                _player++;
            }

            public override bool IsExpelled
            {
                get
                {
                    if (_penaltyTimes == null) return false;
                    for (int k = 0; k < _penaltyTimes.Length; k++)
                    {
                        if (_penaltyTimes[k] >= 10)
                        {
                            return true;
                        }
                    }
                    if (this.Total > 0.1 * _time / _player)
                    {
                        return true;
                    }
                    return false;
                }

            }
            public override void PlayMatch(int Times)
            {
                if (_penaltyTimes == null) return;
                base.PlayMatch(Times);
                _time += Times;
            }
        }
    }
}
