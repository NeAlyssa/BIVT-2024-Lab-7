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
        public abstract class Discipline // Публично-абстрактный класс дисциплины.
        {
            private string _name; // Её название.
            private Participant[] _participants; // Участницы.
            private int _participantCount; // Кол-во участниц.
            public string Name // Свойство имени.
            {
                get { return _name; }
            }
            public Participant[] Participants // Свойство участниц.
            {
                get { return _participants != null ? (Participant[])_participants.Clone() : null; }
            }
            public Discipline(Participant[] participants, string name) // Публичный конструктор дисциплины.
            {
                _name = name;
                _participants = participants;
                _participantCount = 0;
            }
            public void Add(Participant participant) // Метод добавления одного студента.
            {
                if (_participants == null)
                    _participants = new Participant[0];
                Array.Resize(ref _participants, _participantCount + 1);
                _participants[_participantCount] = participant;
                _participantCount++;
            }
            public void Add(Participant[] participants) // Метод добавления студентов.
            {
                for (int i = 0; i < _participants.Length; i++)
                {
                    Add(_participants[i]);
                }
            }
            public void Sort() // Метод сортировки внутри дисциплины.
            {
                if (_participants != null && _participantCount > 0)
                {
                    Participant.Sort(_participants);
                }
            }
            public void Print()
            {
                Console.WriteLine("Дисциплина: " + Name);
                if (_participants != null && _participantCount > 0)
                {
                    for (int i = 0; i < _participantCount; i++)
                    {
                        _participants[i].Print();
                    }
                }
                else
                {
                    Console.WriteLine("Нет участниц.");
                }
            }
            public abstract void Retry(int index); // Публично-абстрактный метод перезачёта.
        }
        public struct Participant // Публичная структура участницы. Здесь находятся:
        {
            private string _name; // Имя.
            private string _surname; // Фамилия.
            private double[] _jumps; // Прыжки.
            private int _index = 0;
            public string Name // Свойство имени.
            {
                get { return _name; }
            }
            public string Surname // Свойство фамилии.
            {
                get { return _surname; }
            }
            public double[] Jumps // Свойство прыжков.
            {
                get
                {
                    return _jumps != null ? (double[])_jumps.Clone() : null;
                }
            }
            public double BestJump // Свойство лучшего из них.
            {
                get
                {
                    if (_jumps == null || _jumps.Length == 0)
                        return 0;
                    return _jumps.Max();
                }
            }
            public Participant(string name, string surname) // Публичный конструктор, принимающий имя и фамилию.
            {
                _name = name;
                _surname = surname;
                _jumps = new double[3];
            }
            public void Jump(double result) // Прыыыыжок.
            {
                if (_jumps == null || _jumps.Length == 0) return;
                if (_index < 3 && result >= 0)
                {
                    _jumps[_index] = result;
                    _index++;
                }
            }
            public static void Sort(Participant[] array) // Сортируем.
            {
                if (array == null || array.Length == 0)
                    return;
                bool swapped;
                do
                {
                    swapped = false;
                    for (int i = 0; i < array.Length - 1; i++)
                    {
                        if (array[i].BestJump < array[i + 1].BestJump)
                        {
                            Participant temp = array[i];
                            array[i] = array[i + 1];
                            array[i + 1] = temp;
                            swapped = true;
                        }
                    }
                } while (swapped);
            }
            public void Print() // Публичный метод для вывода информации о необходимых полях структуры.
            {
                Console.WriteLine("{0,-12} {1,-10} {2,-15:F2}", Name, Surname, BestJump);
            }
        }
        public class LongJump : Discipline // Длинный прыыыыыжок.
        {
            private Participant[] __participants;
            public LongJump(Participant[] participants) : base(participants, "Long jump")
            {
                __participants = participants;
            }
            public override void Retry(int index)
            {
                Participant participant = __participants[index];
                string Name = participant.Name;
                string Surname = participant.Surname;
                double BestJump = participant.BestJump;
                participant = new Participant(Name, Surname);
                participant.Jump(BestJump);
                __participants[index] = participant;
            }
        }
        public class HighJump : Discipline // Высокий прыыыыжок.
        {
            private Participant[] __participants;
            public HighJump(Participant[] participants) : base(participants, "High jump")
            {
                __participants = participants;
            }
            public override void Retry(int index)
            {
                Participant participant = __participants[index];
                string Name = participant.Name;
                string Surname = participant.Surname;
                double[] Jumps = participant.Jumps;
                participant = new Participant(Name, Surname);
                participant.Jump(Jumps[0]);
                participant.Jump(Jumps[1]);
                __participants[index] = participant;
            }
        }
    }
}