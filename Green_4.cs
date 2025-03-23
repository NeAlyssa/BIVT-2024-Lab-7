using System;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Xml.Linq;

namespace Lab_7
{
    public class Green_4
    {
        public abstract class Discipline
        {

            private int count1;
            private string name1;
            private Participant[] participants1;




            public int Count => count1;
            public string Name => name1;
            public Participant[] Participants => participants1;


            public Discipline(string name)
            {
                name1 = name;
                participants1 = new Participant[0];
                count1 = 0;
            }
            public void Add(Participant participant)
            {
                if (participants1 == null)
                {
                    participants1 = new Participant[0];
                }

                Array.Resize(ref participants1, count1 + 1);
                participants1[count1] = participant;
                count1++;
            }
            public void Add(Participant[] participants)
            {
                for (int i = 0; i < participants.Length; i++)
                {
                    Add(participants[i]);
                }
            }
            public void Sort()
            {
                if (participants1 == null)
                {
                    return;
                }

                for (int i = 0; i < participants1.Length - 1; i++)
                {
                    for (int j = 0; j < participants1.Length - i - 1; j++)
                    {
                        if (participants1[j].BestJump < participants1[j + 1].BestJump)
                        {
                            Participant temp = participants1[j];
                            participants1[j] = participants1[j + 1];
                            participants1[j + 1] = temp;
                        }
                    }
                }
            }
            private protected Participant GetParticipantAt(int index)
            {
                if (index >= 0 && participants1 != null)
                {


                    return participants1[index];
                }


                return default(Participant);
            }
            private protected void SetParticipant(int index, Participant participant)
            {
                if (participants1 != null && index >= 0)
                {


                    participants1[index] = participant;
                }
            }
            public void Print()
            {
                Console.WriteLine($"Игры: {Name}");


                foreach (var participant in participants1)
                {
                    participant.Print();
                }
            }
            public class LongJump : Discipline
            {
                public LongJump() : base("Long jump") { }

                public override void Retry(int index)
                {
                    Participant participant = GetParticipantAt(index);

                    double BestJump = participant.BestJump;



                    participant = new Participant(participant.Name, participant.Surname);

                    participant.Jump(BestJump);

                    SetParticipant(index, participant);
                }
            }
            public class HighJump : Discipline
            {
                public HighJump() : base("High jump") { }

                public override void Retry(int index)
                {
                    Participant participant = GetParticipantAt(index);

                    double[] jumps = participant.Jumps;
                    Participant new_ = new Participant(participant.Name, participant.Surname);

                    for (int i = 0; i < jumps.Length - 1; i++)
                    {
                        new_.Jump(jumps[i]);
                    }
                    SetParticipant(index, new_);
                }
            }

            public abstract void Retry(int index);

        }

        public struct Participant
        {
            private string name1;
            private string surname1;
            private double[] jumps1;

            public string Name => name1;
            public string Surname => surname1;
            public double[] Jumps
            {
                get
                {
                    if (jumps1 == null)
                    {
                        return null;
                    }
                    double[] arrays = new double[jumps1.Length];
                    Array.Copy(jumps1, arrays, jumps1.Length);
                    return arrays;
                }
            }

            public double BestJump
            {
                get
                {
                    if (jumps1 == null || jumps1.Length == 0) return 0;

                    return jumps1.Max();
                }
            }

            public Participant(string name, string surname)
            {
                name1 = name;
                surname1 = surname;
                jumps1 = new double[3] { 0, 0, 0 };
            }

            public void Jump(double result)
            {
                if (jumps1 == null || jumps1.Length == 0)
                {
                    return;
                }

                for (int i = 0; i < jumps1.Length; i++)
                {
                    if (jumps1[i] == 0)
                    {
                        jumps1[i] = result;
                        return;
                    }
                }
            }

            public static void Sort(Participant[] array)
            {
                if (array == null || array.Length == 0)
                {
                    return;
                }

                int n = array.Length;
                for (int i = 0; i < n - 1; i++)
                {
                    for (int j = 0; j < n - i - 1; j++)
                    {
                        if (array[j].BestJump < array[j + 1].BestJump)
                        {

                            Participant temp = array[j];
                            array[j] = array[j + 1];
                            array[j + 1] = temp;
                        }
                    }
                }
            }

            public void Print()
            {
                Console.WriteLine($"Спортсмен: {Name} {Surname}");
                Console.WriteLine($"все прыжки участника: {string.Join(", ", Jumps)}");
                Console.WriteLine($"самый лучший прыжок: {BestJump:F2}");
            }
        }
    }
}
