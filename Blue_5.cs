using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    public class Blue_5
    {
        // Класс, представляющий спортсмена
        public class Sportsman
        {
            private string _name;    // Имя спортсмена
            private string _surname; // Фамилия спортсмена
            private int _place;      // Место, занятое в соревновании (по умолчанию 0)

            // Свойства для доступа к полям
            public string Name => _name;
            public string Surname => _surname;
            public int Place => _place;

            // Конструктор для инициализации имени и фамилии спортсмена
            public Sportsman(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _place = 0; // По умолчанию место еще не установлено
            }

            // Метод для установки занятого места (если место еще не задано)
            public void SetPlace(int place)
            {
                if (_place > 0) return; // Место можно установить только один раз
                _place = place;
            }

            // Метод для вывода информации о спортсмене
            public void Print()
            {
                Console.WriteLine($"Name: {_name}, Surname: {_surname}, Place: {_place}");
            }
        }

        // Абстрактный класс команды
        public abstract class Team
        {
            private string _name;              // Название команды
            private Sportsman[] _sportsmen;    // Массив спортсменов
            private int _curInd;               // Текущий индекс для добавления спортсменов

            public string Name => _name;       // Свойство для доступа к названию команды
            public Sportsman[] Sportsmen => _sportsmen; // Свойство для получения списка спортсменов

            // Свойство для подсчета общего количества очков команды
            public int SummaryScore
            {
                get
                {
                    if (_sportsmen == null) return 0;
                    int sum = 0;

                    // Проходим по всем спортсменам и суммируем их очки в зависимости от занятого места
                    for (int i = 0; i < _sportsmen.Length; i++)
                    {
                        if (_sportsmen[i] == null) continue;
                        switch (_sportsmen[i].Place)
                        {
                            case 1: sum += 5; break;
                            case 2: sum += 4; break;
                            case 3: sum += 3; break;
                            case 4: sum += 2; break;
                            case 5: sum += 1; break;
                            default: sum += 0; break;
                        }
                    }
                    return sum;
                }
            }

            // Свойство для определения лучшего (минимального) занятого места в команде
            public int TopPlace
            {
                get
                {
                    if (_sportsmen == null) return 0;
                    int min = int.MaxValue;

                    foreach (Sportsman sportsman in _sportsmen)
                    {
                        if (sportsman == null) continue;
                        if (sportsman.Place < min && sportsman.Place > 0)
                            min = sportsman.Place;
                    }
                    return min;
                }
            }

            // Конструктор класса
            public Team(string name)
            {
                _name = name;
                _sportsmen = new Sportsman[6]; // Максимальное число спортсменов в команде - 6
                _curInd = 0;
            }

            // Метод для добавления спортсмена в команду
            public void Add(Sportsman sportsman)
            {
                if (_sportsmen == null || sportsman == null || _curInd >= _sportsmen.Length) return;
                _sportsmen[_curInd++] = sportsman;
            }

            // Перегруженный метод для добавления массива спортсменов
            public void Add(Sportsman[] sportsmen)
            {
                if (sportsmen == null || _sportsmen == null) return;
                foreach (var sportsman in sportsmen)
                    Add(sportsman);
            }

            // Метод для сортировки команд по очкам и лучшему месту
            public static void Sort(Team[] teams)
            {
                if (teams == null || teams.Length == 0) return;

                for (int i = 0; i < teams.Length; i++)
                {
                    for (int j = 0; j < teams.Length - i - 1; j++)
                    {
                        if (teams[j].SummaryScore < teams[j + 1].SummaryScore ||
                            (teams[j].SummaryScore == teams[j + 1].SummaryScore && teams[j].TopPlace > teams[j + 1].TopPlace))
                        {
                            (teams[j], teams[j + 1]) = (teams[j + 1], teams[j]); // Обмен местами
                        }
                    }
                }
            }

            // Метод для вывода информации о команде
            public void Print()
            {
                Console.WriteLine($"Name: {_name}, Summary score: {SummaryScore}, Top Place: {TopPlace}");
                foreach (var sportsman in _sportsmen)
                {
                    sportsman.Print();
                }
                Console.WriteLine();
            }

            // Абстрактный метод для вычисления силы команды (реализуется в наследниках)
            protected abstract double GetTeamStrength();

            // Метод для определения чемпиона среди команд
            public static Team GetChampion(Team[] teams)
            {
                if (teams == null || teams.Length == 0)
                    return null;

                Team winner = null;
                double maxStrength = double.MinValue;

                foreach (Team team in teams)
                {
                    if (team != null)
                    {
                        double strength = team.GetTeamStrength();
                        if (strength > maxStrength)
                        {
                            maxStrength = strength;
                            winner = team;
                        }
                    }
                }
                return winner;
            }
        }

        // Класс команды мужчин
        public class ManTeam : Team
        {
            public ManTeam(string name) : base(name) { }

            // Реализация метода для вычисления силы команды мужчин
            protected override double GetTeamStrength()
            {
                if (Sportsmen == null || Sportsmen.Length == 0)
                    return 0;

                double totalPlace = 0;
                int count = 0;

                for (int i = 0; i < Sportsmen.Length; i++)
                {
                    Sportsman sportsman = Sportsmen[i];

                    if (sportsman != null && sportsman.Place > 0)
                    {
                        totalPlace += sportsman.Place;
                        count++;
                    }
                }

                if (count == 0)
                    return 0;

                double averagePlace = totalPlace / count;
                return 100.0 / averagePlace; // Чем ниже среднее место, тем выше сила команды
            }
        }

        // Класс команды женщин
        public class WomanTeam : Team
        {
            public WomanTeam(string name) : base(name) { }

            // Реализация метода для вычисления силы команды женщин
            protected override double GetTeamStrength()
            {
                if (Sportsmen == null || Sportsmen.Length == 0)
                    return 0;

                double sumPlace = 0;
                double multPlace = 1;
                int count = 0;

                for (int i = 0; i < Sportsmen.Length; i++)
                {
                    Sportsman sportsman = Sportsmen[i];

                    if (sportsman != null && sportsman.Place > 0)
                    {
                        sumPlace += sportsman.Place;
                        multPlace *= sportsman.Place;
                        count++;
                    }
                }

                if (count == 0 || multPlace == 0)
                    return 0;

                return (100.0 * sumPlace * count) / multPlace; // Формула для расчета силы женской команды
            }
        }
    }
}
