using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    public class Blue_1
    {
        // класс (вместо структуры)
        public class Response
        {
            // приватные поля
            private string _name;
            protected int _votes;

            // свойства
            public string Name => _name;
            public int Votes => _votes;

            // конструктор 
            public Response(string Name)
            {
                _name = Name;
                _votes = 0;
            }

            // методы
            public virtual int CountVotes(Response[] responses)
            {
                if (responses == null || responses.Length == 0) return 0;

                foreach (var response in responses)
                {
                    if (response.Name == _name) { _votes++; }
                }
                return _votes;
            }
            public virtual void Print()
            {
                Console.WriteLine($"{_name}, {_votes}");
            }
        }

        // наследник класса
        public class HumanResponse : Response
        {
            // приватные поля
            private string _surname;

            // свойства
            public string Surname => _surname;

            // конструктор дочернего класса 
            public HumanResponse(string Name, string Surname) : base(Name)
            {
                _surname = Surname;
                _votes = 0;
            }

            // переопределенный методы
            public override int CountVotes(Response[] responses)
            {
                if (responses == null || responses.Length == 0) return 0;
                foreach (var res in responses)
                {
                    if ((res is HumanResponse hmn) && (hmn != null) && (hmn.Surname == _surname) && (hmn.Name == this.Name))
                    { _votes++; }
                }
                return _votes;
            }

            public override void Print()
            {
                Console.WriteLine($"{Name}, {_surname}, {_votes}");
            }

        }
    }
}
