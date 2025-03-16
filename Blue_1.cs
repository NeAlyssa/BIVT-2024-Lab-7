using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lab_7
{
    public class Blue_1
    {
        public class Response // структура
        {
            private string _name; // поле с именем
            protected int _votes; // голоса

            // свойства
            public string Name { get { return _name; } }
            public int Votes => _votes;

            // конструктор
            public Response(string name)
            {
                _name = name;
                _votes = 0; 
            }

            // методы 
            public virtual int CountVotes(Response[] responses) // сколько голосов из общего списка ответов соответствуют текущему кандидату  охранять это значение в поле количества голосов текущего кандидата
            {
                if (responses == null || responses.Length == 0) return 0;
                _votes = 0;
                foreach (var i in responses)
                {
                    // имя и фамилия совпадают с текущим кандидатом увеличиваем
                    if (i.Name == _name)
                    {
                        _votes++;
                    }
                }
                return _votes;  //общее количество голосов
            }

            public virtual void Print()
            {
                Console.WriteLine($"{_name}: {_votes} голосов");
            }
        }
        // Наследующий класс HumanResponse
        public class HumanResponse : Response
        {
            private string _surname;

            public string Surname => _surname;

            // Конструктор класса HumanResponse
            public HumanResponse(string name, string surname) : base(name)
            {
                _surname = surname;
                _votes = 0;
            }

            // Переопределение метода CountVotes
            public override int CountVotes(Response[] responses)
            {
                // Используем преобразование типов для доступа к полной информации
                int count = 0;
                foreach (var response in responses)
                {
                    if (response is HumanResponse humanResponse &&
                        humanResponse.Name == Name &&
                        humanResponse._surname == _surname)
                    {
                        count++;
                    }
                }
                _votes = count;
                return _votes;
            }

            // Переопределение метода Print() для вывода информации о всех полях
            public override void Print()
            {
                Console.WriteLine($"Имя: {Name}, Фамилия: {_surname}, Голосов: {Votes}");
            }
        }
    }
}
