using System;
using System.Linq;

namespace Lab_7
{
    public class Purple_1
    {
        public class Judge
        {
            private string _name;
            private int[] _marks;
            private int _counter;
            public string Name => _name;

            public Judge(string name, int[] marks)
            {
                _name = name;
                _marks = marks != null ? marks.ToArray() : new int[0];
                _counter = 0;
            }

            public int CreateMark()
            {
                if (_marks == null || _marks.Length == 0) return 0;
                return _marks[_counter++ % _marks.Length];
            }

            public void Print()
            {
                Console.Write($"{_name,7} - ");
                foreach (int x in _marks) Console.Write($"{x} ");
                Console.WriteLine();
            }
        }

        public class Participant
        {
            private string _name;
            private string _surname;
            private double[] _coefs;
            private int[,] _marks;
            private int _jump;

            public string Name => _name;
            public string Surname => _surname;

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _coefs = Enumerable.Repeat(2.5, 4).ToArray();
                _marks = new int[4, 7];
                _jump = 0;
            }

            public double[] Coefs
            {
                get
                {
                    if (_coefs == null) return null;
                    var clone = new double[_coefs.Length];
                    Array.Copy(_coefs, clone, _coefs.Length);
                    return clone;
                }
            }

            public int[,] Marks
            {
                get
                {
                    if (_marks == null) return null;
                    var copy = new int[_marks.GetLength(0), _marks.GetLength(1)];
                    Array.Copy(_marks, copy, _marks.Length);
                    return copy;
                }
            }

            public void Jump(int[] marks)
            {
                if (marks == null || _marks == null || _jump > 3 || marks.Length != 7) return;
                for (int i = 0; i < 7; i++)
                {
                    _marks[_jump, i] = marks[i];
                }
                _jump++;
            }

            public void SetCriterias(double[] coefs)
            {
                if (_coefs == null || coefs == null || _coefs.Length != 4 || coefs.Length != 4) return;
                for (int i = 0; i < 4; i++)
                    _coefs[i] = coefs[i];
            }

            public static void Sort(Participant[] array)
            {
                if (array == null) return;
                double[] scores = array.Select(p => p.TotalScore).ToArray();
                for (int i = 1, j = 2; i < array.Length;)
                {
                    if (i == 0 || scores[i] < scores[i - 1])
                    {
                        i = j;
                        j++;
                    }
                    else
                    {
                        (scores[i], scores[i - 1]) = (scores[i - 1], scores[i]);
                        (array[i], array[i - 1]) = (array[i - 1], array[i]);
                        i--;
                    }
                }
            }

            public void Print()
            {
                Console.WriteLine($"{Name}   {Surname}   {TotalScore}");
            }

            public double TotalScore
            {
                get
                {
                    if (_marks == null || _coefs == null) return 0;
                    double total = 0;
                    for (int i = 0; i < 4; i++)
                    {
                        double sum = 0;
                        int min = 0, max = 0;
                        for (int j = 0; j < 7; j++)
                        {
                            if (_marks[i, j] > _marks[i, max]) max = j;
                            if (_marks[i, j] < _marks[i, min]) min = j;
                        }
                        for (int j = 0; j < 7; j++)
                        {
                            if (j != min && j != max) sum += _marks[i, j];
                        }
                        total += sum * _coefs[i];
                    }
                    return total;
                }
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
                _judges = judges;
                _participants = Array.Empty<Participant>();
            }

            public void Evaluate(Participant jumper)
            {
                if (_judges == null || jumper == null) return;
                int[] marks = new int[_judges.Length];
                for (int i = 0; i < _judges.Length; i++)
                {
                    marks[i] = _judges[i].CreateMark();
                }
                jumper.Jump(marks);
            }

            public void Add(Participant jumper)
            {
                if (jumper == null) return;
                Evaluate(jumper);
                int len = _participants.Length;
                var updated = new Participant[len + 1];
                Array.Copy(_participants, updated, len);
                updated[len] = jumper;
                _participants = updated;
            }

            public void Add(Participant[] jumpers)
            {
                if (jumpers == null) return;
                foreach (var jumper in jumpers)
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
