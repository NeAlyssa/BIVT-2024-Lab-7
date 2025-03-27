using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lab_7
{
    public class Green_4
    {
        public abstract class Discipline
        {
            private readonly string _name;
            private readonly List<Participant> _participants = new();
            public string Name => _name;
            public IReadOnlyList<Participant> Participants => _participants.AsReadOnly();
            protected Discipline(string name)
            {
                _name = name;
            }
            public void Add(Participant participant)
            {
                _participants.Add(participant);
            }
            public void Add(IEnumerable<Participant> participants)
            {
                _participants.AddRange(participants);
            }
            public void Sort()
            {
                _participants.Sort((p1, p2) => p2.BestJump.CompareTo(p1.BestJump));
            }
            public void Print()
            {
                Console.WriteLine($"Дисциплина: {Name}");
                if (_participants.Count > 0)
                {
                    foreach (var participant in _participants)
                        participant.Print();
                }
                else
                {
                    Console.WriteLine("Нет участников.");
                }
            }
            protected Participant GetParticipantAt(int index)
            {
                return index >= 0 && index < _participants.Count ? _participants[index] : default;
            }
            protected void SetParticipant(int index, Participant participant)
            {
                if (index >= 0 && index < _participants.Count)
                    _participants[index] = participant;
            }
            public abstract void Retry(int index);
        }
        public readonly struct Participant
        {
            public string Name { get; }
            public string Surname { get; }
            private readonly double[] _jumps;
            public IReadOnlyList<double> Jumps => _jumps.ToArray();
            public double BestJump => _jumps.Length > 0 ? _jumps.Max() : 0;
            public Participant(string name, string surname)
            {
                Name = name;
                Surname = surname;
                _jumps = new double[3];
            }
            public Participant(string name, string surname, double[] jumps)
            {
                Name = name;
                Surname = surname;
                _jumps = jumps ?? new double[3];
            }
            public Participant Jump(double result)
            {
                if (result < 0) return this;

                double[] newJumps = (double[])_jumps.Clone();
                for (int i = 0; i < newJumps.Length; i++)
                {
                    if (newJumps[i] == 0)
                    {
                        newJumps[i] = result;
                        break;
                    }
                }
                return new Participant(Name, Surname, newJumps);
            }
            public static void Sort(Participant[] participants)
            {
                Array.Sort(participants, (p1, p2) => p2.BestJump.CompareTo(p1.BestJump));
            }
            public void Print()
            {
                Console.WriteLine($"{Name,-12} {Surname,-12} {BestJump,8:F2}");
            }
        }
        public class LongJump : Discipline
        {
            public LongJump() : base("Long Jump") { }
            public override void Retry(int index)
            {
                Participant participant = GetParticipantAt(index);
                SetParticipant(index, new Participant(participant.Name, participant.Surname).Jump(participant.BestJump));
            }
        }
        public class HighJump : Discipline
        {
            public HighJump() : base("High Jump") { }
            public override void Retry(int index)
            {
                Participant participant = GetParticipantAt(index);
                double[] jumps = participant.Jumps.ToArray();

                if (jumps.Length > 1)
                {
                    SetParticipant(index, new Participant(participant.Name, participant.Surname, jumps[..^1]));
                }
            }
        }
    }
