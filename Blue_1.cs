using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;

namespace Lab_7{

public class Blue_1
{
    public class Response
    {
        private string _name;
        protected int _votes;
        public string Name => _name;
        public int Votes => _votes;
        public Response (string name)
        {
            _name=name;
            _votes=0;
        }
        public virtual int CountVotes(Response[] responses)
        {
            if (responses.Length == 0 || responses == null) return 0;
            int count = 0;
            foreach(Response r in responses){
                if (r.Name == _name){
                    count++;
                }

            }
            _votes = count;
            return count;
        }
        public virtual void Print()
        {
            Console.WriteLine($"Имя: {Name}, Голосов: {Votes}");
        }
    }
    public class HumanResponse : Response
    {
        private string _surname;
        public string Surname => _surname;
        public HumanResponse (string name, string surname) : base(name){
            _surname=surname;
            _votes = 0;
        }
        public override int CountVotes(Response[] responses)
        {
            if (responses.Length == 0 || responses == null) return 0;
            int count = 0;
            foreach(Response r in responses){
                HumanResponse person = r as HumanResponse;
                if (person == null) continue;
                if (person.Name == this.Name && person.Surname == this.Surname){
                    count++;
                }

            }
            _votes = count;
            return count;
        }
        public override void Print()
        {
            Console.WriteLine($"Имя: {Name}, Фамилия: {Surname}, Голосов: {Votes}");
        }
    }
}
}
