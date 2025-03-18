using System;
using System.Linq;

namespace Lab_7
{
    public class Purple_3
    {
        public struct Participant
        {
            private string _name;
            private string _surname;
            private double[] _marks;
            private int[] _places;

            private int _marksAcquired;

            public string Name => _name;
            public string Surname => _surname;
            public double[] Marks
            {
                get
                {
                    if (_marks == null) return default(double[]);

                    var newArray = new double[_marks.Length];
                    Array.Copy(_marks, newArray, _marks.Length);
                    return newArray;
                }
            }
            public int[] Places
            {
                get
                {
                    if (_places == null) return default(int[]);

                    var newArray = new int[_places.Length];
                    Array.Copy(_places, newArray, _places.Length);
                    return newArray;
                }
            }
            public int Score
            {
                get
                {
                    if (_places == null) return 0;
                    return _places.Sum();
                }
            }
            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marksAcquired = 0;
                _marks = new double[7];
                _places = new int[7];
            }

            public void Evaluate(double result)
            {
                if (_marks == null || _marksAcquired >= 7) return;
                _marks[_marksAcquired++] = result;
            }

            public static void SetPlaces(Participant[] participants)
            {
                if (participants == null) return;

                for (int judgeIndex = 0; judgeIndex < 7; judgeIndex++)
                {
                    var sortedParticipants = participants
                        .Where(p => p.Marks != null && p.Places != null) 
                        .OrderByDescending(p => p.Marks[judgeIndex])  
                        .ToArray();

                    for (int i = 0; i < sortedParticipants.Length; i++)
                    {
                        sortedParticipants[i]._places[judgeIndex] = i + 1;
                    }


                    if (judgeIndex == 6)
                    {
                        sortedParticipants = sortedParticipants
                            .Concat(participants.Where(p => p.Marks == null))
                            .ToArray();

                        Array.Copy(sortedParticipants, participants, sortedParticipants.Length);
                    }
                }
            }
            private static void SortByJudge(Participant[] array, int judgeIndex)
            {
                foreach (var part in array)
                {
                    if (part.Marks == null) return;
                }
                for (int i = 0; i < array.Length; i++)
                {
                    Participant key = array[i];
                    int j = i - 1;

                    while (j >= 0 && array[j].Marks[judgeIndex] < key.Marks[judgeIndex])
                    {
                        array[j + 1] = array[j];
                        j = j - 1;
                    }
                    array[j + 1] = key;
                }
            }

            public static void Sort(Participant[] array)
            {
                if (array == null) return;
                foreach (var part in array)
                {
                    if (part.Places == null) return;
                }
                for (int i = 0; i < array.Length; i++)
                {
                    Participant key = array[i];
                    int j = i - 1;

                    while (j >= 0 && CompareParticipants(array[j], key))
                    {
                        array[j + 1] = array[j];
                        j = j - 1;
                    }
                    array[j + 1] = key;
                }
                foreach (var part in array)
                {
                    part.Print();
                }
            }
            public void Print()
            {
                Console.WriteLine($"{_name} {Score} {Places.Min()} {Marks.Sum()}");
            }
            private static bool CompareParticipants(Participant p1, Participant p2)
            {
                //True: p1>p2
                //False: p1<p2
                if (p1.Score != p2.Score) return p1.Score > p2.Score;
                if (p1.Places.Min() != p2.Places.Min()) return p1.Places.Min() > p2.Places.Min(); //Parity by sum of places
                return p1.Marks.Sum() < p2.Marks.Sum(); //Parity by max judge place
            }
        }

        public abstract class Skating
        {
            protected Participant[] _participants;
            protected double[] _moods;

            public Participant[] Participants => _participants;
            public double[] Moods
            {
                get
                {
                    if (_moods == null) return default(double[]);

                    var newArray = new double[_moods.Length];
                    Array.Copy(_moods, newArray, _moods.Length);
                    return newArray;
                }
            }

            public Skating(double[] moods)
            {
                if (moods == null || moods.Length < 7) return;
                Array.Resize(ref moods, 7);
                _moods = (double[])moods.Clone();
                ModificateMood();
                _participants = new Participant[0];
            }

            protected abstract void ModificateMood();


            public void Evaluate(double[] marks)
            {
                if (_participants == null || marks == null) return;

                foreach (var participant in _participants)
                {
                    if (participant.Score == 0)
                    {
                        for (int i = 0; i < marks.Length; i++)
                        {
                            participant.Evaluate(marks[i] * Moods[i]);
                        }
                        break;
                    }
                }
            }



            public void Add(Participant skater)
            {
                if (_participants == null) _participants = new Participant[0];
                Array.Resize(ref _participants, _participants.Length + 1);
                _participants[_participants.Length - 1] = skater;
            }
            public void Add(Participant[] skaters)
            {
                if (skaters == null) return;
                foreach (var skater in skaters) Add(skater);
            }
        }

        public class FigureSkating : Skating
        {
            public FigureSkating(double[] moods) : base(moods) { }

            protected override void ModificateMood()
            {
                if (_moods == null) return;
                for (int i = 0; i < _moods.Length; i++)
                {
                    _moods[i] += (i + 1) / 10.0;
                }
            }
        }

        public class IceSkating : Skating
        {
            public IceSkating(double[] moods) : base(moods) { }

            protected override void ModificateMood()
            {
                if (_moods == null) return;
                for (int i = 0; i < _moods.Length; i++)
                {
                    _moods[i] *= 1 + (i + 1) / 100.0;
                }
            }
        }
    }
}
