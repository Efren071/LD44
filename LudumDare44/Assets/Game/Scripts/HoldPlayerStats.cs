using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HoldPlayerStats
{
    private static int killScore = 0;

    public static int Score
    {
        get
        {
            return killScore;
        }

        set
        {
            killScore = value;
        }
    }
}
