using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VOHUtil;
using static config.SoundConfig;

public class LobbyManager : MonoBehaviour
{


    private SoundManager soundManager => SoundManager.instance;
    

    // Start is called before the first frame update
    void Start()
    {
        if (this.soundManager != null)
        {
            this.soundManager.playSound(getSoundPath((Int32)BGM_LIST.LOBBY), SoundType.Bgm, true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

}
