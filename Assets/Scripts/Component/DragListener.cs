using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class DragListener : MonoBehaviour
{
    protected DragObject currDragObject;

    // Start is called before the first frame update
    public virtual void Start()
    {
        this.GetComponent<RectTransform>().localPosition = new Vector3(-Screen.width / 2, -Screen.height / 2);
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }

    
    virtual public void DragBegin()
    {
        this.currDragObject.transform.SetParent(this.transform);
    }
    virtual public void DragUpdate()
    {
        Debug.Log($"Current Mouse Pos : {Input.mousePosition} ");
        this.currDragObject.setPosition(Input.mousePosition);
    }
    virtual public void DragEnd()
    {

    }

    virtual public void setDragTarget(DragObject dragObject)
    {
        this.currDragObject = dragObject;
    }
}
