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

            protected double _standard;
            private static int _passedCount;

            public string Surname => _surname;
            public string Group => _group;
            public string Trainer => _trainer;
            public double Result => _result;

            public static int PassedTheStandard => _passedCount;
            public bool HasPassed => _result > 0 && _result <= _standard;

            public static Participant[] GetTrainerParticipants(Participant[] participants, Type participantType, string trainer)
            {
                int k = 0;
                for (int i = 0; i < participants.Length; i++)
                {
                    if (participants[i] != null && participants[i].GetType() == participantType && participants[i].Trainer == trainer)
                    {
                        k++;
                    }
                }
                Participant[] result = new Participant[k];

                int ind = 0;
                for (int i = 0; i < participants.Length; i++)
                {
                    if (participants[i] != null && participants[i].GetType() == participantType && participants[i].Trainer == trainer)
                    {
                        result[ind] = participants[i];
                        ind++;
                    }
                }
                return result;
            }
            static Participant()
            {
                _passedCount = 0;
            }

            public Participant(string surname, string group, string trainer)
            {
                this._surname = surname;
                this._group = group;
                this._trainer = trainer;
                this._result = 0;
            }

            public void Run(double result)
            {
                if (result <= 0)
                {
                    Console.WriteLine("результат должен быть положительным");
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
                    Console.WriteLine("Результат уже установлен");
                }
            }

            public void Print()
            {
                Console.WriteLine($"Фамилия: {Surname}, Группа: {Group}, Тренер: {Trainer}, Результат: {Result} секунд.");
                Console.WriteLine(HasPassed ? "Норматив пройден" : "Норматив не пройден");
            }
        }
        public class Participant100M : Participant
        {
            public Participant100M(string surname, string group, string trainer) : base(surname, group, trainer)
            {
                _standard = 12;
            }
        }
        public class Participant500M : Participant
        {
            public Participant500M(string surname, string group, string trainer) : base(surname, group, trainer)
            {
                _standard = 90;
            }
        }
    }
}
