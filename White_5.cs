using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    public class White_5
    {
        public struct Match
        {
            private int _goals;
            private int _misses;

            public int Goals => _goals;
            public int Misses => _misses;
            public int Difference => _goals - _misses;
            public int Score
            {
                get
                {
                    if (_goals>_misses) return 3;
                    else if (_goals==_misses) return 1;
                    else return 0;
                }
            }
            public Match(int goals, int misses)
            {
                _goals = goals;
                _misses = misses;
            }

            public void Print()
            {
                Console.WriteLine($"Голы: {_goals}, пропущенные: {_misses}, очки: {Score}");
            }
        }
        public abstract class Team
        {
            private string _name;
            private Match[] _matches;
            //private int _count;

            public string Name => _name;
            public Match[] Matches => _matches;

            public int TotalDifference
            {
                get
                {
                    if (_matches == null || _matches.Length == 0)
                        return 0;
                    int totaldifference = 0;
                    for (int i = 0; i < _matches.Length; i++)
                    {
                        totaldifference += _matches[i].Difference;
                    }
                    return totaldifference;
                }
            }

            public int TotalScore
            {
                get
                {
                    if (_matches == null || _matches.Length == 0)
                        return 0;
                    int totalScore = 0;
                    for (int i = 0; i < _matches.Length; i++)
                    {
                        totalScore += _matches[i].Score;
                    }
                    return totalScore;
                }
            }
            public Team(string name)
            {
                _name = name;
                _matches = new Match[0];
            }
            public virtual void PlayMatch(int goals, int misses)
            {
                if (_matches == null) return;
                Match tempMatch = new Match(goals, misses);
                Match[] temp = new Match[_matches.Length + 1];
                for (int i = 0; i < _matches.Length; i++)
                {
                    temp[i] = _matches[i];
                }
                temp[temp.Length - 1] = tempMatch;
                _matches = temp;

            }
            public void Print()
            {
                Console.WriteLine($"Команда - {Name}, общий счёт - {TotalScore}, разница голов - {TotalDifference}");
            }
            public static void SortTeams(Team[] teams)
            {
                for (int i = 0; i < teams.Length - 1; i++)
                {
                    for (int j = 0; j < teams.Length - 1 - i; j++)
                    {
                        if (teams[j].TotalScore < teams[j + 1].TotalScore)
                        {
                            Team temp = teams[j];
                            teams[j] = teams[j + 1];
                            teams[j + 1] = temp;
                        }
                        else if (teams[j].TotalScore == teams[j + 1].TotalScore)
                        {
                            if (teams[j].TotalDifference < teams[j + 1].TotalDifference)
                            {
                                Team temp = teams[j];
                                teams[j] = teams[j + 1];
                                teams[j + 1] = temp;
                            }
                        }
                    }
                }
            }
        }
        public class ManTeam : Team
        {
            private ManTeam _derby;

            public ManTeam Derby => _derby;

            public ManTeam(string name, ManTeam derby = null) : base(name)
            {
                _derby = derby;
            }

            public  void PlayMatch(int goals, int misses, ManTeam team = null)
            {
                if (team == _derby) base.PlayMatch(goals + 1, misses);
                else base.PlayMatch(goals, misses);
            }
        }
        public class WomanTeam : Team
        {
            private int[] _penalties;

            public int[] Penalties => _penalties;

            public int TotalPenalties
            {
                get
                {
                    int total = 0;
                    foreach (var i in _penalties)
                    {
                        total += i;
                    }
                    return total;
                }
            }

            public WomanTeam(string name) : base(name)
            {
                _penalties = new int[0];
            }
            public override void PlayMatch(int goals, int misses)
            {
                base.PlayMatch(goals, misses);

                if (misses > goals)
                {
                    int dif = misses - goals;
                    int[] temp = new int[_penalties.Length + 1];
                    for (int i = 0; i < _penalties.Length; i++)
                    {
                        temp[i] = _penalties[i];
                    }
                    temp[temp.Length - 1] = misses - goals;
                    _penalties = temp;
                }
            }
        }
    }
}