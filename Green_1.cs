using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

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
            private bool _hasResult;
            private protected double _standard;
            private static int _countPassed;
            private static bool _isPrinted = false;
            private static bool _isPrintedTrainer = false;
            static Participant()
            {
                _countPassed = 0;
            }
            public static Participant[] FindParticipantsByTrainer(Participant[] list, Type type, string trainerName)
            {
                int num = 0;
                foreach (var p in list)
                {
                    if (p != null && p.GetType() == type && p.Trainer == trainerName)
                    {
                        num++;
                    }
                }
                Participant[] filtered = new Participant[num];
                int index = 0;
                foreach (var p in list)
                {
                    if (p != null && p.GetType() == type && p.Trainer == trainerName)
                    {
                        filtered[index++] = p;
                    }
                }
                return filtered;
            }
            public Participant(string surname, string group, string trainer)
            {
                _standard = 100;
                _surname = surname;
                _group = group;
                _trainer = trainer;
                _result = 0;
                _hasResult = false;
            }
            public string Surname => _surname;
            public string Group => _group;
            public string Trainer => _trainer;
            public double Result => _result;
            public bool HasPassed => _hasResult && _result <= _standard && _result > 0;
            public static int PassedCount => _countPassed;
            public void Run(double time)
            {
                if (time <= 0 || _hasResult)
                {
                    return;
                }
                _result = time;
                _hasResult = true;

                if (time <= _standard)
                {
                    _countPassed++;
                }
            }
            public void Print()
            {
                if (!_isPrinted)
                {
                    Console.WriteLine("Число участников, выполнивших норматив: {0}", PassedCount);
                    _isPrinted = true;
                }
                Console.WriteLine("{0,-12} {1,-10} {2,-12} {3,-10:F2} {4,-10}", Surname, Group, Trainer, Result, HasPassed);
            }
        }
        public class Sprinter100M : Participant
        {
            public Sprinter100M(string surname, string group, string trainer) : base(surname, group, trainer)
            {
                _standard = 12;
            }
        }
        public class Sprinter500M : Participant
        {
            public Sprinter500M(string surname, string group, string trainer) : base(surname, group, trainer)
            {
                _standard = 90;
            }
        }
    }
}
