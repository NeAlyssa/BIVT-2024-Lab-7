using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;
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
            private string[] _answers;

            public string Animal => _animal;
            public string CharacterTrait => _characterTrait;
            public string Concept => _concept;
            private string[] Answers => _answers;
            public Response(string animal, string characterTrait, string concept)
            {
                _animal = animal;
                _characterTrait = characterTrait;
                _concept = concept;
                _answers = new string[] { _animal, _characterTrait, _concept };
            }

            public int CountVotes(Response[] responses, int questionNumber)
            {
                if (responses == null || questionNumber < 1 || questionNumber > 3) return 0;
                int count = 0;
                foreach( var response in responses ) 
                {
                    switch (questionNumber)
                    {
                        case 1:
                            if (response._answers[0] == _answers[0] && response._answers[0]!=null)
                                count++;
                            break;
                        case 2:
                            if (response._answers[1] == _answers[1] && response._answers[1] != null)
                                count++;
                            break;
                        case 3:
                            if (response._answers[2] == _answers[2] && response._answers[2] != null)
                                count++;
                            break;
                    }
                }
                return count;
            }

            public void Print()
            {
                Console.Write($"animal {_animal} ");
                Console.WriteLine();
                Console.Write($"character {_characterTrait} ");
                Console.WriteLine();
                Console.Write($"concept {_concept} ");
            }

        }
        public class Report
        {
            private static int _index;
            private Research[] _reserches;
            public Research[] Researches => _reserches;
            static Report()
            {
                _index = 1;
            }
            public Report()
            {
                _reserches = new Research[0];

            }
            public Research MakeResearch()
            {
                var month = DateTime.Now.ToString("MM");
                var years = DateTime.Now.ToString("yy");
                var ans = new Research($"No_{_index++}_{month}/{years}");
                if (_reserches == null) _reserches = new Research[0];
                var researches= new Research[Researches.Length+1];
                researches[researches.Length-1] = ans;
                _reserches = researches;
                return ans;
            }
            public (string, double)[] GetGeneralReport(int question)
            {
                if (_reserches == null || question < 1 || question > 3) return null;
                string[] answer = new string[0];
                foreach (var n1 in _reserches)
                {
                    foreach (var n2 in n1.Responses)
                    {
                        if (question == 1)
                        {
                            if (n2.Animal != null) {
                                Array.Resize(ref answer, answer.Length + 1);
                                answer[answer.Length - 1] = n2.Animal;
                            }
                        }
                        if (question == 2)
                        {
                            if (n2.CharacterTrait != null) {
                                {
                                    Array.Resize(ref answer, answer.Length + 1);
                                    answer[answer.Length - 1] = n2.CharacterTrait;
                                }
                            }
                        }
                        if (question == 3)
                        {
                            if (n2.Concept != null) {
                                {
                                    Array.Resize(ref answer, answer.Length + 1);
                                    answer[answer.Length - 1] = n2.Concept;
                                }
                            }
                        }
                    }
                }
                return answer.GroupBy(x => x).Select(y => (y.Key, 100.0 * y.Count() / answer.Length)).ToArray();

            }
        }
        public struct Research
        {
            private string _name;
            private Response[] _responses;

            public string Name => _name;
            public Response[] Responses => _responses;


            public Research(string name)
            {
                _name = name;
                _responses = new Response[0];
            }

            public void Add(string[] answer)
            {
                if (answer == null|| answer.Length!=3|| _responses == null) return;

                string[] a = new string[] { null, null, null };
                int n = Math.Min(3, answer.Length);
                for (int i = 0; i < n; i++)
                {
                    {
                        a[i] = answer[i];
                    }
                }
                var r = new Response[_responses.Length + 1];
                Array.Copy(_responses, r, r.Length - 1);
                r[r.Length - 1] = new Response(a[0], a[1], a[2]);
                _responses = r;
            }

            public string[] GetTopResponses(int question)
            {
                if (_responses == null || question < 1 || question > 3) return null;

                int count = 0;
                int n = _responses.Length;
                string[] ans = new string[_responses.Length];
                for (int i = 0; i < n; i++)
                {

                    int c = 1;
                    var array1 = new string[] { _responses[i].Animal, _responses[i].CharacterTrait, _responses[i].Concept };
                    for (int j = 0; j < i; j++)
                    {
                        var array2 = new string[] { _responses[j].Animal, _responses[j].CharacterTrait, _responses[j].Concept };
                        if (array1[question - 1] == array2[question - 1])
                        {
                            c++;
                            break;
                        }
                    }
                    if (c == 1 && array1[question - 1] != null)
                    {
                        int k = 0;
                        for (int j = 0; j < count; j++)
                        {
                            if (ans[j] == array1[question - 1]) k++;
                        }
                        if (k == 0 && array1[question - 1] != null && array1[question - 1] != "-")
                        {
                            ans[count] = array1[question - 1];
                            count++;
                        }

                    }
                }
                int[] counts = new int[ans.Length];
                for (int i = 0; i < ans.Length; i++)
                {
                    var array1 = new string[] { _responses[i].Animal, _responses[i].CharacterTrait, _responses[i].Concept };
                    for (int j = 0; j < ans.Length; j++)
                    {

                        if (ans[j] != null && array1[question - 1] == ans[j])
                        {
                            counts[j]++;
                        }
                    }
                }
                Array.Sort(counts, ans);
                Array.Reverse(ans);
                string[] answer = new string[Math.Min(ans.Length, 5)];
                Array.Copy(ans, answer, Math.Min(ans.Length, 5));
                return answer;
            }

            public void Print()
            {
                System.Console.WriteLine(_name);
                if (_responses == null) return;
                foreach (var r in _responses) { r.Print(); }

            }
        }


    }
}