using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Lab_7.Blue_4;
using static Lab_7.Blue_5;

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

                    if (_scores == null) return null;
                    int[] copy = new int[_scores.Length];
                    Array.Copy(_scores, copy, copy.Length);
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

                if (_scores == null) return;
                int[] temp = new int[_scores.Length + 1];
                for (int i = 0; i < temp.Length - 1; i++)
                {
                    temp[i] = _scores[i];
                }
                temp[temp.Length - 1] = result;
                _scores = temp;
            }
            public void Print()
            {
                Console.WriteLine($"Name: {Name}");
                // for (int i=0; i < _scores.Length; i++)
                //{
                ////    Console.WriteLine( _scores[i] );
                // }
                Console.WriteLine($"Total score - {TotalScore}");
            }

        }
        //
        public class ManTeam : Team
        {
            public ManTeam(string name) : base(name) 
            { 

            }
        }

        public class WomanTeam : Team
        {
            public WomanTeam(string name) : base(name) 
            {

            }
        } 

        public class  Group
        {
            private string _name;
            private ManTeam[] _manteams;
            private WomanTeam[] _womanteams;
            private int _indm;
            private int _indw;

            public string Name => _name;
            public ManTeam[] ManTeams => _manteams;
            public WomanTeam[] WomanTeams => _womanteams;

            //            {
            //get
            //{

            //if (_teams == null) return null;
            //Team[] copy = new Team[_teams.Length];
            //Array.Copy(_teams, copy, copy.Length);
            //return copy;
            //}
            //}

            public Group(string name)
            {
                _name = name;
                _manteams = new ManTeam[12];
                _womanteams = new WomanTeam[12];
                _indm = 0;
                _indw = 0;

            }


            public void Add(Team team)
            {
               
                if (team == null ) return;
                if (team is ManTeam )
                {
                    if (_indm < _manteams.Length)
                    {
                        ManTeam manteam = team as ManTeam;
                        _manteams[_indm++] = manteam;
                        
                    }
                    
                }
                else if (team is WomanTeam)
                {
                    if (_indm < _womanteams.Length)
                    {
                        WomanTeam womanteam = team as WomanTeam;
                        _womanteams[_indm++] = womanteam;
                        
                    }
                    
                }
            }
            public void Add(Team[] teams)
            {
                if (teams==null ) return;

                for (int i = 0; i < teams.Length;)
                {
                    if (teams[i] == null) continue;
                    this.Add(teams[i]);
                }

            }
            public void Sort()
            {
                Sorti(_manteams, _indm);
                Sorti(_womanteams, _indw);
            }
            public void Sorti(Team[] teams, int ind)
            {
                if (teams == null ) return;
                for (int i = 0; i < ind-1; i++)
                {
                    for (int j = 0; j < ind - i - 1; j++)
                    {
                        if (teams[j].TotalScore < teams[j + 1].TotalScore)
                        {
                            (teams[j], teams[j + 1]) = (teams[j + 1], teams[j]);
                        }
                    }
                }
            }

            public static Group Merge(Group group1, Group group2, int size)
            {
                Group res = new Group("Финалисты");
                Team[] man = Mergi(group1._manteams, group2._manteams, size);
                Team[] woman = Mergi(group1._womanteams, group2._womanteams, size);
                res.Add(man);
                res.Add(woman);
                return res;
            }
            public static Team[] Mergi(Team[] teams1, Team[] teams2, int size)
            {
                if (teams1 == null || teams2 == null) return null;
                Team[] res = new Team[size];
               
                int i = 0, j = 0, c = 0;
                while (i < size / 2 && j < size/2)
                {
                    if (teams1[i].TotalScore >= teams2[j].TotalScore)
                    {
                        res[c]=teams1[i];
                        i++;
                        c++;
                    }
                    else
                    {
                        res[c]= teams2[j];  
                        j++;
                        c++;
                    }
                }
                while (i < size/2)
                {
                    res[c] = teams1[i];
                    i++;
                    c++;
                }
                while (j < size/2)
                {
                    res[c] = teams2[j];
                    j++;
                    c++;
                }
                return res;
            }
            
            public void Print()
            {
                
            }
        }


    }

    //


}
