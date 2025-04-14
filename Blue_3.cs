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
            private string _name;
            private string _surname;
            protected int[] _penaltytimes;

            public string Name => _name;
            public string Surname => _surname;

            public int[] Penalties
            {
                get
                {
                    if (_penaltytimes == null) return null;
                    int[] copy = new int[_penaltytimes.Length];
                    for (int i = 0; i < copy.Length; i++)
                    {
                        copy[i] = _penaltytimes[i];
                    }
                    return copy;
                }
            }

            public int Total
            {
                get
                {
                    if (_penaltytimes == null) return 0;
                    int cnt = 0;
                    for (int i = 0; i < _penaltytimes.Length; i++)
                    {
                        cnt += _penaltytimes[i];
                    }
                    return cnt;
                }
            }


            public virtual bool IsExpelled
            {
                get
                {
                    bool k = false;
                    if (_penaltytimes == null) return false;
                    for (int i = 0; i < _penaltytimes.Length; i++)
                    {
                        if (_penaltytimes[i] == 10)
                        {
                            k = true;
                            break;
                        }
                    }
                    return k;
                }
            }


            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _penaltytimes = new int[0];
            }


            public virtual void PlayMatch(int time)
            {
                if (_penaltytimes == null) return;
                int[] newpenaltytimes = new int[_penaltytimes.Length + 1];
                for (int i = 0; i < newpenaltytimes.Length - 1; i++)
                {
                    newpenaltytimes[i] = _penaltytimes[i];
                }
                newpenaltytimes[_penaltytimes.Length] = time;
                _penaltytimes = newpenaltytimes;
            }



            public static void Sort(Participant[] array)
            {
                if (array == null || array.Length == 0) return;
                for (int i = 0; i < array.Length; i++)
                {
                    for (int j = 0; j < array.Length - i - 1; j++)
                    {
                        if (array[j].Total > array[j + 1].Total)
                        {
                            (array[j], array[j + 1]) = (array[j + 1], array[j]);
                        }
                    }
                }
            }


            public void Print()
            {
                Console.WriteLine($"{_name}   {_surname}   {Total}  {IsExpelled}");
            }
        }

        public class BasketballPlayer : Participant
        {
            public BasketballPlayer(string name, string surname) : base(name, surname)
            {
                _penaltytimes = new int[0];
            }

            public override bool IsExpelled
            {
                get
                {
                    if (_penaltytimes == null) return false;
                    int cnt = 0;
                    for (int i = 0; i < _penaltytimes.Length; i++)
                    {
                        if (_penaltytimes[i] >= 5) cnt++;
                    }
                    if (cnt * 100 / _penaltytimes.Length > 10 || this.Total > 2 * _penaltytimes.Length) return true;
                    return false;
                }
            }
            public override void PlayMatch(int fouls)
            {
                if (fouls < 0 || fouls > 5) return;
                base.PlayMatch(fouls);
            }
        }

        public class HockeyPlayer : Participant
        {
            private static int _cnt;
            private static int _allpenalties;

            public HockeyPlayer(string name, string surname) : base(name, surname)
            {
                _cnt++;
                _penaltytimes = new int[0];
            }

            public override bool IsExpelled
            {
                get
                {
                    if (_penaltytimes == null) return false;
                    for (int i = 0; i < Penalties.Length; i++)
                    {
                        if (Penalties[i] >= 10) return true;
                    }
                    if (Total > _allpenalties / _cnt / 10.0) return true;
                    return false;
                }
            }

            public override void PlayMatch(int penaltytime)
            {
                if (_penaltytimes == null) return;
                if (penaltytime < 0 || penaltytime > 10) return;
                base.PlayMatch(penaltytime);
                _allpenalties += penaltytime;
            }


        }
    }
}
