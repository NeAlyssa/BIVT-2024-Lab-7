using System;
using System.Linq;

namespace Lab_7
{
    public class Purple_2
    {
        public struct Participant
        {
            private string _firstName;
            private string _lastName;
            private int _jumpDistance;
            private int[] _styleScores;
            private int _targetDistance;

            public string Name => _firstName;
            public string Surname => _lastName;
            public int Distance => _jumpDistance;
            public int[] Marks
            {
                get
                {
                    if (_styleScores == null) return default(int[]);
                    var copy = new int[_styleScores.Length];
                    Array.Copy(_styleScores, copy, _styleScores.Length);
                    return copy;
                }
            }

            public int Result
            {
                get
                {
                    if (_styleScores == null || _styleScores.Length != 5) return 0;
                    int[] sorted = new int[5];
                    Array.Copy(_styleScores, sorted, 5);
                    Array.Sort(sorted);
                    int judgePoints = sorted.Skip(1).Take(3).Sum();
                    int distancePoints = 60 + (_jumpDistance - _targetDistance) * 2;
                    return Math.Max(0, judgePoints + distancePoints);
                }
            }

            public Participant(string name, string surname)
            {
                _firstName = name;
                _lastName = surname;
                _jumpDistance = 0;
                _styleScores = new int[5];
                _targetDistance = 120;
            }

            public void Jump(int distance, int[] marks, int target)
            {
                if (marks == null || marks.Length != 5 || _styleScores == null || _jumpDistance > 0) return;
                _jumpDistance = distance;
                _targetDistance = target;
                Array.Copy(marks, _styleScores, marks.Length);
            }

            public static void Sort(Participant[] array)
            {
                if (array == null) return;
                for (int i = 1; i < array.Length; i++)
                {
                    var key = array[i];
                    int j = i - 1;
                    while (j >= 0 && array[j].Result < key.Result)
                    {
                        array[j + 1] = array[j];
                        j--;
                    }
                    array[j + 1] = key;
                }
            }

            public void Print()
            {
                Console.WriteLine($"{_firstName} {_lastName}");
                Console.WriteLine($"Distance: {_jumpDistance}");
                Console.Write("Marks: ");
                foreach (int mark in _styleScores)
                {
                    Console.Write(mark + "  ");
                }
                Console.WriteLine();
                Console.WriteLine("Score: " + Result);
                Console.WriteLine();
            }
        }

        public abstract class SkiJumping
        {
            private string _competitionName;
            private int _requiredDistance;
            private Participant[] _entries;

            public string Name => _competitionName;
            public int Standard => _requiredDistance;
            public Participant[] Participants => _entries;

            public SkiJumping(string name, int standard)
            {
                _competitionName = name;
                _requiredDistance = standard;
                _entries = new Participant[0];
            }

            public void Add(Participant jumper)
            {
                Array.Resize(ref _entries, _entries.Length + 1);
                _entries[^1] = jumper;
            }

            public void Add(Participant[] jumpers)
            {
                if (jumpers == null) return;
                foreach (var jumper in jumpers)
                {
                    Add(jumper);
                }
            }

            public void Jump(int distance, int[] marks)
            {
                for (int i = 0; i < _entries.Length; i++)
                {
                    if (_entries[i].Distance == 0)
                    {
                        var temp = _entries[i];
                        temp.Jump(distance, marks, _requiredDistance);
                        _entries[i] = temp;
                        break;
                    }
                }
            }

            public void Print()
            {
                Console.WriteLine($"Competition: {_competitionName}");
                Console.WriteLine($"Target distance: {_requiredDistance}m\n");
                foreach (var p in _entries)
                {
                    p.Print();
                }
            }
        }

        public class JuniorSkiJumping : SkiJumping
        {
            public JuniorSkiJumping() : base("100m", 100) { }
        }

        public class ProSkiJumping : SkiJumping
        {
            public ProSkiJumping() : base("150m", 150) { }
        }
    }
}
