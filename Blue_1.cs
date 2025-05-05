using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Lab_7
{
    public class Blue_1
    {
        public class Response
        {
            private string _name;
            protected int _votes;
            public string Name { get { return _name; } }
            public int Votes => _votes;

            public Response(string name)
            {
                _name = name;
                _votes = 0;
            }
            public virtual int CountVotes(Response[] responses)
            {
                if (responses == null || responses.Length == 0)
                    return 0;

                foreach (var response in responses)
                {
                    if (response.Name == _name)
                        _votes++;
                }
                return _votes;
            }
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
       
