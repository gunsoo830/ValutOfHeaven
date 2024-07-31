using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sRandom
{
    private int items_size;
    private List<int> items;

    public sRandom(int n)
    {
        Init(n);
        items_size = n;
    }

    private void Init(int n)
    {
        items = new List<int>(n);
        for (int i = 0; i < n; i++)
            items.Add(i);
    }

    public int GetRandom()
    {
        if (items.Count == 0)
            Init(items_size);

        int idx = Random.Range(0, items.Count);
        int n = items[idx];
        items.RemoveAt(idx);
        return n;
    }
}
