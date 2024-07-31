using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
struct GachaItem
{
    public string name;
    public int per;
}

public class GachaSystem : MonoBehaviour
{
    [SerializeField] List<GachaItem> gachaItems;

    public string Pull()
    {
        int n = FindAnyObjectByType<GameManager>().GetGachaRand() +1;

        int persum = 0;
        foreach (var item in gachaItems)
        {
            persum += item.per;
            if (persum >= n) return item.name;
        }
        return gachaItems[gachaItems.Count - 1].name;
    }
}
