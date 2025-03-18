using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Lab_7;
public class Blue_2
{
    public struct Participant
    {
        private string _name;
        private string _surname;    
        private int[,] _marks;

        public string Name => _name;
        public string Surname => _surname;
        public int[,] Marks
        {
            get
            {
                if (_marks == null) return default(int[,]);
                int[,] newMarks = new int[2,5];
                for (int i = 0; i<2; i++)
                {
                    for (int j = 0; j<5; j++)
                        newMarks[i,j] = _marks[i,j];
                }
                return newMarks;
            }
        }
        public int TotalScore
        {
            get
            {
                if (_marks == null) return 0;
                int total = 0; 
                for (int i = 0; i<2; i++)
                {
                    for (int j = 0; j<5; j++)
                        total += _marks[i,j];
                }
                return total;
            }
        }

        public Participant(string name, string surname)
        {
            _name = name;
            _surname = surname;
            _marks = new int[2,5];
        }

         public void Jump(int[] result)
         {
            if (result == null || _marks == null)
                return;
            int sum1 = 0; int sum2 = 0;
            for (int i = 0; i<5; i++)
            {
                sum1 += _marks[0,i];
                sum2 += _marks[1,i];
            }
            if (sum1 == 0 && sum2 == 0)
            {
                for (int i = 0; i<5; i++)
                    _marks[0,i] = result[i]; 
            }
            else if (sum1 != 0 && sum2 == 0)
            {
                for (int i = 0; i<5; i++)
                    _marks[1,i] = result[i]; 
            }
            
         }
        
        public static void Sort(Participant[] array)
        {
            if (array == null || array.Length <= 1) return;
            var sortedArray = array.OrderByDescending(partic => partic.TotalScore).ToArray();
            Array.Copy(sortedArray, array, array.Length);
        }

        public void Print()
        {
            WriteLine ($"{_name} {_surname}");
            Write("First: ");
            for (int i = 0; i<5; i++)
                Write($"{_marks[0,i]} ");
            WriteLine("");
            Write("Second: ");
            for (int i = 0; i<5; i++)
                Write($"{_marks[1,i]} ");
            WriteLine("");
        }
    }

    public abstract class WaterJump 
    {
        private string _name;
        private int _bank;
        private Participant[] _participants;

        public string Name => _name;
        public int Bank => _bank;
        public Participant[] Participants => _participants;
        public abstract double[] Prize {get;} 

        public WaterJump(string name, int bank)
        {
            _name = name;
            _bank = bank;
            _participants = new Participant[0];
        }

        public void Add(Participant participant)
        {
            if (_participants == null)
                return;
            Participant[] newParticipants = new Participant[_participants.Length+1];
            Array.Copy(_participants, newParticipants, _participants.Length);
            newParticipants[_participants.Length] = participant;
            _participants = newParticipants;
        }
        public void Add(Participant[] participants)
        {
            if (_participants == null || participants == null || participants.Length == 0)
                return;
            foreach (Participant participant in participants)
                Add(participant);
        }
    }

    public class WaterJump3m : WaterJump
    {
        public WaterJump3m(string name, int bank) : base(name, bank) {}
        public override double[] Prize
        {
            get
            {
                if (this.Participants == null || this.Participants.Length < 3)
                    return default(double[]);
                double[] prize = new double[3] {0.5*Bank, 0.3*Bank, 0.2*Bank};
                return prize;
            }
        }
    }

    public class WaterJump5m : WaterJump
    {
        public WaterJump5m(string name, int bank) : base(name, bank) {}
        public override double[] Prize
        {
            get
            {
                if (this.Participants == null || this.Participants.Length < 3)
                    return default(double[]);
                int n = (Participants.Length <= 20) ? Participants.Length/2 : 10;
                double[] prize = new double[n];
                prize[0] = 0.4*Bank; prize[1] = 0.25*Bank; prize[2] = 0.15*Bank;

                double N = 20.0/n * Bank/100;
                for (int i = 3; i<n; i++)
                {
                    prize[i] = N;
                }
                return prize;
            }
        }
    }
}