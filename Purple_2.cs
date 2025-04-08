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

            public string Name { get { return _name; } }
            public string Surname { get { return _surname; } }
            public int Distance { get { return _distance; } }
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
            public int Result
            {
                get
                {
                    if (_marks == null) return 0;
                    int result = 0;
                    int best = int.MinValue, worst = int.MaxValue;
                    for (int i = 0; i < _marks.Length; ++i)
                    {
                        result += _marks[i];
                        if (_marks[i] > best) { best = _marks[i]; }
                        if (_marks[i] < worst) { worst = _marks[i]; }
                    }
                    result -= worst + best;
                    result += 60 + (_distance - _target) * 2;
                    return result >= 0 ? result : 0;
                }
            }

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _distance = -1;
                _marks = new int[5];
                _target = 120;
                for (int i = 0; i < _marks.Length; ++i)
                {
                    _marks[i] = 0;
                }
            }

            public void Jump(int distance, int[] marks, int target)
            {
                if (distance < 0 || marks == null || _marks == null || marks.Length != _marks.Length) return;
                _distance = distance;
                for (int i = 0; i < _marks.Length; ++i)
                {
                    _marks[i] = marks[i];
                }
                _target = target;
            }
            public static void Sort(Participant[] array)
            {
                if (array == null) { return; }
                Participant[] query = (from participant in array
                                       orderby participant.Result descending
                                       select participant).ToArray();
                Array.Copy(query, array, array.Length);
            }
            public void Print() { }
        }

        public abstract class SkiJumping
        {
            private string _name;
            private int _standart;
            private Participant[] _participants;

            public string Name => _name;
            public int Standard => _standart;
            public Participant[] Participants
            {
                get
                {
                    if (_participants == null) return null;
                    return _participants;
                }
            }

            public SkiJumping(string name, int standart)
            {
                _name = name;
                _standart = standart;
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
                if (_participants == null || participants == null) return;
                foreach (Participant participant in participants)
                {
                    Add(participant);
                }
            }

            public void Jump(int distance, int[] marks)
            {
                if(_participants == null || marks == null) return;
                for(int i = 0; i < _participants.Length; ++i)
                {
                    if (_participants[i].Distance == -1)
                    {
                        _participants[i].Jump(distance, marks, _standart);
                        break;
                    } 
                }
            }

            public void Print() { }
        }

        public class JuniorSkiJumping : SkiJumping
        {
            public JuniorSkiJumping() : base("100m", 100) {}
        }

        public class ProSkiJumping : SkiJumping
        {
            public ProSkiJumping() : base("150m", 150) {}
        }
    }
}
