using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopGameModeController : MonoBehaviour
{
    public enum GameModeType {
        Campaign = 0,
    }
    public GameObject btnBack;
    public List<Button> btnBannerList;

    private LobbyCanvas lobbyCanvas;

    // Start is called before the first frame update
    void Start()
    {
        this.lobbyCanvas = GameObject.FindAnyObjectByType<LobbyCanvas>();
        this.btnBack.GetComponent<Button>().onClick.AddListener(()=>this.onBtnBackClick());

        this._initBannerButton();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void _initBannerButton()
    {
        this.btnBannerList[(int)GameModeType.Campaign].onClick.AddListener(()=>this.onBtnCampaignClick());
    }
    private void onBtnCampaignClick()
    {
        
    }

    private void onBtnBackClick()
    {
        this.lobbyCanvas.closeAllPopup();
    }
}
