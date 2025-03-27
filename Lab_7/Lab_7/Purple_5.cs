using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static Lab_7.Purple_4;
using static Lab_7.Purple_5;



namespace Lab_7
{
    public class Purple_5
    {
        public struct Response
        {
            private string _animal;
            private string _charactertrait;
            private string _concept;

            public string Animal => _animal;
            public string CharacterTrait => _charactertrait;
            public string Concept => _concept;
            public Response(string animal, string charactertrait, string concept)
            {
                _animal = animal;
                _charactertrait = charactertrait;
                _concept = concept;
            }
            public int CountVotes(Response[] responses, int number)
            {
                if (responses.Length == 0 || responses == null || number < 1 || number > 3) return 0;
                int count = 0;
                for (int i = 0; i < responses.Length; i++)
                {
                    switch (number)
                    {
                        case 1:
                            {
                                if (responses[i].Animal != "" && responses[i].Animal != "-"&& responses[i].Animal != null && responses[i].Animal == Animal)
                                    count++;
                                break;
                            }
                        case 2:
                            {
                                if (responses[i].CharacterTrait != ""&&  responses[i].CharacterTrait != "-" && responses[i].CharacterTrait != null && responses[i].CharacterTrait == CharacterTrait)
                                    count++;
                                break;
                            }
                        case 3:
                            {
                                if (responses[i].Concept != "" && responses[i].Concept != ""&& responses[i].Concept != null && responses[i].Concept == Concept)
                                    count++;
                                break;
                            }
                    }
                }
                return count;
            }
            public void Print()
            {
                Console.WriteLine($"{Animal} {CharacterTrait} {Concept}");
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
                    var newArray = new Response[_responses.Length];
                    Array.Copy(_responses, newArray, _responses.Length);
                    return newArray;
                }
            }



            public Research(string name)
            {
                _name = name;
                _responses = new Response[0];

            }
            public void Add(string[] answers)
            {
                if (_responses == null || answers == null || answers.Length < 3) return;
                Response a = new Response(answers[0], answers[1], answers[2]);
                Array.Resize(ref _responses, _responses.Length + 1);
                _responses[_responses.Length - 1] = a;
            }
            public string[] GetTopResponses(int q)
            {
                if (_responses == null || q < 1 || q > 3) return null;
                string[] answerq = new string[0];
                int[] coutans = new int[0];
                string[] topresp;
                for (int i = 0; i < _responses.Length; i++)
                {
                    string ans = null;
                    switch (q)
                    {
                        case 1:
                            {
                                if (_responses[i].Animal != "" && _responses[i].Animal != null)
                                    ans = _responses[i].Animal;
                                break;
                            }
                        case 2:
                            {
                                if (_responses[i].CharacterTrait != "" && _responses[i].CharacterTrait != null)
                                    ans = _responses[i].CharacterTrait;
                                break;
                            }
                        case 3:
                            {
                                if (_responses[i].Concept != "" && _responses[i].Concept != null)
                                    ans = _responses[i].Concept;
                                break;
                            }
                    }
                    if (ans != null)
                    {
                        if (answerq.Count(s => s == ans) == 0)
                        {
                            Array.Resize(ref answerq, answerq.Length + 1);
                            answerq[answerq.Length - 1] = ans;
                            Array.Resize(ref coutans, coutans.Length + 1);
                            coutans[coutans.Length - 1] = 1;
                        }
                        else
                        {
                            coutans[Array.IndexOf(answerq, ans)]++;
                        }
                    }

                }
                if (answerq.Length >= 5)
                {
                    topresp = new string[5];

                    int k = 0;
                    for (int i = 0; i < 5; i++)
                    {
                        int index = Array.IndexOf(coutans, coutans.Max());
                        topresp[k] = answerq[index];

                        k++;
                        string[] t1 = new string[answerq.Length - 1];
                        int[] t2 = new int[coutans.Length - 1];
                        for (int j = 0; j < answerq.Length - 1; j++)
                        {
                            if (j >= index)
                            {
                                t1[j] = answerq[j + 1];
                                t2[j] = coutans[j + 1];
                            }
                            else
                            {
                                t1[j] = answerq[j];
                                t2[j] = coutans[j];
                            }
                        }
                        answerq = t1;
                        coutans = t2;
                    }


                }
                else
                {
                    topresp = new string[answerq.Length];
                    for (int i = 0; i < answerq.Length; i++)
                    {
                        topresp[i] = answerq[i];

                    }
                }
                return topresp;
            }

            public void Print()
            {
                if (Responses == null) return;
                foreach (var i in Responses)
                {
                    Console.WriteLine($"{i.Animal} {i.CharacterTrait} {i.Concept}");
                }
            }
        }
        
        public class Report
        {
            private Research[] _researches;
            private static int _index;
            private int _thisindex;
            public Research[] Researches
            {
                get
                {
                    if (_researches == null) return null;
                    var newArray = new Research[_researches.Length];
                    Array.Copy(_researches, newArray, _researches.Length);
                    return newArray;
                }
            }
            static Report()
            {
                _index = 1;
            }
            public Report()
            {
                _researches = new Research[0];
            }
            public Research MakeResearch()
            {
                var time = DateTime.Now;
                Array.Resize(ref _researches, _researches.Length + 1);
                var research = new Research($"No_{_index++}_{time.ToString("MM")}/{time.ToString("YY")}");
                _researches[_researches.Length - 1] = research;
                return research;
            }
            
            public (string, double)[] GetGeneralReport(int question)
            {
                if (_researches == null || question < 1 || question > 3) return (null);
                int countans = 0;
                (string, double)[] report = new(string, double)[0];
                string[] answers = new string[0];
                for (int i = 0; i< _researches.Length; i++)
                {
                    foreach (var r in _researches[i].Responses)
                    {
                        switch (question)
                        {
                            case 1:
                                {
                                    string a = r.Animal;
                                    if (a == null || a == "") break;
                                    countans++;
                                    if (answers.Contains(a))
                                    {
                                        report[Array.IndexOf(answers, a)].Item2++;
                                    }
                                    else
                                    {
                                        Array.Resize(ref report, report.Length + 1);
                                        report[report.Length - 1] = (a, 1);
                                        Array.Resize(ref answers, answers.Length + 1);
                                        answers[answers.Length - 1] = a;
                                    }
                                    break;
                                }
                            case 2:
                                {
                                    string a = r.CharacterTrait;
                                    if (a == null || a == "") break;
                                    countans++;
                                    if (answers.Contains(a))
                                    {
                                        report[Array.IndexOf(answers, a)].Item2++;
                                    }
                                    else
                                    {
                                        Array.Resize(ref report, report.Length + 1);
                                        report[report.Length - 1] = (a, 1);
                                        Array.Resize(ref answers, answers.Length + 1);
                                        answers[answers.Length - 1] = a;
                                    }
                                    break;
                                }
                            case 3:
                                {
                                    string a = r.Concept;
                                    if (a == null || a == "") break;
                                    countans++;
                                    if (answers.Contains(a))
                                    {
                                        report[Array.IndexOf(answers, a)].Item2++;
                                    }
                                    else
                                    {
                                        Array.Resize(ref report, report.Length + 1);
                                        report[report.Length - 1] = (a, 1);
                                        Array.Resize(ref answers, answers.Length + 1);
                                        answers[answers.Length - 1] = a;
                                    }
                                    break;
                                }
                        }
                    }
                
                }
                for (int i =0; i < report.Length;i++)
                {
                    if (countans == 0) return null;
                    report[i].Item2 = (report[i].Item2 / countans) * 100;
                }
                return report;
                
            }
            
        }
        
        
    }
}
    

