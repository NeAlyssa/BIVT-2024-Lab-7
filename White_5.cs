namespace Lab_7 {
    public class White_5 {
        public struct Match {
            // Поля
            private readonly int _goals;
            private readonly int _misses;

            // Свойства
            public int Goals => _goals;
            public int Misses => _misses;
            public int Difference => _goals - _misses;
            public int Score {
                get {
                    if (_goals > _misses) {
                        return 3;
                    } else if (_goals == _misses) {
                        return 1;
                    }
                    return 0;
                }
            }
            
            // Конструктор
            public Match(int goals, int misses) {
                if (goals < 0) {
                    Console.WriteLine("Goal count can't be negative");
                    return;
                }
                if (misses < 0) {
                    Console.WriteLine("Miss count can't be negative");
                    return;
                }

                _goals = goals;
                _misses = misses;
            }

            // Методы
            public void Print() {
               Console.WriteLine("Goals: " + Goals + "\tMisses: " + Misses); 
            }
        }

        abstract public class Team {
            // Поля
            private string _name;
            private Match[] _matches;

            // Свойства
            public string Name => _name;
            public Match[]? Matches => _matches != null ? _matches : default(Match[]);
            public int TotalDifference => _matches != null ? _matches.Sum(p => p.Difference) : 0;
            public int TotalScore => _matches != null ? _matches.Sum(p => p.Score) : 0;
            
            // Конструктор
            public Team (string name) {
                _name = name;
                _matches = new Match[0];
            }

            // Методы
            public virtual void PlayMatch(int goals, int misses) {
                if (_matches == null) {
                        Console.WriteLine("Array of matches wasn't initialized");
                        return;
                    }

                var newArray = new Match[_matches.Length + 1];
                Array.Copy(_matches, newArray, _matches.Length);
                Match match = new Match(goals, misses);
                newArray[_matches.Length] = match;
                _matches = newArray;
            }

            public static void SortTeams(Team[] teams) {
                if (teams == null) {
                    Console.WriteLine("Array of teams can't be null");
                    return;
                }
                if (teams.Length == 0) {
                    Console.WriteLine("Array of teams must not be empty");
                    return;
                }

                int l = teams.Length;
                for (int i = 0; i < l - 1; i++) {
                    for (int j = 0; j < l - i - 1; j++) {
                        if ((teams[j].TotalScore < teams[j + 1].TotalScore) ||
                         (teams[j].TotalScore == teams[j + 1].TotalScore && teams[j].TotalDifference < teams[j + 1].TotalDifference)) {
                            Team tmp = teams[j];
                            teams[j] = teams[j + 1];
                            teams[j + 1] = tmp;
                        }
                    }
                }
            }

            public void Print() {
                Console.WriteLine("Name: " + Name + "\tTotal score: " + TotalScore + "\tTotal difference: " + TotalDifference);
            }
        }

        public class ManTeam : Team {
            private ManTeam _derby;

            public ManTeam? Derby => _derby;

            public ManTeam(string name, ManTeam derby = null) : base(name) {
                _derby = derby;
            }

            public void PlayMatch(int goals, int misses, ManTeam team = null) {
                if (team != null && team == _derby) {
                    base.PlayMatch(goals + 1, misses);
                }
                else {
                    base.PlayMatch(goals, misses);
                }
            }
        }

        public class WomanTeam : Team {
            private int[] _penalties;

            // public int[]? Penalties {
            //     get {
            //         if (_penalties == null) {
            //             return default(int[]);
            //         }
            //         var newArray = new int[_penalties.Length];
            //         Array.Copy(_penalties, newArray, _penalties.Length);
            //         return newArray;
            //     }
            // }
            public int[]? Penalties => _penalties != null ? _penalties : default(int[]);

            public int TotalPenalties => _penalties != null ? _penalties.Sum() : 0;

            public WomanTeam(string name) : base(name) {
                _penalties = new int[0];
            }

            public override void PlayMatch(int goals, int misses) {
                base.PlayMatch(goals, misses);
                if (misses > goals) {
                    if (_penalties == null) {
                        Console.WriteLine("Array of penalties can't be null");
                        return;
                    }

                    var newArray = new int[_penalties.Length + 1];
                    Array.Copy(_penalties, newArray, _penalties.Length);
                    newArray[_penalties.Length] = misses - goals;
                    _penalties = newArray;
                }
            }
        }
    }
}