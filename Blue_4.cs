using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lab_7.Blue_4;

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
                    int[] scores = new int[_scores.Length];
                    for (int i = 0; i < _scores.Length; i++)
                    {
                        scores[i] = _scores[i];
                    }
                    return scores;
                }
            }

            public int TotalScore
            {
                get
                {
                    if (_scores == null) return 0;
                    int total_score = 0;
                    for (int i = 0; i < _scores.Length; i++)
                    {
                        total_score += _scores[i];
                    }
                    return total_score;
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
                int[] newres = new int[_scores.Length + 1];
                for (int i = 0; i < newres.Length - 1; i++)
                {
                    newres[i] = _scores[i];
                }
                newres[newres.Length - 1] = result;
                _scores = newres;
            }

            public void Print()
            {
                Console.WriteLine($"{_name}: {TotalScore}");
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

            //private int index;
            private int indM;
            private int indW;

            public string Name => _name;
            public ManTeam[] ManTeams => _manTeams;
            public WomanTeam[] WomanTeams => _womanTeams;

            public Group(string name)
            {
                _name = name;
                _manTeams = new ManTeam[12];
                _womanTeams = new WomanTeam[12];
                //index = 0;
                indM = 0;
                indW = 0;
            }

            public void Add(Team team)
            {
                if (team is ManTeam man_team && indM < _manTeams.Length)
                {
                    _manTeams[indM] = man_team;
                    indM++;
                }
                if (team is WomanTeam woman_team && indW < _womanTeams.Length)
                {
                    _womanTeams[indW] = woman_team;
                    indW++;
                }

            }

            public void Add(Team[] teams)
            {
                for (int i = 0; i < teams.Length; i++)
                {
                    Add(teams[i]);
                }
            }

            //public void Sort()
            //{
            //    if (_teams == null || _teams.Length == 0) return;
            //    for (int i = 0; i < _teams.Length; i++)
            //    {
            //        for (int j = 0; j < _teams.Length - i - 1; j++)
            //        {
            //            if (_teams[j].TotalScore < _teams[j + 1].TotalScore)
            //            {
            //                (_teams[j], _teams[j + 1]) = (_teams[j + 1], _teams[j]);
            //            }
            //        }
            //    }

            //}

            public void Sorting(Team[] _teams, int ind)
            {
                if (_teams == null || _teams.Length == 0 || ind == 0) return;
                for (int i = 0; i < ind - 1; i++)
                {
                    for (int j = 0; j < ind - 1 - i; j++)
                    {
                        if (_teams[j].TotalScore < _teams[j + 1].TotalScore)
                        {
                            (_teams[j], _teams[j + 1]) = (_teams[j + 1], _teams[j]);
                        }
                    }
                }
            }

            public void Sort()
            {
                Sorting(_manTeams, indM);
                Sorting(_womanTeams, indW);
            }

            //public static Group Merge(Group group1, Group group2, int size)
            //{
            //    Group group3 = new Group("Финалисты");
            //    int half = size / 2;
            //    int i = 0;
            //    int j = 0;
            //    while (i < half && j < half)
            //    {
            //        if (group1.Teams[i].TotalScore >= group2.Teams[j].TotalScore)
            //        {
            //            group3.Add(group1.Teams[i]);
            //            i++;
            //        }
            //        else
            //        {
            //            group3.Add(group2.Teams[j]);
            //            j++;
            //        }
            //    }
            //    while (i < half)
            //    {
            //        group3.Add(group1.Teams[i]);
            //        i++;
            //    }
            //    while (j < half)
            //    {
            //        group3.Add(group1.Teams[j]);
            //        j++;
            //    }
            //    return group3;
            //}

            public static Team[] Merging(Team[] group1, Team[] group2, int size)
            {
                if (group1 == null || group2 == null) return null;
                Team[] group3 = new Team[size];
                int half = size / 2;
                int i = 0;
                int j = 0;
                int count = 0;
                while (i < half && j < half)
                {
                    if (group1[i] == null || group2[j] == null) continue;
                    if (group1[i].TotalScore >= group2[j].TotalScore)
                    {
                        group3[count] = group1[i];
                        i++;
                        count++;
                    }
                    else
                    {
                        group3[count] = group2[j];
                        j++;
                        count++;
                    }
                }
                while (i < half)
                {
                    group3[count] = group1[i];
                    i++;
                    count++;
                }
                while (j < half)
                {
                    group3[count] = group2[j];
                    j++;
                    count++;
                }
                return group3;
            }
            public static Group Merge(Group group1, Group group2, int size)
            {
                Group group3 = new Group("Финалисты");
                Team[] men = Merging(group1.ManTeams, group2.ManTeams, size);
                group3.Add(men);
                Team[] women = Merging(group1.WomanTeams, group2.WomanTeams, size);
                group3.Add(women);
                return group3;
            }

            public void Print()
            {
                Console.WriteLine(_name);
                Console.WriteLine("Men:");
                for (int i = 0; i < indM; i++)
                {
                    _manTeams[i].Print();
                }
                Console.WriteLine("Women:");
                for (int i = 0; i < indW; i++)
                {
                    _womanTeams[i].Print();
                }
                //for (int i = 0; i < index; i++)
                //{
                //    _teams[i].Print();
                //}
            }
        }

    }
}
