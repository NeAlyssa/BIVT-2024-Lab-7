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
                    if (_pT == null) return 0;
                    int counter = 0;
                    for (int i = 0; i < _pT.Length; i++) { counter += _pT[i]; }
                    return counter;
                }
            }
            public virtual bool IsExpelled
            {
                get
                {
                    bool b = true;
                    if (_pT == null) return true;
                    for (int i = 0; i < _pT.Length; i++)
                    {
                        if (_pT[i] == 10)
                        {
                            b = false;
                            break;
                        }
                    }
                    return b;
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
                if (_pT == null) return;
                int[] new_pT = new int[_pT.Length + 1];
                for (int i = 0; i < new_pT.Length - 1; i++) { new_pT[i] = _pT[i]; }
                new_pT[_pT.Length] = time;
                _pT = new_pT;

            }
            public static void Sort(Participant[] array)
            {
                if (array == null || array.Length == 0) return;
                for (int i = 0; i < array.Length; i++)
                {
                    for (int j = 0; j < array.Length - i - 1; j++)
                    {
                        if (array[j].Total > array[j + 1].Total) { (array[j], array[j + 1]) = (array[j + 1], array[j]); }
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
            public BasketballPlayer(string name, string surname) : base(name, surname) { }
            public override bool IsExpelled
            {
                get
                {
                    if (Penalties == null) return false;
                    int counterf = 0;
                    foreach (var f in Penalties)
                    {
                        if (f == 5) counterf++;
                    }
                    if (Total > 2 * Penalties.Length || counterf > Penalties.Length * 0.1) return true;
                    else return false;

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
            // приватные статические поля
            private static int _timePlayers;
            private static int _players;

            // конструктор для статик.
            static HockeyPlayer()
            {
                _timePlayers = 0;
                _players = 0;
            }

            // конструктор
            public HockeyPlayer(string name, string surname) : base(name, surname)
            {
                _players++;
            }

            // свойства
            public override bool IsExpelled
            {
                get
                {
                    if (Penalties == null) return false;
                    foreach (var f in Penalties)
                    {
                        if (f == 10) return true;
                    }
                    if (Total > 0.1 * _timePlayers / _players) return true;
                    else return false;
                }
            }
            public override void PlayMatch(int f)
            {
                if (f < 0 || f > 10) return;
                base.PlayMatch(f);
                _timePlayers += f;
            }
        }
    }
}
