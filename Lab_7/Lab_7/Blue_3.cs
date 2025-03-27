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
            //поля
            private string _name;
            private string _surname;
            protected int[] _penalties; 
            
            //свойства
            public string Name => _name;
            public string Surname => _surname;
            public int[] Penalties
            {
                get
                {
                    if (_penalties == null) return null;

                    int[] copy = new int[_penalties.Length];
                    for (int k = 0; k < copy.Length; k++)
                        copy[k] = _penalties[k];
                    return copy;
                }
            }
            public int Total
            {
                get
                {
                    if (_penalties == null) return 0;

                    int totalTime = 0;
                    for (int k = 0; k < _penalties.Length; k++) 
                        totalTime += _penalties[k];
                    return totalTime;
                }
            }
            public virtual bool IsExpelled
            {
                get
                {
                    if(_penalties == null) return false;
                    for (int k = 0; k < _penalties.Length; k++)
                    {
                        if (_penalties[k] == 10)
                            return true;
                    }
                    return false;
                }
            }

            //конструкторы
            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _penalties = new int[0];
            }

            //методы
            private void IncreaseCapasity()
            {
                if (_penalties == null || _penalties.Length == 0)
                {
                    _penalties = new int[1];
                    return;
                }

                int[] tmp = new int[_penalties.Length + 1];
                int k = 0;
                foreach (int time in _penalties)
                    tmp[k++] = time;
                _penalties = tmp;
            }

            public virtual void PlayMatch(int penalty)
            {
                if (_penalties == null) return;

                IncreaseCapasity();
                _penalties[_penalties.Length - 1] = penalty;    
            }
            public static void Sort(Participant[] array)
            {
                if (array == null || array.Length == 0) return;

                for (int i = 0; i < array.Length - 1; i++)
                {
                    for (int j = 0; j < array.Length - i - 1; j++)
                    {
                        if (array[j] == null) 
                        {
                            Participant tmp = array[j + 1];
                            array[j + 1] = array[j];
                            array[j] = tmp;
                        }
                        else if (array[j + 1] == null)
                            continue;
                        else if (array[j + 1].Total < array[j].Total) 
                        {
                            Participant tmp = array[j + 1];
                            array[j + 1] = array[j];
                            array[j] = tmp;
                        }
                    }
                }
            }
            public void Print()
            {
                Console.Write($"{_name} {_surname} ");
                if(this.IsExpelled == true)
                    Console.WriteLine("is exelled");
                Console.WriteLine("penalties:");
                for (int k = 0; k < _penalties.Length; k++)
                    Console.Write($"{_penalties[k],4}");
                Console.WriteLine();
                Console.WriteLine($"Total time - {Total}");
                Console.WriteLine();
            }
        }//class Participant

        public class BasketballPlayer: Participant
        {
            public override bool IsExpelled
            {
                get
                {
                    if (_penalties == null || _penalties.Length == 0) return false;
                    int numMatches = _penalties.Length;
                    int numFalledMatches = 0;
                    foreach (int penalty in _penalties)
                    {
                        if (penalty >= 5)
                            numFalledMatches++;
                    }
                    if (numFalledMatches > 0.1 * numMatches || this.Total >= 2 * numMatches)
                        return true;
                    return false;
                }
            }

            public BasketballPlayer(string name, string surname): base(name, surname)
            {
                _penalties = new int[0];
            }

            public override void PlayMatch(int fallNum)
            {
                if (fallNum < 0 || fallNum > 5)
                {
                    Console.WriteLine("incorrect attempt to record the number of falls\n");
                    return;
                }
                base.PlayMatch(fallNum);
            }
        } // class BasketballPlayer: Participant

        public class HockeyPlayer : Participant
        {
            private static int _hockeyPlayerNum = 0;
            private static int _penaltyTimeOfAllHockeyPlayers = 0;

            public override bool IsExpelled
            {
                get
                {
                    if(_penalties == null || _penalties.Length == 0) return false;
                    foreach(int penalty in _penalties)
                    {
                        if(penalty >= 10)
                            return true;
                    }
                    if( this.Total > 0.1 * _penaltyTimeOfAllHockeyPlayers/_hockeyPlayerNum)
                        return true;
                    return false;
                }
            }

            public HockeyPlayer(string name, string surname) : base(name, surname)
            {
                _penalties = new int[0];
                _hockeyPlayerNum++;
            }

            public override void PlayMatch(int penaltyTime)
            {
                if (_penalties == null) return;
                base.PlayMatch(penaltyTime);
                _penaltyTimeOfAllHockeyPlayers += penaltyTime;
            }
        } // class HockeyPlayer : Participant
    }
}
