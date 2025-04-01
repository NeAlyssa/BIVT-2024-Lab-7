using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Lab_7.Green_4;

namespace Lab_7
{
    public class Green_4
    {
        public abstract class Discipline
        {
            private string _name;
            private Participant[] _participants;
            private int _participantCount;
            public string Name
            {
                get { return _name; }
            }
            public Participant[] Participants
            {
                get { return _participants; }
            }
            public Discipline(string name)
            {
                _name = name;
                _participants = new Participant[0];
                _participantCount = 0;
            }
            public void Add(Participant par)
            {
                if (_participants == null)
                {
                    _participants = new Participant[0];
                }
                Array.Resize(ref _participants, _participantCount + 1);
                _participants[_participantCount] = par;
                _participantCount++;
            }
            public void Add(Participant[] par)
            {
                int k = par.Length;
                for (int i = 0; i < k; i++)
                {
                    Add(_participants[i]);
                }
            }
            public void Sort()
            {
                if (_participants != null && _participantCount > 0)
                {
                    Participant.Sort(_participants);
                }
            }
            public void Print()
            {
                Console.WriteLine("Дисциплина: " + Name);
                if (_participantCount > 0 && _participants != null)
                {
                    for (int i = 0; i < _participantCount; i++)
                    {
                        _participants[i].Print();
                    }
                }
                else
                {
                    Console.WriteLine("Нет участниц");
                }
            }
            private protected Participant GetParticipantAt(int ind)
            {
                if (_participants != null && ind >= 0 && ind < _participantCount)
                {
                    return _participants[ind];
                }
                return default(Participant);
            }
            private protected void SetParticipant(int ind, Participant par)
            {
                if (ind >= 0 && _participants != null && ind < _participantCount)
                {
                    _participants[ind] = par;
                }
            }
            public abstract void Retry(int ind);
        }
        public struct Participant
        {
            private string _name;
            private string _surname;
            private double[] _jumps;
            private int _index;
            public string Name
            {
                get { return _name; }
            }
            public string Surname
            {
                get { return _surname; }
            }
            public double[] Jumps
            {
                get { return _jumps != null ? (double[])_jumps.Clone() : null; }
            }
            public double BestJump
            {
                get
                {
                    if (_jumps.Length == 0 || _jumps == null)
                    {
                        return 0;
                    }
                    return _jumps.Max();
                }
            }
            public Participant(string n, string sn)
            {
                _name = n;
                _surname = sn;
                _jumps = new double[3];
                _index = 0;
            }
            public void Jump(double res)
            {
                if (_jumps.Length == 0 || _jumps == null)
                {
                    return;
                }
                if (_index < 3 && res >= 0)
                {
                    _jumps[_index] = res;
                    _index++;
                }
            }
            public static void Sort(Participant[] arr)
            {
                if (arr == null || arr.Length == 0)
                {
                    return;
                }
                bool swapped;
                do
                {
                    swapped = false;
                    for (int i = 0; i < arr.Length - 1; i++)
                    {
                        if (arr[i].BestJump < arr[i + 1].BestJump)
                        {
                            Participant t = arr[i];
                            arr[i] = arr[i + 1];
                            arr[i + 1] = t;
                            swapped = true;
                        }
                    }
                } while (swapped);
            }
            public void Print()
            {
                Console.WriteLine("{0,-12} {1,-10} {2,-15:F2}", Name, Surname, BestJump);
            }
        }
        public class LongJump : Discipline
        {
            public LongJump() : base("Long jump")
            {
            }
            public override void Retry(int ind)
            {
                Participant participant = GetParticipantAt(ind);
                double BestJump = participant.BestJump;
                participant = new Participant(participant.Name, participant.Surname);
                participant.Jump(BestJump);
                SetParticipant(ind, participant);
            }
        }
        public class HighJump : Discipline
        {
            public HighJump() : base("High jump")
            {
            }
            public override void Retry(int ind)
            {
                Participant participant = GetParticipantAt(ind);
                double[] j = participant.Jumps;
                int k = (j != null) ? j.Length : 0;
                Participant newParticipant = new Participant(participant.Name, participant.Surname);
                for (int i = 0; i < k - 1; i++)
                {
                    newParticipant.Jump(j[i]);
                }
                SetParticipant(ind, newParticipant);
            }
        }
    }
}