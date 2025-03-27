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
            
            //pola
            private string _name;
            //private string _surname;
            protected int _votes;
            //protected bool _count;

            //svoystva
            public string Name => _name;
            //public string Surname => _surname;
            public int Votes => _votes;

            //konstructor
            public Response(string name) //string surname)
            {
                _name = name;
                //_surname = surname;
                _votes = 0;
                //_count = false;
            }

            //metod
            public virtual int CountVotes(Response[] responses)
            {
                //_votes = 0;
                if (responses == null || responses.Length == 0) return 0;
                foreach (Response response in responses)
                {
                    if (response.Name == _name)
                    {
                        _votes++;
                       // _count = true;
                        
                    }
                    // && response.Surname == _surname)
                                               // _votes++;

                }

                return _votes;
            }
            public virtual void Print()
            {
                Console.WriteLine($"Имя: {Name}");
                //Console.WriteLine($"Фамилия: {Surname}");
                Console.WriteLine($"Количество голосов: {Votes}");
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
                foreach (Response response in responses)
                {
                    HumanResponse hum = response as HumanResponse;
                    if (hum==null) continue;
                    
                    if (response.Name == this.Name && hum.Surname == _surname) _votes++;

                }
                return _votes;
            }

            public override void Print()
            {
                //base.Print();
                Console.WriteLine($"Имя: {Name}");
                Console.WriteLine($"Фамилия: {_surname}");
                Console.WriteLine($"Количество голосов: {Votes}");
            }
        }
    }
}
