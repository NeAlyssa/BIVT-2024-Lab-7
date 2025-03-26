using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
	public class Purple_3
	{
		public struct Participant
		{
			// поля
			private string _name;
			private string _surname;
			private double[] _marks;
			private int[] _places;
			private int _filled;

			//свойства
			public string Name { get { return _name; } }
			public string Surname { get { return _surname; } }
			public double[] Marks
			{
				get
				{
					if (_marks == null) { return null; }
					double[] marks = new double[7];
					Array.Copy(_marks, marks, marks.Length);
					return marks;
				}
			}

			public int[] Places
			{
				get
				{
					if (_places == null) { return null; }
					int[] places = new int[7];
					Array.Copy(_places, places, places.Length);
					return places;
				}
			}

			public int Score
			{
				get
				{
					if (_places == null)
					{
						return 0;
					}
					var score = _places.Sum();
					return score;
				}

			}
			//конструктор
			public Participant(string name, string surname)
			{
				_name = name;
				_surname = surname;
				_marks = new double[7];
				_places = new int[7];
				_filled = 0;
				for (int i = 0; i < _marks.Length; i++)
				{
					_marks[i] = 0;
					_places[i] = 0;
				}
			}

			// методы
			public void Evaluate(double result)
			{
				if (_filled > 6 || _marks == null)
				{
					return;
				}
				_marks[_filled] = result;
				_filled++;
			}

			public static void SetPlaces(Participant[] array)
			{
				if (array == null || array.Length == 0) { return; }
				if (array.Any(x => x._marks == null || x._places == null)) { return; }
				if (array.Any(x => x._filled < 6)) { return; }
				for (int i = 0; i < 7; i++)
				{
					var sortedarr = array.Where(x => x.Marks != null && x.Places != null).OrderByDescending(x => x.Marks[i]).ToArray();
					for (int j = 0; j < sortedarr.Length; j++)
					{
						sortedarr[j]._places[i] = j + 1;

					}
					if (i == 6)
					{
						sortedarr = sortedarr.Concat(array.Where(x => x.Marks == null)).ToArray();
						Array.Copy(sortedarr, array, array.Length);
					}
				}
			}
			public static void Sort(Participant[] array)
			{
				if (array == null || array.Length < 2) { return; }
				Participant[] sortedarr = array.Where(x => x.Marks != null).OrderBy(x => x.Score).ThenBy(x => x.Places.Min()).ThenByDescending(x => x.Marks.Sum()).ToArray();
				var nulls = array.Where(x => x.Marks == null || x.Places == null).ToArray();
				sortedarr = sortedarr.Concat(nulls).ToArray();
				Array.Copy(sortedarr, array, array.Length);
			}

			public void Print()
			{
				int topplace = this.Places.Min();
				if (topplace == 0)
				{
					return;
				}
				Console.WriteLine(this.Name + " " + this.Surname + " " + this.Score + " " + topplace + " " + this.Marks.Sum());
			}
		}
		public abstract class Skating
		{
			protected Participant[] _participants;
			protected double[] _moods;

			public Participant[] Participants
			{
				get
				{
					if(_participants == null)
					{
						return null;
					}
					return (Participant[])_participants.Clone();
				}
			}
			public double[] Moods
			{
				get
				{
					if (_moods == null)
					{
						return null;
					}
					else
					{
						return (double[])_moods.Clone();
					}
				}
			}
			public Skating(double[] moods)
			{
				if(moods == null || moods.Length < 7)
				{
					return;
				}

				_moods = new double[7];
				Array.Copy(moods, _moods, _moods.Length);
				ModificateMood();
				_participants = new Participant[0];

			}
			protected abstract void ModificateMood();
			public void Evaluate(double[] marks)
			{
				if(marks == null) { return; }
				for(int i = 0; i < _participants.Length; ++i)
				{
					if (_participants[i].Score == 0)
					{
						for(int j = 0; j  < marks.Length; ++j)
						{
							_participants[j].Evaluate(marks[j] * _moods[j]);
						}
						break;
					}
				}
			}
			public void Add(Participant sportsman)
			{
				if (_participants == null)
				{
					_participants = new Participant[0];
				}
				Array.Resize(ref _participants, _participants.Length + 1);
				_participants[_participants.Length - 1] = sportsman;
			}

			public void Add(Participant[] sportsmen)
			{
				if (sportsmen == null)
				{
					return;
				}

				if(_participants == null)
				{
					_participants = new Participant[0];
				}

				foreach (Participant el in sportsmen)
				{
					Add(el);
				}

			}
		}
		public class FigureSkating : Skating
		{
			public FigureSkating(double[] moods) : base(moods) { }
			protected override void ModificateMood()
			{
				if(_moods == null || _moods.Length < 7) { return; }
				for(int i = 0; i < _moods.Length; ++i)
				{
					_moods[i] += (i + 1) / 10.0;
				}
			}
		}

		public class IceSkating : Skating
		{
			public IceSkating(double[] moods) : base(moods){}
			protected override void ModificateMood()
			{
				if(_moods == null) { return; }
				for(int i = 0; i < _moods.Length; ++i)
				{
					_moods[i] += _moods[i] * (i + 1) / 100.0;
				}

			}
		}
	}

}
