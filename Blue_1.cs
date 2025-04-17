using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    public class Blue_1
    {
        public class Response
        {
            private string _name;
            protected int _votes;

            public string Name => _name;
            public int Votes => _votes;

            public Response(string name)
            {
                _name = name;
                _votes = 0;
            }

            public virtual int CountVotes(Response[] responses)
            {
                if (responses == null || responses.Length == 0) return 0;
                foreach (Response r in responses)
                {
                    if (r.Name == _name)
                    {
                        _votes++;
                    }
                }
                return _votes;
            }

            public virtual void Print()
            {
                Console.WriteLine($"{_name}: {_votes} votes");
            }

        }

        public class HumanResponse : Response
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
                        HumanResponse h = r as HumanResponse;
                        if (h != null)
                        {
                            if (r.Name == this.Name && h.Surname == _surname)
                            {
                                _votes++;
                            }
                        }
                    }
                    
                }
                return _votes;
            }

            public override void Print()
            {
                Console.WriteLine($"{this.Name} {_surname}: {_votes} votes");
            }

        }
    }
}