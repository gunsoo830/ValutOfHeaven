using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.IK;

public class PopupShipDragListener : DragListener
{
    public List<PopShipArrangePlaceHolder> placeHolders;
    private int currHolderIndex = -1;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        for(int i=0; i<placeHolders.Count; i++)
        {
            this.placeHolders[i].setHolderIndex(i);
        }
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void DragBegin()
    {
        for(int i=0; i<this.placeHolders.Count; i++)
        {
            if(this.placeHolders[i].transform == this.currDragObject.transform.parent.transform)
            {
                this.currHolderIndex = i;
                break;
            }
        }
        this.placeHolders[this.currHolderIndex].playDragBegin();

        base.DragBegin();
    }

    public override void DragUpdate()
    {
        base.DragUpdate();
        for(int i=0; i<this.placeHolders.Count; i++)
        {
            if(this.placeHolders[i].IsDragTarget())
                continue;

            if(this.placeHolders[i].checkDragHit(Input.mousePosition) == true)
            {
                this.placeHolders[i].playDragOver();
                continue;
            }

            if(this.placeHolders[i].checkDragHit(Input.mousePosition) == false){
                this.placeHolders[i].playDragNormal();
                continue;
            }
        }
    }

    public override void DragEnd()
    {
        base.DragEnd();

        int hitIndex = -1;
        Vector3 targetpos = this.currDragObject.GetComponent<RectTransform>().localPosition;
        for(int i=0; i<this.placeHolders.Count; i++)
        {
            if(i != this.currHolderIndex && this.placeHolders[i].checkDragHit(targetpos)) {
                hitIndex = i;
                break;
            }
        }

        for(int i=0; i<this.placeHolders.Count; i++)
        {
            this.placeHolders[i].playDragNormal();
        }


        if(hitIndex > -1)
        {
            this.moveShip();
        }
        else
        {
            this.resetShip();
        }
    }

    private void moveShip()
    {

    }
    private void resetShip()
    {
        this.currDragObject.transform.SetParent(this.placeHolders[this.currHolderIndex].transform);
        this.currDragObject.GetComponent<RectTransform>().localPosition = Vector3.zero;
    }


}
