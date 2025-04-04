using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    public class Blue_4
    {
        // класс
        public abstract class Team
        {
            // приватные поля
            private string _name;
            private int[] _scores;

            // свойства
            public string Name => _name;
            public int[] Scores
            {
                get
                {
                    if (_scores == null) return null;
                    int[] new_scr = new int[_scores.Length];
                    for (int i = 0; i < new_scr.Length; i++) { new_scr[i] = _scores[i]; }
                    return new_scr;
                }
            }
            public int TotalScore
            {
                get
                {
                    int score = 0;
                    if (_scores == null) return 0;
                    for (int i = 0; i < _scores.Length; i++) { score += _scores[i]; }
                    return score;
                }
            }

            // конструктор
            public Team(string name)
            {
                _name = name;
                _scores = new int[0];
            }

            // методы
            public void PlayMatch(int result)
            {
                if (_scores == null) return;
                var new_scr = new int[_scores.Length + 1];
                for (int i = 0; i < _scores.Length; i++) { new_scr[i] = _scores[i]; }
                new_scr[new_scr.Length - 1] = result;
                _scores = new_scr;
            }
            public void Print()
            {
                Console.WriteLine($"{_name}, {TotalScore}");
                Console.WriteLine();
            }

        }
        // класс
        public class Group
        {
            // приватные поля
            private string _name;
            private Team[] _manTeams;
            private Team[] _womanTeams;
            private int _mancounter;
            private int _womancounter;

            // свойства
            public string Name => _name;
            public Team[] ManTeams => _manTeams;
            public Team[] WomanTeams => _womanTeams;

            // конструктор
            public Group(string name)
            {
                _name = name;
                _manTeams = new Team[12];
                _womanTeams = new Team[12];
                _womancounter = 0;
                _mancounter = 0;
            }
            // методы
            public void Add(Team team)
            {
                if (_manTeams == null || _womanTeams == null) return;
                if (team is ManTeam && _mancounter < 12)
                {
                    _manTeams[_mancounter] = team;
                    _mancounter++;
                }
                else if (team is WomanTeam && _womancounter < 12)
                {
                    _womanTeams[_womancounter] = team;
                    _womancounter++;
                }
            }
            public void Add(Team[] teams)
            {
                if (_manTeams == null || _womanTeams == null) return;

                foreach (Team team in teams)
                {
                    Add(team);
                }
            }
            private void TeamSort(Team[] team)
            {
                if (team == null) return;
                for (int i = 0; i < team.Length; i++)
                {
                    for (int j = 0; j < team.Length - i - 1; j++)
                    {
                        if (team[j] != null && team[j + 1] != null && team[j].TotalScore < team[j + 1].TotalScore)
                        {
                            var tm = team[j];
                            team[j] = team[j + 1];
                            team[j + 1] = tm;
                        }
                    }
                }
            }
            public void Sort()
            {
                TeamSort(_womanTeams);
                TeamSort(_manTeams);
            }

            public static Group Merge(Group group1, Group group2, int size)
            {
                Group final = new Group("Финалисты");
                Group ManTeam = TeamMerge(group1.ManTeams, group2.ManTeams, group1.ManTeams.Length + group2.ManTeams.Length);
                Group WomanTeam = TeamMerge(group1.WomanTeams, group2.WomanTeams, group1.WomanTeams.Length + group2.WomanTeams.Length);
                final.Add(ManTeam.ManTeams);
                final.Add(WomanTeam.WomanTeams);
                return final;


            }
            public static Group TeamMerge(Team[] team_1, Team[] team_2, int size)
            {
                if (team_1 == null || team_2 == null) return null;
                Group final = new Group("Финалисты");
                int i = 0, j = 0;
                while ((i < size / 2) && (j < size / 2))
                {
                    if ((team_1[i] != null) && (team_2[j] != null))
                    {
                        if (team_1[i].TotalScore >= team_2[j].TotalScore) { final.Add(team_1[i++]); }
                        else { final.Add(team_2[j++]); }
                    }
                }
                while (i < size / 2) { final.Add(team_1[i++]); }
                while (j < size / 2) { final.Add(team_2[j++]); }
                return final;
            }

            public void Print()
            {
                Console.WriteLine(_name);
                for (int k = 0; k < _mancounter; k++) { _manTeams[k].Print(); }
                Console.WriteLine();
                for (int k = 0; k < _mancounter; k++) { _womanTeams[k].Print(); }
            }
        }
        // классы-наследники
        public class ManTeam : Team
        {
            // конструктор
            public ManTeam(string name) : base(name) { }
        }
        public class WomanTeam : Team
        {
            // конструктор
            public WomanTeam(string name) : base(name) { }
        }
    }
}
