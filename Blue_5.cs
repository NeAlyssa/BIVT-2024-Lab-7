using System.Reflection;
using System.Text.RegularExpressions;

namespace Lab_7{

public class Blue_5
{
    public class Sportsman{
        private string _name;
        private string _surname;
        private int _place;
        public string Name => _name;
        public string Surname => _surname;
        public int Place => _place;
        public Sportsman(string name, string surname){
            _name = name;
            _surname = surname;
            _place = 0;
        }
        public void SetPlace(int place){
            if (place<=0||_place > 0) return;
            _place = place;
        }
        public void Print()
            {
                Console.WriteLine($"{Name} {Surname} - {Place}");
            }
    }
    public abstract class Team{
        private string _name;
        private Sportsman[] _sportsmen;
        private int _kol;
        public string Name => _name;
        public Sportsman[] Sportsmen => _sportsmen;
        public int SummaryScore{
            get{
                if (_sportsmen == null) return 0;
                int s=0;
                for (int i=0;i<_sportsmen.Length;i++){
                    if (_sportsmen[i] == null) continue;
                    if (_sportsmen[i].Place == 1) {s+=5;}
                    else if (_sportsmen[i].Place == 2) {s+=4;}
                    else if (_sportsmen[i].Place == 3) {s+=3;}
                    else if (_sportsmen[i].Place == 4) {s+=2;}
                    else if (_sportsmen[i].Place == 5) {s+=1;}
                }
                return s;
            }
        }
        public int TopPlace{
            get{
            if (_sportsmen == null) return 18;
            if (_sportsmen[0] == null) return 18;
            int m=18;
            
            for(int i=0;i<_sportsmen.Length;i++){
                if(_sportsmen[i] == null) continue;
                if (_sportsmen[i].Place < m && _sportsmen[i].Place > 0){
                    m=_sportsmen[i].Place;
                }
            }
            return m;
            }

        }
        public Team(string name){
            _name=name;
            _sportsmen = new Sportsman[6];
            _kol=0;
        }
        public void Add(Sportsman sportsman){
            if (_sportsmen == null || sportsman == null || _kol >= _sportsmen.Length) return;
            _sportsmen[_kol] = sportsman;
            _kol++;
        }
        public void Add(Sportsman[] sportsman){
            if (_sportsmen == null || sportsman.Length == 0|| sportsman == null || _kol >= _sportsmen.Length) return;
            int i=0;
            while (_kol < _sportsmen.Length && i < sportsman.Length)
            {
                if (sportsman[i]!=null)
                {
                    _sportsmen[_kol] = sportsman[i];
                    _kol++;
                }
                i++;
            }
        }
        public static void Sort(Team[] teams){
            if (teams == null || teams.Length == 0) return;
                for (int i = 0; i < teams.Length - 1; i++)
                {
                    for (int j = 0; j < teams.Length - i - 1; j++)
                    {
                        if (teams[j + 1].SummaryScore > teams[j].SummaryScore)
                        {
                            (teams[j + 1], teams[j]) = (teams[j], teams[j + 1]);
                        }
                        else if (teams[j + 1].SummaryScore == teams[j].SummaryScore && teams[j + 1].TopPlace < teams[j].TopPlace)
                        {
                            (teams[j],teams[j+1])=(teams[j+1],teams[j]);
                        }
                    }
        }
        }
        protected abstract double GetTeamStrength();
        public static Team GetChampion(Team[] teams){
            if (teams == null || teams.Length == 0) return null;
            double max;
            int imax=0;
            if (teams[0] == null) 
            { 
                max=0;  
            }
            else 
            { 
                max = teams[0].GetTeamStrength();
            }
            for (int i=1;i<teams.Length;i++)
            {
                if (teams[i] == null) continue;
                if (teams[i].GetTeamStrength() > max)
                {
                    max = teams[i].GetTeamStrength();
                    imax = i ;
                }
            }
            return teams[imax];

        }
        public void Print()
            {
                for (int i = 0; i < _sportsmen.Length; i++)
                {
                    Console.WriteLine($"{Name} {SummaryScore} {TopPlace}");
                }
            }

    }
    public class ManTeam : Team{
        public ManTeam(string name) : base(name) {}
        protected override double GetTeamStrength(){
            double allplaces = 0;
            int k =0;
            foreach (Sportsman person in this.Sportsmen){
                if (person == null) continue;
                allplaces += person.Place;
                k++;
            }
            double sr = allplaces / k;
            double res = 100/sr;
            return res;
        }
    }
    public class WomanTeam: Team{
        public WomanTeam(string name) : base(name) {}
        protected override double GetTeamStrength(){
            double sumplaces = 0;
            //int l = this.Sportsmen.Length;
            int l=0;
            int proizplaces=0;
            foreach(Sportsman person in this.Sportsmen){
                if (person == null) continue;
                sumplaces+= person.Place;
                proizplaces*= person.Place;
                l++;
            }
            double res = 100*sumplaces*l/proizplaces;
            return res;
        }
    }
}
}
