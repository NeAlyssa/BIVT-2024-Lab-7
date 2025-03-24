using System;
using System.Collections.Generic;
using System.Linq;
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
            private int[] _penaltyTimes;

            public string Name => _name;
            public string Surname => _surname;
            public int[] Penalties
            {
                get
                {
                    if (_penaltyTimes == null) return null;

                    int[] array = new int[_penaltyTimes.Length];
                    for (int i = 0; i < _penaltyTimes.Length; i++)
                        array[i] = _penaltyTimes[i];

                    return array;
                }
            }
          
            public int Total
            {
                get
                {
                    if (_penaltyTimes == null) return 0;

                    int total = 0;
                    for (int i = 0; i < _penaltyTimes.Length; i++)
                        total += _penaltyTimes[i];

                    return total;
                }
            }
           
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
                if (_penaltyTimes == null) return;

                int[] newarray = new int[_penaltyTimes.Length + 1];
                for (int i = 0; i < _penaltyTimes.Length; i++)
                {
                    newarray[i] = _penaltyTimes[i];
                }
                newarray[newarray.Length - 1] = time;
                _penaltyTimes = newarray;
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
                            Participant temp = array[j];
                            array[j] = array[j + 1];
                            array[j + 1] = temp;
                        }
                    }
                }
            }

            public void Print()
            {
                Console.WriteLine($"Участник: {Name} {Surname}");
                Console.WriteLine("Штрафные минуты:");

                if (_penaltyTimes != null)
                {
                    for (int i = 0; i < _penaltyTimes.Length; i++)
                    {
                        Console.Write($"{_penaltyTimes[i]} ");
                    }
                    Console.WriteLine();
                }

                Console.WriteLine($"Общее штрафное время: {Total}");

                if (IsExpelled)
                {
                    Console.WriteLine("Статус: Дисквалифицирован");
                }
                else
                {
                    Console.WriteLine("Статус: Активен");
                }
            }
        }
        public class BasketballPlayer : Participant
        {
            public BasketballPlayer(string name, string surname) : base(name, surname) { }
            public override bool IsExpelled
            {
                get
                {
                    if (Penalties == null) return false;
                   int n = Penalties.Length;
                    int count = 0;
                    for (int i = 0; i < n; i++)
                        if (Penalties[i] == 5) count++;
                    if (count > Penalties.Length * 0.1 || Total > n) return true;
                    else return false;
                }
            }
            public override void PlayMatch(int falls)
            {
                if (falls < 0 || falls > 5) return;
                base.PlayMatch(falls);
            }
        }
        public class HockeyPlayer : Participant
        {
            private static int _timePlayers;
            private static int _players;
            static HockeyPlayer()
            {
                _timePlayers = 0;
                _players = 0;
            }
            public HockeyPlayer(string name, string surname) : base(name, surname)
            {
                _players++;
            }
            public override bool IsExpelled
            {
                get
                {
                    if (Penalties == null) return false;
                    foreach (var fall in Penalties)
                    {
                        if (fall == 10) return true;
                    }
                    if (Total > 0.1 * _timePlayers / _players) return true;
                    else return false;
                }
            }
            public override void PlayMatch(int falls)
            {
                if (falls < 0 || falls > 10) return;
                base.PlayMatch(falls);
                _timePlayers += falls;
            }
        }
   

    }

}



            
        
           
        