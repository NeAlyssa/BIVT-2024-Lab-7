﻿using System;
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

            public string Name { get { return _name; } }
            public string Surname { get { return _surname; } }
            public int Distance { get { return _distance; } }
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
                    int rez = 0;
                    int mi = int.MaxValue;
                    int ma = int.MinValue;
                    if (_distance < 0 || _marks == null) return 0;
                    for (int i = 0; i < Marks.Length; i++)
                    {
                        if (Marks[i] > ma) ma = Marks[i];
                        if (Marks[i] < mi) mi = Marks[i];
                        rez += Marks[i];
                    }
                    rez -= ma;
                    rez -= mi;
                    if (Distance == 120) rez += 60;
                    else if (Distance > 120)
                    {
                        rez += 60;
                        rez += (Distance - 120) * 2;
                    }
                    else
                    {
                        rez += (60 - (120 - Distance) * 2);
                    }
                    return rez;
                }
            }

            //конструктор
            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _distance = -1;
                _marks = new int[5];
            }
            public void Jump(int distance, int[] marks, int target)
            {
                if (distance < 0) return;
                if (marks == null || marks.Length != 5 || _marks.Length != 5) return;
                _distance = distance;
                for (int i = 0; i < marks.Length; i++)
                {
                    _marks[i] = marks[i];
                }
            }

            public static void Sort(Participant[] array)
            {
                if (array == null || array.Length == 0) return;
                var array1 = array.OrderByDescending(x => x.Result).ToArray();
                Array.Copy(array1, array, array.Length);
            }
            public void Print()
            {
                Console.WriteLine($"Имя: {Name}. Фамилия: {Surname}. Результат: {Result}");
            }
        }
        public abstract class SkiJumping
        {
            private string _name;
            private int _standart;
            private Participant[] _participant;
            

            public string Name
            {
                get
                {
                    return _name;
                }
            }
            public int Standart
            {
                get
                {
                    return _standart;
                }
            }

            public Participant[] Participant
            {
                get
                {
                    if (_participant == null) return 0;
                    var a = new Participant[Participant.Length];
                    Array.Copy(_participant, a, _participant.Length);
                    return a;
                }
            }

            public SkiJumping(string name, int standart)
            {
                _name = name;
                _standart = standart;
                _participant = new Participant[0];
            }


        
            public void Add(Participant skijumper)
            {
                if (_participant == null) return;
                var a = new Participant[_participant.Length];
                Array.Copy(_participant, a, _participant.Length);
                a = a.Append(skijumper).ToArray();
                _participant = a;
            }

            public void Add(Participant[] skijumpers)
            {
                if (_participant == null || skijumpers == null) return;
                
                var a = new Participant[_participant.Length];
                Array.Copy(_participant, a, _participant.Length);
                a = a.Concat(skijumpers).ToArray();
                _participant = a;
            }

            public void Jump(int distance, int[]marks)
            {
                if (_participant == null || marks==null) return;
                for (int i = 0; i < _participant.Length; i++)
                {
                    if (_participant[i].Distance == -1)
                    {
                        _participant[i].Jump(distance, marks, _standart);
                        break;
                    }
                }

            }
            public void Print()
            {
                Console.WriteLine($"Название события : {Name}, Стандарт : {Standart}, Участники : {Participant}");
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
