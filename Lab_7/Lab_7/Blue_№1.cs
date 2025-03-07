using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lab_7
{
    public class Blue_1
    {
        public class Response
        {
            //поля
            private string _name;
            protected int _votes;

            //свойства
            public string Name => _name;
            public int Votes => _votes;

            //конструкторы
            public Response(string name)
            {
                _name = name;
                _votes = 0;
            }

            //методы
            public virtual int CountVotes(Response[] responses)
            {
                if (responses == null || responses.Length == 0) return 0;

                foreach (Response response in responses)
                {
                    if (response.Name == _name)
                        _votes++;
                }
                return _votes;
            }
            public virtual void Print()
            {
                Console.WriteLine($"{_name} has {_votes} votes");
            }
        }//class Response

        public class HumanResponse : Response
        {
            private string _surname;

            private string Surname => _surname;

            public HumanResponse(string name, string surname) : base(name)
            {
                _surname = surname;
                _votes = 0;
            }

            public override int CountVotes(Response[] responses)
            {
                if (responses == null || responses.Length == 0) return 0;

                foreach (Response response in responses)
                {
                    if (response != null)
                    {
                        HumanResponse human = response as HumanResponse;
                        if (human != null)
                        {
                            if (response.Name == this.Name && human.Surname == _surname)
                                _votes++;
                        }
                    }
                }
                return _votes;
            }

            public override void Print()
            {
                Console.WriteLine($"{this.Name} {_surname} has {_votes} votes");
            }
        }
    }
}
