using System;

namespace Lab_7
{
     public class Blue_4
 {
    public abstract class Team
    {
        protected string _name;
        protected int[] _scores;

        public string Name => _name;
        public int[] Scores
        {
            get
            {
                if (_scores == null) return null;
                int[] newarr = new int[_scores.Length];
                for (int i = 0; i < _scores.Length; i++)
                    newarr[i] = _scores[i];
                return newarr;
            }
        }
        public int TotalScore
        {
            get
            {
                if (_scores == null || _scores.Length == 0) return 0;
                int total = 0;
                for (int i = 0; i < _scores.Length; i++)
                    total += _scores[i];
                return total;
            }
        }

        protected Team(string name)
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
            Console.Write($"{_name}: {TotalScore}");
            Console.WriteLine("");
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
        private int _manCount;
        private int _womanCount;

        public string Name => _name;
        public Team[] ManTeams => _manTeams;
        public Team[] WomanTeams => _womanTeams;

        public Group(string name)
        {
            _name = name;
            _manTeams = new Team[12];
            _womanTeams = new Team[12];
            _manCount = 0;
            _womanCount = 0;
        }

        public void Add(Team team)
        {
            if (team is ManTeam && _manCount < 12)
                _manTeams[_manCount++] = team;
            else if (team is WomanTeam && _womanCount < 12)
                _womanTeams[_womanCount++] = team;
        }

        public void Add(Team[] teams)
        {
            for (int i = 0; i < teams.Length; i++)
                Add(teams[i]);
        }

        private void SortTeams(ref Team[] teams, int count)
        {
            for (int i = 0; i < count - 1; i++)
            {
                for (int j = 0; j < count - i - 1; j++)
                {
                    if (teams[j].TotalScore < teams[j + 1].TotalScore)
                    {
                        Team temp = teams[j];
                        teams[j] = teams[j + 1];
                        teams[j + 1] = temp;
                    }
                }
            }
        }

        public void Sort()
        {
            SortTeams(ref _manTeams, _manCount);
            SortTeams(ref _womanTeams, _womanCount);
        }

        public static Group Merge(Group group1, Group group2)
        {
            Group result = new Group("Финалисты");
            group1.Sort();
            group2.Sort();

            int i = 0, j = 0;
            while (i < 6 && j < 6)
            {
                if (group1.ManTeams[i].TotalScore >= group2.ManTeams[j].TotalScore)
                    result.Add(group1.ManTeams[i++]);
                else
                    result.Add(group2.ManTeams[j++]);
            }
            while (i < 6) result.Add(group1.ManTeams[i++]);
            while (j < 6) result.Add(group2.ManTeams[j++]);

            i = 0; j = 0;
            while (i < 6 && j < 6)
            {
                if (group1.WomanTeams[i].TotalScore >= group2.WomanTeams[j].TotalScore)
                    result.Add(group1.WomanTeams[i++]);
                else
                    result.Add(group2.WomanTeams[j++]);
            }
            while (i < 6) result.Add(group1.WomanTeams[i++]);
            while (j < 6) result.Add(group2.WomanTeams[j++]);

            return result;
        }

        public void Print()
        {
            Console.Write($"{_name}:\nМужские команды:\n");
            for (int i = 0; i < _manCount; i++)
                _manTeams[i].Print();

            Console.Write("Женские команды:\n");
            for (int i = 0; i < _womanCount; i++)
                _womanTeams[i].Print();

            Console.WriteLine("");
        }
    }
}
}
