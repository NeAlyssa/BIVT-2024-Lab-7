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
                get { return _participants; }
            }
            public Discipline(string name) // Публичный конструктор дисциплины.
            {
                _name = name;
                _participants = new Participant[0];
                _participantCount = 0;
            }
            public void Add(Participant participant) // Метод добавления одного студента.
            {
                if (_participants == null)
                {
                    _participants = new Participant[0];
                }
                Array.Resize(ref _participants, _participantCount + 1);
                _participants[_participantCount] = participant;
                _participantCount++;
            }
            public void Add(Participant[] participants) // Метод добавления студентов.
            {
                int count = participants.Length;
                for (int i = 0; i < count; i++)
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
            private protected Participant GetParticipantAt(int index) // Защищённый метод для получения участницы из массива.
            {
                if (_participants != null && index >= 0 && index < _participantCount)
                {
                    return _participants[index];
                }
                return default(Participant);
            }
            private protected void SetParticipant(int index, Participant participant) // Защищённый метод для обновления участницы в массиве.
            {
                if (_participants != null && index >= 0 && index < _participantCount)
                {
                    _participants[index] = participant;
                }
            }
            public abstract void Retry(int index); // Публично-абстрактный метод перезачёта.
        }
        public struct Participant // Публичная структура участницы. Здесь находятся:
        {
            private string _name; // Имя.
            private string _surname; // Фамилия.
            private double[] _jumps; // Прыжки.
            private int _index;
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
                get { return _jumps; }
            }
            public double BestJump // Свойство лучшего из них.
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
            public Participant(string name, string surname) // Публичный конструктор, принимающий имя и фамилию.
            {
                _name = name;
                _surname = surname;
                _jumps = new double[3];
                _index = 0;
            }
            public void Jump(double result) // Прыыыыжок.
            {
                if (_jumps == null || _jumps.Length == 0)
                {
                    return;
                }
                if (_index < 3 && result >= 0)
                {
                    _jumps[_index] = result;
                    _index++;
                }
            }
            public static void Sort(Participant[] array) // Сортируем.
            {
                if (array == null || array.Length == 0)
                {
                    return;
                }
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
            public LongJump() : base("Long jump")
            {
            }
            public override void Retry(int index)
            {
                Participant participant = GetParticipantAt(index);
                double BestJump = participant.BestJump;
                participant = new Participant(participant.Name, participant.Surname);
                participant.Jump(BestJump);
                SetParticipant(index, participant);
            }
        }
        public class HighJump : Discipline // Высокий прыыыыжок.
        {
            public HighJump() : base("High jump")
            {
            }
            public override void Retry(int index)
            {
                Participant participant = GetParticipantAt(index);
                double[] jumps = participant.Jumps;
                int count = jumps.Length;
                Participant newParticipant = new Participant(participant.Name, participant.Surname);
                for (int i = 0; i < count - 1; i++)
                {
                    newParticipant.Jump(jumps[i]);
                }
                SetParticipant(index, newParticipant);
            }
        }
    }
}
