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
        public abstract class Participant // Публичный класс участницы. Здесь находятся:
        {
            private string _surname; // Фамилия.
            private string _group; // Группа.
            private string _trainer; // Фамилия тренера.
            private double _result; // Результат.
            private bool _resultFilled; // Заполненность результата.
            private protected double _standard; // Стандарт в виде норматива.
            private static int _passedCount; // Счётчик прошедших норматив.
            private static bool _Printed = false; // Для определения вывода кол-ва людей, прошедших норматив.
            private static bool _PrintedTrainer = false; // Для определения вывода кол-ва людей, прошедших норматив.
            static Participant() // Статичный конструктор для инициализации значений норматива и счётчика.
            {
                _passedCount = 0;
            }
            public static Participant[] GetTrainerParticipants(Participant[] participants, Type participantType, string trainer) // Метод, который возвращает всех участников выбранного тренера в выбранном забеге.
            {
                int count = 0;
                for (int i = 0; i < participants.Length; i++)
                {
                    if (participants[i] != null && participants[i].GetType() == participantType && participants[i].Trainer == trainer)
                    {
                        count++;
                    }
                }
                Participant[] result = new Participant[count];
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
            public Participant(string surname, string group, string trainer) // Публичный конструктор, принимающий фамилию, группу и тренера.
            {

                _standard = 100;
                _surname = surname;
                _group = group;
                _trainer = trainer;
                _result = 0;
                _resultFilled = false;
            }
            public string Surname // Публичное свойство фамилии.
            {
                get { return _surname; }
            }
            public string Group // Публичное свойство группы.
            {
                get { return _group; }
            }
            public string Trainer  // Публичное свойство тренера.
            {
                get { return _trainer; }
            }
            public double Result // Публичное свойство результата.
            {
                get { return _result; }
            }
            public bool HasPassed  // Публичное свойство пройденности.
            {
                get { return _resultFilled && _result <= _standard && _result > 0; }
            }
            public static int PassedTheStandard // Публичное свойство пройденных норматива.
            {
                get { return _passedCount; }
            }
            public void Run(double result) // Метода бега участницы.
            {
                if (result <= 0)
                {
                    return;
                }
                if (!_resultFilled) // Если её результат не записан, то
                {
                    _result = result; // ... записываем её результат.
                    _resultFilled = true; // Результат записан.
                    if (result <= _standard) // Проверка пройденности и корректности норматива.
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
        public class Participant100M : Participant // Норматив на 100 метров.
        {
            public Participant100M(string surname, string group, string trainer) : base(surname, group, trainer)
            {
                _standard = 12; // Переопределяем норматив для забега на 100 метров.
            }
        }
        public class Participant500M : Participant // Норматив на 500 метров.
        {
            public Participant500M(string surname, string group, string trainer) : base(surname, group, trainer)
            {
                _standard = 90; // Переопределяем норматив для забега на 500 метров.
            }
        }
    }
}