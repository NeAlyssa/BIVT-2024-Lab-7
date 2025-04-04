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

        // Класс, представляющий базовый ответ (Response) в голосовании
        public class Response
        {
            // Поля
            private string _name;  // Имя респондента (или название варианта ответа)
            protected int _votes;  // Количество голосов, полученных этим вариантом

            // Свойства
            public string Name => _name;   // Доступ к имени респондента
            public int Votes => _votes;    // Доступ к количеству голосов

            // Конструктор, принимающий имя респондента и устанавливающий начальное количество голосов в 0
            public Response(string name)
            {
                _name = name;
                _votes = 0;
            }

            // Виртуальный метод подсчета голосов
            public virtual int CountVotes(Response[] responses)
            {
                if (responses == null || responses.Length == 0) return 0; // Проверка на пустой массив
                foreach (var r in responses)
                {
                    if (r.Name == _name) _votes++; // Если имя совпадает, увеличиваем счетчик голосов
                }
                return _votes;
            }

            // Виртуальный метод вывода информации о количестве голосов
            public virtual void Print()
            {
                Console.WriteLine(_votes);
            }
        }

        // Производный класс HumanResponse, представляющий конкретного человека
        public class HumanResponse : Response
        {
            private string _surname;  // Фамилия респондента

            // Свойство для получения фамилии
            public string Surname => _surname;

            // Конструктор, принимающий имя и фамилию
            public HumanResponse(string name, string surname) : base(name)
            {
                _surname = surname;
                _votes = 0; // Количество голосов по умолчанию 0
            }

            // Переопределенный метод подсчета голосов с учетом имени и фамилии
            public override int CountVotes(Response[] responses)
            {
                if (responses == null || responses.Length == 0) return 0; // Проверка на пустой массив
                foreach (Response r in responses)
                {
                    if (r != null)
                    {
                        // Приведение объекта Response к типу HumanResponse
                        HumanResponse human = r as HumanResponse;
                        if (human != null)
                        {
                            // Сравниваем не только имя, но и фамилию
                            if (r.Name == this.Name && human.Surname == _surname)
                                _votes++;
                        }
                    }
                }
                return _votes;
            }

            // Переопределенный метод вывода информации об участнике голосования
            public override void Print()
            {
                Console.WriteLine($"{this.Name} {_surname}: {_votes}");
            }
        }
    }
}
