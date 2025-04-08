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
            Check5();
            Console.WriteLine();
        }
        static void Check5()
        {
            var animals = "Макака,Тануки,Тануки,Кошка,Сима_энага,Макака,Панда,Сима_энага,Серау,Панда,Сима_энага,Кошка,Панда,Кошка,Панда,Серау,Панда,Сима_энага,Панда,Кошка".Split(',');
            var characterTraits = "-,Проницательность,Скромность,Внимательность,Дружелюбность,Внимательность,Проницательность,Проницательность,Внимательность,-,Дружелюбность,Внимательность,-,Уважительность,Целеустремленность,Дружелюбность,-,Скромность,Проницательность,Внимательность".Split(',');
            var concepts = "Манга,Манга,Кимоно,Суши,Кимоно,Самурай,Манга,Суши,Сакура,Кимоно,Сакура,Кимоно,Сакура,Фудзияма,Аниме,-,Манга,Фудзияма,Самурай,Сакура".Split(',');
            var research = new Purple_5.Research("Res 1");
            for (int i = 0; i < 20; i++)
            {
                research.Add(new string[] { animals[i], characterTraits[i], concepts[i] });
            }
            Console.WriteLine("Animal:");
            foreach (var s in research.GetTopResponses(1))
                Console.WriteLine(s);
            Console.WriteLine("CharacterTrait:");
            foreach (var s in research.GetTopResponses(2))
                Console.WriteLine(s);
            Console.WriteLine("Concept:");
            foreach (var s in research.GetTopResponses(3))
                Console.WriteLine(s);
        }
    }
}
