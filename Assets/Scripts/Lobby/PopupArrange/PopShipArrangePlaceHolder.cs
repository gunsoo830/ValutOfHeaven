using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.U2D.IK;
using UnityEngine.UIElements;

public class PopShipArrangePlaceHolder : MonoBehaviour
{
    private int holderIndex = -1;
    private bool isDragTarget = false;
    private GameObject imgHexa = null;
    // Start is called before the first frame update
    void Start()
    {
        for(int i=0; i<this.transform.childCount; i++)
        {
            if(this.transform.GetChild(i).name == "imgHexagon")
            {
                this.imgHexa = this.transform.GetChild(i).gameObject;
            }
        }
    }

    public void playDragBegin()
    {
        this.imgHexa.GetComponent<UnityEngine.UI.Image>().color = new Color(0.7f, 0.7f, 1f, 1f);
    }
    public void playDragOver()
    {
        this.imgHexa.GetComponent<UnityEngine.UI.Image>().color = Color.green;
    }
    public void playDragNormal()
    {
        this.imgHexa.GetComponent<UnityEngine.UI.Image>().color = Color.white;
    }

    public bool checkDragHit(Vector3 point)
    {
        RectTransform holderTrans = this.GetComponent<RectTransform>();
        RectTransform hexaTrans = this.imgHexa.GetComponent<RectTransform>();
        Vector2 size = hexaTrans.sizeDelta;
        Vector3 centerPos = new Vector3(1920/2 - (size.x / 2),1080/2 - (size.y / 2)) + holderTrans.localPosition + hexaTrans.localPosition;

        Rect rect = new Rect(centerPos, size);
        return rect.Contains(point);
    }

    public void setIsDragTarget(bool isDragTarget)
    {
        this.isDragTarget = isDragTarget;
    }
    public bool IsDragTarget()
    {
        return this.isDragTarget;
    }

    public void setHolderIndex(int index)
    {
        this.holderIndex = index;
    }
}
