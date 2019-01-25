using System.Collections.Generic;
using UnityEngine;

public static class Randx
{
    public static void Shuffle<T>(this List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = UnityEngine.Random.Range(0, n + 1);
            var value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    public static Vector3 Position(Bounds b)
    {
        return new Vector3(
            Random.Range(b.min.x, b.max.x),
            Random.Range(b.min.y, b.max.y),
            Random.Range(b.min.z, b.max.z));
    }
}