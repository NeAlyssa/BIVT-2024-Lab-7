using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
                    if (_penaltyTimes == null) return default(int[]);
                    //автоматическое определение типа переменной на основе присваиваемого значения
                    var penaltyTimes = new int[_penaltyTimes.Length];
                    //static method class Array
                    Array.Copy(_penaltyTimes, penaltyTimes, _penaltyTimes.Length);
                    //           from             where        length
                    return penaltyTimes;
                }
            }
            public int Total
            {
                get
                {
                    if (_penaltyTimes == null) return 0;
                    int totalTime = 0;
                    foreach (var x in _penaltyTimes)
                    {
                        totalTime += x;
                    }
                    return totalTime;
                }
            }
            public virtual bool IsExpelled  //!!!!
            {
                get
                {
                    if (_penaltyTimes == null) return false; 
                    foreach (int i in _penaltyTimes)
                    {
                        if (i == 10) return true;
                    }
                    return false;
                }
            }
            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _penaltyTimes = new int[0]; //не знаем размер(кол-во матчей)

            }
            //заполнение штрафного времени
            public virtual void PlayMatch(int time) //!!!!
            {
                if (_penaltyTimes == null) return;
                var newArray = new int[_penaltyTimes.Length + 1];
                Array.Copy(_penaltyTimes, newArray, _penaltyTimes.Length);
                //           from             where        length
                newArray[_penaltyTimes.Length] = time;
                _penaltyTimes = newArray;
            }
            public static void Sort(Participant[] array)//12345
            {
                if (array == null) return;
                for (int i = 0; i < array.Length; i++)
                {
                    for (int j = 0; j < array.Length - 1 - i; j++)
                    {
                        if (array[j].Total > array[j + 1].Total)
                        {
                            var temp = array[j];
                            array[j] = array[j + 1];
                            array[j + 1] = temp;
                        }
                    }
                }
            }
            public void Print()
            {
                Console.WriteLine($"{Name}\t{Surname}\t{Total}\t{IsExpelled}");
            }
        }

        public class BasketballPlayer : Participant
        {
            public BasketballPlayer(string name, string surname) : base(name, surname) { }
            //нужно переопределить свойства
            //kick if более 10% матчей с 5-ю фолами
            // у него суммарное количество фолов вдвое больше, чем количество матчей

            public override void PlayMatch(int foul)
            {
                if (_penaltyTimes == null) return;
                if (foul >= 0 && foul <= 5)
                {
                    var newArray = new int[_penaltyTimes.Length + 1];
                    Array.Copy(_penaltyTimes, newArray, _penaltyTimes.Length);
                    newArray[_penaltyTimes.Length] = foul;
                    _penaltyTimes = newArray;
                }
            }
            public override bool IsExpelled
            {
                get
                {
                    if (_penaltyTimes == null) return false;
                    if (Total > _penaltyTimes.Length * 2) return true;
                    int matchesWithFiveFouls = 0;
                    foreach (int fouls in _penaltyTimes)
                    {
                        if (fouls >= 5) matchesWithFiveFouls++;
                    }
                    if (matchesWithFiveFouls > _penaltyTimes.Length * 0.1) return true;
                    return false;
                }
            }

        }

        public class HockeyPlayer : Participant
        {
            private static int _totalPenaltyTimeOfAll;
            private static int _countOfAll;
            public HockeyPlayer(string name, string surname) : base(name, surname) 
            {
                _countOfAll++;
            }
            //нет модификатора доступа
            static HockeyPlayer()
            {
                _totalPenaltyTimeOfAll = 0;
                _countOfAll = 0;
            }
            public override void PlayMatch(int time)
            {
                base.PlayMatch(time);
                _totalPenaltyTimeOfAll += time;
            }
            public override bool IsExpelled
            {
                get
                {
                    if (_penaltyTimes == null) return false;
                    foreach(var time in _penaltyTimes)
                    {
                        if (time >= 10) return true;
                    }
                    if (_countOfAll > 0)
                    {
                        double avaragePenaltyTime = (double)_totalPenaltyTimeOfAll / _countOfAll;
                        if (Total > avaragePenaltyTime * 0.1) return true;
                    }
                    return false;
                }
            }

        }



    }
}
