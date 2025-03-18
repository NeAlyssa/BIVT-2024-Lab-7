using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Lab_7;
public class Blue_3
{
    public class Participant 
    {
        private string _name;
        private string _surname;
        protected int[] _penaltyTimes;

        public string Name => _name;
        public string Surname => _surname;
        public int[] Penalties 
        {
            get
            {
                if (_penaltyTimes == null) 
                    return default(int[]);
                int[] newPenaltyTimes = new int[_penaltyTimes.Length];
                for (int i = 0; i<_penaltyTimes.Length; i++)
                    newPenaltyTimes[i] = _penaltyTimes[i];
                return newPenaltyTimes;
            }
        }
        public int Total
        {
            get
            {
                if (_penaltyTimes == null) 
                    return 0;
                int total = 0;
                foreach (int time in _penaltyTimes)
                    total+=time;
                return total;
            }
        }
        public virtual bool IsExpelled
        {
            get 
            {
                if (_penaltyTimes == null || _penaltyTimes.Length == 0) 
                    return false;
                foreach (int time in _penaltyTimes)
                {
                    if (time == 10)
                        return true;
                }
                return false;
            }
        }

        public Participant(string name, string surName)
        {
            _name = name;
            _surname = surName;
            _penaltyTimes = new int[0];
        }

        public virtual void PlayMatch(int time)
        {
            if (_penaltyTimes == null) 
                return;
            int[] newpenaltyTimes = new int[_penaltyTimes.Length+1];
            Array.Copy(_penaltyTimes, newpenaltyTimes, _penaltyTimes.Length);
            newpenaltyTimes[_penaltyTimes.Length] = time;
            _penaltyTimes = newpenaltyTimes;
        }

        public static void Sort(Participant[] array)
        {
            if (array == null || array.Length == 0) return;
            var sortedArray = array.OrderBy(partic => partic.Total).ToArray();
            Array.Copy(sortedArray,array,array.Length);
        }

        public void Print()
        {
            WriteLine($"{_name} {_surname}: ");
            foreach (int time in _penaltyTimes)
                Write($"{time} ");
            WriteLine("");
            WriteLine($"Total time: {Total} - {this.IsExpelled}");
        }
    }

    public class BasketballPlayer : Participant
    {
        public BasketballPlayer(string name, string surName) : base(name, surName) {}
        public override bool IsExpelled
        {
            get
            {
                if (_penaltyTimes == null || _penaltyTimes.Length == 0)
                    return false;
                int countFalls = 0;
                foreach (int penalty in _penaltyTimes)
                {
                    if (penalty >= 5)
                        countFalls++;
                }
                return 0.1*_penaltyTimes.Length < countFalls || Total >= 2*_penaltyTimes.Length;
            }
        }

        public override void PlayMatch(int time)
        {
            if (time < 0 || time > 5)
                return;
            base.PlayMatch(time);
        }
    }

    public class HockeyPlayer : Participant
    {
        private static int _allPenaltyTimes = 0;
        private static int _countPlayers = 0;

        public HockeyPlayer(string name, string surName) : base(name, surName) 
        {
            _countPlayers++;
        }

        private int SummaryTimes => _allPenaltyTimes;
        public override bool IsExpelled
        {
            get
            {
                if (_penaltyTimes == null || _penaltyTimes.Length == 0)
                    return false;
                foreach (int penalty in _penaltyTimes)
                {
                    if (penalty >= 10)
                        return true;
                }
                return Total > 0.1*_allPenaltyTimes/_countPlayers;
            }
        }

        public override void PlayMatch(int time)
        {
            base.PlayMatch(time);
            _allPenaltyTimes+=time;
        }
    }
}