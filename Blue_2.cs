using System.Reflection.Metadata.Ecma335;

namespace Lab_7{

public class Blue_2
{
    public struct Participant{
        private string _name;
        private string _surname;
        private int[,] _marks;
        private int _count;

        public string Name => _name;
        public string Surname => _surname;
        public int[,] Marks
        {
            get
            {
                if (_marks == null) return null;
                int[,] r = new int[_marks.GetLength(0), _marks.GetLength(1)];
                for (int i = 0; i < _marks.GetLength(0); i++)
                {
                    for (int j = 0; j < _marks.GetLength(1); j++)
                    {
                        r[i, j] = _marks[i, j];
                    }
                }
                return r;
                
            }
        }
        public int TotalScore{
            get{
                if (_marks == null ) return 0;
                int k = 0;
                for(int i=0;i<_marks.GetLength(0);i++)  {
                    for(int j=0;j<_marks.GetLength(1) ;j++) {
                        k += _marks[i,j];
                    }
                }
                return  k;
            }
            
        }
        public Participant(string name,string surname)
        {
            _name = name;
            _surname = surname;
            _marks = new int[2,5];
            _count=0;
        }
        public void Jump(int[] result)
        {
            if (result == null || result.Length == 0|| _count>1) return;
            if (_marks == null || _marks.GetLength(0) == 0) return;
            for (int i = 0; i < 5; i++)
            {
                _marks[_count, i] = result[i];
            }
            _count++;
        }
        public static void Sort(Participant[] array)
        {
            if (array == null || array.Length == 0) return;
            for (int i = 0; i < array.Length; i++)
                {
                    for (int j = 0; j < array.Length - i - 1; j++)
                    {
                        if (array[j].TotalScore < array[j + 1].TotalScore)
                        {
                            (array[j], array[j + 1]) = (array[j + 1], array[j]);
                        }
                    }
                }
        }

        
        public void Print(){
            Console.WriteLine($"{Name} {Surname} - {TotalScore}");
        }
    }
    public abstract class WaterJump{
        private string _name;
        private int _bank;
        private int _partid;
        private Participant[] _participants;
        public string Name => _name;
        public int Bank => _bank;
        public Participant[] Participants => _participants;
        public abstract double[] Prize{ get; }
        public WaterJump(string name, int bank){
            _name = name;
            _bank = bank;
            _partid=0;
            _participants = new Participant[0];
        }

        public void Add(Participant part){
            if (_participants == null || _participants.Length == 0 || _partid >= _participants.Length) return;
            if(_partid < _participants.Length){
                _participants[_partid] = part;
                _partid++;
            }
        }
        public void Add(Participant[] parts)
            {
                if (_participants == null || parts == null || parts.Length == 0) return;
                int i = 0;
                while (_partid < _participants.Length && i < parts.Length)
                {
                    _participants[_partid] = parts[i];
                    _partid++;
                    i++;
                }
            }
    }
    public class WaterJump3m : WaterJump{
        public WaterJump3m(string _name, int bank) : base(_name, bank) {}
        public override double[] Prize{
            get{
                if (this.Participants == null || this.Participants.Length <3) return null;
                double[] prizes = new double[3];
                prizes[0] = (double) 0.5*this.Bank;
                prizes[1] = (double) 0.3*this.Bank;
                prizes[2] = (double) 0.2*this.Bank;
                return prizes;
            }
        }
    }
    public class WaterJump5m : WaterJump{
        public WaterJump5m(string _name, int bank) : base(_name, bank) {}
    public override double[] Prize{
        get{
            if (this.Participants == null || this.Participants.Length <3) return null;
            int above = this.Participants.Length/2;
            int k;
            if (above > 10) { k=10;}
            else { k=above; }
            double[] prizes = new double[k];
            double N = 20/k;
            double percentage = N/100;
            for (int i =0;i<k;i++){
                prizes[i] = (double) Math.Round(percentage * this.Bank,5);
            }
            prizes[0] += (double) 0.4 * this.Bank;
            prizes[1] +=  (double) 0.25 * this.Bank;
            prizes[2] += (double) 0.15 * this.Bank;
            return prizes;
        }
    }
    }
}
}
