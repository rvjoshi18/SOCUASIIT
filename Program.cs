using System;
using System.Collections.Generic;

namespace soc_iit_uas_v2
{
    class globals
    {
        public static int NUM_UAS = 32;
    }

    class Mechanism
    {
        public bool? a, b, c;
        public int tracked_blue_uas_id = -1;
        public int cluster_id = -1;
        public int cluster_center = -1;

        public Mechanism(bool? pa = false, bool? pb = false, bool? pc = false)
        {
            a = pa;
            b = pb;
            c = pc;
        }

        public Mechanism next()
        {
            Mechanism new_mechanism = new Mechanism();
            new_mechanism.a = b.Value || c.Value;
            new_mechanism.b = a.Value && c.Value;
            new_mechanism.c = a.Value ^ b.Value;

            return new_mechanism;
        }

        //public static Box operator +(Box b, Box c)
        public static bool operator ==
                                 (Mechanism lhs, Mechanism rhs)
        {
            bool aequal;
            if (lhs.a == null || rhs.a == null || lhs.a == rhs.a)
            {
                aequal = true;
            }
            else
            {
                aequal = false;
            }

            bool bequal;
            if (lhs.b == null || rhs.b == null || lhs.b == rhs.b)
            {
                bequal = true;
            }
            else
            {
                bequal = false;
            }

            bool cequal;
            if (lhs.c == null || rhs.c == null || lhs.c == rhs.c)
            {
                cequal = true;
            }
            else
            {
                cequal = false;
            }

            return (aequal && bequal && cequal);
        }

        public static bool operator !=
                                 (Mechanism lhs, Mechanism rhs)
        {
            return !(lhs == rhs);
        }
    }

    class Truth_Table
    {
        public Mechanism[] past_whole = new Mechanism[8];
        public Mechanism[] past_p1  = new Mechanism[8];
        public Mechanism[] past_p2  = new Mechanism[8];
        public Mechanism[] present = new Mechanism[8];
        public Mechanism[] future = new Mechanism[8];

        public Truth_Table()
        {
            for (int i = 0; i < 8; i++)
            {
                past_p1[i].a = present[i].a = future[i].a
                    = (i / 8 == 1 ? true : false);
                past_p1[i].b = present[i].b = future[i].b
                    = (i % 4 / 10 == 1 ? true : false);
                past_p1[i].c = present[i].c = future[i].c
                    = (i % 2 == 1 ? true : false);
            }
        }
    }

    class CauseEffect
    {
        public Mechanism cur_state = new Mechanism(null, false, false);
        public List<Mechanism> FindCauseSet(Mechanism state)
        {
            Truth_Table tt = new Truth_Table();
            List<Mechanism> cause_list = new List<Mechanism>();

            for (int i = 0; i < 8; i++)
            {
                if (tt.past_p1[i].next() == cur_state)
                {
                    cause_list.Add(tt.past_p1[i].next());
                }
            }
            return cause_list;
        }
    }

    class Battlefield_Gods_Eye_View
    {

        Mechanism[] Reds = new Mechanism[globals.NUM_UAS];
        Mechanism[] Blues = new Mechanism[globals.NUM_UAS];


        //Red Track Inserts
        public void initRedTracks(int[,] red_tracks)
        {
            for (int i = 0; i < red_tracks.Length; i++)
            {
                Reds[red_tracks[i, 0]].tracked_blue_uas_id = red_tracks[i, 1];
            }
        }

        public void insertSingleRedTrack(int red_track_id, int blue_track_id)
        {
            Reds[red_track_id].tracked_blue_uas_id = blue_track_id;
        }

        public void insertMultipleRedTracks(int[,] red_tracks)
        {
            for (int i = 0; i < red_tracks.Length; i++)
            {
                Reds[red_tracks[i, 0]].tracked_blue_uas_id = red_tracks[i, 1];
            }
        }

        //Red Track Deletes
        public void clearRedTracks(int[,] red_tracks)
        {
            for (int i = 0; i < red_tracks.Length; i++)
            {
                Reds[i].tracked_blue_uas_id = -1;
            }
        }

        public void deleteSingleRedTrack(int red_track_id)
        {
            Reds[red_track_id].tracked_blue_uas_id = -1;
        }


        public void deleteMultipleRedTracks(int[] red_tracks)
        {
            for (int i = 0; i < red_tracks.Length; i++)
            {
                Reds[i].tracked_blue_uas_id = -1;
            }
        }


        //Red Track Prints
        public void printRedTracks()
        {
            int index = 0;

            for (int i = 0; i < globals.NUM_UAS; i++)
            {
                if (Reds[i].tracked_blue_uas_id >= 0)
                {
                    Console.WriteLine(i + Reds[i].tracked_blue_uas_id);
                }
            }

            index = 0;
            while ( index < globals.NUM_UAS )
            {
                //not in any cluster
                while (index < globals.NUM_UAS && Blues[index].tracked_blue_uas_id == -1)
                    index++;

                //found one cluster
                int cluster_id = Blues[index].cluster_id;

                //print the cluster
                Console.Write("Cluster " + cluster_id + ": ");
                while (index < globals.NUM_UAS &&
                       Blues[index].cluster_id == cluster_id)
                {
                    Console.Write(index + ", ");
                    index++;
                }

            }
        }



    }
}



 
    






    class Program
{
    static int[] uas_sensors = new int[9];
    static int init_uas_sensors()
    {
        return 0;
    }
    static int update_uas_sensors()
    {
        return 0;
    }

    static void Main(string[] args)
    {
        Console.WriteLine("Hello World!");
    }
}

