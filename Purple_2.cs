using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
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
            public int[] Marks
            {
                get
                {
                    if (_marks == null) return null;
                    var NewArray = new int[_marks.Length];
                    Array.Copy(_marks, NewArray, _marks.Length);
                    return NewArray;
                }
            }
            public int Distance => _distance;
            public int Result
            {
                get
                {
                    if (_marks == null || _target == 0) return 0;
                    var NewArray = new int[_marks.Length];
                    Array.Copy(_marks, NewArray, _marks.Length);
                    Array.Sort(NewArray);
                    int res = 0;
                    for (int i = 1; i < 4; i++)
                    {
                        res += NewArray[i];
                    }
                    res += 60 + 2 * (_distance - _target);
                    if (res < 0) return 0;
                    return res;
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
                _distance = distance;
                if (marks != null) 
                {
                    if (marks.Length > 5) Array.Resize(ref marks, 5);
                    for (int i = 0; i < marks.Length; i++)
                    {
                        _marks[i] = marks[i];
                }
                }
                _target = target;
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
                            var p = array[j];
                            array[j] = array[j + 1];
                            array[j + 1] = p;
                        }
                    }
                }
            }

            public void Print()
            {
                Console.WriteLine(Name + " " + Surname + " " + Distance);
            }
        }

        public abstract class SkiJumping
        {
            private string _name;
            private int _standart;
            private Participant[] _participants;
            private static int _number;

            public string Name => _name;
            public int Standard => _standart;
            public Participant[] Participants => _participants;

            public SkiJumping(string name, int standart)
            {
                _name = name;
                _standart = standart;
                _participants = new Participant[0];
            }

            static SkiJumping()
            {
                _number = 0;
            }

            public void Add(Participant participant)
            {
                Array.Resize(ref _participants, _participants.Length + 1);
                _participants[_participants.Length - 1] = participant;
            }

            public void Add(Participant[] participants)
            {
                foreach (var r in participants)
                {
                    Add(r);
                }
            }

            public void Jump(int distanse, int[] marks)
            {
                _participants[_number++].Jump(distanse, marks, _standart);
            }

            public void Print()
            {
                Console.WriteLine();
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
