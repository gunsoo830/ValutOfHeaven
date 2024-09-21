using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class LobbyCanvas : MonoBehaviour
{
    // 순서를 지켜야 함.. 이게 맞나
    public enum LobbyBottomButtonType {
        Shop = 0,
        Hire,
        Menu,
        ShipBuild,
        Ship,
        Battle,
    }

    public GameObject imgCurrent;
    public List<GameObject> panels;
    public List<Button> buttons;
    public float bottomMenuMovePerSec = 1;

    private int currBottomMenuIndex = -1;
    private bool isBottomMenuMove = false;
    private float bottomMenuMoveDistance = 0;


    // Start is called before the first frame update
    void Start()
    {
        if(!!imgCurrent)
            imgCurrent.SetActive(false);

        if(buttons.Count > 0)
        {
            for(int i=0; i<buttons.Count; i++)
            {
                // how can i bind index?
                int test = i;
                buttons[i].onClick.AddListener(() => this.onButtonClick(test));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isBottomMenuMove)
        {
            float gap = this.bottomMenuMoveDistance / this.bottomMenuMovePerSec * Time.deltaTime;
            Vector3 movePos = this.getRectPosition(this.imgCurrent.gameObject);
            movePos.x += gap;
            
            Vector3 targetPos = this.getRectPosition(this.buttons[this.currBottomMenuIndex].gameObject);
            if(this.bottomMenuMoveDistance > 0 && movePos.x > targetPos.x || this.bottomMenuMoveDistance < 0 && movePos.x < targetPos.x)
            {
                movePos.x = targetPos.x;
                this.isBottomMenuMove = false;
            }

            this.setRectPosition(movePos, this.imgCurrent.gameObject);
        }
    }

    private void onButtonClick(int type)
    {
        this.currBottomMenuIndex = type;

        if(!this.imgCurrent)
            return;   

        // move imgCurrent
        bool isActive = this.imgCurrent.activeInHierarchy;
        if(!isActive)
            this.imgCurrent.SetActive(true);

        this.isBottomMenuMove = true;
        this.bottomMenuMoveDistance = -(this.getRectPosition(this.imgCurrent.gameObject).x - this.getRectPosition(this.buttons[type].gameObject).x);

        // set imgCurrent Text.
        Text imgCurrText = this.imgCurrent.GetComponentInChildren<Text>();
        Text buttonText = this.buttons[this.currBottomMenuIndex].transform.GetChild(0).GetComponent<Text>();

        imgCurrText.text = buttonText.text;
    }
    private Vector3 getRectPosition(GameObject gameObject)
    {
        Vector3 retVal = Vector3.zero;
        try {
            retVal = gameObject.gameObject.GetComponent<RectTransform>().localPosition; 
        } 
        catch (Exception err) 
        {
            Debug.LogError("GameObject has no rect transform.");
        }

        return retVal;
    }
    private void setRectPosition(Vector3 pos, GameObject gameObject)
    {
        try {
            gameObject.GetComponent<RectTransform>().localPosition = pos;
        } catch (Exception err)
        {
            Debug.LogError("GameObject has no Rect Transform.");
        }
    }

    public void onShopClick()
    {

    }

    public void onHireClick()
    {

    }

    public void onMenuClick()
    {

    }

    public void onShipBuildClick()
    {

    }

    public void onShipClick()
    {

    }

    public void onBattleClick()
    {

    }

    public List<GameObject> getPanelList()
    {
        return this.panels;
    }
}
