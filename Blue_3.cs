using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lab_6
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
            public int[] PenaltyTimes
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
           
          
            public int TotalTime
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
           
            //конструктор
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
                newarray[_penaltyTimes.Length] = time;
                _penaltyTimes = newarray;
            }

            public static void Sort(Participant[] array)
            {
                if (array == null || array.Length == 0) return;

                for (int i = 0; i < array.Length; i++)
                {
                    for (int j = 0; j < array.Length - i - 1; j++)
                    {
                        if (array[j].TotalTime > array[j + 1].TotalTime)
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

                Console.WriteLine($"Общее штрафное время: {TotalTime}");

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
                    if (_penaltyTimes == null) return false;
                    int k = 0;
                    foreach (var falls in _penaltyTimes)
                    {
                        if (falls == 5) k++;
                    }
                    if (k > _penaltyTimes.Length * 0.1 || TotalTime > _penaltyTimes.Length * 2) return true; 
                    return false;
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
           
            private static int players = 0;
            private static double Time = 0;
            public HockeyPlayer(string name, string surname) : base(name, surname)
            {
                players++;
            }
           
            public override bool IsExpelled
            {
                get
                {
                    if (_penaltyTimes == null) return false;
                    foreach (int t in _penaltyTimes)
                    {
                        if (t == 10) return true;
                    }
                   
                    if (TotalTime > Time / players * 0.1) return true; 
                    return false;
                }
            }

            public override void PlayMatch(int count)
            {
                if (_penaltyTimes == null) return;
                base.PlayMatch(count);
                Time += count;

            }
        }

        }

    }




