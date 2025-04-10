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

            // поля
            private string _name;
            private string _surname;
            protected int[] _penaltyTimes;

            // свойства
            public string Name { get { return _name; } }
            public string Surname { get { return _surname; } }
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

                    int res = 0;
                    foreach (var item in _penaltyTimes)
                    {
                        res += item;
                    }
                    return res;
                }
            }
            public virtual bool IsExpelled
            {
                get
                {
                    if (_penaltyTimes == null || _penaltyTimes.Length == 0) return false;
                    
                    for (int i = 0; i < _penaltyTimes.Length; i++)
                    {
                        if (_penaltyTimes[i] == 10)
                            return true;
                    }
                    return false;
                }
            }

            // конструктор
            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _penaltyTimes = new int[0];
            }

            // методы
            // добавляет в массив штрафов штрафное время в очередном матче
            public virtual void PlayMatch(int time)
            {
                if (_penaltyTimes == null) return;

                int[] newArr = new int[_penaltyTimes.Length + 1];
                for (int i = 0; i < newArr.Length - 1; i++)
                {
                    newArr[i] = _penaltyTimes[i];
                }

                newArr[newArr.Length - 1] = time;
                _penaltyTimes = newArr;
            }

            // по возрастанию общего штрафного времени спортсмена
            public static void Sort(Participant[] array)
            {
                if (array == null || array.Length == 0) return;

                for (int i = 0; i < array.Length; i++)
                    for (int j = 0; j < array.Length - i - 1; j++)
                        if (array[j].Total > array[j + 1].Total)
                        {
                            var temp = array[j];
                            array[j] = array[j + 1];
                            array[j + 1] = temp;
                        }
            }

            public void Print()
            {
                Console.WriteLine($"{_name} {_surname} - Общее штрафное время: {Total} исключение спортсмена: {IsExpelled}");
            }
        }

        public class BasketballPlayer : Participant
        {
            public override bool IsExpelled
            {
                get
                {
                    if (_penaltyTimes == null || _penaltyTimes.Length == 0) return false;

                    int matches = _penaltyTimes.Length, lostMatches = 0;
                    foreach (int pen in _penaltyTimes)
                        if (pen >= 5)
                            lostMatches++;

                    if (lostMatches > 0.1 * matches || Total > 2 * matches) return true;

                    return false;
                }
            }

            public BasketballPlayer(string name, string surname) : base(name, surname) { }

            public override void PlayMatch(int fallCnt)
            {
                if (fallCnt < 0 || fallCnt > 5) return;

                base.PlayMatch(fallCnt);
            }
        }

        public class HockeyPlayer : Participant
        {
            private static int _playerCnt = 0;
            private static int _penaltyAllCnt = 0;

            public override bool IsExpelled
            {
                get
                {
                    if (_penaltyTimes == null || _penaltyTimes.Length == 0) return false;
                    if (_playerCnt == 0) return false;

                    foreach (var pen in _penaltyTimes)
                        if (pen >= 10) return true;

                    if (Total > (_penaltyAllCnt / _playerCnt) * 0.1) return true;

                    return false;
                }
            }

            public HockeyPlayer(string name, string surname) : base(name, surname)
            {
                _playerCnt++;
            }

            public override void PlayMatch(int time)
            {
                if (_penaltyTimes == null) return;

                base.PlayMatch(time);
                _penaltyAllCnt += time;
            }
        }
    }
}
