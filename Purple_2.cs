using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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
            private int _target;

            public string Name => _name;
            public string Surname => _surname;
            public int Distance => _distance;
            public int[] Marks => _marks;

            public int Result
            {
                get
                {
                    if (_marks == null) return 0;
                    int sum = 0;
                    for (int i = 0; i < _marks.Length; i++)
                    {
                        sum += _marks[i];
                    }
                    sum -= (_marks.Min() + _marks.Max());
                    sum += 60 + (_distance - _target) * 2 > 0 ? 60 + (_distance - _target) * 2 : 0;
                    return sum;
                }
            }

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _distance = 0;
                _marks = new int[5];
                _target = 0;
            }

            public void Jump(int distance, int[] marks, int target)
            {
                if (_distance > 0 || marks == null || _marks == null) return;
                _distance = distance;
                _target = target;
                Array.Copy(marks, _marks, marks.Length);
            }

            public static void Sort(Participant[] array)
            {
                if (array == null) return;

                for (int i = 0; i < array.Length; i++)
                {
                    for (int j = 0; j < array.Length - i - 1; j++)
                    {
                        if (array[j].Result < array[j + 1].Result)
                        {
                            var t = array[j];
                            array[j] = array[j + 1];
                            array[j + 1] = t;
                        }

                    }
                }
            }
            public void Print()
            {
                Console.WriteLine(_name + " " + _surname);
                Console.WriteLine(Result);
                Console.WriteLine($"Distance: {_distance}");
                Console.Write("Marks: ");
                foreach (double var in _marks)
                {
                    Console.Write(var + "  ");
                }
                Console.WriteLine();
            }
        }

        public abstract class SkiJumping
        {
            private string _name;
            private int _standard;
            private Participant[] _participants;

            public string Name => _name;
            public int Standard => _standard;
            public Participant[] Participants => _participants;

            public SkiJumping(string name, int standard)
            {
                _name = name;
                _standard = standard;
                _participants = new Participant[0];
            }

            public void Add(Participant jumper)
            {
                Array.Resize(ref _participants, _participants.Length + 1);
                _participants[_participants.Length - 1] = jumper;
            }
            public void Add(Participant[] jumpers)
            {
                foreach (var jumper in jumpers) Add(jumper);
            }

            public void Jump(int distance, int[] marks)
            {
                for (int i = 0; i < _participants.Length; i++)
                {
                    if (_participants[i].Distance == 0)
                    {
                        var jumper = _participants[i];
                        jumper.Jump(distance, marks, Standard);
                        _participants[i] = jumper;
                        break;
                    }
                }
            }

            public void Print()
            {
                Console.WriteLine(Name);
                Console.WriteLine(Standard);
                foreach (var participant in _participants) participant.Print();
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