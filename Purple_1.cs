using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lab_7
{
    public class Purple_1
    {
        public class Participant
        {
            private string _name;
            private string _surname;
            private double[] _coefs;
            private int[,] _marks;
            private int _jumps;

            public string Name { get { return _name; } }
            public string Surname { get { return _surname; } }
            public double[] Coefs
            {
                get
                {
                    if (_coefs == null) return default(double[]);

                    var newArray = new double[_coefs.Length];
                    Array.Copy(_coefs, newArray, _coefs.Length);
                    return newArray;
                }
            }
            public int[,] Marks
            {
                get
                {
                    if (_marks == null) return default(int[,]);

                    int rows = _marks.GetLength(0), cols = _marks.GetLength(1);
                    var newArray = new int[rows, cols];
                    for (int i = 0; i < rows; ++i)
                    {
                        Array.Copy(_marks, i * cols, newArray, i * cols, cols);
                    }
                    return newArray;
                }
            }

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _coefs = new double[4];
                _marks = new int[4, 7];
                _jumps = 0;
                for (int i = 0; i < _coefs.Length; ++i) { _coefs[i] = 2.5; }
                for (int i = 0; i < _marks.GetLength(0); ++i)
                {
                    for (int j = 0; j < _marks.GetLength(1); ++j)
                    {
                        _marks[i, j] = 0;
                    }
                }
            }

            public double TotalScore
            {
                get
                {
                    if (_coefs == null || _marks == null) return 0;
                    double totalScore = 0;
                    for (int i = 0; i < _marks.GetLength(0); ++i)
                    {
                        int worst = int.MaxValue, best = int.MinValue;
                        for (int j = 0; j < _marks.GetLength(1); ++j)
                        {
                            totalScore += (double)_marks[i, j] * _coefs[i];
                            if (_marks[i, j] < worst) { worst = _marks[i, j]; }
                            if (_marks[i, j] > best) { best = _marks[i, j]; }
                        }
                        totalScore -= (double)(best + worst) * _coefs[i];
                    }
                    return totalScore;
                }
            }

            public void SetCriterias(double[] coefs)
            {
                if (coefs == null || _coefs == null) return;
                if (coefs.Length != _coefs.Length) return;
                for (int i = 0; i < coefs.Length; ++i)
                {
                    _coefs[i] = coefs[i];
                }
            }
            public void Jump(int[] marks)
            {
                if (_marks == null || marks == null) return;
                if (marks.Length != _marks.GetLength(1)) return;
                if (_jumps >= _marks.GetLength(0)) return;
                for (int i = 0; i < _marks.GetLength(1); ++i)
                {
                    _marks[_jumps, i] = marks[i];
                }
                ++_jumps;
            }
            public static void Sort(Participant[] array)
            {
                if (array == null) return;
                Participant[] query = (from participant in array
                                       orderby participant.TotalScore descending
                                       select participant).ToArray();
                Array.Copy(query, array, array.Length);
            }
            public void Print() { }
        }

        public class Judge
        {
            private string _name;
            private int[] _marks;
            private int _curMark;

            public string Name => _name;

            public Judge(string name, int[] marks)
            {
                _name = name;
                _marks = marks;
                _curMark = 0;
            }

            public int CreateMark()
            {
                if (_marks == null) return 0;
                return _marks[(_curMark++) % _marks.Length];
            }

            public void Print() { }
        }

        public class Competition
        {
            private Judge[] _judges;
            private Participant[] _participants;

            public Judge[] Judges
            {
                get
                {
                    if (_judges == null) return null;
                    return _judges;
                }
            }
            public Participant[] Participants {
                get
                {
                    if (_participants == null) return null;
                    return _participants;
                }
            }

            public Competition(Judge[] judges) {
                _judges = judges;
                _participants = new Participant[0];
            }

            public void Evaluate(Participant jumper)
            {
                if (jumper == null || _judges == null || _judges.Length == 0) return;

                int[] marks = new int[_judges.Length];

                for (int i = 0; i < _judges.Length; ++i)
                {
                    marks[i] = _judges[i].CreateMark();
                }
                jumper.Jump(marks);
            }

            public void Add(Participant jumper) {
                if (jumper == null || _participants == null || _judges == null) return;

                Evaluate(jumper);
                
                Array.Resize(ref _participants, _participants.Length + 1);
                _participants[_participants.Length - 1] = jumper;
            }

            public void Add(Participant[] jumpers)
            {
                if (jumpers == null) return;
                foreach (Participant jumper in jumpers)
                {
                    Add(jumper);
                }
            }

            public void Sort()
            {
                Participant.Sort(_participants);
            }

        }
    }
}
