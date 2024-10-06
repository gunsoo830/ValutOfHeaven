using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopBattlePageController : MonoBehaviour
{
    public GameObject pnStageSpecific;
    public Button btnBack;
    public List<Button> btnStageList;
    // Start is called before the first frame update
    void Start()
    {
        btnBack.onClick.AddListener(() => this.onBtnBackClick());

        if(btnStageList != null)
        {
            for(int i=0; i<btnStageList.Count; i++)
            {
                int temp = i;
                this.btnStageList[i].onClick.AddListener(()=>this.onBtnStageClick(temp));
            }
        }

        this.initStageStatus(new List<bool> { false , true, true, true });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void onBtnBackClick()
    {
        this.gameObject.SetActive(false);
    }
    private void onBtnStageClick(int index)
    {
        if(!!this.pnStageSpecific)
            this.pnStageSpecific.SetActive(true);
    }

    public void initStageStatus(List<bool> isStageClearList)
    {
        for(int i=0; i<isStageClearList.Count; i++)
        {
            this.btnStageList[i].interactable = !isStageClearList[i];
            for(int j=0; j<this.btnStageList[i].transform.childCount; j++)
            {
                if(isStageClearList[i] == true)
                {
                    if(this.btnStageList[i].transform.GetChild(j).name == "imgDestroyed")
                        this.btnStageList[i].transform.GetChild(j).gameObject.SetActive(true);

                    if(this.btnStageList[i].transform.GetChild(j).name == "imgNormal")
                        this.btnStageList[i].transform.GetChild(j).gameObject.SetActive(false);
                }
                else
                {
                    if(this.btnStageList[i].transform.GetChild(j).name == "imgDestroyed")
                        this.btnStageList[i].transform.GetChild(j).gameObject.SetActive(false);

                    if(this.btnStageList[i].transform.GetChild(j).name == "imgNormal")
                        this.btnStageList[i].transform.GetChild(j).gameObject.SetActive(true);
                }
            }
        }
    }
}
