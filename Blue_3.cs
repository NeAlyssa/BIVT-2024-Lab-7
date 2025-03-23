
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    public class Blue_3
    {
        //класс
        public class Participant
        {
            //приватные поля
            private string _name;
            private string _surname;
            protected int[] _minutes;

            //свойства только для чтения
            public string Name => _name;
            public string Surname => _surname;
            public int[] Penalties
            {
                get
                {
                    if (_minutes == null) return null;
                    int[] New = new int[_minutes.Length];
                    for (int i = 0; i < New.Length; i++)
                    {
                        New[i] = _minutes[i];
                    }
                    return New;
                }

            }
            
            public int Total
            {
                get
                {
                    if (_minutes == null) return 0;
                    int time = 0;
                    for (int i = 0; i < _minutes.Length; i++)
                    {
                        time += _minutes[i];
                    }
                    return time;
                }
            }
            //проверка на то выбыл человек из списка или нет
            public virtual bool IsExpelled
            {
                get
                {
                    if (_minutes == null) return false;
                    foreach (int time in _minutes)
                    {
                        if (time == 10) return true;
                    }
                    return false;
                }
            }

            //конструктор
            public Participant(string Name, string Surname)
            {
                _name = Name;
                _surname = Surname;
                _minutes = new int[0];
            }


            //метод
            public virtual void PlayMatch(int time)
            {
                if (_minutes == null) return;
                var New = new int[_minutes.Length + 1];
                for (int i = 0; i < _minutes.Length; i++)
                {
                    New[i] = _minutes[i];
                }
                New[_minutes.Length] = time;
                _minutes = New;

            }
            //сортировка по возрастанию времени
            public static void Sort(Participant[] array)
            {
                if (array == null || array.Length == 0) return;
                for (int i = 0; i < array.Length; i++)
                {
                    for (int j = 0; j < array.Length - 1 - i; j++)
                    {
                        if (array[j].Total > array[j + 1].Total)
                        {
                            Participant a = array[j];
                            array[j] = array[j + 1];
                            array[j + 1] = a;
                        }
                    }
                }
            }

            public void Print()
            {
                Console.WriteLine($"{_name} {_surname} {Total} {IsExpelled}");
            }

        }

        public class BasketballPlayer: Participant
        {
            public BasketballPlayer ( string Name, string Surname) : base(Name, Surname) { }
            //переопределяем свойство
            public override bool IsExpelled
            {
                get
                {
                    if (_minutes == null) return false;
                    int FallsMore5 = 0;
                    foreach (var fall in _minutes)
                    {
                        if (fall == 5)  FallsMore5++;
                    }
                    if (FallsMore5 > _minutes.Length * 0.1 || Total > _minutes.Length * 2) { return true; }
                    return false;
                }
            }

            // переопределяем метод
            public override void PlayMatch(int fall)
            {
                if (fall<0 || fall>5 ) return;
                base.PlayMatch(fall);

            }
        }

        public class HockeyPlayer : Participant
        {
            //поля статические - считаем для всего класса
            private static int players=0;
            private static double TotalT=0;
            public HockeyPlayer ( string Name, string Surname) : base(Name, Surname) 
            {
                players++;
            }
            //переопределяем свойство
            public override bool IsExpelled
            {
                get
                {
                    if (_minutes == null) return false;
                    foreach (int time in _minutes)
                    {
                        if (time == 10) return true;
                    }
                    double allTime = TotalT / players;
                    if (Total > allTime * 0.1) { return true; }
                    return false;
                }
            }

            // переопределяем метод
            public override void PlayMatch(int time)
            {
                if (_minutes==null) return;
                base.PlayMatch(time);
                TotalT += time;

            }
        }


    }
}
