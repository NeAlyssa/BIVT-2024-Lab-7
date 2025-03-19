using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    public class Purple_3
    {
        const int judges = 7;
        public struct Participant
        {
            //polya
            private string _name;
            private string _surname;
            private double[] _marks;
            private int[] _places;
            private int _current;

            //svoystva
            public string Name => _name;
            public string Surname => _surname;

            public double[] Marks
            {
                get
                {
                    if (_marks == null) { return null; }
                    double[] copy = new double[_marks.Length];
                    Array.Copy(_marks, copy, _marks.Length);
                    return copy;
                }
            }
            public int[] Places
            {
                get
                {
                    if (_places == null) { return null; }
                    int[] copy = new int[_places.Length];
                    Array.Copy(_places, copy, _places.Length);
                    return copy;
                }
            }
            public int Score
            {
                get
                {
                    if (_places == null) return 0;
                    int score = 0;
                    foreach (int x in _places)
                    {
                        score += x;
                    }
                    return score;
                }
            }
            private int Top
            {
                get
                {
                    if (_places == null) return 0;
                    int imin = 0;
                    for (int i = 0; i < _places.Length; i++)
                    {
                        if (_places[i] < _places[imin])
                        {
                            imin = i;
                        }
                    }
                    return _places[imin];
                }
            }
            private double TotalMark
            {
                get
                {
                    if (_marks == null) return 0;
                    double s = 0;
                    foreach (double x in _marks)
                    {
                        s += x;
                    }
                    return s;
                }
            }
            //konstructor
            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new double[judges];
                _places = new int[judges];
                _current = 0;
                for (int i = 0; i < _marks.Length; i++)
                {
                    _marks[i] = 0;
                    _places[i] = 0;
                }
            }
            //metody
            public void Evaluate(double result)
            {
                if (_marks == null || _current >= _marks.Length || result < 0 || result > 6) return;
                _marks[_current] = result;
                _current++;

            }
            public static void SetPlaces(Participant[] participants)
            {
                if (participants == null) return;

                for (int i = 0; i < judges; i++)
                {
                    //var sortedparticipants = participants.OrderByDescending(p => p.Marks != null && i < p.Marks.Length ? p.Marks[i] : 0).ToArray();
                    //for(int j = 0;  j < sortedparticipants.Length; j++)
                    //{
                    //    sortedparticipants[j].FillArrayPlaces(i, j + 1);
                    //}

                    Array.Sort(participants, (a, b) =>
                    {
                        double A = a.Marks != null ? a.Marks[i] : 0, B = b.Marks != null ? b.Marks[i] : 0;
                        double x = A - B;
                        if (x < 0) return 1;
                        else if (x > 0) return -1;
                        else return 0;
                    });
                    for (int j = 0; j < participants.Length; j++)
                        participants[j].FillArrayPlaces(i, j + 1);
                }

            }
            private void FillArrayPlaces(int i, int j)
            {
                if (_places == null || _places.Length == 0 || i < 0 || i > judges || i > _places.Length) return;
                _places[i] = j;
            }
            public static void Sort(Participant[] array)
            {
                if (array == null) return;
                Array.Sort(array, (a, b) =>
                {
                    if (a.Score == b.Score)
                    {
                        if (a.Top == b.Top)
                        {
                            double x = a.TotalMark - b.TotalMark;
                            if (x < 0) return 1;
                            else if (x > 0) return -1;
                            else return 0;
                        }
                        return a.Top - b.Top;
                    }
                    return a.Score - b.Score;
                });
            }
            public void Print()
            {
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
                if (moods.Length != 7) return;
                for (int i = 0; i < moods.Length; i++)
                {
                    _moods[i] = moods[i];
                    ModificateMood();
                }
            }
            protected abstract void ModificateMood();

            public void Evaluate(double[] marks)
            {
                if(marks == null || _participants ==  null || _moods == null) return;
                foreach (Participant participant in _participants)
                {
                    if (participant.Score == 0)
                    {
                        for (int i = 0; i < marks.Length; i++)
                        {
                            participant.Evaluate(marks[i] * _moods[i]);
                        }
                    }
                }
            }
            public void Add(Participant man)
            {
                Array.Resize(ref _participants, _participants.Length+1);
                _participants[_participants.Length - 1] = man;
            }
            public void Add(Participant[] men)
            {
                Array.Resize(ref _participants, _participants.Length + men.Length);
                Array.Copy(men, _participants, men.Length);
            }
        }
        public class FigureSkating : Skating
        {
            public FigureSkating(double[] moods) : base(moods) { }
            protected override void ModificateMood()
            {
                for(int i = 0; i < _moods.Length; i++)
                {
                    _moods[i] += i+1 / 10;
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
                    _moods[i] *= 1 + (i + 1) / 100;
                }
            }
        }
    }
}
