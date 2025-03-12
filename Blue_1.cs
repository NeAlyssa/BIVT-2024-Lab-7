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
            //поля хранение данных
            private string _name;
            protected int _votes;


            //свойства только для чтения каждого из приватных полей
            public string Name => _name;
            public int Votes => _votes;

            //конструктор
            public Response(string name)
            {
                _name = name;
                _votes = 0;
            }
            //метод счетчик голосов, соответствующих текущему кандидату из списка
            public virtual int CountVotes(Response[] responses)
            {
                if (responses == null || responses.Length == 0) return 0;
                _votes = 0;
                foreach (var x in responses)
                {
                    if (x.Name == _name)
                    {
                        _votes++;
                    }
                }
                return _votes;
            }

            public virtual void Print()
            {
                Console.WriteLine(_name + " " + _votes);
            }



        }
        // статический метод сортировки массива ответов Sort(Response[] responses) по значению votes
        //public static void Sort(Response[] responses)
        //{
        //    if (responses==null || responses.Length == 0) return;
        //    for (int i = 0; i < responses.Length; i++)
        //    {
        //        for (int j=0; j < responses.Length - i - 1; j++)
        //        {
        //            if (responses[j].Votes < responses[j+1].Votes)
        //            {
        //                var temp = responses[j];
        //                responses[j] = responses[j+1];
        //                responses[j+1] = temp;
        //            }
        //        }
        //    }
        //}
        //вывод
        public class HumanResponse : Response
        {
            private string _surname;

            public string Surname => _surname;

            public HumanResponse(string name, string surname) : base(name)
            {
                _surname = surname;
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
                Console.WriteLine(Name + " " + Surname + " " + _votes);
            }

        }
    }
}
