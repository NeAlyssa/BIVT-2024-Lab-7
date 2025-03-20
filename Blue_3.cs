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
            protected int[] _penaltyTimes;

            //свойства
            public string Name => _name;
            public string Surname => _surname;
            public int[] Penalties
            {
                get
                {
                    if (_penaltyTimes == null) return default(int[]);
                    int[] newPenaltyTimes = new int[_penaltyTimes.Length];
                    Array.Copy(_penaltyTimes, newPenaltyTimes, _penaltyTimes.Length);
                    return newPenaltyTimes;
                }
            }
            //все время штрафных
            public int Total
            {
                get
                {
                    if (_penaltyTimes == null) return 0;
                    int count = 0;
                    for (int i = 0; i < _penaltyTimes.Length; i++)
                    {
                        count += _penaltyTimes[i];
                    }
                    return count;
                }
            }
            //есть или нет штрафа 10 мин(исключен или нет)
            public virtual bool IsExpelled
            {
                get
                {
                    if (_penaltyTimes == null || _penaltyTimes.Length == 0) return true;
                    for (int i = 0; i < _penaltyTimes.Length; i++)
                    {
                        if (_penaltyTimes[i] == 10) return true;
                    }
                    return false;
                }
            }
            //Конструктор
            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _penaltyTimes = new int[0];
            }
            //методы

            public virtual void PlayMatch(int time)
            {
                if (_penaltyTimes == null || time < 0) return;
                int[] New = new int[_penaltyTimes.Length + 1];
                Array.Copy(_penaltyTimes, New, _penaltyTimes.Length);
                New[_penaltyTimes.Length] = time;  //добавляем штрафной
                _penaltyTimes = New;

            }
            //сортируем по возрастанию по сумме штрафных
            public static void Sort(Participant[] array)
            {
                if (array == null) return;
                for (int i = 0; i < array.Length; i++)
                {
                    for (int j = 0; j < array.Length - i - 1; j++)
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
                Console.WriteLine($"{Name}\t{Surname}\t{Total}\tИсключен: {(IsExpelled ? "Да" : "Нет")}");
            }
        }
        public class BasketballPlayer : Participant
        {
            public BasketballPlayer(string name, string surname) : base(name, surname) { }
            public override bool IsExpelled
            {
                get
                {
                    if(Penalties ==  null) return false;
                    int countfall = 0;
                    foreach (var fall in Penalties)
                    {
                        if(fall == 5) countfall++;
                    }
                    if (countfall > Penalties.Length * 0.1 || Total > 2 * Penalties.Length) return true;
                    else return false;

                }
            }
            public override void PlayMatch(int falls)
            {
                if(falls < 0 || falls > 5) return;
                base.PlayMatch(falls);
            }
        }
        public class HockeyPlayer : Participant
        {
            //поля(статические, так как считаем для всего класса)
            private static int _timePlayers;
            private static int _players;
            //конструктор для статика
            static HockeyPlayer()
            { 
                _timePlayers = 0;
                _players = 0;
            }
            //конструктор
            public HockeyPlayer(string name, string surname) : base(name, surname) 
            {
                _players++;
            }
            //свойства
            public override bool IsExpelled
            {
                get
                {
                    if (Penalties == null) return false;
                    foreach (var fall in Penalties)
                    {
                        if (fall == 10) return true;
                    }
                    if(Total > 0.1 * _timePlayers / _players) return true;
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
