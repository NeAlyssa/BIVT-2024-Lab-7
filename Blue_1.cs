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
            public string Name => _name;
            public int Votes => _votes;

            // конструктор 
            public Response(string name)
            {
                _name = name;
               _votes = 0;
            }

            // метод
            public virtual int CountVotes(Response[] responses)
            {
                if (responses == null || responses.Length == 0) return 0;
                foreach (var r in responses)
                {
                    if (r.Name == _name) _votes++;
                }
                return _votes;
            }

            
            public virtual void Print()
            {
                Console.WriteLine(_votes);
            }
        }

        public class HumanResponse: Response
        {
            private string _surname;

            public string Surname => _surname;

            public HumanResponse(string name, string surname) : base(name)
            {
                _surname = surname;
                _votes = 0;
            }

            public override int CountVotes(Response[] responses)
            {
                if (responses == null || responses.Length == 0) return 0;
                foreach (Response r in responses)
                {
                    if (r != null)
                    {
                        HumanResponse human = r as HumanResponse;
                        if (human != null)
                        {
                            if (r.Name == this.Name && human.Surname == _surname)
                                _votes++;
                        }
                    }
                }
                return _votes;
            }


            public override void Print()
            {
                Console.WriteLine($"{this.Name} {_surname}: {_votes}");
            }
        }
    }
}
