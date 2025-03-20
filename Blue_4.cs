using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab_7
{
    public class Blue_4
    {
        public abstract class Team
        {
            //поля
            private string _name;
            private int[] _scores;
            //свойства
            public string Name { get { return _name; } }
            public int[] Scores
            {
                get
                {
                    if (_scores == null) return null;
                    int[] copy = new int[_scores.Length];
                    for (int i = 0; i < copy.Length; i++)
                    {
                        copy[i] = _scores[i];
                    }
                    return copy;
                }
            }
            public int TotalScore
            {
                get
                {
                    if (_scores == null) return 0;
                    int count = 0;
                    for (int i = 0; i < _scores.Length; i++)
                    {
                        count += _scores[i];
                    }
                    return count;
                }
            }
            //конструктор
            public Team(string name)
            {
                _name = name;
                _scores = new int[0];
            }
            //методы
            public void PlayMatch(int result)
            {
                if (_scores == null || _scores.Length == 0)
                {
                    int[] _sc = new int[1];
                    _sc[0] = result;
                    _scores = new int[1];
                    Array.Copy(_sc, _scores, _sc.Length);
                }
                else
                {
                    int[] _sc = new int[_scores.Length + 1];
                    _sc[_sc.Length - 1] = result;
                    for (int i = 0; i < _scores.Length; i++)
                    {
                        _sc[i] = _scores[i];
                    }
                    _scores = new int[_sc.Length];
                    Array.Copy(_sc, _scores, _sc.Length);
                }
            }
            public void Print()
            {
                Console.WriteLine($"{_name} {TotalScore}");
            }
        }
        public class Group
        {
            //поля
            private string _name;
            private ManTeam[] _manTeams;
            private WomanTeam[] _womanTeams;
            private int _countMan;
            private int _countWoman;
            //свойства
            public string Name => _name;
            public ManTeam[] ManTeams => _manTeams;
            public WomanTeam[] WomanTeams => _womanTeams;
            private int CountMan { get { return _countMan; } }
            private int CountWoman { get { return _countWoman; } }
            //конструктор
            public Group(string name)
            {
                _name = name;
                _manTeams = new ManTeam[12];
                _womanTeams = new WomanTeam[12];
                _countMan = 0;
                _countWoman = 0;
            }
            //методы
            public void Add(Team team)
            {
                if (team == null) return;
                if (team is ManTeam)
                {
                    if (_countMan < _manTeams.Length)
                    {
                        ManTeam manTeam = team as ManTeam;
                        _manTeams[_countMan] = manTeam;
                        _countMan++;
                        return;
                    }
                }
                else if (team is WomanTeam)
                {
                    if (_countWoman < _womanTeams.Length)
                    {
                        WomanTeam womanTeam = team as WomanTeam;
                        _womanTeams[_countWoman] = womanTeam;
                        _countWoman++;
                        return;
                    }
                }
            }
            public void Add(Team[] teams)
            {
                if (teams == null || _manTeams == null || _womanTeams == null || teams.Length == 0) return;
                for (int i = 0; i < teams.Length;)
                {
                    if (teams[i] == null) continue;
                    this.Add(teams[i]);
                }
            }
            private void TeamSort(Team[] teams, int countTeam)
            {
                if (teams == null || teams.Length == 0 || countTeam == 0) return;
                for (int i = 1, j = 2; i < countTeam;)
                {
                    if (i == 0 || teams[i - 1].TotalScore >= teams[i].TotalScore)
                    {
                        i = j;
                        j++;
                    }
                    else
                    {
                        Team temp = teams[i];
                        teams[i] = teams[i - 1];
                        teams[i - 1] = temp;
                        i--;
                    }
                }
            }
            public void Sort()
            {
                TeamSort(_manTeams, _countMan);
                TeamSort(_womanTeams, _countWoman);
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
            public static Group Merge (Group group1, Group group2, int size)
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
                for (int k = 0; k < _countMan; k++)
                {
                    _manTeams[k].Print();
                }
                Console.WriteLine();
                for (int k = 0; k < _countMan; k++)
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
