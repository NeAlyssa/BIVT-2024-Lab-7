using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    public class Green_1
    {
        public abstract class Participant
        {
            private string _surname;
            private string _group;
            private string _trainer;
            private double _result;
            private bool _resultFilled;
            protected double _standard;
            private static int _passedCount;
            public string Surname => _surname;
            public string Group => _group;
            public string Trainer => _trainer;
            public double Result => _result;
            public static int PassedTheStandard => _passedCount;
            private static bool _Printed = false;
            public bool HasPassed => _resultFilled && _result > 0 && _result <= _standard;
            static Participant()
            {
                _passedCount = 0;
            }
            public static Participant[] GetTrainerParticipants(Participant[] participants, Type participantType, string trainer)
            {
                int cnt = 0;
                for (int i = 0; i < participants.Length; i++)
                {
                    if (participants[i] != null && participants[i].GetType() == participantType && participants[i].Trainer == trainer)
                    {
                        cnt++;
                    }
                }
                Participant[] res = new Participant[cnt];
                int ind = 0;
                for (int i = 0; i < participants.Length; i++)
                {
                    if (participants[i] != null && participants[i].GetType() == participantType && participants[i].Trainer == trainer)
                    {
                        res[ind] = participants[i];
                        ind++;
                    }
                }
                return res;
            }
            public Participant(string surname, string trainer, string group)
            {
                _standard = 100;
                _surname = surname;
                _trainer = trainer;
                _group = group;
                _result = 0;
                _resultFilled = false;
            }
            public void Run(double res)
            {
                if (res <= 0)
                {
                    return;
                }
                if (!_resultFilled)
                {
                    _result = res;
                    _resultFilled = true;
                    if (res <= _standard)
                    {
                        _passedCount++;
                    }
                }
            }
            public void Print()
            {
                if (!_Printed)
                {
                    Console.WriteLine("Всего нормативов за все забеги преодолено: {0}", PassedTheStandard);
                    _Printed = true;
                }
                Console.WriteLine("{0,-12} {1,-10} {2,-12} {3,-10} {4,-10}", Surname, Group, Trainer, Result.ToString("F2"), HasPassed);
            }
        }
        public class Participant100M : Participant
        {
            public Participant100M(string surname, string trainer, string group) : base(surname, trainer, group)
            {
                _standard = 12;
            }
        }
        public class Participant500M : Participant
        {
            public Participant500M(string surname, string trainer, string group) : base(surname, trainer, group)
            {
                _standard = 90;
            }
        }
    }



}