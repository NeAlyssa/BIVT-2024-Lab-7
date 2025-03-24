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
            protected string _name;
            protected int[] _scores;

            public string Name => _name;
            public int TotalScore
            {
                get
                {
                    int total = 0;
                    foreach (var score in _scores)
                        total += score;
                    return total;
                }
            }

            public Team(string name)
            {
                _name = name;
                _scores = new int[0];
            }

            public void PlayMatch(int result)
            {
                int[] newarr = new int[_scores.Length + 1];
                for (int i = 0; i < _scores.Length; i++)
                    newarr[i] = _scores[i];
                newarr[_scores.Length] = result;
                _scores = newarr;
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
            private int _manCount;
            private int _womanCount;

            public string Name => _name;
            public ManTeam[] ManTeams => _manTeams;
            public WomanTeam[] WomanTeams => _womanTeams;

            public Group(string name)
            {
                _name = name;
                _manTeams = new ManTeam[12];
                _womanTeams = new WomanTeam[12];
                _manCount = 0;
                _womanCount = 0;
            }

            public void Add(Team team)
            {
                if (team is ManTeam && _manCount < 12)
                {
                    _manTeams[_manCount++] = (ManTeam)team;
                }
                else if (team is WomanTeam && _womanCount < 12)
                {
                    _womanTeams[_womanCount++] = (WomanTeam)team;
                }
            }

            public void Add(Team[] teams)
            {
                for (int i = 0; i < teams.Length; i++)
                {
                    Add(teams[i]);
                }
            }

            public void Sort()
            {
                if (_manTeams == null || _manCount < 2) return;
                for (int i = 0; i < _manCount - 1; i++)
                {
                    for (int j = 0; j < _manCount - i - 1; j++)
                    {
                        if (_manTeams[j].TotalScore < _manTeams[j + 1].TotalScore)
                        {
                            ManTeam temp = _manTeams[j];
                            _manTeams[j] = _manTeams[j + 1];
                            _manTeams[j + 1] = temp;
                        }
                    }
                }

                if (_womanTeams == null || _womanCount < 2) return;
                for (int i = 0; i < _womanCount - 1; i++)
                {
                    for (int j = 0; j < _womanCount - i - 1; j++)
                    {
                        if (_womanTeams[j].TotalScore < _womanTeams[j + 1].TotalScore)
                        {
                            WomanTeam temp = _womanTeams[j];
                            _womanTeams[j] = _womanTeams[j + 1];
                            _womanTeams[j + 1] = temp;
                        }
                    }
                }
            }

            public static Group Merge(Group group1, Group group2, int size)
            {
                Group result = new Group("Финалисты");
                group1.Sort();
                group2.Sort();

                int i = 0, j = 0;
                while (i < size / 2 && j < size / 2)
                {
                    if (group1.ManTeams[i].TotalScore >= group2.ManTeams[j].TotalScore)
                        result.Add(group1.ManTeams[i++]);
                    else
                        result.Add(group2.ManTeams[j++]);
                }
                while (i < size / 2) result.Add(group1.ManTeams[i++]);
                while (j < size / 2) result.Add(group2.ManTeams[j++]);

                i = 0;
                j = 0;
                while (i < size / 2 && j < size / 2)
                {
                    if (group1.WomanTeams[i].TotalScore >= group2.WomanTeams[j].TotalScore)
                        result.Add(group1.WomanTeams[i++]);
                    else
                        result.Add(group2.WomanTeams[j++]);
                }
                while (i < size / 2) result.Add(group1.WomanTeams[i++]);
                while (j < size / 2) result.Add(group2.WomanTeams[j++]);

                return result;
            }

            public void Print()
            {
                Console.WriteLine(Name);
                Print(_manTeams);
                Print(_womanTeams);
            }
            private void Print(Team[] team)
            {
                if (team == null) return;
                foreach (var i in team) Console.Write(i + " ");
                Console.WriteLine();
            }
        }

    }
}