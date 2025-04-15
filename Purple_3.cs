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
            private int _kolvo;
            public string Name
            {
                get
                {
                    return _name;
                }
            }
            public string Surname
            {
                get
                {
                    return _surname;
                }
            }
            public double[] Marks
            {
                get
                {
                    if (_marks == null) return null;
                    double[] copy = new double[_marks.Length];
                    Array.Copy(_marks,copy,_marks.Length);
                    return copy;
                }
            }
            public int[] Places
            {
                get
                {
                    if (_places == null) return null;

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
                    return _places.Sum();
                }
            }

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new double[] {0,0,0,0,0,0,0};
                _places = new int[] {0,0,0,0,0,0,0};
                _kolvo = 0;
            }

            public void Evaluate(double result)
            {
                if (_marks == null || _kolvo >= 7) return;
                _marks[_kolvo] = result;
                _kolvo ++;
            }
            private int Top
            {
                get
                {
                    if (_places == null) return 0;
                    int max = int.MaxValue;
                    for (int i =0;i<_places.Length;i++)
                    {
                        if (_places[i] < max) max = _places[i]; 
                    }
                    return max;
                }
            }
            private double Sum
            {
                get
                {
                    if (_marks == null) return 0;
                    double a =0;
                    for (int i =0;i<+Marks.Length;i++) a+=_marks[i];
                    return a;
                }
            }
            public static void SetPlaces(Participant[] participants)
            {
                if (participants == null) return;
                Participant[] copy = new Participant[participants.Length];
                Array.Copy(participants, copy, participants.Length);
                for (int i =0;i<7;i++)
                {
                    copy = copy.OrderByDescending(x => x._marks != null ? x._marks[i] : int.MinValue).ToArray();
                    for (int j =0;j<participants.Length;j++)
                    {
                        if (copy[j]._places != null)
                        {
                            copy[j]._places[i] = j+1;
                        }
                    }
                }
                Array.Copy(copy,participants,copy.Length);
            }

            public static void Sort (Participant[] array)
            {
                Participant[] cop = new Participant[array.Length];
                Array.Copy(array, cop, array.Length);
                cop = cop.OrderBy(a => a._places != null ? a.Score : int.MaxValue).ThenBy(b => b.Top).ThenByDescending(c => c.Sum).ToArray();
                Array.Copy(cop, array, array.Length);
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
                _moods = new double[7];
                _participants = new Participant[0];
                if (moods == null) return;
                Array.Copy(moods, _moods,Math.Min(7,moods.Length));
                ModificateMood();
            }
            protected abstract void ModificateMood();
            public void Evaluate(double[] marks)
            {
                if (marks == null) return;
                int n = _participants.Length;
                for (int i =0;i<n;i++)
                {
                    if (_participants[i].Score == 0)
                    {
                        for (int j =0;j<marks.Length;j++)
                        {
                            _participants[i].Evaluate(marks[j] * _moods[j]);
                        }
                        break;
                    }
                }
            }
            public void Add(Participant a)
            {
                if (_participants == null) return;
                var copy = new Participant[_participants.Length +1];
                Array.Copy(_participants,copy,_participants.Length);
                copy[_participants.Length] = a;
                _participants = copy;
            }
            public void Add(Participant[] arr)
            {
                if (_participants == null || arr == null) return;
                for (int i =0;i<arr.Length;i++)
                {
                    Add(arr[i]);
                }
            }
        } 
        public class FigureSkating : Skating
        {
            public FigureSkating(double[] moods) : base(moods) { }
            protected override void ModificateMood()
            {
                if (_moods == null) return;
                for (int i =0;i<_moods.Length;i++)
                {
                    _moods[i] += (i+1)/10.0;
                }
            }
        }  
        public class IceSkating : Skating
        {
            public IceSkating(double[] moods) : base(moods) { }
            protected override void ModificateMood()
            {
                if (_moods == null) return;
                for (int i =0;i < _moods.Length;i++)
                {
                    _moods[i] += (_moods[i] * (1+i)/100.0);
                }
            }
        }
    }
}
