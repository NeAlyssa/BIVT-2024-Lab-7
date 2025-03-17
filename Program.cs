using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Check1();
            Console.WriteLine();
            Check2();
            Console.WriteLine();
            Check3();
            Console.WriteLine();
            Check4();
            Console.WriteLine();
            Check5();
        }

        static void Check1()
        {
            var names = "Дарья,Тихонова,Александр,Козлов,Никита,Павлов,Юрий,Луговой,Юрий,Степанов,Мария,Луговая,Виктор,Жарков,Марина,Иванова,Марина,Полевая,Максим,Тихонов".Split(',');
            var jumps = "3,4,1,2,1,3,1,5,3,4,3,3,3,3,2,4,1,5,6,1,2,6,4,3,2,2,1,1,3,5,4,4,5,1,4,1,6,5,2,1,4,1,6,2,4,1,2,6,5,6,5,2,2,4,3,4,1,1,3,5,5,5,2,4,1,1,2,2,2,5,5,2,3,3,2,2,3,3,1,3,4,2,4,5,3,3,5,2,1,2,4,5,5,4,2,3,2,2,6,3,1,2,2,6,6,5,1,6,6,3,2,5,4,3,5,4,5,1,1,5,3,4,2,1,1,2,2,2,4,2,6,3,4,3,2,1,3,5,1,5,6,5,5,4,2,6,4,5,4,3,2,4,6,1,1,1,3,4,4,1,6,3,1,5,1,4,3,1,4,6,1,4,5,3,4,1,2,3,1,5,4,3,3,6,2,3,1,6,3,3,3,6,6,3,6,6,6,5,3,2,6,5,3,5,4,4,2,1,2,4,4,2,2,5,1,3,1,6,5,6,1,6,3,3,3,6,3,5,4,2,3,4,6,1,4,2,1,5,1,1,3,1,3,2,6,1,4,4,6,6,2,5,3,3,1,4,5,6,2,6,4,5,4,2,3,1,3,3,4,2,2,3,6,5,1,5,5,1,3,4".Split(',');
            var crids = new double[] { 2.58, 2.90, 3.04, 3.43, 2.95, 2.63, 3.16, 2.89, 2.56, 3.40, 2.91, 2.69, 2.86, 2.90, 3.19, 3.14, 2.81, 2.64, 2.76, 3.20, 2.74, 3.30, 2.94, 3.27, 2.57, 2.79, 2.71, 3.46, 3.09, 2.67, 2.90, 3.50, 2.65, 3.47, 3.11, 3.39, 3.14, 3.46, 2.96, 2.76 };
            var part = new Purple_1.Participant[10];
            int ind = 0;
            for (int i = 0; i < 10; i++)
            {
                part[i] = new Purple_1.Participant(names[i * 2], names[i * 2 + 1]);
                var crid = new double[4];
                for (int j = 0; j < 4; j++)
                    crid[j] = crids[i * 4 + j];
                part[i].SetCriterias(crid);
                for (int j = 0; j < 4; j++)
                {
                    int[] arr = new int[7];
                    for (int k = 0; k < 7; k++)
                    {
                        arr[k] = int.Parse(jumps[ind]);
                        ind++;
                    }
                    part[i].Jump(arr);
                }
            }

            Purple_1.Participant.Sort(part);
            foreach (var p in part)
            {
                Console.WriteLine(p.Name + " " + p.Surname + " " + p.TotalScore);
            }
        }

        static void Check2()
        {
            var names = "Оксана,Сидорова,Полина,Полевая,Дмитрий,Полевой,Евгения,Распутина,Савелий,Луговой,Евгения,Павлова,Егор,Свиридов,Степан,Свиридов,Анастасия,Козлова,Светлана,Свиридова".Split(',');
            var distance = "135,191,147,115,112,151,186,166,112,197".Split(',');
            var marks = "15,1,3,9,15,19,14,9,11,4,20,9,1,13,6,5,20,17,9,16,19,8,1,6,17,16,12,5,20,4,5,20,3,19,18,16,12,5,4,15,7,4,19,11,12,14,3,6,17,1".Split(',');
            var part = new Purple_2.Participant[10];
            int ind = 0;
            for (int i = 0; i < 10; i++)
            {
                part[i] = new Purple_2.Participant(names[i * 2], names[i * 2 + 1]);
                var m = new int[5];
                for (int j = 0; j < 5; j++)
                    m[j] = int.Parse(marks[ind++]);
                part[i].Jump(int.Parse(distance[i]), m, 60);
            }

            Purple_2.Participant.Sort(part);
            foreach (var p in part)
            {
                Console.WriteLine(p.Name + " " + p.Surname + " " + p.Result);
            }
        }

        static void Check3()
        {
            var names = "Степан,Свиридов,Савелий,Сидоров,Савелий,Сидоров,Степан,Козлов,Степан,Сидоров,Юлия,Свиридова,Мирослав,Козлов,Степан,Петров,Светлана,Свиридова,Мария,Павлова".Split(',');
            var marks = new double[] { 2.53, 5.13, 5.79, 4.91, 2.98, 3.74, 5.76, 2.49, 0.86, 1.52, 3.06, 1.17, 4.95, 4.89, 1.73, 5.65, 3.29, 0.72, 2.36, 0.88, 4.61, 1.82, 4.69, 4.30, 2.20, 4.33, 5.32, 4.49, 0.32, 3.34, 4.50, 4.94, 0.65, 5.60, 4.12, 3.10, 4.45, 1.87, 4.30, 1.09, 4.72, 3.62, 3.74, 1.02, 1.61, 0.01, 3.34, 4.19, 2.65, 1.49, 3.38, 1.50, 2.32, 3.57, 2.61, 2.34, 1.60, 4.49, 3.39, 1.30, 2.97, 5.18, 1.11, 5.26, 4.97, 2.73, 2.22, 1.69, 1.78, 0.75 };
            int ind = 0;
            var part = new Purple_3.Participant[10];
            for (int i = 0; i < 10; i++)
            {
                part[i] = new Purple_3.Participant(names[i * 2], names[i * 2 + 1]);
                for (int j = 0; j < 7; j++)
                {
                    part[i].Evaluate(marks[ind++]);
                }
            }
            Purple_3.Participant.SetPlaces(part);
            Purple_3.Participant.Sort(part);
            foreach (var p in part)
            {
                Console.WriteLine(p.Name + " " + p.Surname + " " + p.Score);
            }
        }

        static void Check4()
        {
            var names1 = "Полина,Луговая,Савелий,Козлов,Екатерина,Жаркова,Дмитрий,Иванов,Дмитрий,Полевой,Савелий,Петров,Евгения,Распутина,Екатерина,Луговая,Мария,Иванова,Степан,Павлов,Ольга,Павлова,Ольга,Полевая,Дарья,Павлова,Дарья,Свиридова,Евгения,Свиридова".Split(',');
            var names2 = "Анастасия,Жаркова,Александр,Павлов,Степан,Свиридов,Игорь,Сидоров,Евгения,Сидорова,Мария,Сидорова,Лев,Петров,Савелий,Козлов,Егор,Свиридов,Оксана,Жаркова,Светлана,Петрова,Полина,Петрова,Екатерина,Павлова,Юлия,Полевая,Евгения,Павлова".Split(',');
            var times1 = new double[] { 422.64, 142.05, 185.23, 294.32, 79.26, 230.63, 35.16, 376.12, 279.20, 292.38, 467.60, 473.82, 290.14, 368.60, 212.67 };
            var times2 = new double[] { 112.49, 472.11, 213.92, 102.13, 263.21, 350.75, 248.68, 325.28, 300.00, 252.16, 402.20, 397.33, 384.94, 8.09, 480.52 };
            var sp1 = new Purple_4.Sportsman[15];
            for (int i = 0; i < sp1.Length; i++)
            {
                sp1[i] = new Purple_4.Sportsman(names1[i * 2], names1[i * 2 + 1]);
                sp1[i].Run(times1[i]);
            }
            var sp2 = new Purple_4.Sportsman[15];
            for (int i = 0; i < sp2.Length; i++)
            {
                sp2[i] = new Purple_4.Sportsman(names2[i * 2], names2[i * 2 + 1]);
                sp2[i].Run(times2[i]);
            }

            var g1 = new Purple_4.Group("group 1");
            g1.Add(sp1);
            var g2 = new Purple_4.Group("group 2");
            g2.Add(sp2);
            g1.Sort();
            g2.Sort();
            var g = Purple_4.Group.Merge(g1, g2);
            //g.Sort();

            foreach (var s in g.Sportsmen)
                Console.WriteLine(s.Name + " " + s.Surname + " " + s.Time);
        }

        static void Check5()
        {
            var animals = "Макака,Тануки,Тануки,Кошка,Сима_энага,Макака,Панда,Сима_энага,Серау,Панда,Сима_энага,Кошка,Панда,Кошка,Панда,Серау,Панда,Сима_энага,Панда,Кошка".Split(',');
            var characterTraits = "-,Проницательность,Скромность,Внимательность,Дружелюбность,Внимательность,Проницательность,Проницательность,Внимательность,-,Дружелюбность,Внимательность,-,Уважительность,Целеустремленность,Дружелюбность,-,Скромность,Проницательность,Внимательность".Split(',');
            var concepts = "Манга,Манга,Кимоно,Суши,Кимоно,Самурай,Манга,Суши,Сакура,Кимоно,Сакура,Кимоно,Сакура,Фудзияма,Аниме,-,Манга,Фудзияма,Самурай,Сакура".Split(',');
            
            var rep = new Purple_5.Report();
            rep.MakeResearch();
            Console.WriteLine(rep.Researches[0].Name);
            for (int i = 0; i < 20; i++)
            {
                rep.Researches[0].Add(new string[] { animals[i], characterTraits[i], concepts[i] });
            }
            var cor = rep.GetGeneralReport(1);
            foreach (var x in cor) {
                Console.WriteLine(x.Item1 + " " + x.Item2);
            }
        }
    }
}
