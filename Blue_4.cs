namespace Lab_7{

public class Blue_4
{
    public abstract class Team{
        private string _name;
        private int[] _scores;
        public string Name => _name;
        
        public int[] Scores{
            get{
                if (_scores == null) return null;
                
                int[] sc= new int[_scores.Length];
                Array.Copy(_scores,sc,_scores.Length);
                return sc;
            }
        }
        public int TotalScore
            {
                get
                {
                    if (_scores == null) return 0;
                    int k = 0;
                    for (int i = 0; i < _scores.Length; i++)
                    {
                        k += _scores[i];
                    }
                    return k;
                }
            }
        public Team(string name){
            _name=name;
            _scores = new int[0];
        }
        public void PlayMatch(int result)
            {
                if (_scores == null) return;
                int[] copy = new int[_scores.Length + 1];
                Array.Copy(_scores, copy,_scores.Length);
                copy[copy.Length - 1] = result;
                _scores = copy;
                
            }
        public void Print(){
            Console.WriteLine($"{Name} - {Scores}");
        }
    }
    public class ManTeam : Team{
        public ManTeam(string name) : base(name) { }
    }
    public class WomanTeam : Team{
        public WomanTeam(string name) : base(name) { }
    }

    public class Group{
        private string _name;
        private ManTeam[] _manteams;
        private WomanTeam[] _womanteams;
        private int _manteamid;
        private int _womanteamid;
        public string Name => _name;
        public ManTeam[] ManTeams => _manteams;
        public WomanTeam[] WomanTeams => _womanteams;
        
        public Group(string name){
            _name=name;
            _manteams = new ManTeam[12];
            _womanteams = new WomanTeam[12];
            _manteamid = 0;
            _womanteamid = 0;
        }
        public void Add(Team team){
            if (team == null) return;
            ManTeam man = team as ManTeam;
            if (man != null){
            if (_manteams == null || _manteams.Length == 0 || _manteamid >= _manteams.Length) return;
            if(_manteamid < _manteams.Length){
                _manteams[_manteamid] = man;
                _manteamid++;
                return;
            }
            }
            WomanTeam woman = team as WomanTeam;
            if (woman != null){
                if (_womanteams == null || _womanteams.Length == 0 || _womanteamid >= _womanteams.Length) return;
            if(_womanteamid < _womanteams.Length){
                _womanteams[_womanteamid] = woman;
                _womanteamid++;
                return;
            }
            }
        }
        public void Add(Team[] teams)
            {
                if (_manteams == null || _womanteams==null || teams == null || teams.Length == 0) return;
                foreach (Team team in teams){
                    if (team == null) continue;
                    if (_manteamid < _manteams.Length){
                    ManTeam man = team as ManTeam;
                    if (man != null){
                        _manteams[_manteamid] = man;
                        _manteamid++;
                        continue;
                    }
                    }
                    if (_womanteamid < _womanteams.Length){
                        WomanTeam woman = team as WomanTeam;
                        if (woman !=null){
                            _womanteams[_womanteamid] = woman;
                            _womanteamid++;
                            continue;

                        }
                    }
                    if (_manteamid >= _manteams.Length && _womanteamid >= _womanteams.Length) break;
                }
            }
        private void SeparateSort(Team[] teams, int teamid){
            if (teams == null || teamid == 0) return;
                for (int i = 0; i < teamid - 1; i++)
                {
                    for (int j = 0; j < teamid - i - 1; j++)
                    {
                        if (teams[j].TotalScore < teams[j + 1].TotalScore)
                        {
                            (teams[j], teams[j + 1]) = (teams[j + 1], teams[j]);
                        }
                    }
                }
        }
        public void Sort(){
            SeparateSort(_manteams,_manteamid);
            SeparateSort(_womanteams,_womanteamid);
        }
        private static Team[] TMerge(Team[] one, Team[] two, int size){
            Team[] fin = new Team[size];
            int c = size/2;
            int h=0;
            int i=0;
            int j=0;
            while(i<c && j<c){
                if (one[i].TotalScore >= two[j].TotalScore){
                    fin[h] = one[i];
                    i++;
                    h++;
                }else{
                    fin[h] = two[j];
                    h++;
                    j++;
                }

            }
            while (i<c){
                fin[h]=one[i];
                h++;
                i++;
            }
            while(j<c){
                fin[h] =two[j];
                h++;
                j++;
            }
            return fin;
        }
        public static Group Merge(Group group1, Group group2,int size){
            Group res = new Group("Финалисты");
            Team[] manteam = TMerge(group1._manteams,group2._manteams, size);
            Team[] womanteam = TMerge(group1._womanteams,group2._womanteams, size);
            res.Add(manteam);
            res.Add(womanteam);
            return res;
        }
        public void Print()
            {
                Console.WriteLine(_name);
                Console.WriteLine("Мужики:");
                for (int i = 0; i < _manteamid; i++)
                    _manteams[i].Print();
                Console.WriteLine("Дамы:");
                for (int l = 0; l < _womanteamid; l++)
                    _womanteams[l].Print();
            }
    }
}
}