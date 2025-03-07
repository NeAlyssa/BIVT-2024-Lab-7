using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Lab_7
{
    public class Blue_4
    {
        public abstract class Team
        {
            //поля
            private string _name;
            private int[] _scores;
            private int _playedMatchesNum;

            //свойства
            public string Name => _name;
            public int[] Scores
            {
                get
                {
                    if (_scores == null) return null;

                    int[] copy = new int[_scores.Length];
                    for (int k = 0; k < copy.Length; k++)
                        copy[k] = _scores[k];
                    return copy;
                }
            }
            public int TotalScore
            {
                get
                {
                    if (_scores == null) return 0;

                    int totalScore = 0;
                    for (int k = 0; k < _scores.Length; k++)
                        totalScore += _scores[k];
                    return totalScore;
                }
            }
            //public int PlayedMatchesNum => _playedMatchesNum;

            //конструкторы
            public Team(string name)
            {
                _name = name;
                _scores = new int[0];
                _playedMatchesNum = 0;
            }

            //методы
            private void IncreaseCapasity()
            {
                if (_scores == null || _scores.Length == 0)
                {
                    _scores = new int[1];
                    return;
                }

                int[] tmp = new int[_scores.Length + 1];
                int k = 0;
                foreach (int score in _scores)
                    tmp[k++] = score;
                _scores = tmp;
            }

            public void PlayMatch(int result)
            {
                if (_scores == null) return;
                if (_playedMatchesNum >= _scores.Length)
                    IncreaseCapasity();
                _scores[_playedMatchesNum] = result;
                _playedMatchesNum++;
            }
            public void Print()
            {
                Console.Write($"{_name,12} :");
                for (int k = 0; k < _scores.Length; k++)
                    Console.Write($"{_scores[k],4}");
                //Console.WriteLine();
                Console.WriteLine($"    Total score - {TotalScore}");
            }
        }// abstract class Team

        public class ManTeam : Team
        {
            public ManTeam(string name) : base(name) { }
        } //  class ManTeam : Team

        public class WomanTeam : Team
        {
            public WomanTeam(string name) : base(name) { }
        } //  class WomanTeam : Team


        public class Group
        {
            //поля
            private string _name;
            private ManTeam[] _manTeams; //12
            private WomanTeam[] _womanTeams; //12
            private int _manTeamsNum;
            private int _womanTeamsNum;

            //свойства
            public string Name => _name;
            public ManTeam[] ManTeams => _manTeams;
            public WomanTeam[] WomanTeams => _womanTeams;
            
            //конструкторы
            public Group(string name)
            {
                _name = name;
                _manTeams = new ManTeam[12];
                _womanTeams = new WomanTeam[12];
                _manTeamsNum = 0;
                _womanTeamsNum = 0;
            }

            //методы
            public void Add(Team team)
            {
                if (team == null) return;

                ManTeam manTeam = team as ManTeam;
                if (manTeam != null)
                {
                    if (_manTeams == null || _manTeamsNum >= _manTeams.Length) return;
                    _manTeams[_manTeamsNum] = manTeam;
                    _manTeamsNum++;
                    return;
                }
                WomanTeam womanTeam = team as WomanTeam;
                if (womanTeam != null)
                {
                    if (_womanTeams == null || _womanTeamsNum >= _womanTeams.Length) return;
                    _womanTeams[_womanTeamsNum] = womanTeam;
                    _womanTeamsNum++;
                    return;
                }
            }
            public void Add(Team[] teams)
            {
                if (teams == null || _manTeams == null || _womanTeams == null || teams.Length == 0) return;
                foreach (Team team in teams)
                {
                    if(team == null) continue;

                    if(_manTeamsNum < _manTeams.Length)
                    {
                        ManTeam manTeam = team as ManTeam;
                        if (manTeam != null)
                        {
                            _manTeams[_manTeamsNum] = manTeam;
                            _manTeamsNum++;
                            continue;
                        }
                    }
                    if(_womanTeamsNum < _womanTeams.Length)
                    {
                        WomanTeam womanTeam = team as WomanTeam;
                        if (womanTeam != null)
                        {
                            _womanTeams[_womanTeamsNum] = womanTeam;
                            _womanTeamsNum++;
                            continue;
                        }
                    }
                    if (_manTeamsNum >= _manTeams.Length && _womanTeamsNum >= _womanTeams.Length) break;
                }
            }

            private void TeamSort(Team[] teams, int teamsNum)
            {
                if (teams == null || teamsNum == 0) return;

                for (int i = 0; i < teamsNum - 1; i++)
                {
                    for (int j = 0; j < teamsNum - i - 1; j++)
                    {
                        //if (teams[j] == null)
                        //{
                        //    Team tmp = teams[j + 1];
                        //    teams[j + 1] = teams[j];
                        //    teams[j] = tmp;
                        //}
                        //else if (teams[j + 1] == null)
                        //    continue;
                        if (teams[j + 1].TotalScore > teams[j].TotalScore)
                        {
                            Team tmp = teams[j + 1];
                            teams[j + 1] = teams[j];
                            teams[j] = tmp;
                        }
                    }
                }
            }
            public void Sort()
            {
                TeamSort(_manTeams, _manTeamsNum);
                TeamSort(_womanTeams, _womanTeamsNum);
            }

            private static Team[] TeamMerge(Team[] team1, Team[] team2, int size)
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
                        k++;
                        i++;
                    }
                    else
                    {
                        result[k] = team2[j];
                        k++;
                        j++;
                    }
                }
                while (i < n)
                {
                    result[k] = team1[i];
                    k++;
                    i++;
                }
                while (j < n)
                {
                    result[k] = team2[j];
                    k++;
                    j++;
                }
                return result;
            }
            public static Group Merge(Group group1, Group group2, int size)
            {
                Group result = new Group("Финалисты");
                Team[] manTeam = TeamMerge(group1._manTeams, group2._manTeams, size);
                Team[] WomanTeam = TeamMerge(group1._womanTeams, group2._womanTeams, size);
                result.Add(manTeam);
                result.Add(WomanTeam);
                return result;
            }


            public void Print()
            {
                Console.WriteLine(_name);
                Console.WriteLine("    man teams :");
                for (int k = 0; k < _manTeamsNum; k++)
                    _manTeams[k].Print();
                Console.WriteLine("  woman teams:");
                for (int k = 0; k < _womanTeamsNum; k++)
                    _womanTeams[k].Print();
                Console.WriteLine();
            }

        }//struct Group
    }
}
