using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Schema;

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
            private int index;
            public string Name => _name;
            public string Surname => _surname;
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
            public int Score
            {
                get
                {
                    if (_places == null || _marks == null) return 0;
                    int score = 0;
                    for (int i = 0; i < _places.Length; i++)
                    {
                        score += _places[i];

                    }

                    return score;
                }
            }
            private double TotalMark
            {
                get
                {
                    if (_places == null || _marks == null) return 0;
                    double t = 0;
                    for (int i = 0; i < _marks.Length; i++)
                        t += _marks[i];
                    return t;
                }
            }
            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _places = new int[7];
                _marks = new double[7];
                index = 0;
            }
            public void Evaluate(double result)
            {
                if (_marks == null || index == _marks.Length || index >= 7) return;
                _marks[index] = result;
                index++;
            }

            public static void SetPlaces(Participant[] participants)
            {
                if (participants == null || participants.Length == 0) return;
               
                for (int i = 0; i < 7; i++)
                {
                    for (int j = 0; j < participants.Length-1; j++)
                    {
                        double max = participants[j].Marks[i];
                        int ind = j;
                        for (int k = j + 1; k < participants.Length; k++)
                        {
                            if (participants[k].Marks[i] > max)
                            {
                                max = participants[k].Marks[i];
                                ind = k;
                            }
                        }
                        Participant temp = participants[j];
                        participants[j] = participants[ind];
                        participants[ind] = temp;
                    }
                    for (int k = 0; k < participants.Length; k++)
                        participants[k]._places[i] = k + 1;
                }


            }


            public static void Sort(Participant[] array)
            {
                if (array == null) return;
                for (int i = 0; i < array.Length; i++)
                {
                    if (array[i].Places == null) return;
                }
                for (int i = 0; i < array.Length; i++)
                {
                    for (int j = 0; j < array.Length - 1 - i; j++)
                    {
                        SortPlaces(array[j]);
                        SortPlaces(array[j + 1]);
                        if (SumP(array[j]) > SumP(array[j + 1]))
                        {
                            Participant temp = array[j];
                            array[j] = array[j + 1];
                            array[j + 1] = temp;
                        }
                        else if (SumP(array[j]) == SumP(array[j + 1]))
                        {


                            if (array[j].Places[0] > array[j + 1].Places[0])
                            {
                                Participant temp = array[j];
                                array[j] = array[j + 1];
                                array[j + 1] = temp;
                            }

                            else if (array[j].Places[0] == array[j + 1].Places[0])
                            {
                                if (SumM(array[j]) < SumM(array[j + 1]))
                                {
                                    Participant temp = array[j];
                                    array[j] = array[j + 1];
                                    array[j + 1] = temp;

                                }
                            }

                        }
                    }

                }

            }

            private static void SortPlaces(Participant a)
            {
                if (a.Places == null) return;
                for (int i = 0; i < a.Places.Length; i++)
                {
                    for (int j = 0; j < a.Places.Length - 1 - i; j++)
                    {
                        if (a.Places[j] > a.Places[j + 1])
                        {
                            int temp = a._places[j];
                            a._places[j] = a._places[j + 1];
                            a._places[j + 1] = temp;
                        }
                    }
                }
            }
            private static int SumP(Participant a)
            {
                int sum = 0;
                if (a.Places == null) return 0;
                for (int i = 0; i < a.Places.Length; i++)
                    sum += a.Places[i];
                return sum;
            }
            private static double SumM(Participant a)
            {
                double sum = 0;
                if (a.Marks == null) return 0;
                for (int i = 0; i < a.Marks.Length; i++)
                    sum += a.Marks[i];
                return sum;
            }
            public void Print()
            {
                Console.WriteLine($"{Name} {Surname}: {Score} {Places[0]} {TotalMark}");
            }
        }
        public abstract class Skating
        {
            private Participant[] _participants;
            protected double[] _moods;
            public Participant[] Participants
            {
                get
                {
                    if (_participants == null) return null;
                    var newArray = new Participant[_participants.Length];
                    Array.Copy(_participants, newArray, _participants.Length);
                    return newArray;
                }
            }
            public double[] Moods
            {
                get
                {
                    if (_moods == null) return null;
                    var newArray = new double[_moods.Length];
                    Array.Copy(_moods, newArray, _moods.Length);
                    return newArray;
                }
            }
            public Skating(double[] moods)
            {
                if (moods == null || moods.Length < 7) return;
                Array.Resize(ref moods, 7);
                var newArray = new double[7];
                Array.Copy(moods, newArray, 7);
                _moods = newArray;
                ModificateMood();
            }
            protected abstract void ModificateMood();
            public void Evaluate(double[] marks)
            {
                if (marks == null) return;
                int index = -1;
                for (int i = 0; i < _participants.Length; i++)
                {
                    if(_participants[i].Marks.All(mark => mark == 0))
                    {
                        index = i; break;
                    }    
                }
                if (index == -1) return;
                for (int i = 0; i < _moods.Length; i++)
                    _participants[index].Evaluate(marks[i] * _moods[i]);
            }
            public void Add(Participant participant)
            {
                if (_participants == null) _participants = new Participant[0];
                Array.Resize(ref _participants, _participants.Length + 1);
                _participants[_participants.Length - 1] = participant;
            }
            public void Add(Participant[] participants)
            {
                if (_participants == null) _participants = new Participant[0];
                if (participants == null || participants.Length == 0) return;
                var newArray = new Participant[participants.Length];
                Array.Copy(participants, newArray, participants.Length);
                _participants = newArray;
            }
        }
        public class FigureSkating : Skating
        {
            public FigureSkating(double[] moods) : base(moods) { }
            protected override void ModificateMood()
            {
                if (_moods == null) return;
                for (int i = 0; i < _moods.Length; i++)
                    _moods[i] += (i+1) / 10.0;
            }
        }
        public class IceSkating : Skating
        {
            public IceSkating(double[] moods) : base(moods) { }
            protected override void ModificateMood()
            {
                if (_moods == null) return;
                for (int i = 0; i < _moods.Length; i++)
                    _moods[i] += _moods[i] * i / 100.0;
            }
        }
    }



}
