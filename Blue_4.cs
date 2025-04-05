
using System;

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
                    int[] newarr = new int[_scores.Length];
                    for (int i = 0; i < newarr.Length; i++)
                    {
                        newarr[i] = _scores[i];
                    }
                    return newarr;
                }
            }
            public int TotalScore
            {
                get
                {
                    int time;
                    time = 0;
                    if (_scores == null) return 0;
                    for (int i = 0; i < _scores.Length; i++)
                    {
                        time += _scores[i];
                    }
                    return time;
                }
            }

            public Team(string Name)
            {
                _name = Name;
                _scores = new int[0];
            }

            public void PlayMatch(int result)
            {
                if (_scores == null) return;

                var newarr = new int[_scores.Length + 1];
                for (int i = 0; i < _scores.Length; i++)
                {
                    newarr[i] = _scores[i];
                }
                newarr[newarr.Length - 1] = result;
                _scores = newarr;
            }

            public void Print()
            {
                Console.WriteLine($"{_name} {TotalScore}");
            }
        }
        public class ManTeam : Team
        {
            public ManTeam(string Name) : base(Name) { }
        }
        public class WomanTeam : Team
        {
            public WomanTeam(string Name) : base(Name) { }
        }

        public class Group
        {

            private string _name;
            private ManTeam[] _ManTeams;
            private WomanTeam[] _WomanTeams;
            private int _countM;
            private int _countW;

            public string Name => _name;

            public Team[] ManTeams => _ManTeams;
            public Team[] WomanTeams => _WomanTeams;
            public int countM => _countM;
            public int countW => _countW;

            public Group(string Name)
            {
                _name = Name;
                _ManTeams = new ManTeam[12];
                _WomanTeams = new WomanTeam[12];
                _countM = 0;
                _countW = 0;
            }

            public void Add(Team team)
            {
                if (team == null || _ManTeams == null || _WomanTeams == null) return;
                if (team is ManTeam)
                {
                    if (_countM < _ManTeams.Length)
                    {
                        ManTeam manTeam = team as ManTeam;
                        _ManTeams[_countM] = manTeam;
                        _countM++;
                    }
                }
                else if (team is WomanTeam)
                {
                    if (_countW < _WomanTeams.Length)
                    {
                        WomanTeam womanTeam = team as WomanTeam;
                        _WomanTeams[_countW] = womanTeam;
                        _countW++;
                    }
                }
            }

            public void Add(Team[] teams)
            {
                if (_ManTeams == null || _WomanTeams == null) return;
                foreach (var team in teams)
                {
                    Add(team);
                }
            }
            private void EachSort(Team[] teams)
            {
                if (teams == null || teams.Length == 0) return;
                for (int i = 0; i < teams.Length; i++)
                {
                    for (int j = 0; j < teams.Length - i - 1; j++)
                    {
                        if (teams[j] != null && teams[j + 1] != null)
                            if (teams[j].TotalScore < teams[j + 1].TotalScore)
                            {
                                var a = teams[j];
                                teams[j] = teams[j + 1];
                                teams[j + 1] = a;
                            }
                    }
                }
            }
            public void Sort()
            {
                EachSort(_ManTeams);
                EachSort(_WomanTeams);
            }

            public static Group Merge(Group group1, Group group2, int size)
            {
                if (size <= 0) return null;
                group1.Sort();
                group2.Sort();
                Group New = new Group("Финалисты");
                Group M = MergeEach(group1.ManTeams, group2.ManTeams, size);
                Group W = MergeEach(group1.WomanTeams, group2.WomanTeams, size);
                New.Add(M.ManTeams);
                New.Add(W.WomanTeams);
                return New;
            }

            private static Group MergeEach(Team[] Team1, Team[] Team2, int len)
            {
                Group New = new Group("Финалисты");
                int i = 0, j = 0;
                if (len <= 0) return null;
                while (i < len / 2 && j < len / 2)
                {
                    if (Team1[i] == null || Team2[j] == null) continue;
                    if (Team1[i].TotalScore >= Team2[j].TotalScore)
                    {
                        New.Add(Team2[i++]);
                    }
                    else
                    {
                        New.Add(Team2[j++]);
                    }
                }
                while (i < len / 2)
                {
                    New.Add(Team1[i++]);
                }

                while (j < len / 2)
                {
                    New.Add(Team2[j++]);
                }
                return New;
            }

            public void Print()
            {
                Console.Write($"{_name} ");
                for (int i = 0; i < _ManTeams.Length; i++)
                {
                    _ManTeams[i].Print();
                }
                Console.WriteLine("");

                Console.Write($"{_name} ");
                for (int i = 0; i < _WomanTeams.Length; i++)
                {
                    _WomanTeams[i].Print();
                }
                Console.WriteLine("");
            }

        }
    }
}
