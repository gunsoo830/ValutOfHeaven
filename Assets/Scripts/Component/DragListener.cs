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
        this.GetComponent<RectTransform>().localPosition = new Vector2(-1920/2, -1080/2);
        this.GetComponent<RectTransform>().sizeDelta = new Vector3(Screen.width, Screen.height);
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
        this.currDragObject.setPosition(this._getMousePosition());
    }
    virtual public void DragEnd()
    {

    }

    virtual public void setDragTarget(DragObject dragObject)
    {
        this.currDragObject = dragObject;
    }

    protected Vector2 _getMousePosition()
    {
        return new Vector2(Input.mousePosition.x / Screen.width * 1920, Input.mousePosition.y / Screen.height * 1080);
    }
}
