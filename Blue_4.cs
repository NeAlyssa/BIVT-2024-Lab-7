using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Lab_7.Blue_4;

namespace Lab_7
{
    public class Blue_4 //две группы по 12 команд
    {
        public abstract class Team // структура
        {
            private string _name;
            private int[] _scores; //очки за матчи

            // свойства
            public string Name => _name;
            public int[] Scores
            {
                get
                {
                    if (_scores == null) return null;
                    return _scores; //чтобы массив дальше менялся, а не его копия
                }
            }
            public int TotalScore
            {
                get
                {
                    if (_scores == null) return 0;
                    return _scores.Sum();
                }
            }


            // конструктор
            public Team(string name)
            {
                _name = name;
                _scores = new int[0];
            }

            // методы 

            public void PlayMatch(int result) //добавляем результат матча
            {
                if (_scores == null) return;
                int[] newscores = new int[_scores.Length + 1];
                for (int i = 0; i < _scores.Length; i++)
                {
                    newscores[i] = _scores[i];
                }
                newscores[newscores.Length - 1] = result;
                _scores = newscores;
            }

            public void Print()
            {
                Console.WriteLine(Name + " " + TotalScore);
            }
        }

        public class ManTeam : Team
        {
            public ManTeam(string name) : base(name) { }
        }

        public class WomanTeam : Team
        {
            public WomanTeam(string name) : base(name) { }
        }

        public class Group 
        {
            private string _name;
            private ManTeam[] _manteams;
            private int _manCnt; //счетчик команд для добавления
            private WomanTeam[] _womanteams;
            private int _womanCnt; //счетчик команд для добавления

            // свойства
            public string Name => _name;
            public ManTeam[] ManTeams
            {
                get
                {
                    if (_manteams == null) return null;
                    return _manteams;
                }
            }
            public WomanTeam[] WomanTeams
            {
                get
                {
                    if (_womanteams == null) return null;
                    return _womanteams;
                }
            }

            // конструктор

            public Group(string name)
            {
                _name = name;
                _manteams = new ManTeam[12];
                _manCnt = 0;
                _womanteams = new WomanTeam[12];
                _womanCnt = 0;
            }

            //методы

            public void Add(Team team) //добавление объекта типа Тим в массив тимс
            {
                if (team == null) return;

                ManTeam manTeam = team as ManTeam;
                if (manTeam != null && _manCnt < _manteams.Length) //проверяем заполненность массива и добавляем команду в массив
                { 
                     _manteams[_manCnt] = manTeam;
                     _manCnt++;
                }
                else return;

                WomanTeam womanTeam = team as WomanTeam;
                if (womanTeam != null && _womanCnt < _womanteams.Length) //проверяем заполненность массива и добавляем команду в массив
                {
                    _womanteams[_womanCnt] = womanTeam;
                    _womanCnt++;
                }
                else return;
            }
            public void Add(Team[] teams) //добавление массива объектов типа Тим в массив тимс
            {
                if (_manteams == null || _womanteams==null || teams.Length == 0 || teams == null) return;
                foreach (var team in teams)
                {
                    if (_manCnt < _manteams.Length)
                    {
                        ManTeam manTeam = team as ManTeam;
                        if (manTeam != null)
                        {
                            Add(manTeam);
                            continue;
                        }
                    }
                    if (_womanCnt < _womanteams.Length)
                    {
                        WomanTeam womanTeam = team as WomanTeam;
                        if (womanTeam != null)
                        {
                            Add(womanTeam);
                            continue;
                        }
                    }
                }
            }
            public void Sort() //пузырьком <3 по убыванию суммарных очков
            {
                if (_manteams == null || _womanteams == null || _manteams.Length == 0 || _womanteams.Length==0) return;
                SortTeams(_manteams);
                SortTeams(_womanteams);
            }
            private void SortTeams(Team[] teams)
            {
                for (int i = 0; i < teams.Length; i++)
                {
                    for (int j = 0; j < teams.Length - i - 1; j++)
                    {
                        if (teams[j].TotalScore < teams[j + 1].TotalScore)
                        {
                            var temp = teams[j];
                            teams[j] = teams[j + 1];
                            teams[j + 1] = temp;
                        }
                    }
                }
            }
            public static Group Merge(Group group1, Group group2, int size) // сайз-ограничение по размеру (6) слияние массивов в новую группу
            { 
                Group result = new Group("Финалисты");
                Team[] manTeam=MergeTeams(group1._manteams, group2._manteams, size);
                Team[] womanTeam = MergeTeams(group1._womanteams, group2._womanteams, size);

                result.Add(manTeam);
                result.Add(womanTeam);
                return result;

            }

            private static Team[] MergeTeams(Team[] firstteam, Team[] secondteam, int size)
            {
                if (firstteam==null || secondteam==null || firstteam.Length == 0 || secondteam.Length == 0 || size<=0) return null;
                Team[] result = new Team[size];
                int group1Count = Math.Min(size / 2, firstteam.Length);
                int group2Count = Math.Min(size - group1Count, secondteam.Length);

                int i = 0, j = 0,k=0;
                while (i < group1Count && j < group2Count)
                {
                    if (firstteam[i].TotalScore >= secondteam[j].TotalScore)
                    {
                        result[k++] = firstteam[i++];
                    }
                    else
                    {
                        result[k++]=secondteam[j++];
                    }
                }
                while (i < group1Count)
                {
                    result[k++] = firstteam[i++];
                }
                while (j < group2Count)
                {
                    result[k++] = secondteam[j++];
                }
                return result;
            }

            public void Print()
            {
                Console.WriteLine(_name);
                foreach (Team x in _manteams)
                {
                    x.Print();
                }
                Console.WriteLine();
                foreach (Team x in _womanteams)
                {
                    x.Print();
                }
                Console.WriteLine();
            }
        }

    }
}
