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
                    if (_scores == null) return default(int[]);
                    var newArray = new int[_scores.Length];
                    Array.Copy(_scores, newArray, _scores.Length);
                    return newArray;
                }
            }
            public int TotalScore 
            {
                get
                {
                    if (_scores == null) return 0;
                    int totalScore = 0;
                    for (int i = 0; i < _scores.Length; i++)
                    {
                        totalScore += _scores[i];
                    }
                    return totalScore;
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
                var newArray = new int[_scores.Length + 1];
                Array.Copy(_scores, newArray, _scores.Length);
                newArray[_scores.Length] = result;
                _scores = newArray;
            }
            public void Print()
            {
                Console.WriteLine($"{Name}\t {TotalScore}");
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
            private Team[] _manTeams;
            private Team[] _womanTeams;
            private int _countWoman;
            private int _countMan;

            public string Name => _name;
            public Team[] ManTeams => _manTeams; // есть ли инкапсуляция????
            public Team[] WomanTeams => _womanTeams;
            public Group(string name)
            {
                _name = name;
                _manTeams = new Team[12]; //group<=12
                _womanTeams = new Team[12];
                _countWoman = 0;
                _countMan = 0;
            }
            //TWO TEAM  12 GROUPS in each TEAM
            //6 GROUP in each TEAM
            public void Add(Team team)
            {
                //max 12 if 12 no team should be added
                if (_manTeams == null || _womanTeams==null) return;
                if (team is WomanTeam && _countWoman < 12)
                {
                    _womanTeams[_countWoman++] = team;
                }
                else if (team is ManTeam && _countMan < 12)
                {
                    _manTeams[_countMan++] = team;
                }
            }
            public void Add(Team[] team)
            {
                if (_manTeams == null || _womanTeams == null) return;
                foreach(var player in team)
                {
                    if (player is WomanTeam) Add(player);
                    else if (player is ManTeam) Add(player);
                }
                //if (team is WomanTeam) тк массив не получится
            }
            public void Sort()//54321 
            {
                SortTeam(_womanTeams);
                SortTeam(_manTeams);
            }
            private void SortTeam(Team[] team)
            {
                if (team == null) return;
                for (int i = 0; i < team.Length; i++)
                {
                    for (int j = 0; j < team.Length - 1 - i; j++)
                    {
                        if (team[j].TotalScore < team[j + 1].TotalScore)
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
                if (size < -1) return default(Group);
                Group newArray = new Group("Финалисты");
                Team[] manTeam = MergeTeams(group1._manTeams, group2._manTeams, size);
                Team[] WomanTeam = MergeTeams(group1._womanTeams, group2._womanTeams, size);
                newArray.Add(manTeam);
                newArray.Add(WomanTeam);
                return newArray;
            }
            private static Team[] MergeTeams(Team[] team1, Team[] team2, int size)
            {
                if (size < -1) return default(Team[]);
                Team[] newTeams = new Team[size];
                int i = 0, j = 0, k = 0;
                while(i < size / 2 && j < size/2)
                {
                    if (team1[i] != null && team2[j] != null)
                    {
                        if (team1[i].TotalScore >= team2[j].TotalScore)
                            newTeams[k++] = team1[i++];
                        else newTeams[k++] = team2[j++];
                    }
                    else if (team1[i] != null) newTeams[k++] = team1[i++];
                    else if (team2[j] != null) newTeams[k++] = team2[j++];
                }
                while(i < size / 2 && team1[i] != null) newTeams[k++] = team1[i++];
                while (j < size / 2 && team2[j] != null) newTeams[k++] = team2[j++];
                return newTeams;
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
                foreach(var i in team) Console.Write(i + " ");
                Console.WriteLine();
            }
        }
    }
}
