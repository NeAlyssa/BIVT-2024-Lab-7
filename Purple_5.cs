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
            private string _oobject;
            private string[] _answer;

            public string Animal => _animal;
            public string CharacterTrait => _characterTrait;
            public string Concept => _oobject;
            private string[] Ans => _answer;

            public Response(string animal, string characterTrait, string oobject)
            {
                _animal = animal;
                _characterTrait = characterTrait;
                _oobject = oobject;
                _answer = new string[] { _animal, _characterTrait, _oobject };

            }

            public int CountVotes(Response[] responses, int questionNumber)
            {
                if (responses == null || questionNumber < 1 || questionNumber > 3) return 0;

                int copi_questionNumber = questionNumber - 1;
                int total = 0;
                foreach (var i in responses)
                {
                    if (i.Ans[copi_questionNumber] == Ans[copi_questionNumber] && i.Ans[copi_questionNumber] != null)
                        total++;
                }
                return total;
            }

            public void Print()
            {
                Console.Write(Animal + " " + CharacterTrait + " " + Concept);

            }
        }
        public struct Research
        {
            private string _title;
            private Response[] _responses;

            public string Name => _title;
            public Response[] Responses => _responses;

            public Research(string title)
            {
                _title = title;
                _responses = new Response[0];
            }
            public void Add(string[] answers)
            {
                if (answers == null || _responses == null) return;
                string[] ans = new string[] { null, null, null };
                for (int i = 0; i < Math.Min(3, answers.Length); i++)
                {
                    ans[i] = answers[i];
                }

                var res = new Response[_responses.Length + 1];
                Array.Copy(_responses, res, res.Length - 1);
                res[res.Length - 1] = new Response(ans[0], ans[1], ans[2]);
                _responses = res;
            }

            public string[] GetTopResponses(int question)
            {
                if (_responses == null || question < 1 || question > 3) return null;
                
                int total = 0;
                int n = _responses.Length;
                string[] answer = new string[n];
                for (int i = 0; i < n; i++)
                {
                    int c = 1;
                    var array_1 = new string[] { _responses[i].Animal, _responses[i].CharacterTrait, _responses[i].Concept };
                    for (int j = 0; j < i; j++)
                    {
                        var array_2 = new string[] { _responses[j].Animal, _responses[j].CharacterTrait, _responses[j].Concept };
                        if (array_1[question - 1] == array_2[question - 1])
                        {
                            c++;
                            break;
                        }
                    }
                    if (c == 1 && array_1[question - 1] != null)
                    {
                        int q = 0;
                        for (int j = 0; j < total; j++)
                        {
                            if (answer[j] == array_1[question - 1]) q++;
                        }
                        if (q == 0 && array_1[question - 1] != null && array_1[question - 1] != "-")
                        {
                            answer[total] = array_1[question - 1];
                            total++;
                        }
                    }

                }
                int[] counts = new int[answer.Length];
                for (int i = 0; i < answer.Length; i++)
                {
                    var answer_1 = new string[] { _responses[i].Animal, _responses[i].CharacterTrait, _responses[i].Concept };
                    for (int j = 0; j < answer.Length; j++)
                    {
                        if (answer[j] != null && answer_1[question - 1] == answer[j])
                        {
                            counts[j]++;
                        }
                    }
                }
                Array.Sort(counts, answer);
                Array.Reverse(answer);
                string[] total_answer = new string[Math.Min(answer.Length, 5)];
                Array.Copy(answer, total_answer, 5);
                return total_answer;
            }

            public void Print()
            {
            }

        }
        public class Report 
        {
            private Research[] _respons;
            private static int _i;

            public Research[] Researches => _respons;
            static Report()
            {
                _i = 1;
            }
            public Report()
            {
                _respons = new Research[0];
            }
            
            public Research MakeResearch()
            {
                var M = DateTime.Now.ToString("MM");
                var Y = DateTime.Now.ToString("yy");
                var new_name = new Research($"No_{_i}_{M}/{Y}");
                _i += 1;
                if (_respons == null) 
                { 
                    _respons = new Research[0];
                }
                
                var res = new Research[Researches.Length + 1];
                res[res.Length - 1] = new_name;
                _respons = res;
                return new_name;
            }
            public (string, double)[] GetGeneralReport(int question)
            {
                if (_respons == null || question < 1 || question > 3) return null;
                string[] tot = new string[0];
                foreach (var i in _respons) 
                {
                    foreach(var j in i.Responses)
                    {
                        if(question == 1)
                        {
                            if(j.Animal != null)
                            {
                                Array.Resize(ref tot, tot.Length + 1);
                                tot[tot.Length - 1] = j.Animal;
                            }
                        }
                        else if (question == 2)
                        {
                            if (j.CharacterTrait != null)
                            {
                                Array.Resize(ref tot, tot.Length + 1);
                                tot[tot.Length - 1] = j.CharacterTrait;
                            }
                        }
                        else if (question == 3)
                        {
                            if (j.Concept != null)
                            {
                                Array.Resize(ref tot, tot.Length + 1);
                                tot[tot.Length - 1] = j.Concept;
                            }
                        }
                    }
                }
                var ans = tot.GroupBy(x => x).Select(r => (r.Key, (double)r.Count() / tot.Length * 100.0)).ToArray();
                return ans;
            }
        }
        
    }
}
