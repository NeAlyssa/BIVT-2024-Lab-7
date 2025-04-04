using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    public class Blue_2
    {
        // структура
        public struct Participant 
        {
            // приватные поля
            private string _name; 
            private string _surname; 
            private int[,] _marks; 
            private bool[] _jumped; 

            // свойства
            public string Name => _name;
            public string Surname => _surname; 
            public int[,] Marks
            {
                get
                {
                    if (_marks == null) return null;
                    int[,] new_array = new int[2, 5];
                    for (int i = 0; i < new_array.GetLength(0); i++)
                    {
                        for (int j = 0; j < new_array.GetLength(1); j++) { new_array[i, j] = _marks[i, j]; }
                    }
                    return new_array;
                }
            }
            public int TotalScore
            {
                get
                {
                    if (_marks == null) return 0;
                    int scr = 0;
                    for (int i = 0; i < _marks.GetLength(0); i++)
                    {
                        for (int j = 0; j < _marks.GetLength(1); j++) { scr += _marks[i, j]; }
                    }
                    return scr;
                }
            }

            // конструктор
            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new int[2, 5];
                _jumped = new bool[2];
            }

            // методы 
            public void Jump(int[] result) 
            {
                if (result == null) return;
                if (_marks == null) return;
                int no_jump = Array.FindIndex(_jumped, jump => !jump); 
                if (no_jump == -1) { return; }
                for (int i = 0; i < 5; i++) { _marks[no_jump, i] = result[i]; }
                _jumped[no_jump] = true;
            }

            public static void Sort(Participant[] array) // статический метод
            {
                if (array == null || array.Length == 0) return;
                for (int i = 0; i < array.Length; i++)
                {
                    for (int j = 0; j < array.Length - i - 1; j++)
                    {
                        if (array[j + 1].TotalScore > array[j].TotalScore) { (array[j], array[j + 1]) = (array[j + 1], array[j]); }
                    }
                }
            }
            public void Print()
            {
                Console.WriteLine($"{_name} {_surname} {TotalScore}");
                Console.WriteLine();
            }
        }

        // абстрактный класс
        public abstract class WaterJump
        {
            // приватные поля
            private string _name;
            private int _bank;
            private Participant[] _participants;

            // свойства
            public string Name => _name;
            public int Bank => _bank;
            public Participant[] Participants => _participants;
            public abstract double[] Prize { get; }
            
            // методы
            public WaterJump(string name, int bank)
            {
                _name = name;
                _bank = bank;
                _participants = new Participant[0];
            }

            public void Add(Participant participant) 
            {
                if (_participants == null) return;
                Participant[] participant_2 = new Participant[_participants.Length + 1];
                for (int i = 0; i < _participants.Length; i++) { participant_2[i] = _participants[i]; }
                participant_2[_participants.Length] = participant;
                _participants = participant_2;
            }
            public void Add(Participant[] participants)
            {
                if (_participants == null || participants.Length == 0 || participants == null) return;
                foreach (Participant t in participants) { Add(t); }
            }
        }

        // классы-наследники
        public class WaterJump3m : WaterJump
        {
            public WaterJump3m(string name, int bank) : base(name, bank) { }
            public override double[] Prize
            {
                get
                {
                    double[] prs = new double[3];
                    if (this.Participants.Length < 3 || this.Participants == null) return default(double[]);
                    prs[0] = this.Bank * 0.5; // 1 место
                    prs[1] = this.Bank * 0.3; // 2 место
                    prs[2] = this.Bank * 0.2; // 3 место
                    return prs;
                }
            }
        }
        public class WaterJump5m : WaterJump
        {
            public WaterJump5m(string name, int bank) : base(name, bank) { }
            public override double[] Prize
            {
                get
                {
                    if (this.Participants.Length < 3 || this.Participants == null) return default(double[]);
                    int с;
                    double[] prs;
                    if (Participants.Length / 2 < 10)
                    {
                        prs = new double[Participants.Length / 2];
                        с = Participants.Length / 2;
                    }
                    else
                    {
                        prs = new double[10];
                        с = 10;
                    }
                    double С = 20.0 / с;
                    for (int i = 0; i < с; i++) { prs[i] = this.Bank * (С / 100); }
                    prs[0] += this.Bank * 0.4; 
                    prs[1] += this.Bank * 0.25; 
                    prs[2] += this.Bank * 0.15; 
                    return prs;
                }
            }
        }
    }
}
