using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_6
{
    public class Purple_4
    {
        public struct Sportsman {
            private string _name;
            private string _surname;
            private double _time;

            public string Name => _name;
            public string Surname => _surname;
            public double Time => _time;

            public Sportsman(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _time = default(double);
            }

            public void Run(double time)
            {
                if (_time != default(double)) return;

                _time = time;
            }

            public void Print()
            {
                Console.WriteLine(_name + " " + _surname);
                Console.WriteLine($"Time: {_time}");
                Console.WriteLine();
            }
        }

        public struct Group {
            private string _name;
            private Sportsman[] _teammates;

            public string Name => _name;

            public Sportsman[] Sportsmen
            {
                get
                {
                    if (_teammates == null) return default(Sportsman[]);

                    var newArray = new Sportsman[_teammates.Length];
                    Array.Copy(_teammates, newArray, _teammates.Length);
                    return newArray;
                }
            }

            public Group(string name){
                _name = name;
                _teammates = new Sportsman[0]; //zero because we resize the array everytime Add() is called
            }
            public Group(Group group)
            {
                _name = group.Name;
                if (group.Sportsmen == null)//no sportsmen to copy
                {
                    _teammates = new Sportsman[0];
                    return;
                }
                _teammates = new Sportsman[group.Sportsmen.Length];
                Array.Copy(group.Sportsmen, _teammates, group.Sportsmen.Length);
            }

            public void Add(Sportsman newSportsman)
            {
                if (_teammates == null) return;
                Array.Resize(ref _teammates, _teammates.Length + 1); //resize to n+1
                _teammates[_teammates.Length-1] = newSportsman; //add new sportsman to the end of new array
            }

            public void Add(Sportsman[] newSportsmen)
            {
                if (newSportsmen == null || _teammates == null) return;
                int oldLength = _teammates.Length;
                Array.Resize(ref _teammates, _teammates.Length + newSportsmen.Length); //resize to n+k
                Array.Copy(newSportsmen, 0, _teammates, oldLength, newSportsmen.Length); //add the new sportsmen to the end of the new array  
            }

            public void Add(Group group) //copy ALL the group members of the given group? Seems so
            {
                if (group.Sportsmen == null || _teammates == null) return;
                int oldLength = _teammates.Length;
                Array.Resize(ref _teammates, _teammates.Length + group.Sportsmen.Length); //resize to n+k
                Array.Copy(group.Sportsmen, 0, _teammates, oldLength, group.Sportsmen.Length); //add the new sportsmen to the end of the new array 
            }

            public void Sort()
            {
                if (_teammates == null) return;
                for (int i = 0; i < _teammates.Length; i++)
                {
                    Sportsman key = _teammates[i];
                    int j = i - 1;

                    while (j >= 0 && _teammates[j].Time > key.Time) //ascending
                    {
                        _teammates[j + 1] = _teammates[j];
                        j = j - 1;
                    }
                    _teammates[j + 1] = key;
                }
            }

            public static Group Merge(Group group1, Group group2)
            {
                if (group1.Sportsmen == null || group2.Sportsmen == null) return default(Group); //Should non-empty group + empty group = the non-empty group?
                Group finalists = new Group("Финалисты");

                group1.Sort();
                group2.Sort();
                
                int i = 0, j = 0;
                while (i < group1.Sportsmen.Length && j < group2.Sportsmen.Length)
                {
                    if (group1.Sportsmen[i].Time <= group2.Sportsmen[j].Time)
                        finalists.Add(group1.Sportsmen[i++]);
                    
                    else
                        finalists.Add(group2.Sportsmen[j++]);
                }

                while (i < group1.Sportsmen.Length)
                    finalists.Add(group1.Sportsmen[i++]);

                while (j < group2.Sportsmen.Length)
                    finalists.Add(group2.Sportsmen[j++]);

                return finalists;
            }

            public void Print()
            {
                Console.WriteLine($"Group name: {_name}");
                foreach(Sportsman sportsman in _teammates)
                {
                    sportsman.Print();
                }
            }
        }


    }
}
