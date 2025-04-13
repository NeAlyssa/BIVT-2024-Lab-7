using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lab_7
{
    public class Blue_3
    {
        public class Participant
        {
            private string _name;
            private string _surname;
            protected int[] _penalty;

            public string Name => _name;
            public string Surname => _surname;
            public int[] Penalties
            {
                get
                {
                    if (_penalty == null) return null;
                    int[] penalty = new int[_penalty.Length];
                    for (int i = 0; i < penalty.Length; i++)
                    {
                        penalty[i] = _penalty[i];
                    }
                    return penalty;
                }
            }

            public int Total
            {
                get
                {
                    if (_penalty == null || _penalty.Length == 0) return 0;
                    int total_time = 0;
                    for (int i = 0; i < _penalty.Length; i++)
                    {
                        total_time += _penalty[i];
                    }
                    return total_time;
                }
            }

            public virtual bool IsExpelled
            {
                get
                {
                    if (_penalty == null || _penalty.Length == 0) return false;
                    for (int i = 0; i < _penalty.Length; i++)
                    {
                        if (_penalty[i] == 10)
                        {
                            return true;
                        }
                    }
                    return false;
                }
            }

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _penalty = new int[0];
            }

            public virtual void PlayMatch(int time)
            {
                if (_penalty == null) return;
                int[] newpen = new int[_penalty.Length + 1];
                for (int i = 0; i < newpen.Length - 1; i++)
                {
                    newpen[i] = _penalty[i];
                }
                newpen[newpen.Length - 1] = time;
                _penalty = newpen;
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
                Console.WriteLine($"{_name} {_surname}: {Total}. IsExpelled: {IsExpelled}");
            }
        }
        public class BasketballPlayer : Participant
        {
            public override bool IsExpelled
            {
                get
                {
                    if (_penalty == null || _penalty.Length == 0) return false;
                    int countM = _penalty.Length;
                    int countP = 0;
                    for (int i = 0; i < _penalty.Length; i++)
                    {
                        if (_penalty[i] >= 5) countP++;
                    }
                    if (countP > 0.1 * countM || this.Total >= 2 * countM) return true;
                    return false;
                }
            }

            public BasketballPlayer(string name, string surname) : base (name, surname)
            {
                _penalty = new int[0];
            }

            public override void PlayMatch(int count)
            {
                if (count < 0 || count > 5) return;
                base.PlayMatch(count);
            }
        }
        public class HockeyPlayer : Participant
        {
            public static int sum = 0;
            public static int count = 0;
            public override bool IsExpelled
            {
                get
                {
                    if (_penalty == null || _penalty.Length == 0) return false;
                    for (int i = 0; i < _penalty.Length; i++)
                    {
                        if (_penalty[i] == 10) return true;
                    }
                    if (this.Total > 0.1 * sum/count) return true;
                    return false;
                }
            }

            public HockeyPlayer(string name, string surname) : base(name, surname)
            {
                _penalty = new int[0];
                count++;
            }

            public override void PlayMatch(int time)
            {
                if (_penalty == null) return;
                base.PlayMatch(time);
                sum += time;
            }
        }
    }
}
