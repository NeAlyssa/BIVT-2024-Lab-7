using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Lab_7
{
    public class Purple_2
    {
        public struct Participant
        {
            private string _name;
            private string _surname;
            private int _distance;
            private int[] _marks;
            private int _extrapoint;

            public string Name => _name;
            public string Surname => _surname;
            public int Distance => _distance;
            public int[] Marks
            {
                get
                {
                    if (_marks == null) return default(int[]);
                    var newArray = new int[_marks.Length];
                    Array.Copy(_marks, newArray, _marks.Length);
                    return newArray;
                }
            }

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _distance = 0;
                _marks = new int[5];
                _extrapoint = 0;
            }
            public int Result
            {
                get
                {
                    if (_marks == null) return 0;
                    int sum = 0, max = -100000, min = 100000;
                    for (int i = 0; i < _marks.Length; i++)
                    {
                        sum += _marks[i];
                        if (_marks[i] > max) max = _marks[i];
                        if (_marks[i] < min) min = _marks[i];
                    }
                    sum -= max + min;
                    sum += _extrapoint;
                    if (sum < 0) sum = 0;
                    return sum;
                }
            }
            public void Jump(int distance, int[] marks, int target)
            {
                if (marks == null || marks.Length != 5 || _marks == null) return;
                _distance = distance;
                for (int i = 0; i < marks.Length; i++)
                    _marks[i] = marks[i];
                _extrapoint = 60 + (distance - target) * 2;
            }
            public static void Sort(Participant[] array)
            {
                double[] sortarr = new double[array.Length];
                for (int i = 0; i < array.Length; i++)
                    sortarr[i] = array[i].Result;
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
                Console.WriteLine($"{Name} {Surname}: {Result}");
            }
        }
        public abstract class SkiJumping
        {
            private string _name;
            private int _standard;
            private Participant[] _participants;
            public string Name => _name;
            public int Standard => _standard;
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
            public SkiJumping(string name, int standard)
            {
                _name = name;
                _standard = standard;
                _participants = new Participant[0];
            }
            public void Add(Participant participant)
            {
                if (_participants == null) return;
                Array.Resize(ref _participants, _participants.Length + 1);
                _participants[_participants.Length - 1] = participant;
            }
            public void Add(Participant[] participants)
            {
                if (_participants == null || participants == null || participants.Length == 0) return;
                var newArray = new Participant[participants.Length];
                Array.Copy(participants, newArray, participants.Length);
                _participants = newArray;


            }
            public void Jump(int distance, int[] marks)
            {
                for (int i =0; i < _participants.Length; i++)
                {
                    if (_participants[i].Distance == 0)
                    {
                        _participants[i].Jump(distance, marks, _standard);
                        return;
                    }
                }
            }
            public void Print()
            {
                Console.WriteLine($"{_name} {_standard}");
                if (_participants == null) return;
                foreach (var p in _participants)
                {
                    p.Print();
                }
            }
        }
        public class JuniorSkiJumping : SkiJumping
        {
            public JuniorSkiJumping() : base("100m", 100) { }
        }
        public class ProSkiJumping : SkiJumping
        {
            public ProSkiJumping() : base("150m", 150) { }
        }

    }
}
