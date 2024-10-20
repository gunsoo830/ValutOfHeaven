using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PopShipArrangeController : MonoBehaviour
{
    public Button btnBack;
    public Button btnHome;
    private LobbyCanvas lobbyCanvas;
    private PlayerManager _playerMgr;
    private List<PopShipArrangePlaceHolder> _placeHolders = new List<PopShipArrangePlaceHolder>();

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
        this._placeHolders = GameObject.FindObjectsByType<PopShipArrangePlaceHolder>(FindObjectsSortMode.None).ToList();
        this._placeHolders.Sort((a,b) => a.name.CompareTo(b.name));

        this._initShipList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // init
    private void _initShipList()
    {
        var shipPosList = PlayerDataManager.getInstance().getShipArrangePosList();
        var shipList = PlayerDataManager.getInstance().getPlayerShipList();
        for(int i=0; i<shipPosList.Count; i++)
        {
            if (shipPosList[i] > -1)
            {
                StartCoroutine(this._loadShipAsync(shipList[i].getAssetPath(), i));
            }
        }
    }

    // Ship
    private IEnumerator _loadShipAsync(string assetPath, int index)
    {
        var req = Resources.LoadAsync<GameObject>(assetPath);
        while(!req.isDone)
        {
            yield return null;
        }

        GameObject result = Instantiate<GameObject>(req.asset as GameObject);
        result.GetComponent<PopupShipDragObject>().dragLayer = GameObject.FindAnyObjectByType<PopupShipDragListener>().gameObject;
        result.transform.SetParent(this._placeHolders[index].transform);
        result.transform.localScale = Vector3.one;
        result.GetComponent<RectTransform>().localPosition = Vector3.zero;
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
