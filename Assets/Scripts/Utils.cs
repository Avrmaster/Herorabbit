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

    public static string ToFixedLengthString(this int cur, int digitsCount = 4)
    {
        var res = "" + cur;
        while (res.Length < digitsCount)
            res = "0" + res;
        return res;
    }

    public static string ToProportion(this int cur, int maxCount)
    {
        return string.Format("{0}/{1}", cur, maxCount);
    }
}