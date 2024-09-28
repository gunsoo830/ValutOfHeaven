using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopShipArrangeController : MonoBehaviour
{
    public Button btnBack;
    public Button btnHome;
    private LobbyCanvas lobbyCanvas;

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
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
