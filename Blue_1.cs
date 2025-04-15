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
        public class Response
        {
            // поля
            private string _name;

            protected int _votes;

            // свойства
            public string Name { get { return _name; } }
            
            public int Votes { get { return _votes; } }

            // конструктор
            public Response(string name)
            {
                _name = name;
                _votes = 0;
            }

            // методы
            public virtual int CountVotes(Response[] responses)
            {
                if (responses == null || responses.Length == 0) return 0;

                foreach (Response resp in responses)
                {
                    if (resp.Name == _name)
                        _votes++;
                }
                return _votes;
            }

            /*публичный метод Print() для вывода информации о необходимых полях структуры */
            public virtual void Print()
            {
                Console.WriteLine($"{_name}: {_votes} votes.");
            }
        }

        public class HumanResponse : Response
        {
            private string _surname;

            public string Surname { get { return _surname; } }

            public HumanResponse(string name, string surname) : base(name)
            {
                _surname = surname;
            }

            public override int CountVotes(Response[] responses)
            {
                if (responses == null || responses.Length == 0) return 0;

                foreach (Response resp in responses)
                {
                    if (resp != null)
                    {
                        HumanResponse humresp = resp as HumanResponse;
                        if (humresp != null)
                            if (resp.Name == this.Name && humresp.Surname == _surname)
                                _votes++;
                    }
                }

                return _votes;
            }

            public override void Print()
            {
                Console.WriteLine($"{this.Name} {_surname}: {_votes} votes.");
            }
        }
    }
}
