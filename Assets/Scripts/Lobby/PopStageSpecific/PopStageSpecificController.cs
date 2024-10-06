using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopStageSpecificController : MonoBehaviour
{
    public Button btnBack;
    private LobbyCanvas _lobbyCanvas;

    // Start is called before the first frame update
    void Start()
    {
        this._lobbyCanvas = GameObject.FindAnyObjectByType<LobbyCanvas>();
        this.btnBack.onClick.AddListener(() => this.onBtnBackClick());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void onBtnBackClick()
    {
        this.gameObject.SetActive(false);
    }

    //todo 나중에 데이터에서 스테이지 정보 받아와서 버튼들 세팅하자...
}
