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
            //приватные поля
            private string _name;
            //private string _surname;
            protected int _vote;   

            //свойства для чтения приватных полей
            public string Name => _name;
            //public string Surname => _surname;
            public int Votes => _vote;   

            //конструктор тоже метод
            public Response(string name) 
            {
                _name = name;
                _vote = 0;
            }
            //метод
            public virtual int CountVotes(Response[] responses) 
            {
                if (responses == null) return 0;
                int result = 0;
                foreach (var response in responses)
                {
                    if (response.Name == _name)
                    {
                        result++;
                    }
                }

                _vote = result;
                return result;
            }
            public virtual void Print()
            {
                Console.WriteLine($"{Name}\t{Votes}");
            }

        }
        //создаем наследника 
        public class HumanResponse : Response
        {
            private string _surname;
            public HumanResponse(string name, string surname) : base(name)
            {
                _surname = surname;
            }
            public string Surname => _surname;
            //переопределяем метод
            public override int CountVotes(Response[] responses) 
            {
                if (responses == null) return 0;
                int result = 0;
                foreach (var response in responses)
                {
                    HumanResponse human = response as HumanResponse;
                    if (human == null) continue;
                    if (response.Name == Name && human.Surname == _surname)
                    {
                        result++;
                    }
                }

                _vote = result;
                return result;
            }
            public override void Print()
            {
                Console.WriteLine($"{Name}\t{Surname}\t{Votes}");
                //base.Print();
            }
        }

    }
}
