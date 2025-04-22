using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    public class White_2
    {
        public class Participant
        {
            private readonly string _name;
            private readonly string _surname;
            private double _firstjump;
            private double _secondjump;
            private static readonly double _norma;

            public string Name => _name;
            public string Surname => _surname;
            public double FirstJump => _firstjump;
            public double SecondJump => _secondjump;
            public double BestJump => Math.Max(_firstjump, _secondjump);

            public bool IsPassed => FirstJump >= _norma && SecondJump >= _norma;

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _firstjump = 0;
                _secondjump = 0;
            }
            static Participant()
            {
                _norma = 3;
            }
            public Participant(string name, string surname, double firstjump, double secondjump)
            {
                _name = name;
                _surname = surname;
                _firstjump = firstjump;
                _secondjump = secondjump;
            }
            public void Jump(double result)
            {
                if (FirstJump == 0) _firstjump = result;
                else if (SecondJump == 0) _secondjump = result;

            }
            public static void Sort(Participant[] array)
            {
                int length = array.Length;
                for (int i = 0; i < length - 1; i++)
                {
                    for (int j = 0; j < length - i - 1; j++)
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
            public static Participant[] GetPassed(Participant[] participants)
            {
                if (participants == null || participants.Length == 0) return null;
                Participant[] checked_participants = new Participant[0];
                foreach (Participant participant in participants)
                {
                    if (participant.BestJump < _norma)
                    {
                        continue;
                    }
                    else
                    {
                        Array.Resize(ref checked_participants, checked_participants.Length + 1);
                        checked_participants[checked_participants.Length - 1] = participant;
                    }
                }
                return checked_participants;
            }
            public void Print()
            {
                Console.WriteLine($"{Surname} {Name}, лучший из 2 прыжков - {BestJump}");
            }
        }
    }
}
