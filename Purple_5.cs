using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
            private string[] _response;

            public string Animal { get { return _animal; } }
            public string CharacterTrait { get { return _characterTrait; } }
            public string Concept { get { return _concept; } }

            public Response(string animal, string characterTrait, string concept)
            {
                _animal = animal;
                _characterTrait = characterTrait;
                _concept = concept;
                _response = new string[] {animal, characterTrait, concept};
            }

            public int CountVotes(Response[] responses, int questionNumber)
            {
                if (responses == null || questionNumber < 1 || questionNumber > 3 
                    || _response == null || _response.Length < questionNumber || _response[questionNumber - 1] == null) { return 0; }
                int count = 0;
                foreach (Response response in responses)
                {
                   if(response._response == null) { continue;  }
                   var resp = response._response;
                   if(resp.Length >= questionNumber && resp[questionNumber - 1] != null && resp[questionNumber - 1] == _response[questionNumber - 1]) { count++; }
                }
                return count;
            }
            public void Print() 
            {
                Console.WriteLine(_animal + " " + _characterTrait +" " + _concept);
            }
        }

        public struct Research
        {
            private string _name;
            private Response[] _responses;

            public string Name { get { return _name; } }
            public Response[] Responses { get { return _responses; } }

            public Research(string name)
            {
                _name = name;
                _responses = new Response[0];
            }

            public void Add(string[] answers)
            {
                if (answers == null || _responses == null) { return; }
                Response[] newArray = new Response[_responses.Length + 1];
                Array.Copy(_responses, newArray, _responses.Length);
                string[] resp = new string[] {null, null, null};
                for(int i = 0; i < Math.Min(answers.Length, 3); ++i)
                {
                    resp[i] = answers[i]; 
                }
                Response answer = new Response(resp[0], resp[1], resp[2]);
                newArray[newArray.Length - 1] = answer;
                _responses = newArray;
            }
            public string[] GetTopResponses(int question)
            {
                if (_responses == null || question < 1 || question > 3) { return null; }
                int[] counts = new int[_responses.Length];
                int currEmptyInd = 0;
                string[] answers = new string[_responses.Length];
                for (int i = 0; i < _responses.Length; ++i) {
                    string[] resp = { _responses[i].Animal, _responses[i].CharacterTrait, _responses[i].Concept };
                    if (resp[question - 1] == null || answers.Contains(resp[question - 1])) continue;
                    answers[currEmptyInd] = resp[question - 1];
                    counts[currEmptyInd++] = _responses[i].CountVotes(_responses, question); 
                }
                var zipped = counts.Select((count, index) => new { Count = count, Answer = answers[index] })
                   .OrderByDescending(pair => pair.Count)
                   .ToArray();
                counts = zipped.Select(pair => pair.Count).ToArray();
                answers = zipped.Select(pair => pair.Answer).ToArray();
                string[] result = new string[Math.Min(currEmptyInd, 5)];
                int cur = 0;
                for (int i = 0; i < result.Length; ++i) {
                    result[cur++] = answers[i];
                }
                return result;
            }
            public void Print()
            {
                foreach (Response response in _responses)
                {
                    response.Print();
                }
            }
        }
        public class Report
        {
            private Research[] _researches;
            private static int _id;

            static Report() { _id = 1; }
            public Report() { _researches = new Research[0]; }

            public Research[] Researches => _researches;

            public Research MakeResearch()
            {
                var res = new Research($"No_{_id++}_{DateTime.Today.Month:d2}/{(DateTime.Today.Year % 100):d2}");
                _researches = _researches.Append(res).ToArray();
                return res;
            }

            public (string, double)[] GetGeneralReport(int question)
            {
                if (_researches == null || question < 1 || question > 3) return null;
                Research temp = new Research("temporary");
                string[] temps = new string[0];
                foreach( Research research in _researches)
                {
                    foreach (Response response in research.Responses)
                    {
                        string[] ans = new string[]{ response.Animal, response.CharacterTrait, response.Concept };
                        if (ans[question - 1] != null)
                        {
                            temp.Add(ans);
                            temps = temps.Append(ans[question - 1]).ToArray();
                        }
                    }
                }
                temps = temps.Distinct().ToArray();
                double count_all = 0;
                (string, double)[] answer = new (string, double)[0];
                foreach (string s in temps) {
                    Response t = new Response(question == 1 ? s : null, question == 2 ? s : null, question == 3 ? s : null);
                    var cnt = t.CountVotes(temp.Responses, question);
                    count_all += cnt;
                    answer = answer.Append((s, cnt)).ToArray();
                }
                for(int i = 0; i < answer.Length; ++i)
                {
                    answer[i].Item2 /= count_all;
                    answer[i].Item2 *= 100.0;
                }
                return answer;
            }
        }
    }
}
