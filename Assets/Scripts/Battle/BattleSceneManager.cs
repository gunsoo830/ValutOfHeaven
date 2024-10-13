using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSceneManager : MonoBehaviour
{
    public BattleStartPanel panelBegin;
    // Start is called before the first frame update
    void Start()
    {
        this.onEnterGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onEnterGame()
    {
        StartCoroutine(this._startBeginAnimation());
    }

    IEnumerator _startBeginAnimation()
    {
        this.panelBegin.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        this.panelBegin.PlayGameBegin();
    }
}
