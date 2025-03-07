using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Program program = new Program();
            //program.Test1();

            Purple_2.Participant participant = new Purple_2.Participant("John", "Smith");

            participant.Jump(115, new int[] { 1, 2, 3, 4, 5 });
            //participant.Print();

            //program.Test2();

            //program.Test4();
            
            //program.Test5();
        }

        public void Test1()
        {
            string[] names = new string[] {"Дарья", "Тихонова", "Александр", "Козлов", "Никита",
                "Павлов", "Юрий", "Луговой", "Юрий", "Степанов", "Мария", "Луговая", "Виктор",
                "Жарков", "Марина", "Иванова", "Марина", "Полевая", "Максим", "Тихонов"};

            double[][] coefs = new double[][]
            {
                new double[] {2.58, 2.9, 3.04, 3.43},
                new double[] {2.95, 2.63, 3.16, 2.89},
                new double[] {2.56, 3.4, 2.91, 2.69},
                new double[] {2.86, 2.9, 3.19, 3.14},
                new double[] {2.81, 2.64, 2.76, 3.2},
                new double[] {2.74, 3.3, 2.94, 3.27},
                new double[] {2.57, 2.79, 2.71, 3.46},
                new double[] {3.09, 2.67, 2.9, 3.5},
                new double[] {2.65, 3.47, 3.11, 3.39},
                new double[] {3.14, 3.46, 2.96, 2.76}
            };

            int[,] jumps = new int[,]
            {
                {3, 4, 1, 2, 1, 3, 1}, {5, 3, 4, 3, 3, 3, 3}, {2, 4, 1, 5, 6, 1, 2}, {6, 4, 3, 2, 2, 1, 1},
                {3, 5, 4, 4, 5, 1, 4}, {1, 6, 5, 2, 1, 4, 1}, {6, 2, 4, 1, 2, 6, 5}, {6, 5, 2, 2, 4, 3, 4},
                {1, 1, 3, 5, 5, 5, 2}, {4, 1, 1, 2, 2, 2, 5}, {5, 2, 3, 3, 2, 2, 3}, {3, 1, 3, 4, 2, 4, 5},
                {3, 3, 5, 2, 1, 2, 4}, {5, 5, 4, 2, 3, 2, 2}, {6, 3, 1, 2, 2, 6, 6}, {5, 1, 6, 6, 3, 2, 5},
                {4, 3, 5, 4, 5, 1, 1}, {5, 3, 4, 2, 1, 1, 2}, {2, 2, 4, 2, 6, 3, 4}, {3, 2, 1, 3, 5, 1, 5},
                {6, 5, 5, 4, 2, 6, 4}, {5, 4, 3, 2, 4, 6, 1}, {1, 1, 3, 4, 4, 1, 6}, {3, 1, 5, 1, 4, 3, 1},
                {4, 6, 1, 4, 5, 3, 4}, {1, 2, 3, 1, 5, 4, 3}, {3, 6, 2, 3, 1, 6, 3}, {3, 3, 6, 6, 3, 6, 6},
                {6, 5, 3, 2, 6, 5, 3}, {5, 4, 4, 2, 1, 2, 4}, {4, 2, 2, 5, 1, 3, 1}, {6, 5, 6, 1, 6, 3, 3},
                {3, 6, 3, 5, 4, 2, 3}, {4, 6, 1, 4, 2, 1, 5}, {1, 1, 3, 1, 3, 2, 6}, {1, 4, 4, 6, 6, 2, 5},
                {3, 3, 1, 4, 5, 6, 2}, {6, 4, 5, 4, 2, 3, 1}, {3, 3, 4, 2, 2, 3, 6}, {5, 1, 5, 5, 1, 3, 4} 
            };

            //Make a list for the participants
            Purple_1.Participant[] leaderboard = new Purple_1.Participant[10];

            //Initialize with names
            for (int i = 0; i < leaderboard.Length; i++)
            {
                leaderboard[i] = new Purple_1.Participant(names[i*2], names[i*2+1]);
            }
                    
            //Set criterias/coefficients 
            //and jumps
            for (int i = 0; i < leaderboard.Length; i++)
            {
                leaderboard[i].SetCriterias(coefs[i]);
                for (int k = 0; k < 4; k++)
                {
                    int[] newJump = new int[7];
                    for(int j = 0; j < 7; j++)
                    {
                        newJump[j] = jumps[i*4+k, j];
                    }
                    leaderboard[i].Jump(newJump);
                }
            }

            //Sort the leaderboard
            Purple_1.Participant.Sort(leaderboard);

            //Print the leaderboard
            foreach (Purple_1.Participant var in leaderboard)
            {
                Console.WriteLine(var.Name + " " + var.Surname + "  " + var.TotalScore);
            }
        }
        public void Test2()
        {
            string[] names = new string[] { "Оксана", "Сидорова", "Полина", "Полевая","Дмитрий", 
                "Полевой", "Евгения", "Распутина", "Савелий", "Луговой",
                "Евгения", "Павлова", "Егор", "Свиридов", "Степан", "Свиридов", 
                "Анастасия", "Козлова", "Светлана", "Свиридова" };

            int[] distances = new int[] { 135, 191, 147, 115, 112, 151, 186, 166, 112, 197 };

            int[][] marks =
            {
                new int[] {15, 1, 3, 9, 15},
                new int[] {19, 14, 9, 11, 4},
                new int[] {20, 9, 1, 13, 6},
                new int[] {5, 20, 17, 9, 16},
                new int[] {19, 8, 1, 6, 17},
                new int[] {16, 12, 5, 20, 4},
                new int[] {5, 20, 3, 19, 18},
                new int[] {16, 12, 5, 4, 15},
                new int[] {7, 4, 19, 11, 12},
                new int[] {14, 3, 6, 17, 1}
            };

            //Make a list for the participants
            Purple_2.Participant[] leaderboard = new Purple_2.Participant[10];

            //Initialize with names
            for (int i = 0; i < leaderboard.Length; i++)
            {
                leaderboard[i] = new Purple_2.Participant(names[i * 2], names[i * 2 + 1]);
            }

            //Set distances and marks
            for (int i = 0; i < leaderboard.Length; i++)
            {
                leaderboard[i].Jump(distances[i], marks[i]);
            }

            //Sort the leaderboard
            Purple_2.Participant.Sort(leaderboard);

        
            //Print the leaderboard
            foreach (Purple_2.Participant var in leaderboard)
            {
                Console.WriteLine(var.Name + " " + var.Surname + "  " + var.Result);
            }
        }
        public void Test3()
        {
            string[] names = new string[] { "Виктор", "Полевой", "Алиса", "Козлова", "Ярослав", 
                "Зайцев", "Савелий", "Кристиан", "Алиса", "Козлова", "Алиса", "Луговая", 
                "Александр", "Петров", "Мария", "Смирнова", "Полина", "Сидорова", "Татьяна", 
                "Сидорова" };

            double[,] marks = new double[,]
            {
                {5.93, 5.44, 1.2, 0.28, 1.57, 1.86, 5.89},
                {1.68, 3.79, 3.62, 2.76, 4.47, 4.26, 5.79},
                {2.93, 3.1, 5.46, 4.88, 3.99, 4.79, 5.56},
                {4.2, 4.69, 3.9, 1.67, 1.13, 5.66, 5.4},
                {3.27, 2.43, 0.9, 5.61, 3.12, 3.76, 3.73},
                {0.75, 1.13, 5.43, 2.07, 2.68, 0.83, 3.68},
                {3.78, 3.42, 3.84, 2.19, 1.2, 2.51, 3.51},
                {1.35, 3.4, 1.85, 2.02, 2.78, 3.23, 3.03},
                {0.55, 5.93, 0.75, 5.15, 4.35, 1.51, 2.77},
                {3.86, 0.19, 0.46, 5.14, 5.37, 0.94, 0.84}
            };

            //Make a list for the participants
            Purple_3.Participant[] leaderboard = new Purple_3.Participant[10];

            //Initialize with names
            for (int i = 0; i < leaderboard.Length; i++)
            {
                leaderboard[i] = new Purple_3.Participant(names[i * 2], names[i * 2 + 1]);
            }

            //Set distances and marks
            for (int i = 0; i < marks.GetLength(0); i++)
            {
                for(int j = 0; j < marks.GetLength(1); j++)
                {
                    leaderboard[i].Evaluate(marks[i, j]);
                }
            }

            //Set places
            Purple_3.Participant.SetPlaces(leaderboard);

            //Sort the leaderboard
            Purple_3.Participant.Sort(leaderboard);

            //Print the leaderboard
            foreach (Purple_3.Participant var in leaderboard)
            {
                Console.WriteLine(var.Name + " " + var.Surname + "  " + var.Score + " "
                    + var.Places.Min() + " " + var.Marks.Sum());
            }
        }
        public void Test4()
        {
            string[] names1 = new string[] { "Полина", "Луговая", "Савелий", "Козлов", "Екатерина",
                "Жаркова", "Дмитрий", "Иванов", "Дмитрий", "Полевой", "Савелий", "Петров", "Евгения",
                "Распутина", "Екатерина", "Луговая", "Мария", "Иванова", "Степан", "Павлов", "Ольга",
                "Павлова", "Ольга", "Полевая", "Дарья", "Павлова", "Дарья", "Свиридова", "Евгения",
                "Свиридова" };

            double[] times1 = new double[] { 422.64, 142.05, 185.23, 294.32, 79.26, 230.63, 35.16, 376.12,
                279.20, 292.38, 467.60, 473.82, 290.14, 368.60, 212.67 };

            string[] names2 = new string[] { "Анастасия", "Жаркова", "Александр", "Павлов", "Степан",
                "Свиридов", "Игорь", "Сидоров", "Евгения", "Сидорова", "Мария", "Сидорова", "Лев",
                "Петров", "Савелий", "Козлов", "Егор", "Свиридов", "Оксана", "Жаркова", "Светлана",
                "Петрова", "Полина", "Петрова", "Екатерина", "Павлова", "Юлия", "Полевая", "Евгения",
                "Павлова" };

            double[] times2 = new double[] { 112.49, 472.11, 213.92, 102.13, 263.21, 350.75, 248.68, 325.28,
                300.00, 252.16, 402.20, 397.33, 384.94, 8.09, 480.52 };

            //Create a list of the 1st group
            Purple_4.Sportsman[] team1 = new Purple_4.Sportsman[15];
            //Set their data
            for (int i = 0; i < team1.Length; i++)
            {
                team1[i] = new Purple_4.Sportsman(names1[i * 2], names1[i * 2 + 1]);
                team1[i].Run(times1[i]);
            }

            //Create a list of the 2nd group
            Purple_4.Sportsman[] team2 = new Purple_4.Sportsman[15];
            //Set their data
            for (int i = 0; i < team2.Length; i++)
            {
                team2[i] = new Purple_4.Sportsman(names2[i * 2], names2[i * 2 + 1]);
                team2[i].Run(times2[i]);
            }

            //Create the two groups
            Purple_4.Group group1 = new Purple_4.Group("Spartak");
            Purple_4.Group group2 = new Purple_4.Group("CSKA");

            //Add the sportsmen to groups
            group1.Add(team1);
            group2.Add(team2);

            //Merge
            var finalists = Purple_4.Group.Merge(group1, group2);

            //Print the group
            finalists.Print();

        }
        public void Test5()
        {
            string[][] responses = new string[][]
            {
                new string[] {"Макака", null, "Манга"},
                new string[] {"Тануки", "Проницательность", "Манга"},
                new string[] {"Тануки", "Скромность", "Кимоно"},
                new string[] {"Кошка", "Внимательность", "Суши"},
                new string[] {"Сима_энага", "Дружелюбность", "Кимоно"},
                new string[] {"Макака", "Внимательность", "Самурай"},
                new string[] {"Панда", "Проницательность", "Манга"},
                new string[] {"Сима_энага", "Проницательность", "Суши"},
                new string[] {"Серау", "Внимательность", "Сакура"},
                new string[] {"Панда", null, "Кимоно"},
                new string[] {"Сима_энага", "Дружелюбность", "Сакура"},
                new string[] {"Кошка", "Внимательность", "Кимоно"},
                new string[] {"Панда", null, "Сакура"},
                new string[] {"Кошка", "Уважительность", "Фудзияма"},
                new string[] {"Панда", "Целеустремленность", "Аниме"},
                new string[] {"Серау", "Дружелюбность", null},
                new string[] {"Панда", null, "Манга"},
                new string[] {"Сима_энага", "Скромность", "Фудзияма"},
                new string[] {"Панда", "Проницательность", "Самурай"},
                new string[] {"Кошка", "Внимательность", "Сакура"}
            };

            //Create the research, add the responses
            Purple_5.Research research = new Purple_5.Research("Test");
            for (int i = 0; i < responses.Length; i++)
            {
                research.Add(responses[i]);
            }

            //Get top responses for all questions
            var topResponsesAnimal = research.GetTopResponses(1);
            var topResponsesCharacterTrait = research.GetTopResponses(2);
            var topResponsesConcept = research.GetTopResponses(3);

            //Print
            Console.WriteLine("Animals:");
            foreach (var response in topResponsesAnimal)
            {
                Console.WriteLine(response);
            }
            Console.WriteLine();

            Console.WriteLine("Character traits:");
            foreach (var response in topResponsesCharacterTrait)
            {
                Console.WriteLine(response);
            }
            Console.WriteLine();

            Console.WriteLine("Concepts:");
            foreach (var response in topResponsesConcept)
            {
                Console.WriteLine(response);
            }
        }
    }
}
