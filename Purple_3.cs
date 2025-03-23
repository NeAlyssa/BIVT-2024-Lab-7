using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

            public string Name => _name;
            public string Surname => _surname;
            public double[] Marks
            {
                get
                {
                    if (_marks == null) return null;
                    var NewArray = new double[_marks.Length];
                    Array.Copy(_marks, NewArray, _marks.Length);
                    return NewArray;
                }
            }
            public int[] Places
            {
                get
                {
                    if (_places == null) return null;
                    var NewArray = new int[_places.Length];
                    Array.Copy(_places, NewArray, _places.Length);
                    return NewArray;
                }
            }
            public int Score
            {
                get
                {
                    if (_places == null) return 0;
                    return Places.Sum();
                }
            }
            private int TopPlace
            {
                get
                {
                    if (_places == null) return 0;
                    return _places.Min();
                }
            }
            private double TotalMark
            {
                get
                {
                    if (_marks == null) return 0;
                    return _marks.Sum();
                }
            }

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new double[7];
                _places = new int[7];
            }

            public void Evaluate(double result)
            {
                for (int i = 0; i < 7; i++)
                {
                    if (_marks[i] == 0)
                    {
                        _marks[i] = result;
                        break;
                    }
                }
            }

            public static void SetPlaces(Participant[] participants)
            {
                if (participants == null) return;
                for (int i = 0; i < 7; i++)
                {
                    for (int k = 0; k < participants.Length; k++)
                    {
                        for (int j = 0; j < participants.Length - 1 - k; j++)
                        {
                            if (participants[j]._marks[i] < participants[j + 1]._marks[i])
                            {
                                var p = participants[j];
                                participants[j] = participants[j + 1];
                                participants[j + 1] = p;
                            }
                        }
                    }
                    for (int k = 0; k < participants.Length; k++)
                    {
                        participants[k]._places[i] = k + 1;
                    }
                }
            }

            public static void Sort(Participant[] array)
            {
                if (array == null) return;
                for (int k = 0; k < array.Length; k++)
                {
                    for (int j = 0; j < array.Length - 1 - k; j++)
                    {
                        if (array[j].Score > array[j + 1].Score)
                        {
                            var p = array[j];
                            array[j] = array[j + 1];
                            array[j + 1] = p;
                        }
                        else if (array[j].Score == array[j + 1].Score)
                        {
                            if (array[j].TopPlace > array[j + 1].TopPlace)
                            {
                                var p = array[j];
                                array[j] = array[j + 1];
                                array[j + 1] = p;
                            }
                            else if (array[j].TopPlace == array[j + 1].TopPlace)
                            {
                                if (array[j].TotalMark < array[j + 1].TotalMark)
                                {
                                    var p = array[j];
                                    array[j] = array[j + 1];
                                    array[j + 1] = p;
                                }
                            }
                        }
                    }
                }
            }

            public void Print()
            {
                Console.WriteLine(Name + " " + Surname + " " + Score + " " + TopPlace + " " + TotalMark);
            }
        }


        public abstract class Skating
        {
            protected Participant[] _participants;
            protected double[] _moods;
            private int _number;

            public Participant[] Participants => _participants;

            public double[] Moods
            {
                get
                {
                    if (_moods == null) return null;
                    var NewArray = new double[_moods.Length];
                    Array.Copy(_moods, NewArray, _moods.Length);
                    return NewArray;
                }
            }

            public Skating(double[] moods)
            {
                _moods = moods;
                ModificateMood();
                _participants = new Participant[0];
                _number = 0;
            }

            protected abstract void ModificateMood();

            public void Evaluate(double[] marks)
            {
                if (marks == null) return;
                if (marks.Length > 7) Array.Resize(ref marks, 7);
                for (int i = 0; i < marks.Length; i++)
                {
                    _participants[_number].Evaluate(marks[i] * _moods[i]);
                }
                _number++;
            }

            public void Add(Participant participant)
            {
                Array.Resize(ref _participants, _participants.Length + 1);
                _participants[_participants.Length + 1] = participant;
            }

            public void Add(Participant[] participants)
            {
                foreach (var r in participants)
                {
                    Add(r);
                }
            }
        }


        public class FigureSkating : Skating
        {
            public FigureSkating(double[] Moods) : base(Moods) { }

            protected override void ModificateMood()
            {
                for (double i = 0; i < 7; i++)
                {
                    _moods[i] += (i + 1) / 10;
                }
            }
        }

        public class IceSkating : Skating
        {
            public IceSkating(double[] Moods) : base(Moods) { }

            protected override void ModificateMood()
            {
                for (double i = 0; i < 7; i++)
                {
                    _moods[i] *= (1 + (i + 1) / 100);
                }
            }
        }
    }
}