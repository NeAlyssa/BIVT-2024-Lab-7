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
        public abstract class Team
        {

            private string _name;
            private int[] _scores; // массив очков, полученных за матчи 

            public string Name => _name;
            public int[] Scores
            {
                get
                {
                    if (_scores == null) return default(int[]);
                    int[] copy = new int[_scores.Length];
                    Array.Copy(_scores, copy, _scores.Length);
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

            public Team(string name)
            {
                _name = name;
                _scores = new int[0];

            }

            public void PlayMatch(int result)
            {
                if (_scores == null) return;
                Array.Resize(ref _scores, _scores.Length + 1);
                _scores[_scores.Length - 1] = result;
            }

            public void Print()
            {
                Console.Write($"Team: {_name}, TotalScore: {TotalScore}");
                Console.WriteLine();
            }
        }



        public class Group
        {
            private string _name;
            private ManTeam[] _manTeams;
            private WomanTeam[] _womanTeams;
            private int _currentIndexMan;
            private int _currentIndexWoman;
            public string Name => _name;
            public Team[] ManTeams => _manTeams;
            public Team[] WomanTeams => _womanTeams;


            public Group(string name)
            {
                _name = name;
                _manTeams = new ManTeam[12];
                _womanTeams = new WomanTeam[12];
                _currentIndexMan = 0;
                _currentIndexWoman = 0;
            }

            public void Add(Team team)
            {
                if (team == null) return; 
                if (team is ManTeam)
                {
                    if (_manTeams == null || _currentIndexMan >= 12)
                        return;
                    else
                        _manTeams[_currentIndexMan++] = team as ManTeam;
                } else if (team is WomanTeam)
                {
                    if (_womanTeams == null || _currentIndexWoman >= 12)
                        return;
                    else
                        _womanTeams[_currentIndexWoman++] = team as WomanTeam;
                }

            }

            public void Add(Team[] teams)
            {
                if (teams == null) return;

                foreach (Team team in teams)
                    Add(team);

            }

            private void SortGroup(Team[] teams)
            {
                if (teams == null) return;
                // гномья эффективная сортировка 
                for (int i = 1, j = 2; i < teams.Length;)
                {
                    if (i == 0 || teams[i].TotalScore <= teams[i - 1].TotalScore)
                    {
                        i = j;
                        j++;
                    }
                    else
                    {
                        (teams[i], teams[i - 1]) = (teams[i - 1], teams[i]);
                        i--;
                    }

                }
            }
            public void Sort()
            {
                SortGroup(_manTeams);
                SortGroup(_womanTeams);
            }

            public static Group MergeTeams(Team[] group1, Team[] group2, int length)
            {
                if (length <= 0) return null;
                Group finalGroup = new Group("Финалисты");
                int i = 0; int j = 0;
                while (i < length / 2 && j < length / 2)
                {
                    if (group1[i].TotalScore >= group2[i].TotalScore)
                    {
                        finalGroup.Add(group1[i]);
                        i++;
                    }
                    else
                    {
                        finalGroup.Add(group2[j]);
                        j++;
                    }
                }

                while (i < length/2)
                {
                    finalGroup.Add(group1[i]);
                    i++;
                }

                while (j < length/2)
                {
                    finalGroup.Add(group2[j]);
                    j++;
                }

                return finalGroup;

            }
       
            public static Group Merge(Group group1, Group group2, int length)
            {
                if (length <= 0) return null;
                group1.Sort();
                group2.Sort();
                Group manTeam = MergeTeams(group1.ManTeams, group2.ManTeams, group1.ManTeams.Length + group2.ManTeams.Length);
                Group womanTeam = MergeTeams(group1.WomanTeams, group2.WomanTeams, group1.WomanTeams.Length + group2.WomanTeams.Length);
                Group total = new Group("Финалисты");
                total.Add(manTeam.ManTeams);
                total.Add(womanTeam.WomanTeams);
                return total;
            }
            public void Print()
            {
                Console.WriteLine($"{_name}: ");
                Console.Write("Women teams: ");
                foreach (var team in _womanTeams)
                {
                    team.Print();
                }
                Console.Write("Men teams: ");
                foreach (var team in _manTeams)
                {
                    team.Print();
                }
                Console.WriteLine("");
            }
        }


        public class ManTeam : Team
        {

            public ManTeam(string name) : base(name) { }



            public class WomanTeam : Team
            {
                public WomanTeam(string name) : base(name) { }

            }
        }
    }
}