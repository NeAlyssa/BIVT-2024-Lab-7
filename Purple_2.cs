using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            private int _jump;
            private int[] _marks;
            private int _ttarget;


            public string Name => _name;
            public string Surname => _surname;
            public int Distance => _jump;
            public int[] Marks
            {
                get
                {
                    if (_marks == null) return null;
                    int[] copy = new int[_marks.Length];
                    Array.Copy(_marks, copy, _marks.Length);
                    return copy;
                }
            }


            public int Result
            {
                get
                {
                    if (_marks == null || _jump < 0) return 0;
                    int summ = 0;
                    int max = _marks[0];
                    int min = _marks[0];
                    for (int i = 0; i < _marks.Length; i++)
                    {

                        summ += _marks[i];
                        if (_marks[i] > max)
                        {
                            max = _marks[i];
                        }
                        if (_marks[i] < min)
                        {
                            min = _marks[i];
                        }
                    }
                    int jump = _jump;
                    summ -= max + min;
                    summ += 60;
                    if (jump > 120)
                    {
                        jump -= 120;
                        summ += jump * 2;
                    }
                    else if (jump < 120)
                    {
                        jump = 120 - jump;
                        summ -= jump * 2;
                    }
                    if (summ < 0)
                        summ = 0;
                    return summ;
                }
            }

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new int[5];
                _jump = -1;
                _ttarget = 0;
                for (int i = 0; i < _marks.Length; i++)
                    _marks[i] = 0;
            }

            public void Jump(int distance, int[] marks, int target)
            {
                if (_marks == null || marks == null || _marks.Length == 0 || distance == null || _marks.Length != marks.Length || distance < 0) return;
                _jump = distance;
                Array.Copy(marks, _marks, marks.Length);
                _ttarget = (_jump - target) * 2 + 60;

            }

            public static void Sort(Participant[] array)
            {

                if (array == null) return;
                Array.Sort(array, (a, b) =>
                {
                    double x = b.Result - a.Result;
                    if (x < 0) return -1;
                    else if (x > 0) return 1;
                    else return 0;
                });

            }

            public void Print()
            {

            }
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
                    var copy = new Participant[_participants.Length];
                    Array.Copy(_participants, copy, _participants.Length);
                    return copy;
                }
            }

            public SkiJumping(string name, int standard)
            {
                _name = name;
                _standart = standard;
                _participants = new Participant[0];
            }
            public void Add(Participant participant)
            {
                if (_participants == null) return;
                var new_copy = new Participant[_participants.Length + 1];
                Array.Copy(_participants, new_copy, _participants.Length);
                new_copy[new_copy.Length - 1] = participant;
                _participants = new_copy;
            }
            public void Add(Participant[] participant)
            {
                if(_participants == null || participant == null) return;
                foreach(var i in participant)
                {
                    Add(i);
                }
            }
            public void Jump(int distance, int[] marks)
            {
                if (_participants == null) return;
                foreach(var i in _participants)
                {
                    if(i.Distance == 0)
                    {
                        i.Jump(distance, marks, _standart);
                    }
                }
            }
            public void Print()
            {

            }
        }
        public class JuniorSkiJumping : SkiJumping
        {
            public JuniorSkiJumping() : base ("100m", 100) { }
        }
        public class ProSkiJumping : SkiJumping
        {
            public ProSkiJumping() : base("150m", 150) { }
        }

    }
}
