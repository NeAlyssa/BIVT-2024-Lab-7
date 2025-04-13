using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lab_7.Blue_4.Group;

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
                    int[] copy_scores = new int[_scores.Length];
                    for (int i = 0; i < copy_scores.Length; i++)
                    {
                        copy_scores[i] = _scores[i];
                    }
                    return copy_scores;
                }
            }

            public int TotalScore
            {
                get
                {
                    if (_scores == null) return 0;
                    int res = 0;
                    for (int i = 0; i < _scores.Length; i++)
                    {
                        res += _scores[i];
                    }
                    return res;
                }
            }


            public Team(string name)
            {
                _name = name;
                _scores = new int[0];
            }

            public void PlayMatch(int result)
            {
                if (_scores == null) return;
                int[] copy = new int[_scores.Length + 1];
                for (int i = 0; i < _scores.Length; i++)
                {
                    copy[i] = _scores[i];
                }
                copy[copy.Length - 1] = result;
                _scores = copy;
            }


            public void Print()
            {
                Console.WriteLine($"{_name}  {TotalScore}");
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
            private WomanTeam[] _womanteams;
            private int _cntman;
            private int _cntwoman;


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

            public Group(string name)
            {
                _name = name;
                _manteams = new ManTeam[12];
                _womanteams = new WomanTeam[12];
                _cntman = 0;
                _cntwoman = 0;
            }

            public void Add(Team team)
            {
                if (team == null) return;
                if (team is ManTeam mt)
                {
                    if (_cntman < _manteams.Length)
                    {
                        _manteams[_cntman++] = mt;
                        return;
                    }
                } else if (team is WomanTeam wt)
                {
                    if (_cntwoman < _womanteams.Length)
                    {
                        _womanteams[_cntwoman++] = wt;
                        return;
                    }
                }
            }


            public void Add(Team[] teams)
            {
                if (teams == null || _manteams.Length == 0 || _womanteams == null) return;
                for (int i = 0; i < teams.Length; i++)
                {
                    if (teams[i] == null) continue;
                    Add(teams[i]);
                }
            }


            private void SortTeams(Team[] teams)
            {
                if (teams == null) return;
                for (int i = 0; i < teams.Length; i++)
                {
                    for (int j = 0; j < teams.Length - i - 1; j++)
                    {
                        if (teams[j] == null || teams[j + 1] == null)continue;
                        if (teams[j].TotalScore < teams[j + 1].TotalScore) (teams[j], teams[j + 1]) = (teams[j + 1], teams[j]);
                    }
                }
            }
            public void Sort()
            {
                SortTeams(_manteams);
                SortTeams(_womanteams);
            }




            public static Team[] MergeTeams(Team[] teams1, Team[] teams2, int size)
            {
                Team[] result = new Team[size];
                int i = 0, j = 0, k = 0;
                while (i < size / 2 && j < size / 2)
                {
                    if (teams1[i].TotalScore >= teams2[j].TotalScore)
                    {
                        result[k++] = teams1[i++];
                    } else
                    {
                        result[k++] = teams2[j++];
                    }
                }
                while (i < size / 2)
                {
                    result[k++] = teams1[i++];
                }
                while (j < size / 2)
                {
                    result[k++] = teams2[j++];
                }
                return result;
            }

            public static Group Merge(Group group1, Group group2, int size)
            {
                Group result = new Group("Финалисты");
                Team[] manTeam = MergeTeams(group1._manteams, group2._manteams, size);
                Team[] womanTeam = MergeTeams(group1._womanteams, group2._womanteams, size);
                result.Add(manTeam);
                result.Add(womanTeam);
                return result;
            }

            public void Print()
            {

                for (int i = 0; i < _manteams.Length; i++)
                {
                    Console.WriteLine($" {_manteams[i].Name} {_manteams[i].TotalScore}");
                }
                for (int i = 0; i < _womanteams.Length; i++)
                {
                    Console.WriteLine($" {_womanteams[i].Name} {_womanteams[i].TotalScore}");
                }

            }

            
        }
    }
}
