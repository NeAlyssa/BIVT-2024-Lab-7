using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
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
                for (int i = 0; i < responses.Length; i++)
                {
                    if (responses[i].Name == _name) _votes++;
                }
                return _votes;
            }


            public virtual void Print()
            {
                Console.WriteLine($"Кандидат: {Name}, Голосов: {Votes}");
            }
        }

        public class HumanResponse : Response
        {
            public string Surname { get; private set; }

            public HumanResponse(string name, string surname) : base(name)
            {
                Surname = surname;
                _votes = 0;
            }

            public override int CountVotes(Response[] responses)
            {
                if (responses == null || responses.Length == 0) return 0;
                for (int i = 0; i < responses.Length; i++)
                {
                    if(responses[i] is HumanResponse hi && hi != null && hi.Surname == this.Surname && responses[i].Name == this.Name)
                    {
                        _votes++;
                    }
                }
                return _votes;
            }

            public override void Print()
            {
                Console.WriteLine($"Кандидат: {Name} {Surname}, Голосов: {Votes}");
            }

        }
    }
}
