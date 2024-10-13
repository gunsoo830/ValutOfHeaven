using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitSceneController : MonoBehaviour
{
    private VOHSceneManager sceneManager;
    // Start is called before the first frame update
    void Start()
    {
        this.sceneManager = GameObject.FindAnyObjectByType<VOHSceneManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onBtnGameStartClick()
    {
        this.sceneManager.ChangeSceneByName("LobbyScene");
    }
}