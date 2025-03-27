using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Lab_7.Blue_1;

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
            public string Name { get { return _name; } }
            public int Votes { get { return _votes; } }
            //конструктор 
            public Response(string name)
            {
                _name = name;
                _votes = 0;
            }
            //методы
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
                Console.WriteLine($"{_name} {_votes}");
            }
        }
        public class HumanResponse : Response
        {
            //поля
            private string _surname;
            //свойства 
            public string Surname { get { return _surname; } }
            public HumanResponse(string name, string surname) : base(name)
            {
                _surname = surname;
                _votes = 0;
            }
            //переопределение метода - ключевое слово override
            public override int CountVotes(Response[] responses)
            {
                if (responses == null || responses.Length == 0) return 0;
                for (int i = 0; i < responses.Length; i++)
                {
                    HumanResponse human = responses[i] as HumanResponse;
                    if (human == null) continue;
                    if (human.Name == this.Name && human.Surname == _surname) _votes++;
                }
                return _votes;
            }
            public override void Print()
            {
                base.Print();
                Console.WriteLine(_surname);
            }
        }
    }
}
