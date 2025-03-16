using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

            // свойства
            public string Name => _name;
            public int[] Scores => _scores;
            public int TotalScore
            {
                get
                {
                    if (_scores == null) return 0;

                    return _scores.Sum();
                }
            }


            // конструктор
            public Team(string name)
            {
                _name = name;
                _scores = new int[0];
            }

            // методы 

            public void PlayMatch(int result) // добавляет в массив игр результат очередного матча
            {
                if (_scores == null) return;
                int[] t = new int[_scores.Length + 1];
                for(int i = 0; i < _scores.Length; i++)
                {
                    t[i] = _scores[i];
                }
                t[t.Length - 1] = result;
                _scores = t;
            }

            public void Print()
            {
                Console.WriteLine($"{Name}: {TotalScore}");
            }
        }

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


        public class Group
        {
            private string _name;
            private Team[] _man;
            private Team[] _woman;
            private int _index; //индекс команды
            private int _manind;
            private int _womanind;

            // свойства
            public string Name => _name;
            public Team[] Man => _man;
            public Team[] Woman => _woman;


            // конструктор

            public Group(string name)
            {
                _name = name;
                _man = new Team[12];
                _woman = new Team[12];
                _index = 0;
                _manind = 0;
                _womanind = 0;
            }

            //методы

            public void Add(Team team) // одна команда в группу
            {
                if (team == null || _man == null || _woman == null) return;

                if (team is ManTeam manteam)
                {
                    if (_manind < _man.Length)
                    {
                        _man[_manind] = manteam;
                        _manind++;
                    }
                }
                else if (team is WomanTeam womanteam)
                {
                    if (_womanind < _woman.Length)
                    {
                        _woman[_womanind++] = womanteam;
                        _womanind++;
                    }
                }
            }
            public void Add(Team[] teams) // несколько
            {
                if (_man == null || _woman == null || teams.Length == 0 || teams == null) return;

                foreach (var team in teams)
                {
                    Add(team);
                }
            }
            public void Sort()
            {
                if (_man == null || _woman == null || _man.Length == 0 || _woman.Length == 0) return;
                // делаем сортировку
                SortOneTeam(_man);
                SortOneTeam(_woman);

            }

            private void SortOneTeam(Team[] team)
            {
                for (int i = 0; i < team.Length; i++)
                {
                    for (int j = 0; j < team.Length - i - 1; j++)
                    {
                        if (team[j].TotalScore < team[j + 1].TotalScore)
                        {       
                            (team[j], team[j + 1]) = (team[j + 1], team[j]);
                        }
                    }
                }
            }
            public static Group Merge(Group group1, Group group2, int size) // слияния двух отсортированных массивов команд двух групп в новую группу с ограничением по размеру
            {
                Group resultat = new Group("Финалисты");
                Team[] mans = OneMerge(group1._man, group2._man, size);
                Team[] womans = OneMerge(group1._woman, group2._woman, size);

                resultat.Add(mans);
                resultat.Add(womans);
                return resultat;

            }
            private static Team[] OneMerge(Team[] group1, Team[] group2, int size) // слияния двух отсортированных массивов команд двух групп в новую группу с ограничением по размеру
            {
                Team[] resultat = new Team[size];
                int i = 0, k = 0, h = 0, j = 0, n = 0;
                while (i < size && j < size)
                {
                    if (group1[i].TotalScore >= group2[j].TotalScore)
                    {
                        resultat[n] = group1[i] ;
                        i++;
                        n++;
                    }
                    else
                    {
                        resultat[n] = group2[j];
                        n++;
                        j++;
                    }
                }
                while (k < size)
                {
                    resultat[n] = group1[k];
                    k++;
                    n++;
                }
                while (h < size)
                {
                    resultat[n] = group2[h];
                    h++;
                    n++;
                }
                return resultat;
            }

            public void Print()
            {
                Console.WriteLine(_name);

                foreach(Team i in _man)
                {
                    i.Print();
                }
                foreach (Team i in _woman)
                {
                    i.Print();
                }

                Console.WriteLine();
            }
        }
    }
}
