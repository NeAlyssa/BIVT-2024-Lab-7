using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    public class Blue_4
    {
        public abstract class Team
        {
            // поля
            private string _name;
            private int[] _scores;

            // свойства
            public string Name { get { return _name; } }
            public int[] Scores
            {
                get
                {
                    if (_scores == null) return null;

                    int[] copy = new int[_scores.Length];
                    for (int i = 0; i < copy.Length; i++)
                        copy[i] = _scores[i];

                    return copy;
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
            public void PlayMatch(int result)
            {
                if (_scores == null) return;

                int[] newscores = new int[_scores.Length + 1];
                for (int i = 0; i < _scores.Length; i++)
                    newscores[i] = _scores[i];

                newscores[newscores.Length - 1] = result;
                _scores = newscores;
            }

            public void Print()
            {
                Console.WriteLine($"{Name}: {TotalScore}");
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
            // поля
            private string _name;
            private ManTeam[] _manTeams; 
            private WomanTeam[] _womanTeams;
            private int _countM;
            private int _countW;

            // свойства
            public string Name { get { return _name; } }
            public Team[] ManTeams
            {
                get
                {
                    if (_manTeams == null) return null;
                    return _manTeams;
                }
            }
            public Team[] WomanTeams
            {
                get
                {
                    if (_womanTeams == null) return null;
                    return _womanTeams;
                }
            }

            // конструктор
            public Group(string name)
            {
                _name = name;
                _manTeams = new ManTeam[12];
                _womanTeams = new WomanTeam[12];
                _countM = 0;
                _countW = 0;
            }

            // методы
            public void Add(Team team)
            {
                if (team == null) return;

                ManTeam mTeam = team as ManTeam;
                if (mTeam != null)
                {
                    if (_countM < _manTeams.Length)
                    {
                        _manTeams[_countM] = mTeam;
                        _countM++;
                        return;
                    }
                }

                WomanTeam wTeam = team as WomanTeam;
                if (wTeam != null)
                {
                    if (_countW < _womanTeams.Length)
                    {
                        _womanTeams[_countW] = wTeam;
                        _countW++;
                        return;
                    }
                }

            }

            public void Add(Team[] teams)
            {
                if (_manTeams == null || _womanTeams == null || teams == null || teams.Length == 0) return;
                foreach (var team in teams)
                {
                    if (team == null) continue;

                    Add(team);
                }
            }


            // по убыванию
            public void SortProcess(Team[] teams)
            {
                if (teams == null || teams.Length == 0) return;

                for (int i = 0; i < teams.Length; i++)
                    for (int j = 0; j < teams.Length - i - 1; j++)
                        if (teams[j].TotalScore < teams[j + 1].TotalScore)
                            (teams[j], teams[j + 1]) = (teams[j + 1], teams[j]);

            }

            public void Sort()
            {
                SortProcess(_womanTeams);
                SortProcess(_manTeams);
            }

            // слияние двух отсортированных массивов команд двух групп в новую группу с ограничением по размеру
            public static Group MergeProcess(Team[] team1, Team[] team2, int size)
            {
                if (size <= 0) return null;

                Group result = new Group("Финалисты");
                int index1 = 0, index2 = 0;

                while (index1 < size / 2 && index2 < size / 2)
                {
                    if (team1[index1].TotalScore >= team2[index2].TotalScore)
                    {
                        result.Add(team1[index1]);
                        index1++;
                    }
                    else
                    {
                        result.Add(team2[index2]);
                        index2++;
                    }
                }

                while (index1 < size / 2)
                    result.Add(team1[index1++]);

                while (index2 < size / 2)
                    result.Add(team2[index2++]);

                return result;
            }

            public static Group Merge(Group group1, Group group2, int size)
            {
                if (size <= 0) return null;

                Group result = new Group("Финалисты");
                group1.Sort();
                group2.Sort();

                Group Man = MergeProcess(group1.ManTeams, group2.ManTeams, size);
                Group Woman = MergeProcess(group1.WomanTeams, group2.WomanTeams, size);

                result.Add(Man.ManTeams);
                result.Add(Woman.WomanTeams);
                return result;
            }

            public void Print()
            {
                Console.WriteLine(_name);
                foreach (Team p in _manTeams)
                    p.Print();

                Console.WriteLine();

                foreach (Team p in _womanTeams)
                    p.Print();

                Console.WriteLine();
            }
        }
    }
}
