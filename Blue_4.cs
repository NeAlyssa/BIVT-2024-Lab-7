using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lab_7.Blue_4;
using static Lab_7.Blue_5;

namespace Lab_7
{
    public class Blue_4
    {
        public abstract class Team 
        {
            private string _name;
            private int[] _scores;

            // свойства
            public string Name => _name;
            public int[] Scores => _scores;
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

            public void PlayMatch(int result) // добавляет в массив игр результат очередного матча
            {
                if (_scores == null) return;
                int[] t = new int[_scores.Length + 1];
                for(int i = 0; i < _scores.Length; i++)
                {
                    t[i] = _scores[i];
                }
                t[t.Length - 1] = result;
                _scores = t;
            }

            public void Print()
            {
                Console.WriteLine($"{Name}: {TotalScore}");
            }
        }

        public class ManTeam : Team
        {
            public ManTeam(string name) : base(name)
            {
            }
        }

        public class WomanTeam : Team
        {
            public WomanTeam(string name) : base(name)
            {
            }
        }


        public class Group
        {
            private string _name;
            private ManTeam[] _manteams;
            private WomanTeam[] _womanteams;
            private int _index; //индекс команды
            private int _manind;
            private int _womanind;

            // свойства
            public string Name => _name;
            public ManTeam[] ManTeams => _manteams;
            public WomanTeam[] WomanTeams => _womanteams;


            // конструктор

            public Group(string name)
            {
                _name = name;
                _manteams = new ManTeam[12];
                _womanteams = new WomanTeam[12];
                _index = 0;
                _manind = 0;
                _womanind = 0;
            }

            //методы

            public void Add(Team team) // одна команда в группу
            {
                if (team == null || _manteams == null || _womanteams == null) return;

                if (team is ManTeam manteam)
                {
                    if (_manind < _manteams.Length)
                    {
                        _manteams[_manind] = manteam;
                        _manind++;
                    }
                }
                else if (team is WomanTeam womanteam)
                {
                    if (_womanind < _womanteams.Length)
                    {
                        _womanteams[_womanind++] = womanteam;
                        _womanind++;
                    }
                }
            }
            public void Add(Team[] teams) // несколько
            {
                if (_manteams == null || _womanteams == null) return;

                foreach (var team in teams)
                {
                    Add(team);
                }
            }
            public void Sort()
            {
                if (_manteams == null || _womanteams == null) return;
                // делаем сортировку
                SortOneTeam(_manteams);
                SortOneTeam(_womanteams);

            }

            private void SortOneTeam(Team[] team)
            {
                for (int i = 0; i < team.Length; i++)
                {
                    for (int j = 0; j < team.Length - i - 1; j++)
                    {
                        if (team[j].TotalScore < team[j + 1].TotalScore)
                        {       
                            (team[j], team[j + 1]) = (team[j + 1], team[j]);
                        }
                    }
                }
            }
            public static Group Merge(Group group1, Group group2, int size) // слияния двух отсортированных массивов команд двух групп в новую группу с ограничением по размеру
            {
                Group resultat = new Group("Финалисты");
                Team[] mans = OneMerge(group1._manteams, group2._manteams, size);
                Team[] womans = OneMerge(group1._womanteams, group2._womanteams, size);

                resultat.Add(mans);
                resultat.Add(womans);
                return resultat;

            }
            private static Team[] OneMerge(Team[] group1, Team[] group2, int size) // слияния двух отсортированных массивов команд двух групп в новую группу с ограничением по размеру
            {
                Team[] resultat = new Team[size];
                int i = 0, k = 0, h = 0, j = 0, n = 0;
                while (i < size && j < size)
                {
                    if (group1[i].TotalScore >= group2[j].TotalScore)
                    {
                        resultat[n] = group1[i] ;
                        i++;
                        n++;
                    }
                    else
                    {
                        resultat[n] = group2[j];
                        n++;
                        j++;
                    }
                }
                while (k < size)
                {
                    resultat[n] = group1[k];
                    k++;
                    n++;
                }
                while (h < size)
                {
                    resultat[n] = group2[h];
                    h++;
                    n++;
                }
                return resultat;
            }

            public void Print()
            {
                Console.WriteLine(_name);

                foreach(Team i in _manteams)
                {
                    i.Print();
                }
                foreach (Team i in _womanteams)
                {
                    i.Print();
                }

                Console.WriteLine();
            }
        }
    }
}
