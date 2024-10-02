using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VOHUtil;

public class LobbyManager : MonoBehaviour
{
    public enum BGM_LIST
    {
        LOBBY = 0,
        BATTLE_STAGE,
        FREET,
        COUNT,
    }

    private SoundManager soundManager;
    private string[] bgmSourcePath = null;

    // Start is called before the first frame update
    void Start()
    {
        this._initBgmSourcePath();

        this.soundManager = GameObject.FindAnyObjectByType<SoundManager>();
        if (this.soundManager != null)
            soundManager.playSound(this.bgmSourcePath[(int)BGM_LIST.LOBBY], SoundManager.SoundType.Bgm, true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void _initBgmSourcePath()
    {
        this.bgmSourcePath = new string[(int)BGM_LIST.COUNT];

        this.bgmSourcePath[(int)BGM_LIST.LOBBY] = "Sound/Bgm/Bgm_Lobby";
        this.bgmSourcePath[(int)BGM_LIST.BATTLE_STAGE] = "Sound/Bgm/Bgm_BattleStage";
        this.bgmSourcePath[(int)BGM_LIST.FREET] = "Sound/Bgm/Bgm_Freet";
    }
}
