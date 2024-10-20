using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopShipArrangeController : MonoBehaviour
{
    public Button btnBack;
    public Button btnHome;
    private LobbyCanvas lobbyCanvas;
    private PlayerManager _playerMgr;
    private List<GameObject> _placeHolders;

    // Start is called before the first frame update
    void Start()
    {
        if(!!btnBack)
        {
            btnBack.onClick.AddListener(() => this.onBtnBackClick());
        }

        if(!!btnHome)
        {
            btnHome.onClick.AddListener(() => this.onBtnHomeClick());
        }

        lobbyCanvas = GameObject.FindAnyObjectByType<LobbyCanvas>();
        this._playerMgr = GameObject.FindAnyObjectByType<PlayerManager>();
        this._placeHolders = GameObject.FindObjectsByType<PopShipArrangePlaceHolder(FindObjectsSortMode.None) as GameObject[];

        this._initShipList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // init
    private void _initShipList()
    {
        var shipPosList = this._playerMgr.getShipPosIndexList();
        var shipList = this._playerMgr.getPlayerShipList();
        for(int i=0; i<shipList.Count; i++)
        {
            
        }
    }

    private void onBtnBackClick()
    {
        this.gameObject.SetActive(false);
    }
    private void onBtnHomeClick()
    {
        if(!!lobbyCanvas)
        {
            lobbyCanvas.closeAllPopup();
        }
    }

    public void setHomeButtonEnable(bool isenable)
    {
        this.btnHome.gameObject.SetActive(isenable);
    }
}
