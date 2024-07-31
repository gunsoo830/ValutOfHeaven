using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GachaUI : MonoBehaviour
{
    public Text result;
    public Text deb;

     int pulls;
     int pull_5;

    public void Pull_1()
    {
        string tex = FindObjectOfType<GachaSystem>().Pull();


        pulls++;
        if (tex == "5¼º ¿µ¿õ È¹µæ!") pull_5++;

        result.text = tex;
        deb.text = string.Format("PULL : {0}, 5¼º : {1}", pulls, pull_5);
    }
}
