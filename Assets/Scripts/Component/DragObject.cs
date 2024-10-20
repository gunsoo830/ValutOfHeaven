using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragObject : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public GameObject dragLayer = null;
    protected DragListener dragListener;

    // Start is called before the first frame update
    public virtual void Start()
    {
        this.dragListener = this.dragLayer.GetComponent<DragListener>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }


    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        this.onDragStart();
    }
    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        this.onDragUpdate();
    }
    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        this.onDragEnd();
    }

    protected virtual void onDragStart()
    {
        this.dragListener.setDragTarget(this);
        this.dragListener.DragBegin();
    }
    protected virtual void onDragUpdate()
    {
        this.dragListener.DragUpdate();
    }
    protected virtual void onDragEnd()
    {
        this.dragListener.DragEnd();
    }

    public void setPosition(Vector3 pos)
    {
        float gap = ( ( (float)1920 / Screen.width ) - 1 ) / 2;
        this.GetComponent<RectTransform>().localPosition = new Vector3(pos.x * (1 + gap), pos.y * (1 + gap));
    }
}
