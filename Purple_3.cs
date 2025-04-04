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
            public static void SetPlaces(Participant[] participants)
            {
                if (participants == null || participants.Length == 0) return ;
                int n = participants.Length;
                foreach (var participant in participants)
                {
                    if (participant._marks == null || participant._places == null) return;
                }

                double [,] marks = new double[n,7];
                for (int i =0;i<n;i++)
                {
                    for (int j =0;j<7;j++)
                    {
                        marks[i,j] = participants[i]._marks[j];
                    }
                }

                double [,] copy = new double [n,7];
                Array.Copy(marks,copy,marks.Length);

                for (int j =0;j<7;j++)
                {
                    for (int i =1, g=2;i<n;)
                    {
                        if(i == 0 || copy[i - 1, j] >= copy[i, j])
                        {
                            i = g;
                            g++;
                        }
                        else
                        {
                            double temp = copy[i-1,j];
                            copy[i-1,j] = copy[i,j];
                            copy[i,j] = temp;
                            i--;
                        }
                    }
                }

                double [,] places = new double[n,7];
                for (int j =0;j<7;j++)
                {
                    double ex = copy[0,j];
                    int place = 1;
                    places[0,j] = place;
                    for (int i = 1;i< n;i++)
                    {
                        if (copy[i,j] != ex)
                        {
                            ex = copy[i,j];
                            places[i,j] = place+1;
                            place++;
                        }
                        else
                        {
                            places[i,j] = places[i-1,j];
                        }
                    }
                }

                for (int j = 0;j<7;j++)
                {
                    for (int i =0;i<n;i++)
                    {
                        for (int p =0; p<n;p++)
                        {
                            if (marks[i,j] == copy[p,j])
                            {
                                marks[i,j] = places[p,j];
                                break;
                            }
                        }
                    }
                }

                for (int i = 0; i < n; i++)
                {
                    for(int j = 0; j < 7; j++)
                    {
                        participants[i]._places[j] = (int)marks[i, j];
                    }
                }

                for(int j = 1, k = 2; j < n; )
                {
                    if(j == 0 || marks[j - 1, 6] <= marks[j, 6])
                    {
                        j = k;
                        k++;
                    }
                    else
                    {
                        (marks[j - 1, 6], marks[j, 6]) = (marks[j, 6], marks[j - 1, 6]);
                        (participants[j - 1], participants[j]) = (participants[j], participants[j - 1]);
                        j--;
                    }
                } 

            }

            public static void Sort (Participant[] array)
            {
                if (array == null) return;
                foreach(var participant in array)
                {
                    if (participant._marks == null || participant._places == null) return;
                }

                Array.Sort(array, (a,b) =>
                {
                    if (a.Score > b.Score)
                        return 1;
                    else if (a.Score < b.Score) return -1;
                    
                    //минимальное место
                    if (a._places.Min() > b._places.Min())
                        return 1;
                    else if(a._places.Min() < b._places.Min())
                        return -1;

                    //по сумме очков
                    if (a._marks.Sum() > b._marks.Sum())
                        return -1;
                    else if (a._marks.Sum() < b._marks.Sum())
                        return 1;
                    
                    return 0;
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
                    _moods[i] *= 1 + (i+1)/10.0;
                }
            }
        }
    }
}