using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyCanvas : MonoBehaviour
{
    public const int PANEL_COUNT = 7;

    // 순서를 지켜야 함.. 이게 맞나
    public enum LobbyBottomButtonType {
        Shop = 1,
        Hire = 2,
        Menu = 3,
        ShipBuild = 4,
        Ship = 5,
        Battle = 6,
        ShipArrange = 7,
        BattleStage = 8,
        StageSpecific = 9,
    }

    public Image imgCurrent;
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
            imgCurrent.gameObject.SetActive(false);

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
        // if(isBottomMenuMove)
        // {
        //     float gap = this.bottomMenuMoveDistance / this.bottomMenuMovePerSec * Time.deltaTime;
        //     Vector3 movePos = this.getRectPosition(this.imgCurrent.gameObject);
        //     movePos.x += gap;
        //     
        //     Vector3 targetPos = this.getRectPosition(this.buttons[this.currBottomMenuIndex].gameObject);
        //     if(this.bottomMenuMoveDistance > 0 && movePos.x > targetPos.x || this.bottomMenuMoveDistance < 0 && movePos.x < targetPos.x)
        //     {
        //         movePos.x = targetPos.x;
        //         this.isBottomMenuMove = false;
        //     }
        //
        //     this.setRectPosition(movePos, this.imgCurrent.gameObject);
        // }
    }

    private IEnumerator StartMoveBottomMenu(int targetIndex)
    {
        Vector3 startPosition = this.imgCurrent.rectTransform.anchoredPosition;
        Vector3 targetPosition = this.buttons[targetIndex].targetGraphic.rectTransform.anchoredPosition;
        float elapsedTime = 0;
        float moveDuration = 0.3f;

        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / moveDuration;
            this.imgCurrent.rectTransform.anchoredPosition = Vector3.Lerp(startPosition, targetPosition, t);
            yield return null;
        }

        this.imgCurrent.rectTransform.anchoredPosition = targetPosition;
        this.currBottomMenuIndex = targetIndex;
    }


    private void onButtonClick(int type)
    {
        this.currBottomMenuIndex = type;

        if(!this.imgCurrent)
            return;   

        // move imgCurrent
        bool isActive = this.imgCurrent.gameObject.activeInHierarchy;

        this.isBottomMenuMove = true;
        this.bottomMenuMoveDistance = -(this.getRectPosition(this.imgCurrent.gameObject).x - this.getRectPosition(this.buttons[type].gameObject).x);
        if (isActive)
        {
            StartCoroutine(StartMoveBottomMenu(type));     
        }
        else
        {
            imgCurrent.rectTransform.anchoredPosition = buttons[type].targetGraphic.rectTransform.anchoredPosition;
            this.imgCurrent.gameObject.SetActive(true);
        }
       


        // set imgCurrent Text.
        Text imgCurrText = this.imgCurrent.GetComponentInChildren<Text>();
        Text buttonText = this.buttons[this.currBottomMenuIndex].transform.GetChild(0).GetComponent<Text>();

        //imgCurrText.text = buttonText.text;
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
        this.panels[(int)LobbyBottomButtonType.ShipBuild].gameObject.SetActive(true);
    }

    public void onShipClick()
    {
        this.panels[(int)LobbyBottomButtonType.ShipArrange].GetComponent<PopShipArrangeController>().setBackButtonEnable(false);

        this.panels[(int)LobbyBottomButtonType.ShipArrange].gameObject.SetActive(true);
    }

    public void onBattleClick()
    {
        this.panels[(int)LobbyBottomButtonType.Battle].gameObject.SetActive(true);

    }

    public void openPopup(LobbyBottomButtonType type)
    {
        this.panels[(int)type].gameObject.SetActive(true);
    }
    public void closePopup(LobbyBottomButtonType type)
    {
        if (!!this.panels[(int)type])
        {
            this.panels[(int)type].SetActive(false);
            this.resetImgCurrent();
        }
    }
    public void closeAllPopup()
    {
        for(int i=0; i<this.panels.Count; i++)
        {
            if (this.panels[i] != null && i > 0)
                this.panels[i].SetActive(false);
        }

        this.resetImgCurrent();
    }
    private void _hideAllPanels()
    {
        for(int i=0; i<this.panels.Count; i++)
        {
            if(i == 0)
                continue;

            this.panels[i].SetActive(false);
        }
    }
    private void resetImgCurrent()
    {
        float xpos = this.buttons[0].transform.position.x;
        this.imgCurrent.transform.position = new Vector3(xpos, this.imgCurrent.transform.position.y);
        this.imgCurrent.gameObject.SetActive(false);
    }

    public List<GameObject> getPanelList()
    {
        return this.panels;
    }
}
