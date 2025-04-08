
using System;
namespace Lab_7
{
    public class Blue_3
    {
        public class Participant
        {
            private string _name;
            private string _surname;
            protected int[] _ptimes;

            public string Name => _name;
            public string Surname => _surname;
            public int[] Penalties
            {
                get
                {
                    if (_ptimes == null) return null;
                    int[] newarr = new int[_ptimes.Length];
                    for (int i = 0; i < newarr.Length; i++)
                    {
                        newarr[i] = _ptimes[i];
                    }
                    return newarr;
                }

            }

            public int Total
            {
                get
                {
                    if (_ptimes == null) return 0;
                    int time = 0;
                    for (int i = 0; i < _ptimes.Length; i++)
                    {
                        time += _ptimes[i];
                    }
                    return time;
                }
            }
        
            public virtual bool IsExpelled
            {
                get
                {
                    if (_ptimes == null) return false;
                    foreach (int time in _ptimes)
                    {
                        if (time == 10) return true;
                    }
                    return false;
                }
            }

            public Participant(string Name, string Surname)
            {
                _name = Name;
                _surname = Surname;
                _ptimes = new int[0];
            }


            public virtual void PlayMatch(int time)
            {
                if (_ptimes == null) return;
                var newarr = new int[_ptimes.Length + 1];
                for (int i = 0; i < _ptimes.Length; i++)
                {
                    newarr[i] = _ptimes[i];
                }
                newarr[_ptimes.Length] = time;
                _ptimes = newarr;

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

        public class BasketballPlayer : Participant
        {
            public BasketballPlayer(string Name, string Surname) : base(Name, Surname) { }
         
            public override bool IsExpelled
            {
                get
                {
                    if (_ptimes == null) return false;
                    int FallsMore5 = 0;
                    foreach (var fall in _ptimes)
                    {
                        if (fall == 5) FallsMore5++;
                    }
                    if (FallsMore5 > _ptimes.Length * 0.1 || Total > _ptimes.Length * 2) { return true; }
                    return false;
                }
            }

            public override void PlayMatch(int fall)
            {
                if (fall < 0 || fall > 5) return;
                base.PlayMatch(fall);

            }
        }

        public class HockeyPlayer : Participant
        {
        
            private static int players = 0;
            private static double TotalT = 0;
            public HockeyPlayer(string Name, string Surname) : base(Name, Surname)
            {
                players++;
            }
         
            public override bool IsExpelled
            {
                get
                {
                    if (_ptimes == null) return false;
                    foreach (int time in _ptimes)
                    {
                        if (time == 10) return true;
                    }
                    double allTime = TotalT / players;
                    if (Total > allTime * 0.1) { return true; }
                    return false;
                }
            }

        
            public override void PlayMatch(int time)
            {
                if (_ptimes == null) return;
                base.PlayMatch(time);
                TotalT += time;

            }
        }


    }
}
