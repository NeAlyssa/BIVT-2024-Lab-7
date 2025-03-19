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
                    int[]copyScores=new int[_scores.Length];
                    Array.Copy(_scores, copyScores, _scores.Length);
                    return copyScores;
                }
            }
            public int TotalScore
            {
                get
                {
                    if (_scores == null || _scores.Length==0) return 0;
                    int score = 0;
                    for (int i=0; i < _scores.Length; i++)
                    {
                        score+= _scores[i];
                    }
                    return score;
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
            public ManTeam[] ManTeams => _manteams;
            public WomanTeam[] WomanTeams => _womanteams;

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

                if (team is ManTeam)
                {
                    if (_manteams != null && _manCnt < 12 && team != null)
                    {
                        _manteams[_manCnt] = team as ManTeam;
                        _manCnt++;
                    }
                    else return;
                }
                else if (team is WomanTeam)
                {
                    if (_womanteams != null && _womanCnt < 12 && team != null)
                    {
                        _womanteams[_womanCnt] = team as WomanTeam;
                        _womanCnt++;
                    }
                    else return;
                }
                //ManTeam manTeam = team as ManTeam;
                //if (manTeam!=null && _manteams != null && _manCnt < _manteams.Length) //проверяем заполненность массива и добавляем команду в массив
                //{
                //        _manteams[_manCnt] = manTeam;
                //        _manCnt++;
                //}
                //else return;

                //WomanTeam womanTeam = team as WomanTeam;
                //if (womanTeam!=null && _womanteams != null && _womanCnt < _womanteams.Length) //проверяем заполненность массива и добавляем команду в массив
                //{
                //    _womanteams[_womanCnt] = womanTeam;
                //    _womanCnt++;
                //}
                //else return;
            }
            public void Add(Team[] teams) //добавление массива объектов типа Тим в массив тимс
            {
                if (teams.Length == 0 || teams == null) return;
                foreach (Team team in teams)
                {
                    Add(team);
                }
            }
          
            private void SortTeams(Team[] teams)
            {
                if (teams == null) return;
                if (teams.Length <= 1) return;
                for (int i = 0; i < teams.Length-1; i++)
                {
                    for (int j = 0; j < teams.Length - i - 1; j++)
                    {
                        if (teams[j].TotalScore < teams[j + 1].TotalScore)
                        {
                            (teams[j + 1], teams[j]) = (teams[j], teams[j + 1]);
                        }
                    }
                }
            }
            public void Sort() //пузырьком <3 по убыванию суммарных очков
            {
                if (_manteams == null || _womanteams == null) return;
                SortTeams(_manteams);
                SortTeams(_womanteams);
            }
            public static Group Merge(Group group1, Group group2, int size) // сайз-ограничение по размеру (6) слияние массивов в новую группу
            { 
                if (size<1) return null;
                Group result = new Group("Финалисты");
                group1.Sort();
                group2.Sort();
                Group manTeam=MergeTeams(group1.ManTeams, group2.ManTeams, group1.ManTeams.Length+group2.ManTeams.Length);
                Group womanTeam = MergeTeams(group1.WomanTeams, group2.WomanTeams, group1.WomanTeams.Length + group2.WomanTeams.Length);

                result.Add(manTeam.ManTeams);
                result.Add(womanTeam.WomanTeams);

                return result;

            }

            private static Group MergeTeams(Team[] firstteam, Team[] secondteam, int size)
            {
                if (firstteam==null || secondteam==null || firstteam.Length == 0 || secondteam.Length == 0 || size<=0) return null;
                Group result = new Group("Финалисты");

                int i = 0, j = 0;
                while (i < (size/2) && j < (size/2))
                {
                    if (firstteam[i].TotalScore >= secondteam[j].TotalScore)
                    {
                        result.Add(firstteam[i++]);
                    }
                    else
                    {
                        result.Add(secondteam[j++]);
                    }
                }
                while (i < size/2)
                {
                    result.Add(firstteam[i++]);
                }
                while (j < size/2)
                {
                    result.Add(secondteam[j++]);
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
