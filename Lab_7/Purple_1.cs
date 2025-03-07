using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.Remoting.Messaging;

namespace Lab_6
{
    public class Purple_1
    {
        public struct Participant
        {
            private string _name;
            private string _surname;
            private double[] _coefs;
            private int[,] _marks;

            private int _amount_of_jumps;


            public string Name => _name;
            public string Surname => _surname;
            public double[] Coefs
            {
                get
                {
                    if (_coefs == null) return default(double[]);

                    var newArray = new double[_coefs.Length];
                    Array.Copy(_coefs, newArray, _coefs.Length);
                    return newArray;
                }
            }
            public int[,] Marks
            {
                get
                {
                    if (_marks == null) return default(int[,]);

                    var newMatrix = new int[_marks.GetLength(0), _marks.GetLength(1)];
                    Array.Copy(_marks, newMatrix, _marks.Length);
                    return newMatrix;
                }
            }
            public double TotalScore
            {
                get
                {
                    if (_marks == null || _coefs == null) return 0;

                    double totalScore = 0;
                    for (int i = 0; i < _marks.GetLength(0); i++)
                    {
                        int[] newArr = new int[7];
                        for (int j = 0; j < _marks.GetLength(1); j++) newArr[j] = _marks[i, j];
                        Array.Sort(newArr);

                        double sum = 0;

                        for (int k = 1; k < newArr.Length - 1; k++) sum += newArr[k];

                        sum *= _coefs[i];
                        totalScore += sum;
                    }
                    return totalScore;
                }
            }
            


            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _coefs = new double[] { 2.5, 2.5, 2.5, 2.5 };
                _marks = new int[4, 7];
                _amount_of_jumps = 0;
            }


            public void SetCriterias(double[] coefs)
            {
                if (coefs == null || _coefs == null || coefs.Length != 4) return;
                Array.Copy(coefs, _coefs, coefs.Length);
            }

            public void Jump(int[] marks)
            {
                if (_amount_of_jumps > 4 || marks == null || _marks == null || marks.Length != 7) return; //needed or not?
                for (int j = 0; j < _marks.GetLength(1); j++)
                {
                    _marks[_amount_of_jumps, j] = marks[j];
                }
                _amount_of_jumps++;
            }

            public static void Sort(Participant[] array)
            {
                if( array == null ) return;
                for (int i = 0; i < array.Length; i++)
                {
                    Participant key = array[i];
                    int j = i - 1;

                    while (j >= 0 && array[j].TotalScore < key.TotalScore)
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
                Console.Write("Coefs: ");
                foreach (double var in _coefs)
                {
                    Console.Write(var + "  ");
                }
                Console.WriteLine();
                Console.WriteLine("Marks:");
                for (int i = 0; i < _marks.GetLength(0); i++)
                {
                    for (int j = 0; j < _marks.GetLength(1); j++)
                    {
                        Console.Write(_marks[i, j] + " ");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
        }

        



        
    }
}
