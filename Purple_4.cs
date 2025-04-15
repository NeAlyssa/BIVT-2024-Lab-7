using System;
using System.Linq;

namespace Lab_7
{
    public class Purple_4
    {
        public class Sportsman
        {
            private string _firstName;
            private string _lastName;
            private double _time;
            private bool _hasRun;

            public string Name => _firstName;
            public string Surname => _lastName;
            public double Time => _time;

            public Sportsman(string name, string surname)
            {
                _firstName = name;
                _lastName = surname;
                _time = 0;
                _hasRun = false;
            }

            public Sportsman(string name, string surname, double time)
            {
                _firstName = name;
                _lastName = surname;
                _time = time;
                _hasRun = true;
            }

            public void Run(double time)
            {
                if (!_hasRun)
                {
                    _time = time;
                    _hasRun = true;
                }
            }

            public virtual bool IsMan() => false;

            public void Print()
            {
                if (_hasRun)
                    Console.WriteLine($"{_firstName} {_lastName} - Time: {_time}");
                else
                    Console.WriteLine($"{_firstName} {_lastName} - Time: DNS");
            }

            public static void Sort(Sportsman[] array)
            {
                if (array == null) return;
                Array.Sort(array, (a, b) => a.Time.CompareTo(b.Time));
            }
        }

        public class SkiMan : Sportsman
        {
            public SkiMan(string name, string surname) : base(name, surname) { }
            public SkiMan(string name, string surname, double time) : base(name, surname, time) { }
            public override bool IsMan() => true;
        }

        public class SkiWoman : Sportsman
        {
            public SkiWoman(string name, string surname) : base(name, surname) { }
            public SkiWoman(string name, string surname, double time) : base(name, surname, time) { }
            public override bool IsMan() => false;
        }

        public class Group
        {
            private string _name;
            private Sportsman[] _sportsmen;
            private int _count;

            public string Name => _name;
            public Sportsman[] Sportsmen => _sportsmen;

            public Group(string name)
            {
                _name = name;
                _sportsmen = new Sportsman[0];
                _count = 0;
            }

            public Group(Group group)
            {
                _name = group._name;
                _sportsmen = new Sportsman[group._sportsmen.Length];
                Array.Copy(group._sportsmen, _sportsmen, group._sportsmen.Length);
                _count = group._count;
            }

            public void Add(Sportsman sportsman)
            {
                Array.Resize(ref _sportsmen, _count + 1);
                _sportsmen[_count] = sportsman;
                _count++;
            }

            public void Add(Sportsman[] sportsmen)
            {
                if (sportsmen == null) return;
                foreach (var sportsman in sportsmen)
                {
                    Add(sportsman);
                }
            }

            public void Add(Group group)
            {
                Add(group.Sportsmen);
            }

            public void Sort()
            {
                Sportsman.Sort(_sportsmen);
            }

            public static Group Merge(Group group1, Group group2)
            {
                group1.Sort();
                group2.Sort();

                Sportsman[] merged = group1.Sportsmen.Concat(group2.Sportsmen).ToArray();
                Group result = new Group("Финалисты");
                result._sportsmen = merged;
                result._count = merged.Length;
                return result;
            }

            public void Split(out Sportsman[] men, out Sportsman[] women)
            {
                men = _sportsmen.Where(s => s.IsMan()).ToArray();
                women = _sportsmen.Where(s => !s.IsMan()).ToArray();
            }

            public void Shuffle()
            {
                Sort();
                Split(out Sportsman[] men, out Sportsman[] women);
                Sportsman[] shuffled = new Sportsman[_count];
                int i = 0, m = 0, w = 0;

                while (i < _count)
                {
                    if (m < men.Length) shuffled[i++] = men[m++];
                    if (i < _count && w < women.Length) shuffled[i++] = women[w++];
                }

                _sportsmen = shuffled;
            }

            public void Print()
            {
                Console.WriteLine($"=========== {_name} ===========");
                foreach (var s in _sportsmen)
                {
                    s.Print();
                }
            }
        }
    }
}