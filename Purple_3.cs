using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            private int _judgements;

            public string Name { get { return _name; } }
            public string Surname { get { return _surname; } }
            public double[] Marks
            {
                get
                {
                    if (_marks == null) { return default(double[]); }
                    var newArray = new double[_marks.Length];
                    Array.Copy(_marks, newArray, newArray.Length);
                    return newArray;
                }
            }
            public int[] Places
            {
                get
                {
                    if (_places == null) { return default(int[]); }
                    var newArray = new int[_places.Length];
                    Array.Copy(_places, newArray, newArray.Length);
                    return newArray;
                }
            }
            public int Score
            {
                get
                {
                    if (_places == null) { return 0; }
                    int score = 0;
                    for (int i = 0; i < _places.Length; ++i)
                    {
                        score += _places[i];
                    }
                    return score;
                }
            }
            private int HighestPlace
            {
                get
                {
                    if (_places == null) { return 0; }
                    int highestPlace = int.MaxValue;
                    for (int i = 0; i < _places.Length; ++i)
                    {
                        if (_places[i] < highestPlace)
                        {
                            highestPlace = _places[i];
                        }
                    }
                    return highestPlace;
                }
            }
            private double MarksSum
            {
                get
                {
                    if (_marks == null) { return 0; }
                    double marksSum = 0;
                    foreach (double x in _marks)
                    {
                        marksSum += x;
                    }
                    return marksSum;
                }
            }

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new double[7];
                _places = new int[7];
                for (int i = 0; i < _places.Length; ++i)
                {
                    _marks[i] = 0;
                    _places[i] = 0;
                }
                _judgements = 0;
            }

            public void Evaluate(double result)
            {
                if (_marks == null || _judgements == _marks.Length) { return; }
                _marks[_judgements] = result;
                _judgements++;
            }
            public static void SetPlaces(Participant[] participants)
            {
                if (participants == null) { return; }
                for (int i = 0; i < 7; ++i)
                {
                    Participant[] arr = participants
                                           .OrderByDescending(p => p.Marks != null && i < p.Marks.Length ? p.Marks[i] : int.MinValue)
                                           .ThenBy(p => p.Places != null && p.Places.Length > 0 ? p.Places[p.Places.Length - 1] : int.MaxValue)
                                           .ToArray();
                    Array.Copy(arr, participants, participants.Length);
                    for (int j = 1; j <= participants.Length; ++j)
                    {
                        if (participants[j - 1]._places == null) { continue; }
                        participants[j - 1]._places[i] = j;
                    }
                }
                Participant[] array = participants
                                           .OrderBy(p => p.Places != null && p.Places.Length > 0 ? p.Places[p.Places.Length - 1] : int.MaxValue)
                                           .ToArray();
                Array.Copy(array, participants, array.Length);
            }
            public static void Sort(Participant[] array)
            {
                if (array == null) { return; }

                Participant[] arr = array
                         .OrderBy(participant => participant.Score)
                         .ThenBy(participant => participant.HighestPlace)
                         .ThenByDescending(participant => participant.MarksSum)
                         .ToArray();
                Array.Copy(arr, array, arr.Length);
            }
            public void Print() { }
        }

        public abstract class Skating
        {
            protected Participant[] _participants;
            protected double[] _moods;

            public Participant[] Participant
            { get
                {
                    if (_participants == null) return null;
                    return _participants;
                }
            }

            public double[] Moods
            {
                get
                {
                    if (_moods == null) return default(double[]);
                    var newArr = new double[_moods.Length];
                    Array.Copy(_moods, newArr, _moods.Length);
                    return newArr;
                }
            }

            public Skating(double[] moods)
            {
                _participants = new Participant[0];
                _moods = new double[7];
                if (moods == null) return;
                for (int i = 0; i < Math.Min(_moods.Length, moods.Length); ++i)
                {
                    _moods[i] = moods[i];
                }
                ModificateMood();
            }

            protected abstract void ModificateMood();
            public void Evaluate(double[] marks)
            {
                if (marks == null || _moods == null || _participants == null) return;

                for (int i = 0; i < _participants.Length; ++i) {
                    if (_participants[i].Score == 0)
                    {
                        for (int j = 0; j < marks.Length; ++j)
                        {
                            _participants[i].Evaluate(marks[j] * _moods[j]);
                        }
                        break;
                    }
                }
            }

            public void Add(Participant participant)
            {
                if (_participants == null) return;
                Array.Resize(ref _participants, _participants.Length + 1);
                _participants[_participants.Length - 1] = participant;
            }

            public void Add(Participant[] participants)
            {
                if (participants == null || _participants == null) return;
                foreach (Participant participant in participants)
                {
                    Add(participant);
                }
            }
        }

        public class FigureSkating : Skating
        {
            public FigureSkating(double[] moods) : base(moods) {}

            protected override void ModificateMood()
            {
                for(int i = 0; i < _moods.Length; ++i)
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
                for (int i = 0; i < _moods.Length; ++i)
                {
                    _moods[i] = _moods[i] * (1 + (double)(i + 1) / 100.0);
                }
            }
        }
    }
}
