using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    public class Purple_1
    {
        public  class Participant
        {
            private string _name;
            private string _surname;
            private double[] _coefs;
            private int[,] _marks;
            private int _numberJump;
            private double _total; 

            public string Name => _name;
            public string Surname => _surname;
            public double[] Coefs
            {
                get
                {
                    if (_coefs == null) return null;
                    double[] copy = new double[_coefs.Length];
                    Array.Copy(_coefs, copy, _coefs.Length);
                    return copy;
                }
            }

            public int[,] Marks
            {
                get
                {
                    if (_marks == null) return null;
                    int[,] copy = new int[_marks.GetLength(0), _marks.GetLength(1)];
                    Array.Copy(_marks, copy, _marks.Length);
                    return copy;
                }
            }

            public double TotalScore
            {
                get
                {
                    if (_coefs == null || _marks == null) return 0;
                    return _total;
                }
            }

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _coefs = new double[] { 2.5, 2.5, 2.5, 2.5 };
                _marks = new int[4, 7];
                _numberJump = 0;
                _total = 0; //
                for (int i = 0; i < _marks.GetLength(0); i++)
                {
                    for (int j = 0; j < _marks.GetLength(1); j++)
                    {
                        _marks[i, j] = 0;
                    }
                }

            }

            public void SetCriterias(double[] coefs)
            {
                if (_coefs == null || coefs == null || coefs.Length != _coefs.Length) return;
                for (int i = 0; i < _coefs.Length; i++)
                {
                    _coefs[i] = coefs[i];
                }
            }

            public void Jump(int[] marks)
            {
                if (_marks == null || marks == null || _numberJump >= _marks.GetLength(0) || _marks.GetLength(1) != marks.Length) return;
                for (int i = 0; i < marks.Length; i++)
                {
                    _marks[_numberJump, i] = marks[i];
                }

                double summ = 0;
                for (int i = 0; i < _marks.GetLength(0); i++)
                {
                    double total = 0;
                    double max = double.MinValue;
                    double min = double.MaxValue;
                    for (int j = 0; j < _marks.GetLength(1); j++)
                    {
                        total += _marks[i, j];
                        if (_marks[i, j] > max)
                        {
                            max = _marks[i, j];
                        }
                        if (_marks[i, j] < min)
                        {
                            min = _marks[i, j];
                        }
                    }
                    total = total - max - min;
                    total *= _coefs[i];
                    summ += total;

                }
                _total = summ;

                _numberJump++;

            }

            public static void Sort(Participant[] array)
            {
                if (array == null) return;


                Array.Sort(array, (a, b) => {
                    double x = b.TotalScore - a.TotalScore;
                    if (x < 0) return -1;
                    else if (x > 0) return 1;
                    else return 0;
                });

            }


            public void Print()
            {

                Console.WriteLine(Name + " " + Surname + " " + TotalScore);

            }
        }

        public class Judge
        {
            private string _name;
            private int[] _marks;
            private int _r;

            private string Name => _name;

            public Judge(string name, int[] marks) 
            { 
                _name = name;
                _r = 0;
                if (marks != null) 
                {
                    _marks = new int[marks.Length];
                    Array.Copy(marks, _marks, marks.Length);
                }
            }

            public int CreateMark()
            {
                if(_marks == null) return 0;
                if(_r == _marks.Length)
                {
                    _r = 0;
                }
                int q = _marks[_r];
                _r++;
                return q;
            }

            public void Print()
            {

            }

        }
        public class Competition
        {
            private Judge[] _judges;
            private Participant[] _participants;

            public Judge[] Judges => _judges;
            public Participant[] Participants => _participants;

            public Competition(Judge[] judges)
            {
                if(judges != null)
                {
                    _judges = new Judge[judges.Length];
                    Array.Copy(judges, _judges, judges.Length);
                }
                _participants = new Participant[0];
            }
            public void Evaluate(Participant jumper)
            {
                if (_judges == null) return;
                int[] marks = new int[7];
                int t = 0;

                foreach (var judge in _judges)
                {
                    if (t >= 7) break;
                    if (judge != null)
                    {
                        marks[t++] = judge.CreateMark();
                    }
                }
                jumper.Jump(marks);
            }
            public void Add(Participant participant)
            {
                if (participant == null || _participants == null) return;
                Evaluate(participant);
                var new_list = new Participant[_participants.Length + 1];
                Array.Copy(_participants, new_list, _participants.Length);
                new_list[new_list.Length - 1] = participant;
                _participants = new_list;
            }
            public void Add(Participant[] participants)
            {
                if(participants == null || _participants == null) return;
                var new_list_2 = new Participant[_participants.Length + participants.Length];
                for(int i = 0; i < _participants.Length; i++)
                {
                    if (participants[i] == null) return;
                    Evaluate(participants[i]);
                }
                Array.Copy(_participants, new_list_2, _participants.Length);
                Array.Copy(participants, new_list_2, participants.Length);
                _participants = new_list_2;
            }
            public void Sort()
            {
                Participant.Sort(_participants);
            }
        }
    }
}
