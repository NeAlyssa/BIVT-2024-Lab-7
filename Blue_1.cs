using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    public class Blue_1
    {
        public class Response
        {
            //поля
            private string _name;
            private string _surname;
            protected int _votes;

            //свойства
            public string Name => _name;
            public int Votes => _votes;

            //конструктор
            public Response(string name)
            {
                _name = name;
                _votes = 0;
            }
            //методы
            public virtual int CountVotes(Response[] responses)
            {
                if (responses == null) return 0;
                int count = 0;
                foreach (var response in responses)
                {
                    if (response.Name == _name) count++;
                }
                _votes = count;
                return count;
            }

            public virtual void Print()
            {
                Console.WriteLine($"{Name}\t{Votes}");
            }
        }
        public class HumanResponse : Response
        {
            private string _surname;
            public string Surname => _surname;
            public HumanResponse(string name, string surname) : base (name)
            {
                _surname = surname;
            }
            public override int CountVotes(Response[] responses)
            {
                if (responses == null) return 0;
                int count = 0;
                foreach (var response in responses)
                {
                    if(response is HumanResponse humanResponse)
                    {
                        if (humanResponse.Name == Name && humanResponse.Surname == Surname)
                        {
                            count++;
                        }
                    }
                    else if(response.Name == _surname) count++;
                    
                }
                _votes = count;
                return _votes;
            }
            public override void Print()
            {
                Console.WriteLine($"{Name}\t{Surname}\t{Votes}");
            }
        }
        
    }
    
}
