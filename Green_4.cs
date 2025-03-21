using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    public class Green_4
    {
        public abstract class Discipline
        {
            private string _name;
            private Participant[] _participants;
            private int _count;

            public string Name => _name;
            public Participant[] Participants => _participants;
            public int Count => _count;

            public Discipline(string name)
            {
                _name = name;
                _participants = new Participant[0];
                _count = 0;
            }
            public void Add(Participant participant)
            {
                if (_participants == null)
                {
                    _participants = new Participant[0];
                }

                Array.Resize(ref _participants, _count + 1);
                _participants[_count] = participant;
                _count++;
            }
            public void Add(Participant[] participants)
            {
                for (int i = 0; i < participants.Length; i++)
                {
                    Add(participants[i]);
                }
            }
            public void Sort()
            {
                if (_participants == null || _participants == 0)
                {
                    return;
                }

                for (int i = 0; i < _participants.Length - 1; i++)
                {
                    for (int j = 0; j < _participants.Length - 1 - i; j++)
                    {
                        if (_participants[j].BestJump < _participants[j + 1].BestJump)
                        {
                            Participant t = _participants[j];
                            _participants[j] = _participants[j + 1];
                            _participants[j + 1] = t;
                        }
                    }
                }
            }
            private protected Participant GetParticipantAt(int index)
            {
                if (index >= 0 && _participants != null)
                {
                    return _participants[index];
                }
                return default(Participant);
            }
            private protected void SetParticipant(int index, Participant participant)
            {
                if (_participants != null && index >= 0)
                {
                    _participants[index] = participant;
                }
            }
            public void Print()
            {
                Console.WriteLine($"Дисциплина: {Name}");
                foreach (var participant in _participants)
                {
                    participant.Print();
                }
            }
            public abstract void Retry(int index);
        }
        public class LongJump : Discipline
        {
            public LongJump() : base("Long jump") { }

            public override void Retry(int index)
            {
                Participant participant = GetParticipantAt(index);

                double BestJump = participant.BestJump;
                participant = new Participant(participant.Name, participant.Surname);
                participant.Jump(BestJump);
                SetParticipant(index, participant);
            }
        }
        public class HighJump : Discipline
        {
            public HighJump() : base("High jump") { }

            public override void Retry(int index)
            {
                Participant participant = GetParticipantAt(index);

                double[] jumps = participant.Jumps;
                Participant new_ = new Participant(participant.Name, participant.Surname);

                for (int i = 0; i < jumps.Length - 1; i++)
                {
                    new_.Jump(jumps[i]);
                }
                SetParticipant(index, new_);
            }
        }
        public struct Participant
        {
            private string _name;
            private string _surname;
            private double[] _jumps;

            public string Name => _name;
            public string Surname => _surname;

            public double[] Jumps
            {
                get
                {
                    if (_jumps == null)
                    {
                        return null;
                    }

                    double[] arr = new double[_jumps.Length];
                    Array.Copy(_jumps, arr, _jumps.Length);
                    return arr;
                }
            }
            public double BestJump
            {
                get
                {
                    if (_jumps == null || _jumps.Length == 0)
                    {
                        return 0;
                    }
                    return _jumps.Max();
                }
            }


            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _jumps = new double[3];
            }

            public void Jump(double result)
            {
                if (_jumps == null)
                {
                    return;
                }

                for (int i = 0; i < _jumps.Length; i++)
                {
                    if (_jumps[i] == 0)
                    {
                        _jumps[i] = result;
                        return;
                    }
                }
            }

            public static void Sort(Participant[] array)
            {
                if (array == null || array.Length == 0)
                {
                    return;
                }

                for (int i = 0; i < array.Length - 1; i++)
                {
                    for (int j = 0; j < array.Length - 1 - i; j++)
                    {
                        if (array[j].BestJump < array[j + 1].BestJump)
                        {
                            Participant temp = array[j];
                            array[j] = array[j + 1];
                            array[j + 1] = temp;
                        }
                    }
                }
            }

            public void Print()
            {
                Console.WriteLine($"Участник: {Name} {Surname}");
                Console.WriteLine($"Попытки: {string.Join(", ", Jumps)}");
                Console.WriteLine($"Лучший результат: {BestJump:F2}");
                Console.WriteLine();
            }
        }
    }
}