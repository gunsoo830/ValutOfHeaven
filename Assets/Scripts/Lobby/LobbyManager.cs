using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VOHUtil;

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

        List<List<string>> test = ExcelParser.getInstance().ParseExcel(
            @"/Datasheet/CardData.xlsx",
            0,
            1,
            5,
            0,
            7
        );
        Debug.Log(ExcelParser.getDataWithType<string>(ref test, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
