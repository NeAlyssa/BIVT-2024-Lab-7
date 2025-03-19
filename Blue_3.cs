using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_6
{
    public class Blue_3
    {
        public class Participant
        {
            //поля
            private string _name;
            private string _surname;
            protected int[] _penalties;
            //свойства 
            public string Name { get { return _name; } }
            public string Surname { get { return _surname; } }
            public int[] Penalties
            {
                get
                {
                    if (_penalties == null) return null;
                    if (_penalties.Length == 0) return _penalties;
                    int[] copy = new int[_penalties.Length];
                    for (int i = 0; i < copy.Length; i++)
                    {
                        copy[i] = _penalties[i];
                    }
                    return copy;
                }
            }
            public int Total
            {
                get
                {
                    if (_penalties == null || _penalties.Length == 0) return 0;
                    int count = 0;
                    for (int i = 0; i < _penalties.Length; i++)
                    {
                        count += _penalties[i];
                    }
                    return count;
                }
            }
            public virtual bool IsExpelled
            {
                get
                {
                    if (_penalties == null) return false;
                    bool t = false;
                    for (int i = 0; i < _penalties.Length; i++)
                    {
                        if (_penalties[i] == 10) return true;
                    }
                    return t;
                }
            }
            //конструктор
            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _penalties = new int[0];
            }
            //методы 
            public static void Sort(Participant[] array)
            {
                if (array == null || array.Length == 0) return;
                for (int i = 1, j = 2; i < array.Length;)
                {
                    if (i == 0 || array[i - 1].Total <= array[i].Total)
                    {
                        i = j;
                        j++;
                    }
                    else
                    {
                        Participant temp = array[i];
                        array[i] = array[i - 1];
                        array[i - 1] = temp;
                        i--;
                    }
                }
            }
            public virtual void PlayMatch(int time)
            {
                if (_penalties == null) return;
                if (_penalties.Length == 0)
                {
                    int[] _pT = new int[1];
                    _pT[0] = time;
                    _penalties = new int[1];
                    Array.Copy(_pT, _penalties, _pT.Length);
                }
                else
                {
                    int[] _pT = new int[_penalties.Length + 1];
                    _pT[_pT.Length - 1] = time;
                    for (int i = 0; i < _penalties.Length; i++)
                    {
                        _pT[i] = _penalties[i];
                    }
                    _penalties = new int[_pT.Length];
                    Array.Copy(_pT, _penalties, _pT.Length);
                }
            }
            public void Print()
            {
                Console.WriteLine($"{this._name} {this._surname} {this.Total} {this.IsExpelled}");
            }
        }
        public class BasketballPlayer : Participant
        {
            //свойства
            public override bool IsExpelled
            {
                get
                {
                    if (_penalties == null) return false;
                    int n = this.Penalties.Length;
                    int count = 0;
                    for (int i = 0; i < n; i++)
                    {
                        if (this.Penalties[i] >= 5) count++;
                    }
                    if ((count * 100 / n) > 10 || this.Total > n * 2) return true;
                    return false;
                }
            }
            //конструктор
            public BasketballPlayer(string name, string surname) : base(name, surname)
            {
                _penalties = new int[0];
            }
            //методы
            public override void PlayMatch(int fall)
            {
                if (fall < 0 || fall > 5) return;
                base.PlayMatch(fall);  
            }
        }
        public class HockeyPlayer : Participant
        {
            //поля
            private int _countPlayers;
            private int _timePlayers;
            //свойства
            public override bool IsExpelled
            {
                get
                {
                    if (_penalties == null) return false;
                    int n = this.Penalties.Length;
                    for (int i = 0; i < n; i++)
                    {
                        if (this.Penalties[i] >= 10) return true;
                    }
                    if (this.Total > 0.1 * _timePlayers / _countPlayers) return true;
                    return false;
                }
            }
            //конструктор
            public HockeyPlayer(string name, string surname) : base(name, surname)
            {
                _penalties = new int[0];
                _countPlayers++;
            }
            //методы
            public override void PlayMatch(int time)
            {
                if (_penalties == null) return;
                if (time < 0 || time > 10) return;
                base.PlayMatch(time);
                _timePlayers += time;
            }
        }
    }
}
