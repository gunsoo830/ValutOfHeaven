using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static config.SoundConfig;

public class BattleSceneManager : MonoBehaviour
{
    public BattleStartPanel panelBegin;
    public GameObject posBullet;
    public PreBattleDialoguePanel panelDialogue;
    
    private SoundManager soundManager => SoundManager.instance;
    
    
    // Start is called before the first frame update
    void Start()
    {
        if (this.soundManager != null)
            soundManager.playSound(getSoundPath((Int32)BGM_LIST.BATTLE_STAGE), SoundType.Bgm, true);
        this.onEnterGame();
    }



    public void onEnterGame()
    {
        panelDialogue.startBattleDialogue(1, startBeginAnimation);
    }

    private void startBeginAnimation()
    {
        StartCoroutine(this._startBeginAnimation());
    }
    IEnumerator _startBeginAnimation()
    {
        this.panelBegin.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        this.panelBegin.PlayGameBegin();
    }

    public GameObject getPosBullet()
    {
        return this.posBullet;
    }
}
