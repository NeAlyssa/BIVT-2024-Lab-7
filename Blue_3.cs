using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab_7
{
    public class Blue_3
    {
        public class Participant
        {
            private string _name;
            private string _surname;
            protected int[] _penaltyTimes;

            public string Name => _name;
            public string Surname => _surname;

            public int[] Penalties
            {
                get
                {
                    if (_penaltyTimes == null) return null;

                    int[] newone = new int[_penaltyTimes.Length];
                    for (int i = 0; i < _penaltyTimes.Length; i++)
                    {
                        newone[i] = _penaltyTimes[i];
                    }
                    return newone;
                }
            }

            public int Total
            {
                get
                {
                    if (_penaltyTimes == null || _penaltyTimes.Length == 0) return 0;
                    int total = 0;
                    foreach (var time in _penaltyTimes)
                    {
                        total += time;
                    }
                    return total;
                }
            }

            public virtual bool IsExpelled
            {
                get
                {
                    if (_penaltyTimes == null) return false;

                    for (int i = 0; i < _penaltyTimes.Length; i++)
                    {
                        if (_penaltyTimes[i] == 10) return true;
                    }
                    return false;
                }
            }

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _penaltyTimes = new int[0];
            }

            public virtual void PlayMatch(int penalties)
            {
                if (_penaltyTimes == null) return;

                int[] newPenalties = new int[_penaltyTimes.Length + 1];
                for (int i = 0; i < _penaltyTimes.Length; i++)
                {
                    newPenalties[i] = _penaltyTimes[i];
                }
                newPenalties[newPenalties.Length - 1] = penalties;
                _penaltyTimes = newPenalties;
            }

            public static void Sort(Participant[] array)
            {
                if (array == null || array.Length == 0) return;

                for (int i = 0; i < array.Length; i++)
                {
                    for (int j = 0; j < array.Length - 1 - i; j++)
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
                Console.WriteLine($" {_name} {_surname} : {Total}, иключён: {IsExpelled}");
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
                    if (_penaltyTimes == null || _penaltyTimes.Length == 0) return false;

                    int m = _penaltyTimes.Length;
                    int f = 0;
                    int totalFouls = 0;

                    foreach (var penalties in _penaltyTimes)
                    {
                        if (penalties >=5) f++;
                        totalFouls += penalties;
                    }

                    if (f > m * 0.1 || totalFouls > m * 2)
                    {
                        return true;
                    }

                    return false;
                }
            }

            public override void PlayMatch(int va)
            {
                if (va < 0 || va > 5) return;
                 

                base.PlayMatch(va);
            }
        }
        public class HockeyPlayer : Participant
        {
            public static int c = 0;
            public static int summ = 0;
            public HockeyPlayer(string name, string surname) : base(name, surname)
            {
                _penaltyTimes = new int[0];
                c++;
            }

            public override bool IsExpelled
            {
                get
                {
                   
                    if (_penaltyTimes == null || _penaltyTimes.Length == 0) return false;

 
                    foreach (var time in _penaltyTimes)
                    {
                        if (time >= 10) return true;
                    }


                    if (this.Total > summ / c * 0.1) return true;

                    return false;
                }
            }

            public override void PlayMatch(int t)
            {

                if (_penaltyTimes == null) return;
                
                base.PlayMatch(t);

                summ += t;

            }
        }

    }
}
