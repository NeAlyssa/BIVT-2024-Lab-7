using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    public class Blue_3 
    {
        public class  Participant
        {
            //
            private string _name;
            private string _surname;
            protected int[] _penaltyTimes;
            //
            public string Name => _name;
            public string Surname => _surname;
            public int[] Penalties
            {
                get
                {

                    if (_penaltyTimes == null) return null;
                    int[] copy = new int[_penaltyTimes.Length];
                    Array.Copy(_penaltyTimes, copy, copy.Length);
                    return copy;
                }
            }
            //
            public int Total
            {
                get
                {
                    if (_penaltyTimes == null || _penaltyTimes.Length == 0) return 0;
                    return _penaltyTimes.Sum();

                }
            }
            public virtual bool IsExpelled
            {
                get
                {
                    if (_penaltyTimes == null) return false;
                    for (int s = 0; s < _penaltyTimes.Length; s++)
                    {
                        if (_penaltyTimes[s] == 10) return true;
                    }
                    return false;
                }
            }
            //
            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _penaltyTimes = new int[0];


            }
            public virtual void PlayMatch(int time)
            {
                if (_penaltyTimes == null) return;
                int[] temp = new int[_penaltyTimes.Length + 1];
                for (int i = 0; i < temp.Length - 1; i++)
                {
                    temp[i] = _penaltyTimes[i];
                }
                temp[temp.Length - 1] = time;
                _penaltyTimes = temp;
            }
            public static void Sort(Participant[] array)
            {
                if (array == null) return;
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
                Console.WriteLine($"Name: {Name}, Surname: {Surname}, TotalTime: {Total}, IsExpelled:{IsExpelled}");

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
                    int fall = 0;
                    foreach (int p in _penaltyTimes)
                    {
                        if (p>=5) fall++;
                    }
                    if (fall > 0.1 * _penaltyTimes.Length || Total >= 2 * _penaltyTimes.Length) 
                    {
                        return true;
                    }
                    
                    return false;
                }

            }

            public override void PlayMatch(int fall)
            {
                if (fall <0 || fall >5)
                {
                    return;
                }
                base.PlayMatch(fall);
            }

        }

        public class HockeyPlayer : Participant
        {
            private static int _ind= 0; 
            private static int _all = 0;
            public HockeyPlayer(string name, string surname) : base(name, surname)
            {
                _penaltyTimes = new int[0];
                _ind++;
            }

            public override bool IsExpelled
            {
                get
                {
                    if (_penaltyTimes == null|| _penaltyTimes.Length==0) return false;
                    foreach (int time in _penaltyTimes)
                    {
                        if (time >= 10)
                        {
                            return true;
                        }
                            
                    }
                    if (this.Total > 0.1 * _all / _ind)
                    {
                        return true;
                    }
                    return false;
                }
            }

            public override void PlayMatch(int time)
            {
                if (_penaltyTimes == null) return;
                base.PlayMatch(time);
                _all += time;
            }
        }
    }
}
