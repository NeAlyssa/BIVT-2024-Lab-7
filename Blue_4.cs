using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Schema;
using static Lab_7.Blue_4;
using static Lab_7.Blue_4.ManTeam;

namespace Lab_7
{
    public class Blue_4
    {
        // Абстрактный класс команды
        public abstract class Team
        {

            private string _name; // Название команды
            private int[] _scores; // Массив очков команды

            // Свойство для получения названия команды
            public string Name { get { return _name; } }

            // Свойство для получения копии массива очков команды
            public int[] Scores
            {
                get
                {
                    if (_scores == null) return null;
                    int[] copy = new int[_scores.Length];
                    for (int i = 0; i < copy.Length; i++)
                    {
                        copy[i] = _scores[i];
                    }
                    return copy;
                }
            }
            // Свойство для подсчёта общего количества очков команды
            public int TotalScore
            {
                get
                {
                    if (_scores == null) return 0;
                    int count = 0;
                    for (int i = 0; i < _scores.Length; i++)
                    {
                        count += _scores[i];
                    }
                    return count;
                }
            }

            // Конструктор команды
            public Team(string name)
            {
                _name = name;
                _scores = new int[0];
            }

            // Метод для добавления результата матча
            public void PlayMatch(int result)
            {
                if (_scores == null || _scores.Length == 0)
                {
                    int[] _sc = new int[1];
                    _sc[0] = result;
                    _scores = new int[1];
                    Array.Copy(_sc, _scores, _sc.Length);
                }
                else
                {
                    int[] _sc = new int[_scores.Length + 1];
                    _sc[_sc.Length - 1] = result;
                    for (int i = 0; i < _scores.Length; i++)
                    {
                        _sc[i] = _scores[i];
                    }
                    _scores = new int[_sc.Length];
                    Array.Copy(_sc, _scores, _sc.Length);
                }
            }
            public void Print()
            {
                Console.WriteLine($"{_name} {TotalScore}");
            }
        }
        // Класс для группирования команд
        public class Group
        {

            private string _name; // Название группы
            private ManTeam[] _manTeams; // Массив мужских команд
            private WomanTeam[] _womanTeams; // Массив женских команд
            private int _countMan; // Количество мужских команд в группе
            private int _countWoman; // Количество женских команд в группе

            public string Name => _name; // Свойство для получения названия группы
            public ManTeam[] ManTeams => _manTeams; // Свойство для получения массива мужских команд
            public WomanTeam[] WomanTeams => _womanTeams; // Свойство для получения массива женских команд

            // Конструктор группы
            public Group(string name)
            {
                _name = name;
                _manTeams = new ManTeam[12];
                _womanTeams = new WomanTeam[12];
                _countMan = 0;
                _countWoman = 0;
            }

            // Метод для добавления команды в группу
            public void Add(Team team)
            {
                if (team == null) return;
                if (team is ManTeam)
                {
                    if (_countMan < _manTeams.Length)
                    {
                        ManTeam manTeam = team as ManTeam;
                        _manTeams[_countMan] = manTeam;
                        _countMan++;
                        return;
                    }
                }
                else if (team is WomanTeam)
                {
                    if (_countWoman < _womanTeams.Length)
                    {
                        WomanTeam womanTeam = team as WomanTeam;
                        _womanTeams[_countWoman] = womanTeam;
                        _countWoman++;
                        return;
                    }
                }
            }
            // Метод для добавления массива команд в группу
            public void Add(Team[] teams)
            {
                if (teams == null || _manTeams == null || _womanTeams == null || teams.Length == 0) return;
                for (int i = 0; i < teams.Length; i++)
                {
                    if (teams[i] == null) continue;
                    this.Add(teams[i]);
                }
            }
            // Метод сортировки команд в группе (гномья сортировка)
            private void SortTeam(Team[] teams, int countTeam)
            {
                if (teams == null || teams.Length == 0 || countTeam == 0) return;
                // гномья эффективная сортировка
                for (int i = 1, j = 2; i < countTeam;)
                {
                    if (i == 0 || teams[i - 1].TotalScore >= teams[i].TotalScore)
                    {
                        i = j;
                        j++;
                    }
                    else
                    {
                        Team temp = teams[i];
                        teams[i] = teams[i - 1];
                        teams[i - 1] = temp;
                        i--;
                    }
                }
            }
            // Метод сортировки всех команд в группе
            public void Sort()
            {
                SortTeam(_manTeams, _countMan);
                SortTeam(_womanTeams, _countWoman);
            }
            // Метод объединения двух массивов команд
            public static Team[] Merge(Team[] team1, Team[] team2, int size)
            {
                if (team1 == null || team2 == null) return null;
                Team[] result = new Team[size];
                int n = size / 2;
                int i = 0, j = 0, k = 0;
                while (i < n && j < n && team1[i] != null && team2[j] != null)
                {
                    if (team1[i].TotalScore >= team2[j].TotalScore)
                    {
                        result[k] = team1[i];
                        i++;
                        k++;
                    }
                    else
                    {
                        result[k] = team2[j];
                        j++;
                        k++;
                    }
                }
                while (i < n)
                {
                    result[k] = team1[i];
                    i++;
                    k++;
                }
                while (j < n)
                {
                    result[k] = team2[j];
                    j++;
                    k++;
                }
                return result;
            }
            // Метод для вывода информации о группе
            public static Group Merge(Group group1, Group group2, int size)
            {
                Group result = new Group("Финалисты");
                Team[] manTeam = Merge(group1._manTeams, group2._manTeams, size);
                Team[] WomanTeam = Merge(group1._womanTeams, group2._womanTeams, size);
                result.Add(manTeam);
                result.Add(WomanTeam);
                return result;
            }
            
            public void Print()
            {
                Console.WriteLine(_name);
                for (int k = 0; k < _countMan; k++)
                {
                    _manTeams[k].Print();
                }
                Console.WriteLine();
                for (int k = 0; k < _countMan; k++)
                {
                    _womanTeams[k].Print();
                }
            }
        }
        //  Класс мужской команды (наследуется от Team)
        public class ManTeam : Team
        {
            public ManTeam(string name) : base(name) { }
        }
        //Класс женской команды (наследуется от Team)
        public class WomanTeam : Team
        {
            public WomanTeam(string name) : base(name) { }
        }
    }
}