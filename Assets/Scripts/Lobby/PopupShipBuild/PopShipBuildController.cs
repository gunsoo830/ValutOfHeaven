using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopShipBuildController : MonoBehaviour
{

    public GameObject btnBack;
    // Start is called before the first frame update
    void Start()
    {
        if(!!this.btnBack)
        {
            this.btnBack.GetComponent<Button>().onClick.AddListener(() => this.onBtnBackClick());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void onBtnBackClick()
    {
        LobbyCanvas canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<LobbyCanvas>();
        if(!!canvas)
        {
            canvas.closePopup(LobbyCanvas.LobbyBottomButtonType.ShipBuild);
        }
        else
        {
            Debug.LogError("Lobby Canvas is not exist.");
        }
    }
}
