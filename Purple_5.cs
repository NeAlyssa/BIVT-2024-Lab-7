using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    public class Purple_5
    {
        public struct Response
        {
            private string _animal;
            private string _characterTrait;
            private string _concept;

            public string Animal => _animal;
            public string CharacterTrait => _characterTrait;
            public string Concept => _concept;
            private string [] a
            {
                get{
                    return new string[] {_animal,_characterTrait,_concept};
                }
            }

            public Response(string animal, string characterTrait, string concept)
            {
                _animal = animal;
                _characterTrait = characterTrait;
                _concept = concept;
            }

            public int CountVotes(Response[] responses, int questionNumber)
            {
                if (responses == null || responses.Length == 0 || questionNumber<1 || questionNumber > 3) return 0;
                int res = 0;
                if (questionNumber == 1)
                {
                    var c = this.Animal;
                    if (c != null)
                    {
                        foreach (var x in responses)
                        {
                            if (x.Animal == c)
                            {
                                res++;
                            }
                        }
                    }
                    
                }
                else if (questionNumber == 2)
                {
                    var c = this.CharacterTrait;
                    if (c != null)
                    {
                        foreach (var x in responses)
                        {
                            if (x.CharacterTrait == c)
                            {
                                res++;
                            }
                        }
                    }
                }
                else if (questionNumber == 3)
                {
                    var c = this.Concept;
                    if (c != null)
                    {
                        foreach (var x in responses)
                        {
                            if (x.Concept == c)
                            {
                                res++;
                            }
                        }
                    }
                }
                return res;
            }
            public void Print()
            {
                Console.WriteLine(_animal + " " + _characterTrait + " " + _concept);
            }
        }

        public struct Research
        {
            private string _name;
            private Response[] _responses;
            
            public string Name => _name;
            public Response[] Responses
            {
                get
                {
                    if (_responses == null) return null;

                    var copy = new Response[_responses.Length];
                    Array.Copy(_responses, copy, _responses.Length);
                    return copy;
                }
            }

            public Research(string name)
            {
                _name = name;
                _responses = new Response[0];
            }
            

            public void Add(string[] answers)
            {
                if (answers == null || _responses == null) return;
                Response res = new Response(answers[0], answers[1],answers[2]);
                Array.Resize(ref _responses, _responses.Length +1);
                _responses[_responses.Length -1] = res;
            }

            public string[] GetTopResponses( int question)
            {
                if(_responses == null) return null;

                string[] array = new string[0];
                string[] answers = new string[1];
                int[] kAnswers = new int[1];

                switch(question)
                {
                    case 1:
                        {
                            for(int i = 0; i < _responses.Length; i++)
                            {
                                if(_responses[i].Animal != null)
                                {
                                    Array.Resize(ref array, array.Length + 1);
                                    array[array.Length - 1] = _responses[i].Animal;
                                }

                            }
                            break;
                        }
                    case 2:
                        {
                            for (int i = 0; i < _responses.Length; i++)
                            {
                                if (_responses[i].CharacterTrait != null)
                                {
                                    Array.Resize(ref array, array.Length + 1);
                                    array[array.Length - 1] = _responses[i].CharacterTrait;
                                }
                            }
                            break;
                        }
                    case 3:
                        {
                            for (int i = 0; i < _responses.Length; i++)
                            {
                                if (_responses[i].Concept != null)
                                {
                                    Array.Resize(ref array, array.Length + 1);
                                    array[array.Length - 1] = _responses[i].Concept;
                                }
                            }
                            break;
                        }
                    default: return null;
                }

                answers[0] = array[0];
                kAnswers[0] = 1;

                for (int i = 1; i < array.Length; i++)
                {
                    bool f = false;
                    int k = -1;

                    for (int j = 0; j < answers.Length; j++)
                    {
                        if (array[i] == answers[j])
                        {
                            f = true;
                            k = j;
                            break;
                        }
                    }

                    if (f)
                    {
                        kAnswers[k]++;
                    }
                    else
                    {
                        Array.Resize(ref answers, answers.Length + 1);
                        Array.Resize(ref kAnswers, kAnswers.Length + 1);
                        answers[answers.Length - 1] = array[i];
                        kAnswers[kAnswers.Length - 1] = 1;
                    }
                }

                for(int i = 1, j = 2; i < answers.Length; )
                {
                    if(i == 0 || kAnswers[i - 1] >= kAnswers[i])
                    {
                        i = j;
                        j++;
                    }
                    else
                    {
                        (kAnswers[i - 1], kAnswers[i]) = (kAnswers[i], kAnswers[i - 1]);
                        (answers[i - 1], answers[i]) = (answers[i], answers[i - 1]);
                        i--;
                    }
                }

                Array.Resize(ref answers, 5);

                return answers;
            }

            public void Print()
            {
                foreach(var a in _responses)
                {
                    a.Print();
                }
            }
        }
        public class Report
        {
            private Research[] _researches;
            private static int k;
            public Research[] Researches => _researches;
            static Report()
            {
                k = 1;
            }
            public Report()
            {
                _researches = new Research[0];
            }
            public Research MakeResearch()
            {
                if (_researches == null) return default;
                string a = $"No_{k++}_{DateTime.Now.ToString("MM")}/{DateTime.Now.ToString("yy")}";
                Research b = new Research(a);
                var copy = new Research[_researches.Length+1];
                Array.Copy(_researches,copy,_researches.Length);
                copy[_researches.Length] = b;
                _researches = copy;
                return b;
            }
            public (string,double)[] GetGeneralReport(int question)
            {
                if (_researches == null || _researches.Length == 0 || question<1 || question > 3) return null;
                Research Rsum = new Research("");
                foreach (var a in _researches)
                {
                    if (a.Responses != null)
                    {
                        foreach (var res in a.Responses)
                        {
                            Rsum.Add(new string[] {res.Animal, res.CharacterTrait, res.Concept});
                        }
                    }
                }
                var count =0;
                foreach (var a in Rsum.Responses)
                {
                    string[] ans = new string[] {a.Animal, a.CharacterTrait, a.Concept};
                    if (ans[question - 1] != null) count++;
                }
                if (count == 0 ) return new (string,double)[0];
                var cort = Rsum.Responses.GroupBy (resp =>
                {
                    switch (question)
                    {
                        case 1:
                            return resp.Animal;
                        case 2:
                            return resp.CharacterTrait;
                        default:
                            return resp.Concept;
                    }
                }).Select(x => (x.Key.ToString(), x.Count()*100.0 / count)).ToArray();
                return cort;
                    
                
            }
        }

    }
}
