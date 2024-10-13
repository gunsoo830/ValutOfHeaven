using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : SingleToneBehaviour<PlayerManager> 
{
    //private static PlayerManager instance = null;


    // Start is called before the first frame update
    void Start()
    {
        // Initialize Data Manager.
        PlayerDataManager.getInstance();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void Awake()
    {
        base.Awake();
        
    }
}
