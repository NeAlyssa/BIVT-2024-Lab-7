using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Lab_7;
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
            if (responses == null || responses.Length == 0) 
                return 0;
            
            foreach (var response in responses)
            {
                if (response != null && response.Name == _name)
                    _votes++;
            }
            return _votes;
        }

        public virtual void Print()
        {
            WriteLine($"{_name}: {_votes}");
        }
    }

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
            if (responses == null || responses.Length == 0) 
                return 0;
            
            foreach (var response in responses)
            {
                if (response != null && response is HumanResponse && Surname == _surname && response.Name == Name)
                    _votes++;
            }
            return _votes;        
        }

        public override void Print()
        {
            WriteLine($"{Name} {_surname}: {_votes}");
        }
    }
}