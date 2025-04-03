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
        //класс
        public class Response
        {
            //приватные поля
            private string _name;
            protected int _votes;

            //свойства для чтения
            public string Name => _name;
            public int Votes => _votes;

            //конструктор 
            public Response(string Name)
            {
                _name = Name;
                _votes = 0;
            }

            //метод
           public virtual int CountVotes(Response[] responses)
            {
                if (responses == null) return 0;

                foreach (var i in responses)
                {
                    if (i.Name == _name)
                        _votes++;
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
            private string _surname;
            public string Surname => _surname;

            public HumanResponse(string Name, string Surname) : base(Name)
            {
                _surname = Surname;
                _votes = 0;
            }

            public override int CountVotes(Response[] responses)
            {
                if (responses == null || responses.Length == 0) return 0;

                foreach (var response in responses)
                {
                    if (response is HumanResponse human &&
                        human != null && 
                        human.Surname == _surname &&
                        human.Name == this.Name)
                    {
                        _votes++;
                    }
                }
                return _votes;
            }
            public override void Print()
            {
                Console.WriteLine($"{Name} {_surname} {_votes}");
            }

        }
    }
}