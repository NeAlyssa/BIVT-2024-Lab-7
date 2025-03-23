using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using static Lab_6.Purple_4;

namespace Lab_7
{
    public class Purple_3
    {
        public abstract class Skating
        {
            protected Participant[] _participants;
            protected double[] _moods;

            public Participant[] Participants => _participants;
            public double[] Moods => _moods;

            public Skating(double[] moods)
            {
                _participants = new Participant[0];
                _moods = new double[7];
                if (moods != null) return;
                for (int i = 0; i < Math.Min(7, moods.Length); i++)
                {
                    _moods[i] = moods[i];
                }
                ModificateMood();
            }
            protected abstract void ModificateMood();
            public void Evaluate(double[] marks)
            {
                if (_participants == null || _moods == null || marks == null) return;
                for (int i = 0; i < _participants.Length; i++)
                {
                    if (_participants[i].Score == 0)
                    {
                        for (int j = 0; j < marks.Length; j++)
                        {
                            _participants[i].Evaluate(marks[j] * _moods[j]);
                        }
                        return;
                    }
                }
            }
            public void Add(Participant s)
            {
                _participants.Append(s);
            }
            public void Add(Participant[] ss)
            {
                if (ss == null) return;
                foreach (Participant s in ss)
                {
                    _participants.Append(s);
                }
            }

        }
        public class FigureSkating : Skating
        {
            public FigureSkating(double[] moods) : base(moods) {}
            protected override void ModificateMood()
            {
                for (int i = 0; i < _moods.Length; i++)
                {
                    _moods[i] += (i + 1) * 1.0 / 10.0;
                }
            }
        }
        public class IceSkating : Skating
        {
            public IceSkating(double[] moods) : base(moods) {}
            protected override void ModificateMood()
            {
                for (int i = 0; i < _moods.Length; i++)
                {
                    _moods[i] *= (100 + i + 1) * 1.0 / 100.0;
                }
            }
        }
        public struct Participant
        {
            private string _name;
            private string _surname;
            private double[] _marks;
            private int[] _places;
            private int _score;
            private int _topplace;
            private double _totalmark;
            private int _numbmark;

            public string Name => _name;
            public string Surname => _surname;
            public int[] Places
            {
                get
                {
                    if (_places == null) return null;
                    int[] p = new int[_places.Length];
                    for (int i = 0; i < _places.Length; i++)
                    {
                        p[i] = _places[i];
                    }
                    return p;
                }
            }
            public double[] Marks
            {
                get
                {
                    if (_marks == null) return null;
                    double[] p = new double[_marks.Length];
                    for (int i = 0; i < _marks.Length; i++)
                    {
                        p[i] = _marks[i];
                    }
                    return p;
                }
            }
            public int Score => _score;
            //public int TopPlace => _topplace;
            //public double TotalMark => _totalmark;


            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new double[7];
                _places = new int[7];
                _score = 0;
                _topplace = int.MaxValue;
                _totalmark = 0;
                _numbmark = 0;
            }
            public void Evaluate(double result)
            {
                if (_numbmark < 7)
                {
                    _marks[_numbmark] = result;
                    _totalmark += result;
                    _numbmark++;
                }
            }
            public static void SetPlaces(Participant[] participants)
            {
                if (participants == null) return;
                for (int p = 0; p < 7; p++)
                {
                    for (int i = 0; i < participants.Length; i++)
                    {
                        for (int j = 0; j < participants.Length - 1; j++)
                        {
                            if (participants[j]._marks[p] < participants[j + 1]._marks[p])
                            {
                                Participant l = participants[j];
                                participants[j] = participants[j + 1];
                                participants[j + 1] = l;
                            }
                        }
                    }
        
                    for (int i = 0; i < participants.Length; i++)
                    {
                        participants[i]._places[p] = i + 1;
                        participants[i]._score += i + 1;
                        participants[i]._topplace = Math.Min(participants[i]._topplace, i + 1);
                    }
                }
            }


            public static void Sort(Participant[] array)
            {
                if (array == null) return;
                for (int i = 0; i < array.Length; i++)
                {
                    for (int j = 0; j < array.Length - 1; j++)
                    {
                        if (array[j]._score > array[j + 1]._score)
                        {
                            Participant l = array[j];
                            array[j] = array[j + 1];
                            array[j + 1] = l;
                        }
                        else if (array[j]._score == array[j + 1]._score) 
                        {
                            if (array[j]._topplace > array[j + 1]._topplace)
                            {
                                Participant l = array[j];
                                array[j] = array[j + 1];
                                array[j + 1] = l;
                            }
                            else if (array[j]._topplace == array[j + 1]._topplace) 
                            { 
                                if (array[j]._totalmark < array[j + 1]._totalmark)
                                {
                                    Participant l = array[j];
                                    array[j] = array[j + 1];
                                    array[j + 1] = l;
                                }
                            }
                        }
                    }
                }
            }
            public void Print()
            {
                Console.WriteLine(_name + " " + _surname + " " + _score + " " + _topplace + " " + _totalmark);
            }
        }
    }
}
