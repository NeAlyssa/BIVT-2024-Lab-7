using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

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
            private int indexJumper;
            public string Name => _name;
            public string Surname => _surname;
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
                    var newMatrix = new int[_marks.GetLength(0), _marks.GetLength(1)];
                    Array.Copy(_marks, newMatrix, _marks.Length);
                    return newMatrix;
                }
            }
            public double TotalScore
            {
                get
                {
                    if (_marks == null || _coefs == null) return 0;
                    double tsum = 0;

                    for (int i = 0; i < _marks.GetLength(0); i++)
                    {
                        int sum = 0, max = -100000, min = 100000;
                        for (int j = 0; j < _marks.GetLength(1); j++)
                        {
                            sum += _marks[i, j];
                            if (_marks[i, j] > max) max = _marks[i, j];
                            if (_marks[i, j] < min) min = _marks[i, j];
                        }
                        sum -= max + min;
                        tsum += sum * _coefs[i];
                    }

                    return tsum;
                }
            }
            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _coefs = new double[] { 2.5, 2.5, 2.5, 2.5 };
                _marks = new int[4, 7];
                indexJumper = 0;
            }
            public void SetCriterias(double[] coefs)
            {
                if (coefs == null || _coefs == null || coefs.Length != 4) return;
                for (int i = 0; i < 4; i++)
                    if (coefs[i] > 3.5 || coefs[i] < 2.5) return;
                for (int i = 0; i < 4; i++)
                    _coefs[i] = coefs[i];
            }
            public void Jump(int[] marks)
            {
                if (marks == null || _marks == null || marks.Length != 7 || indexJumper >=4 ) return;
                for (int i = 0; i < marks.Length; i++)
                {
                    _marks[indexJumper, i] = marks[i];

                }
                indexJumper++;
            }

            public static void Sort(Participant[] array)
            {
                double[] sortarr = new double[array.Length];
                for (int i = 0; i < array.Length; i++)
                    sortarr[i] = array[i].TotalScore;
                for (int i = 0; i < array.Length; i++)
                {
                    for (int j = 0; j < array.Length - 1 - i; j++)
                    {
                        if (sortarr[j] < sortarr[j + 1])
                        {
                            double temp = sortarr[j];
                            sortarr[j] = sortarr[j + 1];
                            sortarr[j + 1] = temp;
                            Participant t = array[j];
                            array[j] = array[j + 1];
                            array[j + 1] = t;
                        }
                    }
                }

            }
            public void Print()
            {
                Console.WriteLine($"{Name} {Surname}: {TotalScore}");
            }
        }

        public class Judge
        {
            private string _name;
            private int[] _marks;
            private int _imarks;
            public string Name => _name;
            public Judge(string name, int[] marks)
            {
                _name = name;
                if (marks != null)
                {
                    _marks = new int[marks.Length];
                    Array.Copy(marks, _marks, marks.Length);
                }
            }
            public int CreateMark()
            {
                if (_marks == null || _marks.Length == 0) return 0;
                int ret = _marks[_imarks];
                _imarks = (_imarks + 1) % _marks.Length;

                return ret;
            }
            public void Print()
            {
                if (_marks == null || _marks.Length == 0) return;
                Console.Write($"{_name}: ");
                for (int i = 0; i< _marks.Length; i++)
                {
                    Console.Write($"{_marks[i]} ");
                }
            }
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
                    var newArray = new Judge[_judges.Length];
                    Array.Copy(_judges, newArray, _judges.Length);
                    return newArray;
                }
            }
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
            public Competition(Judge[] judges)
            {
                if (judges != null)
                {
                    _judges = new Judge[judges.Length];
                    Array.Copy(judges, _judges, judges.Length);
                }
                _participants = new Participant[0];
            }
            public void Evaluate(Participant jumper)
            {
                if (_judges == null || _judges.Length == 0) return;
                int[] marks = new int[7];
                for (int i = 0; i<7; i++)
                {
                    if (_judges[i] == null) return;
                    marks[i] = _judges[i].CreateMark();
                }
                jumper.Jump(marks);
            }
            public void Add(Participant participant)
            {
                if (_participants == null || participant == null) return;
                Evaluate(participant);
                Array.Resize(ref _participants, _participants.Length + 1);
                _participants[_participants.Length - 1] = participant;
            }
            public void Add(Participant[] participants)
            {
                if (_participants == null || participants == null || participants.Length == 0) return;
                foreach (var participant in participants)
                {
                    if (participant != null)
                    {
                        Evaluate(participant);
                        Array.Resize(ref _participants, _participants.Length + 1);
                        _participants[_participants.Length - 1] = participant;
                    }
                }
            }
            public void Sort()
            {
                Participant.Sort(_participants);
            }
        }

    }
}
