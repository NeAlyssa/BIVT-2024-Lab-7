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
            // поля 
            private string _surname;
            private string _club;
            private double _firstJump;
            private double _secondJump;

            // статические поля 
            private static double _normative;
            private static int _jumpers;
            private static int _disjumpers;

            //статические свойства
            public static int Jumpers => _jumpers;
            public static int Disjumpers => _disjumpers;


            //свойства
            public string Surname => _surname;
            public string Club => _club;
            public double FirstJump => _firstJump;
            public double SecondJump => _secondJump;
            public double JumpSum => _firstJump + _secondJump; // свойство суммы прыжков

            // Статический конструктор
            static Participant()
            {
                _normative = 5;
                _jumpers = 0;
                _disjumpers = 0;
            }

            // Конструктор
            public Participant(string surname, string club)
            {
                _surname = surname;
                _club = club;
                _firstJump = 0;
                _secondJump = 0;
                _jumpers++; // Увеличиваем количество участников при создании
            }

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

            // метод дисквалификации участников
            public static void Disqualify(ref Participant[] participants)
            {
                if (participants == null || participants.Length == 0) return;

                int count = 0;

                // подсчет количества участников прошедших норматив
                for (int i = 0; i < participants.Length; i++)
                {
                    if (participants[i].FirstJump >= _normative && participants[i].SecondJump >= _normative)
                    {
                        count++;
                    }
                    else
                    {
                        _disjumpers++; // увеличиваем счетчик дисквалифицированных
                    }
                }

                // создание нового массива для участников, прошедших норматив
                Participant[] newParticipants = new Participant[count];
                int index = 0;

                for (int i = 0; i < participants.Length; i++)
                {
                    if (participants[i].FirstJump >= _normative && participants[i].SecondJump >= _normative)
                    {
                        newParticipants[index++] = participants[i];
                    }
                }

                _jumpers = count; // обновляем количество активных участников
                participants = newParticipants; // заменяем массив участников
            }

            //методы
            // Метод пузырьковой сортировки по убыванию суммы прыжков
            public static void Sort(Participant[] array)
            {
                if (array == null || array.Length == 0) return;
                int n = array.Length;
                for (int i = 0; i < n - 1; i++)
                {
                    for (int j = 0; j < n - i - 1; j++)
                    {
                        if (array[j].JumpSum < array[j + 1].JumpSum)
                        {
                            // Обмен местами
                            Participant temp = array[j];
                            array[j] = array[j + 1];
                            array[j + 1] = temp;
                        }
                    }
                }
            }
            public void Print()
            {
                Console.WriteLine($"Фамилия: {_surname}, Клуб: {_club}, Первый прыжок: {_firstJump}, Второй прыжок: {_secondJump}, Сумма: {JumpSum}");
            }
        }
    }
}
