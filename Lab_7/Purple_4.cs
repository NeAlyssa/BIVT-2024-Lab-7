using System;

namespace Lab_7
{
    public class Purple_4
    {
        public class Sportsman
        {
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
                if (_time != default(double))
                {
                    return;
                }

                _time = time;
            }

            public static void Sort(Sportsman[] array)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    Sportsman key = array[i];
                    int j = i - 1;

                    while (j >= 0 && array[j].Time > key.Time) //ascending
                    {
                        array[j + 1] = array[j];
                        j = j - 1;
                    }
                    array[j + 1] = key;
                }
            }

            public void Print()
            {
                Console.WriteLine(_name + " " + _surname);
                Console.WriteLine($"Time: {_time}");
                Console.WriteLine();
            }
        }

        public class SkiMan : Sportsman
        {
            public SkiMan(string name, string surname) : base(name, surname) { }

            public SkiMan(string name, string surname, double time) : base(name, surname)
            {
                Run(time);
            }
        }

        public class SkiWoman : Sportsman
        {
            public SkiWoman(string name, string surname) : base(name, surname) { }

            public SkiWoman(string name, string surname, double time) : base(name, surname)
            {
                Run(time);
            }
        }

        public class Group
        {
            private string _name;
            private Sportsman[] _teammates;

            public string Name => _name;

            public Sportsman[] Sportsmen
            {
                get
                {
                    if (_teammates == null)
                    {
                        return default(Sportsman[]);
                    }

                    var newArray = new Sportsman[_teammates.Length];
                    Array.Copy(_teammates, newArray, _teammates.Length);
                    return newArray;
                }
            }

            public Group(string name)
            {
                _name = name;
                _teammates = new Sportsman[0]; //zero because we resize the array everytime Add() is called
            }
            public Group(Group group)
            {
                if (group == null)
                {
                    return;
                }

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
                if (_teammates == null)
                {
                    return;
                }

                Array.Resize(ref _teammates, _teammates.Length + 1); //resize to n+1
                _teammates[_teammates.Length - 1] = newSportsman; //add new sportsman to the end of new array
            }

            public void Add(Sportsman[] newSportsmen)
            {
                if (newSportsmen == null || _teammates == null)
                {
                    return;
                }

                int oldLength = _teammates.Length;
                Array.Resize(ref _teammates, _teammates.Length + newSportsmen.Length); //resize to n+k
                Array.Copy(newSportsmen, 0, _teammates, oldLength, newSportsmen.Length); //add the new sportsmen to the end of the new array  
            }

            public void Add(Group group) //copy ALL the group members of the given group? Seems so
            {
                if (group.Sportsmen == null || _teammates == null)
                {
                    return;
                }

                int oldLength = _teammates.Length;
                Array.Resize(ref _teammates, _teammates.Length + group.Sportsmen.Length); //resize to n+k
                Array.Copy(group.Sportsmen, 0, _teammates, oldLength, group.Sportsmen.Length); //add the new sportsmen to the end of the new array 
            }

            public void Sort()
            {
                if (_teammates == null)
                {
                    return;
                }

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
                if (group1.Sportsmen == null || group2.Sportsmen == null)
                {
                    return default(Group);
                }

                Group finalists = new Group("Финалисты");

                group1.Sort();
                group2.Sort();

                int i = 0, j = 0;
                while (i < group1.Sportsmen.Length && j < group2.Sportsmen.Length)
                {
                    if (group1.Sportsmen[i].Time <= group2.Sportsmen[j].Time)
                    {
                        finalists.Add(group1.Sportsmen[i++]);
                    }
                    else
                    {
                        finalists.Add(group2.Sportsmen[j++]);
                    }
                }

                while (i < group1.Sportsmen.Length)
                {
                    finalists.Add(group1.Sportsmen[i++]);
                }

                while (j < group2.Sportsmen.Length)
                {
                    finalists.Add(group2.Sportsmen[j++]);
                }

                return finalists;
            }

            public void Split(out Sportsman[] men, out Sportsman[] women)
            {
                if (Sportsmen == null)
                {
                    men = null;
                    women = null;
                    return;
                }

                int m = 0, w = 0;
                foreach (var s in Sportsmen)
                {
                    if (s is SkiMan)
                    {
                        m++;
                    }
                    else if (s is SkiWoman)
                    {
                        w++;
                    }
                }
                men = new Sportsman[m];
                women = new Sportsman[w];

                m = 0;
                w = 0;
                for (int i = 0; i < Sportsmen.Length; i++)
                {
                    if (Sportsmen[i] is SkiMan)
                    {
                        men[m++] = Sportsmen[i];
                    }
                    else if (Sportsmen[i] is SkiWoman)
                    {
                        women[w++] = Sportsmen[i];
                    }
                }
            }

            public void Shuffle()
            {
                Sort(); //whole team is sorted by time
                Sportsman[] men, women;
                Split(out men, out women); //men and women sorted by time 

                if (men.Length == 0 || men == null || women == null || women.Length == 0) //solved the null problem
                {
                    return;
                }

                int matching = Math.Min(men.Length, women.Length);
                int remaining = men.Length - women.Length;

                int i = 0, w = 0, m = 0;

                if (men[0].Time < women[0].Time)//man first
                {
                    while (i < matching * 2)
                    {
                        _teammates[i++] = men[m++];
                        _teammates[i++] = women[w++];
                    }//finishing with a woman
                }
                else//woman first
                {
                    while (i < matching * 2)
                    {
                        _teammates[i++] = women[w++];
                        _teammates[i++] = men[m++];
                    }//finishing with a man
                }

                if (remaining > 0 && m < men.Length) //more men => add extra men
                {
                    _teammates[i++] = men[w++];
                }
                else if (remaining < 0 && w < women.Length) //more women => add extra women
                {
                    _teammates[i++]=women[w++];
                }
            }

            public void Print()
            {
                Console.WriteLine($"Group name: {_name}");
                foreach (Sportsman sportsman in _teammates)
                {
                    sportsman.Print();
                }
            }
        }


    }
}
