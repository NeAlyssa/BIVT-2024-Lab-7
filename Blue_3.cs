using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    public class Blue_3
    {
        // Базовый класс Участника
        public class Participant
        {
            private string _name;     // Имя участника
            private string _surname;  // Фамилия участника
            protected int[] _penaltyTimes; // Массив штрафных времен (пенальти)

            // Свойства для получения имени и фамилии
            public string Name => _name;
            public string Surname => _surname;

            // Свойство для получения массива штрафных времен (защита от изменения извне)
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

            // Свойство для вычисления общей суммы штрафных времен участника
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

            // Свойство для проверки, был ли участник удален из игры
            public virtual bool IsExpelled
            {
                get
                {
                    if (_penaltyTimes == null) return false;
                    for (int i = 0; i < _penaltyTimes.Length; i++)
                    {
                        if (_penaltyTimes[i] == 10) return true; // Удаление при штрафе в 10 минут
                    }
                    return false;
                }
            }

            // Конструктор класса участника
            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _penaltyTimes = new int[0]; // Изначально нет штрафов
            }

            // Метод для добавления штрафного времени участнику
            public virtual void PlayMatch(int time)
            {
                if (_penaltyTimes == null || time < 0) return;

                if (_penaltyTimes.Length == 0)
                {
                    _penaltyTimes = new int[] { time };
                }
                else
                {
                    int[] _array = new int[_penaltyTimes.Length + 1];
                    Array.Copy(_penaltyTimes, _array, _penaltyTimes.Length);
                    _array[_array.Length - 1] = time;
                    _penaltyTimes = _array;
                }
            }

            // Метод сортировки массива участников по сумме штрафных времен (сортировка вставками)
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

                // Гномья сортировка по штрафному времени (по возрастанию)
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

        // Класс Баскетболиста, наследуется от Участника
        public class BasketballPlayer : Participant
        {
            public BasketballPlayer(string name, string surname) : base(name, surname)
            {
                _penaltyTimes = new int[0]; // Изначально нет штрафов
            }

            // Переопределенный метод проверки на удаление
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

                    // Удаление игрока, если более 10% штрафов >= 5 или сумма штрафов > n * 2
                    if ((count * 100 / n) > 10 || this.Total > n * 2) return true;
                    return false;
                }
            }

            // Переопределенный метод добавления штрафа, ограничение: не более 5 за матч
            public override void PlayMatch(int countMatches)
            {
                if (countMatches < 0 || countMatches > 5) return;
                base.PlayMatch(countMatches);
            }
        }

        // Класс Хоккеиста, наследуется от Участника
        public class HockeyPlayer : Participant
        {
            private static int _numPlayer;     // Количество хоккеистов
            private static int _allPenaltyTime; // Общая сумма штрафных времен всех хоккеистов

            public HockeyPlayer(string name, string surname) : base(name, surname)
            {
                _penaltyTimes = new int[0];
                _numPlayer++; // Увеличиваем счетчик игроков
            }

            // Переопределенный метод проверки на удаление хоккеиста
            public override bool IsExpelled
            {
                get
                {
                    if (_penaltyTimes == null) return false;

                    int n = this.Penalties.Length;
                    for (int i = 0; i < n; i++)
                    {
                        if (this.Penalties[i] >= 10) return true; // Немедленное удаление при штрафе 10 мин
                    }

                    // Удаление игрока, если его суммарное штрафное время превышает 10% от среднего штрафа по команде
                    if (this.Total > 0.1 * _allPenaltyTime / _numPlayer) return true;
                    return false;
                }
            }

            // Переопределенный метод добавления штрафного времени хоккеисту
            public override void PlayMatch(int penaltyTime)
            {
                if (_penaltyTimes == null) return;
                if (penaltyTime < 0 || penaltyTime > 10) return;

                base.PlayMatch(penaltyTime);
                _allPenaltyTime += penaltyTime; // Учитываем общее штрафное время всех хоккеистов
            }
        }

    }
}
