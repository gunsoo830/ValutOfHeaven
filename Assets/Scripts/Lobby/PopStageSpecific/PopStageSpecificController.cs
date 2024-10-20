using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopStageSpecificController : MonoBehaviour
{
    public Button btnBack;
    public Button btnShipArrange;

    private LobbyCanvas _lobbyCanvas;

    // Start is called before the first frame update
    void Start()
    {
        this._lobbyCanvas = GameObject.FindAnyObjectByType<LobbyCanvas>();
        this.btnBack.onClick.AddListener(() => this.onBtnBackClick());

        this.btnShipArrange.onClick.AddListener(()=>this.onBtnShipArrangeClick());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void onBtnBackClick()
    {
        this.gameObject.SetActive(false);
    }

    public void OnBtnStageClick()
    {
        GameObject.FindAnyObjectByType<VOHSceneManager>().ChangeSceneByName("BattleScene");
    }
    private void onBtnShipArrangeClick()
    {
        var pop = this._lobbyCanvas.openPopup(LobbyCanvas.LobbyBottomButtonType.ShipArrange);
        pop.GetComponent<PopShipArrangeController>().setHomeButtonEnable(false);
    }

    //todo ���߿� �����Ϳ��� �������� ���� �޾ƿͼ� ��ư�� ��������...
}
