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
            //1
            Blue_1.Response[] responses = new Blue_1.Response[3];
            responses[0] = new Blue_1.Response("Ilya");
            responses[1] = new Blue_1.HumanResponse("Ilya", "Malinin");
            responses[2] = new Blue_1.HumanResponse("Ilya", "Ilya");
            Blue_1.HumanResponse human = new Blue_1.HumanResponse("Ilya", "Malinin");
            human.CountVotes(responses);
            human.Print();
            Console.WriteLine();

            // 2
            Blue_2.Participant[] participants = new Blue_2.Participant[50];
            Blue_2.WaterJump3m waterJump3 = new Blue_2.WaterJump3m("name1", 10001);
            Blue_2.WaterJump5m waterJump5 = new Blue_2.WaterJump5m("name2", 1009);
            waterJump3.Add(null);
            waterJump3.Print();
            waterJump3.Add(participants);
            waterJump5.Add(participants);
            waterJump3.Print();
            waterJump5.Print();
            Console.WriteLine();

            // 3
            Blue_3.BasketballPlayer[] basketballPlayers = new Blue_3.BasketballPlayer[6];
            for (int n = 1; n < 6; n++)
            {
                string num = n.ToString();
                basketballPlayers[n] = new Blue_3.BasketballPlayer("basketall", "num");
            }
            Blue_3.BasketballPlayer.Sort(basketballPlayers);
            basketballPlayers[0].PlayMatch(6);
            for (int count = 0; count < 10; count++)
            {
                for (int k = 0; k < 5; k++)
                {
                    int penalty = new Random().Next(6);
                    basketballPlayers[k].PlayMatch(penalty);
                }
            }
            Blue_3.BasketballPlayer.Sort(basketballPlayers);
            for (int k = 0; k < 5; k++)
            {
                basketballPlayers[k].Print();
            }

            Blue_3.HockeyPlayer[] hockeyPlayers = new Blue_3.HockeyPlayer[6];
            for (int n = 0; n < 6; n++)
            {
                string num = n.ToString();
                hockeyPlayers[n] = new Blue_3.HockeyPlayer("hockey", num);
            }
            for (int count = 0; count < 10; count++)
            {
                for (int k = 0; k < 6; k++)
                {
                    int penalty = new Random().Next(11);
                    hockeyPlayers[k].PlayMatch(penalty);
                }
            }
            for (int k = 0; k < 6; k++)
            {
                hockeyPlayers[k].Print();
            }
            Console.WriteLine();

            // 4
            //Blue_4.Group group1 = new Blue_4.Group("group 1");
            //Blue_4.Group group2 = new Blue_4.Group("group 1");
            //Blue_4.Team[] teams1 = new Blue_4.Team[] { new Blue_4.ManTeam("man team 1"), new Blue_4.ManTeam("man team 2"), null };
            //Blue_4.Team[] teams2 = new Blue_4.Team[] { new Blue_4.ManTeam("man team 3"), new Blue_4.ManTeam("man team 4"), null };
            //Blue_4.Team[] teams3 = new Blue_4.Team[] { new Blue_4.WomanTeam("woman team 1"), new Blue_4.WomanTeam("woman team 2"), null };
            //Blue_4.Team[] teams4 = new Blue_4.Team[] { new Blue_4.WomanTeam("woman team 3"), new Blue_4.WomanTeam("woman team 4"), null };
            //for (int i = 0; i < 10; i++)
            //{
            //    for (int k = 0; k < 2; k++)
            //    {
            //        teams1[k].PlayMatch(new Random().Next(4));
            //        teams2[k].PlayMatch(new Random().Next(4));
            //        teams3[k].PlayMatch(new Random().Next(4));
            //        teams4[k].PlayMatch(new Random().Next(4));
            //    }
            //}
            //group1.Add(teams1);
            //group2.Add(teams2);
            //group1.Add(teams3);
            //group2.Add(teams4);
            //group1.Sort();
            //group2.Sort();
            //Blue_4.Group group3 = Blue_4.Group.Merge(group1, group2, 6);
            //group1.Print();
            //group2.Print();
            //group3.Print();
            //Console.WriteLine();

            //5
            //Blue_5.Sportsman[] men1 = new Blue_5.Sportsman[7];
            //Blue_5.Sportsman[] women1 = new Blue_5.Sportsman[7];
            //Blue_5.Sportsman[] men2 = new Blue_5.Sportsman[7];
            //Blue_5.Sportsman[] women2 = new Blue_5.Sportsman[7];
            //Blue_5.Sportsman[] men3 = new Blue_5.Sportsman[7];
            //Blue_5.Sportsman[] women3 = new Blue_5.Sportsman[7];
            //for (int k = 1; k < 6; k++)
            //{
            //    men1[k] = new Blue_5.Sportsman("men_1", k.ToString());
            //    women1[k] = new Blue_5.Sportsman("women_1", k.ToString());
            //    men2[k] = new Blue_5.Sportsman("men_2", k.ToString());
            //    women2[k] = new Blue_5.Sportsman("women_2", k.ToString());
            //    men3[k] = new Blue_5.Sportsman("men_3", k.ToString());
            //    women3[k] = new Blue_5.Sportsman("women_3", k.ToString());
            //}
            //for (int k = 1; k < 6; k++)
            //{
            //    men1[k].SetPlace(18 - 3 * k);
            //    women1[k].SetPlace(18 - 3 * k);
            //    men2[k].SetPlace(18 - (3 * k + 1));
            //    women2[k].SetPlace(18 - (3 * k + 1));
            //    men3[k].SetPlace(18 - (3 * k + 2));
            //    women3[k].SetPlace(18 - (3 * k + 2));
            //}
            //Blue_5.ManTeam[] manTeams = new Blue_5.ManTeam[4];
            //Blue_5.WomanTeam[] womanTeams = new Blue_5.WomanTeam[4];
            //for (int k = 1; k < 4; k++)
            //{
            //    manTeams[k] = new Blue_5.ManTeam("man team " + k.ToString());
            //    womanTeams[k] = new Blue_5.WomanTeam("woman team " + k.ToString());
            //}
            //manTeams[1].Add(men1);
            //womanTeams[1].Add(women1);
            //manTeams[2].Add(men2);
            //womanTeams[2].Add(women2);
            //manTeams[3].Add(men3);
            //womanTeams[3].Add(women3);
            //Blue_5.Team.Sort(manTeams);
            //Blue_5.Team.Sort(womanTeams);
            //for (int k = 0; k < 3; k++)
            //    manTeams[k].Print();
            //Console.Write("man champion - ");
            //Console.WriteLine();
            //Blue_5.Team.GetChampion(manTeams).Print();
            //for (int k = 0; k < 3; k++)
            //    womanTeams[k].Print();
            //Console.Write("woman champion - ");
            //Console.WriteLine();
            //Blue_5.Team.GetChampion(womanTeams).Print();

        }
    }
}
