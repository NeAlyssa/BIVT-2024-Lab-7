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
            private string _name;
            private int[] _scores;

            public string Name => _name;
            public int[] Scores
            {
                get
                {
                    if (_scores == null) return null;
                    int[] newscores = new int[_scores.Length];
                    for (int i = 0; i < _scores.Length; i++)
                    {
                        newscores[i] = _scores[i];
                    }
                    return newscores;
                }
            }

            public int TotalScore
            {
                get
                {
                    if (_scores == null) return 0;
                    int total = 0;
                    foreach (var score in _scores)
                    {
                        total += score;
                    }
                    return total;
                }
            }

            public Team (string name)
            {
                _name = name;
                _scores = new int[0];
            }

            public void PlayMatch(int result)
            {
                if (_scores == null) return;
                int[] s = new int[_scores.Length + 1];

                for (int i = 0; i < s.Length - 1; i++)
                {
                    s[i] = _scores[i];
                }

                s[s.Length - 1] = result;
                _scores = s;
            }

            public void Print()
            {
                Console.WriteLine($"{_name},  счёт: {TotalScore}");
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
            private ManTeam[] _manTeams;
            private WomanTeam[] _womanTeams;
            private int _manTeamCount;
            private int _womanTeamCount;

            public string Name => _name;
            public ManTeam[] ManTeams => _manTeams;
            public WomanTeam[] WomanTeams => _womanTeams;

            public Group(string name)
            {
                _name = name;
                _manTeams = new ManTeam[12]; 
                _womanTeams = new WomanTeam[12]; 
                _manTeamCount = 0;
                _womanTeamCount = 0;
            }

            public void Add(Team team)
            {
                if (team is ManTeam manTeam)
                {
                    if (_manTeamCount < _manTeams.Length)
                    {
                        _manTeams[_manTeamCount++] = manTeam;
                    }
                }
                else if (team is WomanTeam womanTeam)
                {
                    if (_womanTeamCount < _womanTeams.Length)
                    {
                        _womanTeams[_womanTeamCount++] = womanTeam;
                    }
                }
            }

            public void Add(Team[] newTeams)
            {
                if (newTeams == null || newTeams.Length == 0) return;

                foreach (var team in newTeams)
                {
                    Add(team);
                }
            }

            

            private void SortTeams(Team[] teams, int count)
            {
                if (teams == null || count == 0) return;

                for (int i = 0; i < count - 1; i++)
                {
                    for (int j = 0; j < count - 1 - i; j++)
                    {
                        if (teams[j].TotalScore < teams[j + 1].TotalScore)
                        {
                            (teams[j], teams[j + 1]) = (teams[j + 1], teams[j]);
                        }
                    }
                }
            }
            public void Sort()
            {
                SortTeams(_manTeams, _manTeamCount);
                SortTeams(_womanTeams, _womanTeamCount);
            }

            public static Team[] Merging(Team[] group1, Team[] group2, int size)
            {
                if (group1 == null || group2 == null) return null;
                
                Team[] finalists = new Team[size];
                int count = 0;
                int h = Math.Min(size / 2, Math.Min(group1.Length, group2.Length));

                for (int i = 0, j = 0; count < size && (i < h || j < h);)
                {
                    if (i < h && (group1[i] != null && (j >= h || group1[i].TotalScore >= group2[j]?.TotalScore)))
                    {
                        finalists[count++] = group1[i++];
                    }
                    else if (j < h && (group2[j] != null && (i >= h || group2[j].TotalScore > group1[i]?.TotalScore)))
                    {
                        finalists[count++] = group2[j++];
                    }
                    else
                    {
                        if (i < h) i++;
                        if (j < h) j++;
                    }
                }

                return finalists;
            }

            public static Group Merge(Group group1, Group group2, int size)
            {
                Group group3 = new Group("Финалисты");
                group3.Add(Merging(group1.ManTeams, group2.ManTeams, size));
                group3.Add(Merging(group1.WomanTeams, group2.WomanTeams, size));
                return group3;
            }

            

            public void Print()
            {
                Console.WriteLine(_name);

                Console.WriteLine("Women:");
                for (int i = 0; i < _womanTeamCount; i++)
                {
                    _womanTeams[i].Print();
                }


                Console.WriteLine("men:");
                for (int i = 0; i < _manTeamCount; i++)
                {
                    _manTeams[i].Print();
                }


            }
        }
    }

}
