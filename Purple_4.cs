using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
	public class Purple_4
	{
		public class Sportsman
		{
			// поля
			private string _name;
			private string _surname;
			private double _time;
			private bool _time_set;

			// свойства
			public string Name { get { return _name; } }
			public string Surname { get { return _surname; } }
			public double Time { get { return _time; } }

			//конструктор
			public Sportsman(string name, string surname)
			{
				_name = name;
				_surname = surname;
				_time = 0.0;
				_time_set = false;
			}

			//методы
			public void Run(double time)
			{
				if (_time_set) { return; }
				if (_time != default(double)) { return; }
				_time = time;
				_time_set = true;
			}
			public static void Sort(Sportsman[] array)
			{
				if(array == null)
				{
					return;
				}
				var sorted = array.OrderBy(t => t.Time).ToArray();
				Array.Copy(sorted, array, sorted.Length);
			}
			public void Print()
			{
				Console.WriteLine(this.Name + " " + this.Surname + " " + this.Time);
			}
		}

		public class SkiMan : Sportsman
		{
			public SkiMan(string name, string surname) : base(name, surname) { }
			public SkiMan(string name, string surname, double time): base(name, surname)
			{
				Run(time);
			}

		}

		public class SkiWoman: Sportsman
		{
            public SkiWoman(string name, string surname) : base(name, surname) { }
            public SkiWoman(string name, string surname, double time) : base(name, surname)
            {
                Run(time);
            }
        }

		public class Group
		{
			// поля
			private string _name;
			private Sportsman[] _sportsmen;

			//свойства
			public string Name { get { return _name; } }
			public Sportsman[] Sportsmen => _sportsmen;
			//{
			//	get
			//	{
			//		if (_sportsmen == null) { return null; }
			//		Sportsman[] sportsmen = new Sportsman[_sportsmen.Length];
			//		Array.Copy(_sportsmen, sportsmen, _sportsmen.Length);
			//		return sportsmen;
			//	}
			//}

			//конструкторы

			public Group(string name)
			{
				_name = name;
				_sportsmen = null;
				_sportsmen = new Sportsman[0];

			}

			public Group(Group group)
			{
				_name = group.Name;
				if (group.Sportsmen == null)
				{
					_sportsmen = null;
				}
				else
				{
					_sportsmen = new Sportsman[group.Sportsmen.Length];
					Array.Copy(group.Sportsmen, _sportsmen, group.Sportsmen.Length);
				}
			}

			//методы
			public void Add(Sportsman man)
			{
				if (_sportsmen == null) return;
				Array.Resize(ref _sportsmen, _sportsmen.Length + 1);
				_sportsmen[_sportsmen.Length - 1] = man;
			}



			public void Add(Sportsman[] sportsmen)
			{
				if (sportsmen == null || _sportsmen == null)
				{
					return;
				}

				int len = _sportsmen.Length;
				Array.Resize(ref _sportsmen, len + sportsmen.Length);
				Array.Copy(sportsmen, 0, _sportsmen, len, sportsmen.Length);
			}

			public void Add(Group group)
			{
				Add(group.Sportsmen);
			}

			public void Sort()
			{
				if (_sportsmen == null) return;
				var newarr = _sportsmen.OrderBy(x => x.Time).ToArray();
				Array.Copy(newarr, _sportsmen, _sportsmen.Length);
			}

			public static Group Merge(Group g1, Group g2)
			{
				if (g1.Sportsmen == null && g2.Sportsmen == null)
				{

					return new Group("Финалисты");
				}
				if (g2.Sportsmen == null)
				{
					return new Group("Финалисты");
				}
				if (g1.Sportsmen == null)
				{
					return new Group("Финалисты");
				}

				g1.Sort();
				g2.Sort();

				Sportsman[] merg = new Sportsman[g1.Sportsmen.Length + g2.Sportsmen.Length];
				int n = g1.Sportsmen.Length, m = g2.Sportsmen.Length, i = 0, j = 0, k = 0;


				while (i < n && j < m)
				{
					if (g1.Sportsmen[i].Time > g2.Sportsmen[j].Time)
					{
						merg[k++] = g2.Sportsmen[j++];
					}
					else
					{
						merg[k++] = g1.Sportsmen[i++];
					}
				}

				while (i < n)
				{
					merg[k++] = g1.Sportsmen[i++];
				}


				while (j < m)
				{
					merg[k++] = g2.Sportsmen[j++];
				}

				Group ng = new Group("Финалисты");
				ng.Add(merg);
				return ng;

			}

			public void Split(out Sportsman[] men, out Sportsman[] women)
			{
				if(_sportsmen == null)
				{
					men = null;
					women = null;
					return;
				}
				men = _sportsmen.Where(t => t is SkiMan).ToArray();
				women = _sportsmen.Where(t => t is SkiWoman).ToArray();

			}

			public void Shuffle()
			{
				if(_sportsmen == null)
				{
					return;
				}
				var sortmen = _sportsmen.Where(t => t is SkiMan).OrderBy(t => t.Time).ToArray();
				var sortwomen = _sportsmen.Where(t => t is SkiWoman).OrderBy(t => t.Time).ToArray();
				int menc = sortmen.Length;
				int womenc = sortwomen.Length;
				var final = new Sportsman[menc + womenc];
				int i = 0, j = 0, pos = 0 ;
				while( i < menc || j < womenc)
				{
					if(i < menc)
					{
						final[pos++] = sortmen[i++];
					}
					if(j < womenc)
					{
						final[pos++] = sortwomen[j++];
					}
				}
				_sportsmen = final;
			}
			public void Print()
			{
				Console.WriteLine("Имя группы: " + this.Name);
				if (this.Sportsmen.Length == null) return;
				foreach (var man in this.Sportsmen)
				{
					Console.WriteLine();
					man.Print();
				}
			}

		}
	}
}
