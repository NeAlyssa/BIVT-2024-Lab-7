using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Lab_6
{
    public class Purple_2
    {
        public struct Participant
        {
            private string _name;
            private string _surname;
            private int _distance;
            private int[] _marks;




            public string Name => _name;
            public string Surname => _surname;
            public int Distance => _distance;
            public int[] Marks
            {
                get
                {
                    if (_marks == null) return default(int[]);

                    var newArray = new int[_marks.Length];
                    Array.Copy(_marks, newArray, _marks.Length);
                    return newArray;
                }
            }

            public int Result
            {
                get
                {
                    if (_marks == null) return 0;
                    int sum = _marks.Sum() - _marks.Max() - _marks.Min(); ;
                    sum += 60 + (_distance-120) * 2 > 0 ? 60 + (_distance-120) * 2 : 0;
                    return sum;
                }
            }

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _distance = 0;
                _marks = new int[5];
            }

            public void Jump(int distance, int[] marks)
            {
                if (_distance != 0 || marks == null || _marks == null || marks.Length != 5) return; //needed or not?
                _distance = distance;
                Array.Copy(marks, _marks, marks.Length);
            }

            public static void Sort(Participant[] array)
            {
                if (array == null) return;  
                for (int i = 0; i < array.Length; i++)
                {
                    Participant key = array[i];
                    int j = i - 1;

                    while (j >= 0 && array[j].Result < key.Result)
                    {
                        array[j + 1] = array[j];
                        j = j - 1;
                    }
                    array[j + 1] = key;
                }
            }

            public void Print()
            {
                Console.WriteLine(_name + " " + _surname);
                Console.WriteLine(Result);
                Console.WriteLine($"Distance: {_distance}");
                Console.Write("Marks: ");
                foreach (double var in _marks)
                {
                    Console.Write(var + "  ");
                }
                Console.WriteLine();
            }
        }
        
    }
}


