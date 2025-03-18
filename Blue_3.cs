namespace Lab_7{

public class Blue_3
{
    public class Participant{
        private string _name;
        private string _surname;
        protected int[] _penaltytimes;
        public string Name => _name;
        public string Surname => _surname;
        public int[] Penalties{
            get{
                if (_penaltytimes== null) return null;
                if (_penaltytimes.Length==0) return _penaltytimes;
                int[] p =new int[_penaltytimes.Length];
                Array.Copy(_penaltytimes, p, _penaltytimes.Length);
                /*for(int i =0;i<_penaltytimes.Length;i++){
                    p[i] = _penaltytimes[i];
                }*/
                return p;
            }
            
        }
        public int Total{
            get{
                if (_penaltytimes == null || _penaltytimes.Length == 0) return 0;
                    return _penaltytimes.Sum();
            }
        }
        public virtual bool IsExpelled{
            get{
                if (_penaltytimes == null || _penaltytimes.Length==0) return true;
                for (int i =0;i<_penaltytimes.Length;i++){
                    if (_penaltytimes[i] == 10)
                    {
                        return true;
                    }
                    
                }
                return false;
            }
        }
        public Participant(string name, string surname){
            _name = name;
            _surname = surname;
            _penaltytimes = new int[0];
        }
        public virtual void PlayMatch(int time)
            {
                if (_penaltytimes== null) return;
                int[] p = new int[_penaltytimes.Length+1];
                for (int i=0; i<p.Length-1; i++)
                {
                    p[i] = _penaltytimes[i];
                }
                p[p.Length-1] = time;
                _penaltytimes = p;
            }
        public static void Sort(Participant[] array)
            {
                if (array == null) return;
                for (int i = 0; i < array.Length; i++)
                {
                    for (int j = 0; j < array.Length - i - 1; j++)
                    {
                        if (array[j].Total > array[j + 1].Total)
                        {
                            (array[j], array[j + 1]) = (array[j + 1], array[j]);
                        }
                    }
                }
            }
        public void Print(){
            Console.WriteLine($"{Name} {Surname} - {Total}");
        }
    }
    public class BasketballPlayer : Participant{
            public override bool IsExpelled{
                get{
                    if (_penaltytimes == null || _penaltytimes.Length == 0) return false;
                    int kolmat = _penaltytimes.Length;
                    int failkol = 0;
                    foreach (int pen in _penaltytimes){
                        if (pen >= 5){
                            failkol++;
                        }
                    }
                    if (failkol  > 0.1*kolmat || this.Total > 2 * kolmat){
                        return true;
                    }
                    return false;

                }
            }
            public BasketballPlayer(string name,string surname) : base(name, surname){
                _penaltytimes=new int[0];
            }
            public override void PlayMatch(int falls)
            {
                if(falls <0 || falls >5){
                    System.Console.WriteLine("Sorry, my dear friend( This is incorrect");
                    return;
                }
                base.PlayMatch(falls);
            }
        }
    public class HockeyPlayer : Participant{
        private static int _kolplayers = 0;
        private static int _alltimekol = 0;
        public override bool IsExpelled{
            get{
                if(_penaltytimes == null || _penaltytimes.Length == 0)return false;
                foreach(int p in _penaltytimes){
                    if (p>=10){
                        return true;
                    }
                }
                if (this.Total > 0.1 * _alltimekol / _kolplayers){
                    return true;
                }
                return false;
            }
        }
            public HockeyPlayer(string name,string surname) : base(name,surname){
            _penaltytimes=new int[0];
            _kolplayers += 1;
        }
        public override void PlayMatch(int penaltytime)
        {
            if(_penaltytimes == null) return;
            base.PlayMatch(penaltytime);
            _alltimekol+= penaltytime;
        }
        
        }

}
}