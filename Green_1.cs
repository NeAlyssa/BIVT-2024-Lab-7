using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Lab_7.Green_1;


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



            protected double _standart;

            private static int _passedCount;



            public string Surname => _surname;
            public string Group => _group;
            public string Trainer => _trainer;
            public double Result => _result;

            public bool HasPassed => _result > 0 && _result <= _standart;
            public static int PassedTheStandard => _passedCount;


            public static Participant[] GetTrainerParticipants(Participant[] participants, Type participantType, string trainer)
            {
                int cnt= 0;
                for (int i = 0; i < participants.Length; i++)
                {
                    if (participants[i] != null && participants[i].GetType() == participantType && participants[i].Trainer == trainer)
                    {
                        cnt++;
                    }
                }
                Participant[] result = new Participant[cnt];

                int index = 0;
                for (int i = 0; i < participants.Length; i++)
                {
                    if (participants[i] != null && participants[i].GetType() == participantType && participants[i].Trainer == trainer)
                    {
                        result[index] = participants[i];
                        index++;
                    }
                }
                return result;
            }


            static Participant()
            {
                _passedCount = 0;
            }
            public void Run(double result)
            {
                if (result <= 0)
                {
                    Console.WriteLine("неккоректный резульатат");
                    return;
                }
                if (this._result == 0)
                {
                    this._result = result;
                    if (HasPassed)
                    {
                        _passedCount++;
                    }
                }
                else
                {
                    Console.WriteLine("уже есть");
                }
            }


            public Participant(string surname, string group, string trainer)
            {
                this._surname = surname;
                this._group = group;
                this._result = 0;
                this._trainer = trainer;
            }
            public void Print()
            {
                Console.WriteLine($"{_surname}/{_group}/{_trainer}/{_result}");
                Console.WriteLine(HasPassed ? "+" : "-");
            }
        }
        public class Participant100M : Participant
        {
            public Participant100M(string surname, string group, string trainer) : base(surname, group, trainer)
            {
                _standart = 12;
            }
        }

        public class Participant500M : Participant
        {
            public Participant500M(string surname, string group, string trainer) : base(surname, group, trainer)
            {
                _standart = 90;
            }
        }

    }
}
