using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataHolder
{

    private static int kills, deaths, assists, points;
    private static Transform pos;
    public static Transform Pos
    {
        get
        {
            return pos;
        }
        set
        {
            pos = value;
        }
    }
    public static int Kills
        {
            get
            {
                return kills;
            }
            set
            {
                kills = value;
            }
        }

        public static int Deaths
        {
            get
            {
                return deaths;
            }
            set
            {
                deaths = value;
            }
        }

        public static int Assists
        {
            get
            {
                return assists;
            }
            set
            {
                assists = value;
            }
        }

        public static int Points
        {
            get
            {
                return points;
            }
            set
            {
                points = value;
            }
        }

}
