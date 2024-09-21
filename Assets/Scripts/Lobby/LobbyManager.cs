using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    public delegate void onButtonClick();

    public GameObject lobbyCanvas;
    private LobbyCanvas lobbyCanvasController;

    // Start is called before the first frame update
    void Start()
    {
        if(!!lobbyCanvas)
        {
            lobbyCanvasController = lobbyCanvas.GetComponent<LobbyCanvas>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
