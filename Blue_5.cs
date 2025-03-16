﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lab_7.Blue_4;
using static Lab_7.Blue_5;

namespace Lab_7
{
    public class Blue_5
    {
        public class Sportsman 
        {
            private string _name;
            private string _surname;
            private int _place;
            private bool _placeSet;

            // свойства
            public string Name => _name;
            public string Surname => _surname;
            public int Place => _place;


            // конструктор
            public Sportsman(string name, string surname)
            {
                _name = name;
                _surname = surname; 
                _place = 0;
                _placeSet = false;
            }

            public void SetPlace(int place) // устанавливает место, которое занял спортсмен в таблице.
            {
                if (!_placeSet)
                {
                    _place = place;
                    _placeSet = true;
                }
                else
                {
                    Console.WriteLine("Место можно установить только один раз.");
                }
            }
            public void Print()
            {
                Console.WriteLine($"{Name} {Surname}: {Place}");
            }
        }

        public abstract class Team 
        {
            private string _name;
            private Sportsman[] _sportsmen;
            private int _count;

            // свойства
            public string Name => _name;
            public Sportsman[] Sportsmen => _sportsmen;
            

            public int SummaryScore //  возвращает количество баллов, набранных командой в зависимости от мест, занятых её участниками
            {
                get
                {
                    if (_sportsmen == null) return 0;

                    if (_sportsmen == null || _sportsmen.Length == 0) return 0;

                    int[] scores = { 0, 5, 4, 3, 2, 1 };

                    return _sportsmen.Sum(s => (s.Place >= 1 && s.Place <= 5) ? scores[s.Place] : 0);
                }
            }
            public int TopPlace // выводит наивысшее место, которое занял кто-то из членов команды. 

            {
                get
                {
                    if (_sportsmen == null || _sportsmen.Length == 0) return 0;

                    int topPlace = 18;
                    for (int i = 0; i < _count; i++)
                    {
                        if (_sportsmen[i].Place < topPlace)
                        {
                            topPlace = _sportsmen[i].Place;
                        }
                    }
                    return topPlace;
                }
            }


            // конструктор
            public Team(string name)
            {
                _name = name;
                _sportsmen = new Sportsman[6];
                _count = 0;
            }

            public void Add(Sportsman sportsman)
            {
                if (_sportsmen == null) return;

                if (_count < _sportsmen.Length)
                {
                    _sportsmen[_count] = sportsman;
                    _count++;
                }
            }
            public void Add(Sportsman[] newSportsmen)
            {
                if (_sportsmen == null || _sportsmen.Length == 0 || _sportsmen == null) return;

                foreach (var sportsman in newSportsmen)
                {
                    Add(sportsman);
                }
            }

            public static void Sort(Team[] teams)
            {
                if (teams == null || teams.Length == 0) return;
                // делаем сортировку


                for (int i = 0; i < teams.Length; i++)
                {
                    for (int j = 0; j < teams.Length - i - 1; j++)
                    {
                        if (teams[j].SummaryScore < teams[j + 1].SummaryScore)
                        {
                            (teams[j], teams[j + 1]) = (teams[j + 1], teams[j]);
                        }
                        else if (teams[j].TopPlace > teams[j + 1].TopPlace && teams[j].SummaryScore == teams[j + 1].SummaryScore)
                        {
                            (teams[j], teams[j + 1]) = (teams[j + 1], teams[j]);
                        }
                    }
                }
            }
            protected abstract double GetTeamStrength();
            public static Team GetChampion(Team[] teams)
            {
                if (teams == null || teams.Length == 0) return null;

                Team person = teams[0];
                double max = person.GetTeamStrength();
                foreach (var team in teams)
                {
                    double power = team.GetTeamStrength();
                    if (power > max)
                    {
                        person = team;
                        max = power;
                    }
                }
                return person;
            }
            public void Print()
            {
                Console.WriteLine(Name);
                for (int i = 0; i < _count; i++)
                {
                    _sportsmen[i].Print();
                }
                Console.WriteLine($"Общий счёт: {SummaryScore}, Наивысшее место: {TopPlace}");
            }
        }
        public class ManTeam : Team
        {
            public ManTeam(string name) : base(name)
            {
            }
            protected override double GetTeamStrength()
            {
                int suma = 0, c = 0, result = 0;
                foreach (Sportsman man in this.Sportsmen)
                {
                    if (man == null) continue;
                    suma += man.Place;
                    c++;
                }
                result = 100 / (suma / c);

                return result;
            }
        }
        public class WomanTeam : Team
        {
            public WomanTeam(string name) : base(name) 
            { 
            }
            protected override double GetTeamStrength()
            {
                int c = 0, result = 0, suma = 0, proiz = 0;
                foreach (Sportsman woman in this.Sportsmen)
                {
                    if (woman == null) continue;
                    suma += woman.Place;
                    c++;
                    proiz *= woman.Place;
                }
                result = 100 * suma * c / proiz;

                return result;
            }
        }
    }
}

