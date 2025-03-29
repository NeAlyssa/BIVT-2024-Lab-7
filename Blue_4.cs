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
            private int[] _scores; 

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
                if (_scores == null || _scores.Length == 0)
                {
                    int[] _array = new int[1];
                    _array[0] = result;
                    _scores = new int[1];
                    Array.Copy(_array, _scores, _array.Length);
                }
                else
                {
                    int[] _array = new int[_scores.Length + 1];
                    _array[_array.Length - 1] = result;
                    for (int i = 0; i < _scores.Length; i++)
                    {
                        _array[i] = _scores[i];
                    }
                    _scores = new int[_array.Length];
                    Array.Copy(_array, _scores, _array.Length);
                }
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
            
            private int CurrentIndexMan => _currentIndexMan;
            private int CurrentIndexWoman => _currentIndexWoman;

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
                    if (_currentIndexMan < _manTeams.Length)
                    {
                        ManTeam manTeam = team as ManTeam;
                        _manTeams[_currentIndexMan] = manTeam;
                        _currentIndexMan++;
                        return;
                    }
                }
                else if (team is WomanTeam)
                {
                    if (_currentIndexWoman < _womanTeams.Length)
                    {
                        WomanTeam womanTeam = team as WomanTeam;
                        _womanTeams[_currentIndexWoman] = womanTeam;
                        _currentIndexWoman++;
                        return;
                    }
                }

            }

            public void Add(Team[] teams)
            {
                if (teams == null || _manTeams == null || _womanTeams == null || teams.Length == 0) return;

                for (int i = 0; i < teams.Length; i++)
                {
                    if (teams[i] == null) continue;
                    this.Add(teams[i]);
                }

            }

            private void SortTeam(Team[] teams, int count)
            {
                if (teams == null || teams.Length == 0 || count == 0) return;
                // гномья эффективная сортировка 
                for (int i = 1, j = 2; i < teams.Length;)
                {
                    if (i == 0 || teams[i].TotalScore >= teams[i - 1].TotalScore)
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
                if (_manTeams == null || _womanTeams == null) return;
                SortTeam(_manTeams, _currentIndexMan);
                SortTeam(_womanTeams, _currentIndexWoman);
            }


            public static Team[] Merge(Team[] team1, Team[] team2, int size)
            {
                if (team1 == null || team2 == null) return null;
                Team[] result = new Team[size];
                int n = size / 2;
                int i = 0, j = 0, k = 0;
                while (i < n && j < n && team1[i] != null && team2[j] != null)
                {
                    if (team1[i].TotalScore >= team2[j].TotalScore)
                    {
                        result[k] = team1[i];
                        i++;
                        k++;
                    }
                    else
                    {
                        result[k] = team2[j];
                        j++;
                        k++;
                    }
                }
                while (i < n)
                {
                    result[k] = team1[i];
                    i++;
                    k++;
                }
                while (j < n)
                {
                    result[k] = team2[j];
                    j++;
                    k++;
                }
                return result;
            }
            public static Group Merge(Group group1, Group group2, int size)
            {
                Group result = new Group("Финалисты");
                Team[] manTeam = Merge(group1._manTeams, group2._manTeams, size);
                Team[] WomanTeam = Merge(group1._womanTeams, group2._womanTeams, size);
                result.Add(manTeam);
                result.Add(WomanTeam);
                return result;
            }
            public void Print()
            {
                Console.WriteLine(_name);
                for (int k = 0; k < _currentIndexMan; k++)
                {
                    _manTeams[k].Print();
                }
                Console.WriteLine();
                for (int k = 0; k < _currentIndexWoman; k++)
                {
                    _womanTeams[k].Print();
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