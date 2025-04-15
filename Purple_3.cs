using System;
using System.Linq;

namespace Lab_7
{
    public class Purple_3
    {
        public struct Participant
        {
            private string _firstName;
            private string _lastName;
            private double[] _marks;
            private int[] _places;
            private int _judgeCount;

            public string Name => _firstName;
            public string Surname => _lastName;
            public double[] Marks
            {
                get
                {
                    if (_marks == null)
                        return null;
                    double[] copy = new double[_marks.Length];
                    Array.Copy(_marks, copy, _marks.Length);
                    return copy;
                }
            }
            public int[] Places
            {
                get
                {
                    if (_places == null)
                        return null;
                    int[] copy = new int[_places.Length];
                    Array.Copy(_places, copy, _places.Length);
                    return copy;
                }
            }
            public int Score => _places == null ? 0 : _places.Sum();

            private int TopPlace => _places == null ? int.MaxValue : _places.Min();
            private double TotalSum => _marks == null ? 0 : _marks.Sum();

            public Participant(string name, string surname)
            {
                _firstName = name;
                _lastName = surname;
                _marks = new double[7];
                _places = new int[7];
                _judgeCount = 0;
            }

            public void Evaluate(double result)
            {
                if (_marks == null || _judgeCount >= 7) return;
                _marks[_judgeCount] = result;
                _judgeCount++;
            }

            public static void SetPlaces(Participant[] participants)
            {
                if (participants == null) return;

                for (int i = 0; i < 7; i++)
                {
                    var sorted = participants
                        .Select((p, index) => new { Participant = p, Index = index })
                        .OrderByDescending(x => x.Participant._marks[i])
                        .ThenBy(x => x.Participant.Score)
                        .ThenByDescending(x => x.Participant.TotalSum)
                        .ToArray();

                    for (int j = 0; j < sorted.Length; j++)
                    {
                        participants[sorted[j].Index]._places[i] = j + 1;
                    }
                }
            }

            public static void Sort(Participant[] array)
            {
                if (array == null) return;
                var sorted = array
                    .OrderBy(p => p.Score)
                    .ThenBy(p => p.TopPlace)
                    .ThenByDescending(p => p.TotalSum)
                    .ToArray();
                Array.Copy(sorted, array, sorted.Length);
            }

            public void Print()
            {
                Console.WriteLine($"{_firstName} {_lastName}");
                Console.Write("Marks: ");
                foreach (double mark in Marks)
                {
                    Console.Write(mark + "  ");
                }
                Console.WriteLine();
                Console.Write("Places: ");
                foreach (int place in Places)
                {
                    Console.Write(place + "  ");
                }
                Console.WriteLine();
                Console.WriteLine("Total Score: " + Score);
                Console.WriteLine();
            }
        }

        public abstract class Skating
        {
            protected Participant[] _participants;
            protected double[] _moods;

            public Participant[] Participants => _participants;
            public double[] Moods => _moods;

            public Skating(double[] moods)
            {
                _moods = new double[7];
                for (int i = 0; i < 7 && i < moods.Length; i++)
                {
                    _moods[i] = moods[i];
                }
                _participants = new Participant[0];
                ModificateMood();
            }

            protected abstract void ModificateMood();

            public void Evaluate(double[] marks)
            {
                if (_participants == null || marks == null || marks.Length != 7) return;

                for (int i = 0; i < _participants.Length; i++)
                {
                    if (_participants[i].Marks.All(x => x == 0))
                    {
                        var updated = _participants[i];
                        for (int j = 0; j < 7; j++)
                        {
                            updated.Evaluate(marks[j] * _moods[j]);
                        }
                        _participants[i] = updated;
                        break;
                    }
                }
            }

            public void Add(Participant p)
            {
                if (_participants == null) _participants = new Participant[0];
                Array.Resize(ref _participants, _participants.Length + 1);
                _participants[^1] = p;
            }

            public void Add(Participant[] arr)
            {
                if (arr == null) return;
                foreach (var p in arr)
                {
                    Add(p);
                }
            }
        }

        public class FigureSkating : Skating
        {
            public FigureSkating(double[] moods) : base(moods) { }

            protected override void ModificateMood()
            {
                for (int i = 0; i < _moods.Length; i++)
                {
                    _moods[i] += i / 10.0;
                }
            }
        }

        public class IceSkating : Skating
        {
            public IceSkating(double[] moods) : base(moods) { }

            protected override void ModificateMood()
            {
                for (int i = 0; i < _moods.Length; i++)
                {
                    _moods[i] += _moods[i] * i / 100.0;
                }
            }
        }
    }
}