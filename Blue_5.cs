using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Lab_6;
public class Blue_5
{
    public class Sportsman
    {
        private string _name;
        private string _surname;
        private int _place;

        public string Name => _name;
        public string Surname => _surname;
        public int Place => _place;

        public Sportsman(string name, string surname)
        {
            _name = name;
            _surname = surname;
            _place = 0;
        }  

        public void SetPlace(int place)
        {
            if (_place == 0 && place >= 1)
                _place = place;
        }
        public void Print()
        {
            WriteLine($"{_name} {_surname} - {_place}");
        }
    }


    public abstract class Team
    {
        private string _name;
        private Sportsman[] _sportsmen;
        private int _count;

        public string Name => _name;
        public Sportsman[] Sportsmen => _sportsmen;
    
        public int SummaryScore
        {
            get
            {
                if (_sportsmen == null) 
                    return 0;
                int sum = 0;
                foreach (Sportsman sportsman in _sportsmen)
                    sum += (sportsman.Place <=5 && sportsman.Place != 0)? 6-sportsman.Place : 0;
                return sum;
            }
        }
        public int TopPlace
        {
            get 
            {
                if (_sportsmen == null) return 0;
                int topPlace = 18;
                foreach (Sportsman sportsman in _sportsmen)
                {
                    if (sportsman.Place < topPlace && sportsman.Place != 0)
                        topPlace = sportsman.Place;
                }
                // if (topPlace != 19)
                    return topPlace;
                // else
                    // return 0;
            }
        }
        
        public Team(string name)
        {
            _name = name;
            _sportsmen = new Sportsman[6];
            _count = 0;
        }

        public void Add(Sportsman sportsman)
        {
            if (_sportsmen == null || _count == 6)
                return;
            _sportsmen[_count++] = sportsman;
        }
        public void Add(Sportsman[] sportsmen)
        {
            if (_sportsmen == null || sportsmen == null) 
                return;
            foreach(Sportsman sportsman in sportsmen)
                Add(sportsman);
        }
        public static void Sort(Team[] teams)
        {
            if (teams == null || teams.Length <= 1) return;
            var sortedTeams = teams.OrderByDescending(team => team.SummaryScore).ToArray();
            Array.Copy(sortedTeams,teams,teams.Length);

            for (int i = 0; i<teams.Length-1; i++)
            {
                if (teams[i].SummaryScore == teams[i+1].SummaryScore && teams[i].TopPlace > teams[i+1].TopPlace)
                {
                    var temp = teams[i];
                    teams[i] = teams[i+1];
                    teams[i+1] = temp;
                    if (i != 0)
                        i -= 2;
                }
            }
        }
        public void Print()
        {
            WriteLine($"{_name}: ");
            foreach (Sportsman sportsman in _sportsmen)
                sportsman.Print();
            WriteLine("");
        }
        protected abstract double GetTeamStrength();
        public static Team GetChampion(Team[] teams)
        {
            if (teams == null || teams.Length == 0)
                return null;
            if (teams.Length == 1)
                return teams[0];
            
            int ind = 0; bool existChampion = false; double maxStrength = 0;
            if (teams[0] != null) 
            {
                existChampion = true;
                maxStrength = teams[0].GetTeamStrength();
            }

            for (int i = 0; i < teams.Length; i++)
            {
                if (teams[i] != null && teams[i].GetTeamStrength() >= maxStrength)
                {
                    ind = i;
                    maxStrength = teams[i].GetTeamStrength();
                    if (!existChampion)
                        existChampion = true;
                }
            }
            if (existChampion)
                return teams[ind];
            else
                return null;
        }
    }

    public class ManTeam : Team
    {
        public ManTeam(string name) : base(name) {}
        protected override double GetTeamStrength()
        {
            double midPlace = 0;
            foreach (Sportsman sportsman in Sportsmen)
                midPlace += sportsman.Place;
            
            midPlace /= 2;
            return 100.0 / midPlace;
        }
    } 

    public class WomanTeam : Team
    {
        public WomanTeam(string name) : base(name) {}
        protected override double GetTeamStrength()
        {
            double sumPlace = 0;
            double multPlace = 1;
            foreach (Sportsman sportsman in Sportsmen)
            {
                sumPlace += sportsman.Place;
                multPlace *= sportsman.Place;
            }
            return 100.0 * sumPlace * Sportsmen.Length / multPlace;
        }
    } 

}