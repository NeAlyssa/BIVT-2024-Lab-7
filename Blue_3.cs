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
            protected int[] _penaltyTimes;


            public string Name => _name;
            public string Surname => _surname;

            public int[] Penalties
            {
                get
                {
                    if (_penaltyTimes == null) return null;
                    if (_penaltyTimes.Length == 0) return _penaltyTimes;
                    int[] copy = new int[_penaltyTimes.Length];
                    Array.Copy(_penaltyTimes, copy, _penaltyTimes.Length);
                    return copy;
                }
            }

            public int Total
            {
                get

                {
                    if (_penaltyTimes == null || _penaltyTimes.Length == 0) return 0;

                    int sum = 0;
                    for (int i = 0; i < _penaltyTimes.Length; i++)
                        sum += _penaltyTimes[i];
                    return sum;
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


            public virtual void PlayMatch(int time)
            {
                if (_penaltyTimes == null || time < 0) return;

                if (_penaltyTimes.Length == 0)
                {
                    int[] _array = new int[1];
                    _array[0] = time;
                    _penaltyTimes = new int[1];
                    Array.Copy(_array, _penaltyTimes, _array.Length);
                }
                else
                {
                    int[] _array = new int[_penaltyTimes.Length + 1];
                    _array[_array.Length - 1] = time;
                    for (int i = 0; i < _penaltyTimes.Length; i++)
                    {
                        _array[i] = _penaltyTimes[i];
                    }
                    _penaltyTimes = new int[_array.Length];
                    Array.Copy(_array, _penaltyTimes, _array.Length);
                }
            }

            public static void Sort(Participant[] array)
            {
                if (array == null) return;

                int countWithoutNull = 0;
               
                for (int k = 0; k < array.Length; k++)
                {
                    if (array[k] != null)
                    {
                        array[countWithoutNull] = array[k];
                        countWithoutNull++;
                    }
                }
                while (countWithoutNull < array.Length)
                {
                    array[countWithoutNull] = null;
                    countWithoutNull++;
                }
                
                for (int i = 1, j = 2; i < countWithoutNull;)
                {
                    if (i == 0 || array[i - 1].Total <= array[i].Total)
                    {
                        i = j;
                        j++;
                    }
                    else
                    {
                        Participant temp = array[i];
                        array[i] = array[i - 1];
                        array[i - 1] = temp;
                        i--;
                    }
                }
            }

            public void Print()
            {
                Console.Write($"Name: {_name}, Surname: {_surname}, Total time: {Total}, is Expelled: {IsExpelled}");
                Console.WriteLine();
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

                    int n = this.Penalties.Length;
                    int count = 0;
                    for (int i = 0; i < n; i++)
                    {
                        if (this.Penalties[i] >= 5) count++;
                    }
                    if ((count * 100 / n) > 10 || this.Total > n * 2) return true;
                    return false;

                }
            }

            public override void PlayMatch(int countMatches)
            {
                if (countMatches < 0 || countMatches > 5) return;
                base.PlayMatch(countMatches);
                
            }


        }



        public class HockeyPlayer : Participant
        {
            private static int _numPlayer;
            private static int _allPenaltyTime;
            public HockeyPlayer(string name, string surname) : base(name, surname)
            {
                _penaltyTimes = new int[0];
                _numPlayer++;
            }

            public override bool IsExpelled
            {
                get
                {
                    if (_penaltyTimes == null) return false;

                    int n = this.Penalties.Length;
                    for (int i = 0; i < n; i++)
                    {
                        if (this.Penalties[i] >= 10) return true;
                    }
                    if (this.Total > 0.1 * _allPenaltyTime / _numPlayer) return true;
                    return false;
                }
            }


            public override void PlayMatch(int penaltyTime)
            {
                if (_penaltyTimes == null) return;
                if (penaltyTime < 0 || penaltyTime > 10) return; 
                base.PlayMatch(penaltyTime);
                _allPenaltyTime += penaltyTime;
            }

        }

    }
}
