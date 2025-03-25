using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    public class Purple_2
    {
        public struct Participant
        {
            //поля
            private string _name;
            private string _surname;
            private int _distance;
            private int[] _marks;
            private int _result;
            private bool flag;
            //свойства
            public string Name => _name;
            public string Surname => _surname;
            public int Distance => _distance;


            public int[] Marks
            {
                get
                {
                    if (_marks == null) { return null; }
                    int[] copy = new int[_marks.Length];
                    Array.Copy(_marks, copy, _marks.Length);
                    return copy;
                }

            }
            public int Result => _result;

            //конструктор
            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _distance = 0;
                _result = 0;
                flag = false;
                _marks = new int[5];

                for (int i = 0; i < _marks.Length; i++)
                {
                    _marks[i] = 0;
                }
            }
            //методы
            public void Jump(int distance, int[] marks, int target)
            {
                if (marks == null || _marks == null || marks.Length != _marks.Length || flag == true || _distance <= 0) { return; }
                _distance = distance;
                flag = true;
                for (int i = 0; i < _marks.Length; i++)
                {
                    _marks[i] = marks[i];
                }
                int result = 0;
                int imax = 0, imin = 0;
                for (int i = 0; i < _marks.Length; i++)
                {
                    if (_marks[i] > _marks[imax])
                    {
                        imax = i;
                    }
                    if (_marks[i] < _marks[imin])
                    {
                        imin = i;
                    }
                    result += _marks[i];
                }
                result -= (_marks[imax] + _marks[imin]);
                result += 60 + 2*(_distance - target);
                if (result > 0)
                {
                    _result = result;
                }
                else
                {
                    _result = 0;
                }
            }
            public static void Sort(Participant[] array)
            {
                if (array == null) return;
                for (int i = 1, j = 2; i < array.Length;)
                {
                    if (i == 0 || array[i - 1].Result >= array[i].Result)
                    {
                        i = j;
                        j++;
                    }
                    else
                    {
                        Participant temp = array[i];
                        array[i] = array[i - 1];
                        array[i - 1] = temp;
                        i--;
                    }
                }
            }
            public void Print()
            {
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
            public void Add(Participant man)
            {
                Array.Resize(ref _participants, _participants.Length + 1);
                _participants[_participants.Length - 1] = man;
            }
            public void Add(Participant[] men)
            {
                Array.Resize(ref _participants, _participants.Length + men.Length);
                Array.Copy(men, _participants, men.Length);
            }
            public void Jump(int distance, int[] marks)
            {
                if(_participants == null || marks == null) return;
                for (int jmpr = 0; jmpr < _participants.Length; jmpr++)
                {
                    if (_participants[jmpr].Distance == 0)
                    {
                        _participants[jmpr].Jump(distance, marks, _standard);
                        break;
                    }
                }
            }
            public void Print()
            {

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
