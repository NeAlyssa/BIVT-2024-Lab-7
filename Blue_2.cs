using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    public class Blue_2
    {

        // Структура, представляющая участника соревнований
        public struct Participant
        {
            // Поля
            private string _name;       // Имя участника
            private string _surname;    // Фамилия участника
            private int[,] _marks;      // Оценки за два прыжка (2 строки, 5 столбцов)
            private int _cnt;           // Счетчик попыток (0 или 1)

            // Свойства
            public string Name => _name;
            public string Surname => _surname;

            // Свойство для получения копии массива оценок
            public int[,] Marks
            {
                get
                {
                    if (_marks == null) return null;
                    int[,] copy = new int[_marks.GetLength(0), _marks.GetLength(1)];
                    for (int i = 0; i < _marks.GetLength(0); i++)
                        for (int j = 0; j < _marks.GetLength(1); j++)
                            copy[i, j] = _marks[i, j];
                    return copy;
                }
            }

            // Свойство для вычисления общей суммы оценок
            public int TotalScore
            {
                get
                {
                    if (_marks == null) return 0;
                    int sum = 0;
                    for (int i = 0; i < _marks.GetLength(0); i++)
                        for (int j = 0; j < _marks.GetLength(1); j++)
                            sum += _marks[i, j];
                    return sum;
                }
            }

            // Конструктор, инициализирующий участника
            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new int[2, 5];  
                _cnt = 0;                // Начальное значение счетчика попыток
            }

            // Метод для добавления оценки за прыжок
            public void Jump(int[] result)
            {
                if (result == null || _marks == null || result.Length == 0) return;

                if (_cnt != 0 && _cnt != 1) return;  // Ограничение на 2 прыжка
                for (int j = 0; j < _marks.GetLength(1); j++)
                    _marks[_cnt, j] = result[j];

                _cnt++;  // Увеличиваем счетчик прыжков
            }

            // Метод сортировки участников по убыванию общего количества баллов (пузырьковая сортировка)
            public static void Sort(Participant[] array)
            {
                if (array == null || array.Length == 0) return;
                for (int i = 0; i < array.Length; i++)
                {
                    for (int j = 0; j < array.Length - i - 1; j++)
                    {
                        if (array[j].TotalScore < array[j + 1].TotalScore)
                        {
                            (array[j], array[j + 1]) = (array[j + 1], array[j]); // Обмен местами
                        }
                    }
                }
            }

            // Метод для вывода информации об участнике
            public void Print()
            {
                for (int i = 0; i < _marks.GetLength(0); i++)
                {
                    for (int j = 0; j < _marks.GetLength(1); j++)
                        Console.WriteLine(_marks[i, j]);

                    Console.WriteLine();
                }
                Console.WriteLine(TotalScore);
            }
        }

        // Абстрактный класс соревнований по прыжкам в воду
        public abstract class WaterJump
        {
            // Поля
            private string _name;                 // Название соревнования
            private int _bank;                    // Призовой фонд
            private Participant[] _participants;  // Участники соревнования

            // Свойства
            public string Name => _name;
            public int Bank => _bank;
            public Participant[] Participants => _participants;

            // Абстрактное свойство для получения призового распределения
            public abstract double[] Prize { get; }

            // Конструктор класса
            public WaterJump(string name, int bank)
            {
                _name = name;
                _bank = bank;
                _participants = new Participant[0];  // Изначально нет участников
            }

            // Метод добавления одного участника
            public void Add(Participant participants)
            {
                if (_participants == null) return;
                Array.Resize(ref _participants, _participants.Length + 1);
                _participants[_participants.Length - 1] = participants;
            }

            // Метод добавления массива участников
            public void Add(Participant[] participants)
            {
                if (participants == null || _participants == null) return;
                foreach (Participant p in participants)
                {
                    Add(p);
                }
            }
        }

        // Класс соревнования по прыжкам в воду с высоты 3 метра
        public class WaterJump3m : WaterJump
        {
            public WaterJump3m(string _name, int bank) : base(_name, bank) { }

            // Переопределенное свойство распределения призового фонда
            public override double[] Prize
            {
                get
                {
                    if (this.Participants == null || this.Participants.Length < 3)
                        return null; // Призовых мест должно быть минимум 3

                    double[] money = new double[3];
                    money[0] = 0.5 * this.Bank;  // 50% победителю
                    money[1] = 0.3 * this.Bank;  // 30% за 2-е место
                    money[2] = 0.2 * this.Bank;  // 20% за 3-е место

                    return money;
                }
            }
        }

        // Класс соревнования по прыжкам в воду с высоты 5 метров
        public class WaterJump5m : WaterJump
        {
            public WaterJump5m(string _name, int bank) : base(_name, bank) { }

            // Переопределенное свойство распределения призового фонда
            public override double[] Prize
            {
                get
                {
                    if (this.Participants == null || this.Participants.Length < 3)
                        return null; // Призовые места минимум 3

                    int mid = this.Participants.Length / 2;
                    double[] money = new double[Math.Min(mid, 10)];  // Количество призовых мест (максимум 10)
                    double N = 20.0 / Math.Min(mid, 10);             // Рассчитываем процент от фонда

                    // Основные призы
                    money[0] += 0.4 * this.Bank;  // 40% победителю
                    money[1] += 0.25 * this.Bank; // 25% за 2-е место
                    money[2] += 0.15 * this.Bank; // 15% за 3-е место

                    // Дополнительные призовые выплаты
                    for (int i = 0; i < money.Length; i++)
                    {
                        money[i] += N * this.Bank / 100;
                    }
                    return money;
                }
            }
        }

    }
}

