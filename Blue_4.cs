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
            //поля
            private string _name;
            private int[] _scores;
            //свойства
            public string Name => _name;
            public int[] Scores
            {
                get
                {
                    if (_scores == null) return default(int[]);
                    int[] newScores = new int[_scores.Length];
                    Array.Copy(_scores, newScores, _scores.Length);
                    return newScores;
                }
            }
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
            //конструктор
            public Team(string name)
            {
                _name = name;
                _scores = new int[0];
            }
            //методы
            public void PlayMatch(int result)
            {
                if (_scores == null) return;
                int[] New = new int[_scores.Length + 1];
                Array.Copy(_scores, New, _scores.Length);
                New[_scores.Length] = result;
                _scores = New;
            }
            public void Print()
            {
                Console.WriteLine($"{_name}\t{TotalScore}");

            }

        }
        public class Group
        {
            //конструктор
            private string _name;
            private Team[] _manteams;
            private Team[] _womanteams;
            private int _mancount;
            private int _womancount;

            //свойства
            public string Name => _name;
            public Team[] ManTeams => _manteams;
            public Team[]WomanTeams => _womanteams;

            //конструктор
            public Group(string name)
            {
                _name = name;
                _manteams = new Team[12];
                _womanteams = new Team[12];
                _womancount = 0;
                _mancount = 0;
            }
            //методы
            public void Add(Team team)
            {
                if (_manteams == null || _womanteams == null) return;
                if(team is ManTeam &&_mancount < 12)
                {
                    _manteams[_mancount] = team;
                    _mancount++;
                }
                else if(team is WomanTeam && _womancount < 12)
                {
                    _womanteams[_womancount] = team;
                    _womancount++;
                }
            }
            public void Add(Team[] teams)
            {
                if (_manteams == null || _womanteams == null) return;

                foreach(Team team in teams)
                {
                    Add(team);
                }
            }
            public void TeamSort()
            {
                Sort(_womanteams);
                Sort(_manteams);

            }
            public void Sort(Team[]team)
            {
                if (team == null) return;
                for (int i = 0; i < team.Length; i++)
                {
                    for (int j = 0; j < team.Length - i - 1; j++)
                    {
                        if (team[j] != null && team[j + 1] != null && team[j].TotalScore < team[j + 1].TotalScore)
                        {
                            var temp = team[j];
                            team[j] = team[j + 1];
                            team[j + 1] = temp;
                        }
                    }
                }
            }

            public static Group Merge(Group group1, Group group2, int size)
            {
                Group finaly = new Group("Финалисты");
                if (size <= 0) return default(Group);
                Group manTeam = TeamMerge(group1.ManTeams, group2.ManTeams, group1.ManTeams.Length + group2.ManTeams.Length);
                Group womanTeam = TeamMerge(group1.WomanTeams, group2.WomanTeams, group1.WomanTeams.Length + group2.WomanTeams.Length);
                finaly.Add(manTeam.ManTeams);
                finaly.Add(womanTeam.WomanTeams);
                return finaly;


            }
            public static Group TeamMerge(Team[]team1, Team[]team2, int size)
            {
                if (team1 == null || team2 == null) return null;
                Group finaly = new Group("Финалисты");
                int i = 0, j = 0;
                while (i < size / 2 && j < size / 2)
                {
                    if (team1[i] != null && team2[j] != null)
                    {
                        if (team1[i].TotalScore >= team2[j].TotalScore)
                        {
                            finaly.Add(team1[i++]);
                        }
                        else
                        {
                            finaly.Add(team2[j++]);
                        }
                    }
                }
                while (i < size / 2)
                    finaly.Add(team1[i++]);
                while (j < size / 2)
                    finaly.Add(team2[j++]);
                return finaly;

            }

            public void Print()
            {
                Console.WriteLine(_name);
                for (int k = 0; k < _mancount; k++)
                {
                    _manteams[k].Print();
                }
                Console.WriteLine();
                for (int k = 0; k < _mancount; k++)
                {
                    _womanteams[k].Print();
                }
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
    }
}
