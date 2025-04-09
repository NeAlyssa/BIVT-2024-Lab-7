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
            // приватные поля
            private string _name;
            private string _surname;
            protected int[] _pT;

            // свойства
            public string Name => _name;
            public string Surname => _surname;
            public int[] Penalties
            {
                get
                {
                    if (_pT == null) return null;
                    int[] cp = new int[_pT.Length];
                    for (int i = 0; i < cp.Length; i++) { cp[i] = _pT[i]; }
                    return cp;
                }
            }
            public int Total
            {
                get
                {
                    if (_pT == null || _pT.Length == 0) return 0;
                    int counter = 0;
                    for (int i = 0; i < _pT.Length; i++) { counter += _pT[i]; }
                    return counter;
                }
            }
            public virtual bool IsExpelled
            {
                get
                {
                    if (_pT == null) return false;
                    for (int i = 0; i < _pT.Length; i++)
                    {
                        if (_pT[i] == 10) return true;
                    }
                    return false;
                }
            }
            // конструктор
            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _pT = new int[0];
            }

            // методы
            public virtual void PlayMatch(int time)
            {
                if (_pT == null || time < 0) return;
                if (_pT.Length == 0) { _pT = new int[] { time }; }
                else
                {
                    int[] _arr = new int[_pT.Length + 1];
                    Array.Copy(_pT, _arr, _pT.Length);
                    _arr[_arr.Length - 1] = time;
                    _pT = _arr;
                }
            }
            public static void Sort(Participant[] array)
            {
                if (array == null) return;
                int counter = 0;
                for (int i = 0; i < array.Length; i++)
                {
                    if (array[i] != null)
                    {
                        array[counter] = array[i];
                        counter++;
                    }
                }
                while (counter < array.Length)
                {
                    array[counter] = null;
                    counter++;
                }

                // сортировка по штрафному времени
                for (int i = 1, j = 2; i < counter;)
                {
                    if (i == 0 || array[i - 1].Total <= array[i].Total)
                    {
                        i = j;
                        j++;
                    }
                    else
                    {
                        Participant t = array[i];
                        array[i] = array[i - 1];
                        array[i - 1] = t;
                        i--;
                    }
                }
            }
            public void Print()
            {
                Console.WriteLine($"{_name} {_surname} {Total} {IsExpelled}");
                Console.WriteLine();
            }
        }

        // наследник класса
        public class BasketballPlayer : Participant
        {
            public BasketballPlayer(string name, string surname) : base(name, surname) 
            {
                _pT = new int[0];
            }
            public override bool IsExpelled
            {
                get
                {
                    if (_pT == null || _pT.Length == 0) return false;

                    int t = this.Penalties.Length;
                    int counter = 0;
                    for (int i = 0; i < t; i++) { if (this.Penalties[i] >= 5) counter++; }
                    if ((counter * 100 / t) > 10 || this.Total > t * 2) return true;
                    return false;
                }
            }
            public override void PlayMatch(int f)
            {
                if (f < 0 || f > 5) return;
                base.PlayMatch(f);
            }
        }
        // ещё один наследник класса
        public class HockeyPlayer : Participant
        {
            private static int _numberPlayer;
            private static int _allPT; 

            public HockeyPlayer(string name, string surname) : base(name, surname)
            {
                _pT = new int[0];
                _numberPlayer++;
            }

            // Переопределенный метод 
            public override bool IsExpelled
            {
                get
                {
                    if (_pT == null) return false;
                    int t = this.Penalties.Length;
                    for (int i = 0; i < t; i++)
                    {
                        if (this.Penalties[i] >= 10) return true;
                    }
                    if (this.Total > 0.1 * _allPT / _numberPlayer) return true;
                    return false;
                }
            }

            public override void PlayMatch(int penaltyTime)
            {
                if (_pT == null) return;
                if (penaltyTime < 0 || penaltyTime > 10) return;
                base.PlayMatch(penaltyTime);
                _allPT += penaltyTime; 
            }
        }
    }
}
