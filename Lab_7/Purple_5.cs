using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_6
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

            public Response(string animal, string characterTrait, string concept)
            {
                _animal = animal;
                _characterTrait = characterTrait;
                _concept = concept;
            }

            public int CountVotes(Response[] responses, int questionNumber)
            {
                if (responses == null) return 0;
                int answered = 0;
                for (int i = 0; i < responses.Length; i++)
                {
                    switch (questionNumber) 
                    {
                        case 1:
                            if (responses[i].Animal != null && responses[i].Animal != "") answered++;
                            break;
                        case 2:
                            if (responses[i].CharacterTrait != null && responses[i].CharacterTrait != "") answered++;
                            break;
                        case 3:
                            if (responses[i].Concept != null && responses[i].Concept != "") answered++;
                            break;
                        default:
                            return 0;
                    }
                }
                return answered;
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
                    if (_responses == null) return default(Response[]);

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
                if (answers == null || _responses == null) return;
                Array.Resize(ref _responses, _responses.Length + 1);
                _responses[_responses.Length - 1] = new Response(answers[0], answers[1], answers[2]);
            }

            public string[] GetTopResponses(int question)
            {
                if (_responses == null) return null;
                string[] responses = new string[0];
                int[] count = new int[0];

                foreach (var response in _responses)
                {
                    switch (question)
                    {
                        case 1:
                            if (response.Animal != null && response.Animal != "")
                            {
                                if (!responses.Contains(response.Animal))
                                {
                                    Array.Resize(ref responses, responses.Length + 1);
                                    Array.Resize(ref count, count.Length + 1);
                                    responses[responses.Length - 1] = response.Animal;
                                    count[responses.Length - 1] = 1;
                                }
                                else count[Array.IndexOf(responses, response.Animal)]++;
                            }
                            break;
                        case 2:
                            if (response.CharacterTrait != null && response.CharacterTrait != "")
                            {
                                if (!responses.Contains(response.CharacterTrait))
                                {
                                    Array.Resize(ref responses, responses.Length + 1);
                                    Array.Resize(ref count, count.Length + 1);
                                    responses[responses.Length - 1] = response.CharacterTrait;
                                    count[responses.Length - 1] = 1;
                                }
                                else count[Array.IndexOf(responses, response.CharacterTrait)]++;
                            }
                            break;
                        case 3:
                            if (response.Concept != null && response.Concept != "")
                            {
                                if (!responses.Contains(response.Concept))
                                {
                                    Array.Resize(ref responses, responses.Length + 1);
                                    Array.Resize(ref count, count.Length + 1);
                                    responses[responses.Length - 1] = response.Concept;
                                    count[responses.Length - 1] = 1;
                                }
                                else count[Array.IndexOf(responses, response.Concept)]++;
                            }
                            break;                     
                    }
                }

                DictionariesAreForTheWeak(responses, count);

                string[] topResponses = new string[responses.Length];
                int i = 0;
                foreach(var pair in responses)
                {
                    topResponses[i] = responses[i];
                    i++;
                }
                if (topResponses.Length > 5)
                {
                    Array.Resize(ref topResponses, 5);
                }

                return topResponses;
            }

            private static void DictionariesAreForTheWeak(string[] keys, int[] values)
            {
                for (int i = 1; i < values.Length; i++)
                {
                    string key = keys[i];
                    int value = values[i];
                    int j = i - 1;

                    while (j >= 0 && values[j] < value)
                    {
                        keys[j + 1] = keys[j];
                        values[j + 1] = values[j];
                        j--;
                    }
                    keys[j + 1] = key;
                    values[j + 1] = value;
                }
            }

            public void Print()
            {
                Console.WriteLine(_name);
                foreach (var response in _responses)
                {
                    response.Print();
                }
            }
        }
    }
}
