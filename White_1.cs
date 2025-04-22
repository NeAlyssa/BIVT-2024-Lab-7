using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    public class White_1
    {
        public class Participant
        {
            private readonly string _surname;
            private readonly string _club;
            private double _firstjump;
            private double _secondjump;
            private static double _norma;
            private static int _jumpers;
            private static int _disqualified;

            public string Surname => _surname;
            public string Club => _club;
            public double FirstJump => _firstjump;
            public double SecondJump => _secondjump;
            public double JumpSum => _firstjump + _secondjump;
            public static int Jumpers => _jumpers;
            public static int Disqualified => _disqualified;


            public Participant(string surname, string club)
            {
                _surname = surname;
                _club = club;
                _firstjump = 0;
                _secondjump = 0;
                _jumpers++;
            }

            static Participant()
            {
                _norma = 5;
                _jumpers = 0;
                _disqualified = 0;
            }

            public void Jump(double result)
            {
                if (_firstjump == 0) _firstjump = result;
                else if (_secondjump == 0) _secondjump = result;
            }

            public static void Disqualify(ref Participant[] participants)
            {
                if (participants == null || participants.Length == 0) return;
                Participant[] checked_participants = new Participant[0];
                foreach (Participant participant in participants)
                {
                    if (participant.FirstJump < _norma || participant.SecondJump < _norma)
                    {
                        _disqualified++;
                        continue;
                    }
                    else
                    {
                        Array.Resize(ref checked_participants, checked_participants.Length + 1);
                        checked_participants[checked_participants.Length - 1] = participant;
                    }
                }
                participants = checked_participants;
                _jumpers = participants.Length;
            }

            public static void Sort(Participant[] array)
            {
                int length = array.Length;
                for (int i = 0; i < length - 1; i++)
                {
                    for (int j = 0; j < length - i - 1; j++)
                    {
                        if (array[j].JumpSum < array[j + 1].JumpSum)
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
                Console.WriteLine($"Фамилия - {Surname}, клуб - {Club}, сумма 2 прыжков: {FirstJump} + {SecondJump} = {JumpSum}");
            }
        }
    }
}
