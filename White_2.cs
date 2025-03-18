using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lab_7
{
    public class White_2
    {
        public class Participant
        {
            // поля 
            private string _surname;
            private string _name;
            private double _firstJump;
            private double _secondJump;

            //статическое поле для норматива
            private static readonly double _normative;

            // cтатический конструктор
            static Participant()
            {
                _normative = 3;
            }

            //свойства
            public string Surname => _surname;
            public string Name => _name;
            public double FirstJump => _firstJump;
            public double SecondJump => _secondJump;
            public double BestJump => Math.Max(_firstJump, _secondJump); // Лучший прыжок
            public bool IsPassed => BestJump >= _normative; //проверка на проходжение норматива

            // конструкторы
            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _firstJump = 0;
                _secondJump = 0;

            }
            // уонструктор с параметрами для прыжков
            public Participant(string name, string surname, double firstJump, double secondJump)
            {
                _name = name;
                _surname = surname;
                _firstJump = firstJump;
                _secondJump = secondJump;
            }

            //методы
            public void Jump(double result)
            {
                if (_firstJump == 0)
                {
                    _firstJump = result;
                }
                else if (_secondJump == 0)
                {
                    _secondJump = result;
                }
            }

            // Метод для получения участников, прошедших норматив
            public static Participant[] GetPassed(Participant[] participants)
            {
                if (participants == null || participants.Length == 0) return new Participant[0];
                //поиск количества участников прошедших норматив
                int count = 0;
                for (int i = 0; i < participants.Length; i++)
                {
                    if (participants[i].IsPassed)//проверка на прохлждение норматива 
                    {
                        count++;
                    }
                }

                Participant[] passedParticipants = new Participant[count];
                int index = 0;
                for (int i = 0; i < participants.Length; i++)
                {
                    if (participants[i].IsPassed)
                    {
                        passedParticipants[index++] = participants[i];
                    }
                }

                return passedParticipants;
            }

            public static void Sort(Participant[] array)
            {
                if (array == null || array.Length == 0) return;
                int n = array.Length;
                bool sw;

                for (int i = 0; i < n - 1; i++)
                {
                    sw = false;
                    for (int j = 0; j < n - 1 - i; j++)
                    {
                        if (array[j].BestJump < array[j + 1].BestJump) // сортировка по убыванию
                        {
                            (array[j], array[j + 1]) = (array[j + 1], array[j]);
                            sw = true;
                        }
                    }
                    if (!sw) break; // перестановок не было, массив уже отсортирован
                }
            }

            public void Print()
            {
                Console.WriteLine($"Имя: {_name}, Фамилия: {_surname}, Первый прыжок: {_firstJump}, Второй прыжок: {_secondJump}, Лучший прыжок: {BestJump}");
            }
        }

    }
}

