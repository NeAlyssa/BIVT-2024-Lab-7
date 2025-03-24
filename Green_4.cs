using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lab_7.Green_4;

namespace Lab_7
{
    public class Green_4
    {
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
        public abstract class Discipline
        {
            //приватные поля
            private string _name;
            private Participant[] _participants;
            private int _count;

            //публичные свойства
            public string Name => _name;
            public Participant[] Participants => _participants;

            //конструктор
            public Discipline(string name)
            {
                _name = name;
                _participants = new Participant[0];
                _count = 0;
            }

            //метод добавляет 1 участника в список
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

            //метод добавляет несколько участников
            public void Add(Participant[] participants)
            {
                for (int i = 0; i < participants.Length; i++)
                {
                    Add(participants[i]);
                }
            }

            //сортировка
            public void Sort()
            {
                Participant.Sort(_participants);
            }

            private protected Participant GetParticipantAt(int index)
            {
                if (index >= 0 && index < _count)
                {
                    return _participants[index];
                }
                return default(Participant);
            }

            private protected void SetParticipant(int index, Participant participant)
            {
                if (index >= 0 && index < _count)
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
            public abstract void Retry(int index);//абстрактный метод добавляет попытки
        }

        //класс-наследник
        public class LongJump : Discipline
        {
            public LongJump() : base("Long jump") { }

            public override void Retry(int index)
            {
                Participant participant = GetParticipantAt(index);
                double bestJump = participant.BestJump;
                participant = new Participant(participant.Name, participant.Surname);

                participant.Jump(bestJump);  // Сохраняем лучший прыжок
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
                Participant new_Participant = new Participant(participant.Name, participant.Surname);
                for (int i = 0; i < jumps.Length - 1; i++)
                {
                    new_Participant.Jump(jumps[i]);
                }
                SetParticipant(index, new_Participant);
            }
        }
    }
}
