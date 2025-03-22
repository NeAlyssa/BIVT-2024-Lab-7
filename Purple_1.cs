using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Lab_6
{
    public class Purple_1
    {
        public class Judge
        {
            private string _name;
            private int[] _marks;
            private int MarkIndex;

            public string Name => _name;

            public Judge(string name, int[] marks)
            {
                _name = name;
                _marks = marks;
                MarkIndex = -1;
            }

            public int CreateMark()
            {
                if (_marks == null) return 0;
                MarkIndex += 1;
                return _marks[MarkIndex % _marks.Length];
            }

            public void Print()
            {
                Console.WriteLine();
            }
        }

        public class Competition
        {
            private Judge[] _judges;
            private Participant[] _participants;

            public Judge[] Judges() => _judges;
            public Participant[] Participants => _participants;

            public Competition(Judge[] judges)
            {
                _judges = judges;
                _participants = new Participant[0];
            }
            public void Evaluate(Participant jumper)
            {
                if (jumper == null || jumper == null) return;
                int[] Marks = new int[_judges.Length];
                for (int i = 0; i < _judges.Length; i++)
                {
                    Marks[i] = _judges[i].CreateMark();
                }
            }
            public void Add(Participant p)
            {
                if (p == null || _participants == null || _judges == null) return;
                Participant[] pp = new Participant[_participants.Length + 1];
                for (int i = 0; i < _participants.Length; i++)
                {
                    pp[i] = _participants[i];
                }
                pp[_participants.Length] = p;
                _participants = pp;
            }
            public void Add(Participant[] p)
            {
                if (p == null) return;
                foreach (Participant pp in p)
                {
                    Add(pp);
                }
            }
            public void Sort()
            {
                Participant.Sort(_participants);
            }
        }
        public class Participant
        {
            private string _name;
            private string _surname;
            private double[] _coefs;
            private int[,] _marks;
            private double _totalscore;
            private int _jumps;

            public string Name => _name;
            public string Surname => _surname;
            public double[] Coefs
            {
                get
                {
                    if (_coefs == null) return null;
                    double[] p = new double[_coefs.Length];
                    for (int i = 0; i < _coefs.Length; i++)
                    {
                        p[i] = _coefs[i];
                    }
                    return p;
                }
            }
            public int[,] Marks
            {
                get
                {
                    if (_marks == null) return null;
                    int[,] p = new int[_marks.GetLength(0),_marks.GetLength(1)];
                    for (int i = 0; i < _marks.GetLength(0); i++)
                    {
                        for (int j = 0; j < _marks.GetLength(1); j++)
                        { 
                            p[i, j] = _marks[i, j];
                        }
                    }
                    return p;
                }
            }
            public double TotalScore => _totalscore;


            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _coefs = new double[4];
                _marks = new int[4, 7];
                _jumps = 0;
                for (int i = 0; i < 4; i++)
                {
                    _coefs[i] = 2.5;
                }
                _totalscore = 0;
            }
            public void SetCriterias(double[] coefs) 
            {
                if (coefs == null) return;
                for (int i = 0; i < Math.Min(coefs.Length, _coefs.Length); i++)
                {
                    _coefs[i] = coefs[i];
                }
            }
            public void Jump(int[] marks)
            {
                if (marks == null) return;
                if (_jumps < 4)
                {
                    int sm = 0;
                    int mn = 8;
                    int mx = 0;
                    for (int i = 0; i < 7; i++)
                    {
                        _marks[_jumps, i] = marks[i];
                        if (marks[i] < mn)
                            mn = marks[i];
                        if (marks[i] > mx)
                            mx = marks[i];
                        sm += marks[i];
                    }
                    _totalscore += (sm - mn - mx) * _coefs[_jumps];
                    _jumps++;
                }
            }
            public static void Sort(Participant[] array)
            {
                if (array == null) return;
                for (int i = 0; i < array.Length; i++)
                {
                    for (int j = 0; j < array.Length - 1; j++)
                    {
                        if (array[j]._totalscore < array[j + 1]._totalscore)
                        {
                            Participant l = array[j];
                            array[j] = array[j + 1];
                            array[j + 1] = l;
                        }
                    }
                }
            }
            public void Print()
            { 
                Console.WriteLine(_name + " " + _surname + " " + _totalscore);
            }
        }
    }
}
