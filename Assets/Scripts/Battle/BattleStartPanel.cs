using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStartPanel : MonoBehaviour
{
    private Animator _anim;
    private GrayLayer _grayLayer;
    // Start is called before the first frame update
    void Start()
    {
        this._anim = GetComponent<Animator>();
        this._grayLayer = transform.Find("grayLayer").GetComponent<GrayLayer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnAnimFinish(string evtName)
    {
        StartCoroutine(this._onBeginFinish());
    }
    private IEnumerator _onBeginFinish()
    {
        yield return new WaitForSeconds(1);
        this.gameObject.SetActive(false);
        GameObject.FindAnyObjectByType<BattleManager>().PlayBattle();
    }

    public void PlayGameBegin()
    {
        this._grayLayer.gameObject.SetActive(true);
        this._grayLayer.StartFadeIn(() =>
        {
            this._anim.SetBool("isBegin", true);
        });
    }
}
