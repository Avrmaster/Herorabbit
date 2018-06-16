using UnityEngine;

public static class Utils
{
    public static float Lerp(this float cur, float goal, float percentage)
    {
        if (percentage > 1)
            percentage = 1;
        if (percentage < 0)
            percentage = 0;
        return cur * (1 - percentage) + goal * percentage;
    }
}