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
                    int[] copy = new int[_penaltyTimes.Length];
                    Array.Copy(_penaltyTimes, copy, _penaltyTimes.Length);
                    return copy;
                }
            }

            public int Total
            {
                get

                {
                    if (_penaltyTimes == null) return 0;

                    return _penaltyTimes.Length > 0 ? _penaltyTimes.Sum() : 0;
                }
            }

            //его надо переопределить 
            public virtual bool IsExpelled
            {
                get
                {
                    if (_penaltyTimes == null || _penaltyTimes.Length == 0) return false;
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
               
                Array.Resize(ref _penaltyTimes, _penaltyTimes.Length + 1);
                _penaltyTimes[_penaltyTimes.Length - 1] = time;
            }

            public static void Sort(Participant[] array)
            {
                if (array == null || array.Length == 0) return;

                for (int i = 0; i < array.Length - 1; i++)
                {
                    for (int j = 0; j < array.Length - i - 1; j++)
                    {
                        if (array[j].Total < array[j + 1].Total)
                        {
                            Participant temp = array[j];
                            array[j] = array[j + 1];
                            array[j + 1] = temp;
                        }
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

                    int count = 0;
                    int countMatches = _penaltyTimes.Length;

                    for (int i = 0; i < _penaltyTimes.Length; i++)
                    {
                        if (_penaltyTimes[i] >= 5) count++;
                    }

                    return (count >= (countMatches / 10) || this.Total >= 2 * countMatches);


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
            private static int _numPlayer = 0;
            private static int _allPenaltyTime = 0;
            public HockeyPlayer(string name, string surname) : base(name, surname)
            {
                _penaltyTimes = new int[0];
                _numPlayer++;
            }

            public override bool IsExpelled
            {
                get
                {
                    if (_penaltyTimes == null || _penaltyTimes.Length == 0) return false;

                    for (int i = 0; i < _penaltyTimes.Length; i++)
                    {
                        if (_penaltyTimes[i] >= 10) return true;
                    }

                    if (_numPlayer == 0) return false;
                    double average = (_numPlayer > 0) ? (double)_allPenaltyTime / _numPlayer : 0;
                    return this.Total > (int)(0.1 * average);
                }
            }


            public override void PlayMatch(int penaltyTime)
            {
                if (_penaltyTimes == null || penaltyTime < 0) return;
                base.PlayMatch(penaltyTime);
                _allPenaltyTime += penaltyTime;
            }

        }

    }
}
