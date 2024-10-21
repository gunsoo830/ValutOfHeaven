using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using VOHModel;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager instance = null;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize Data Manager.
        PlayerDataManager.getInstance();

        // Test
        PlayerDataManager.getInstance().addUnitByID(UnitDefines.UnitID.JinMu, 0);
        PlayerDataManager.getInstance().addUnitByID(UnitDefines.UnitID.TuyenQuang, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        if (instance != null && this != instance)
        {
            Destroy(this.gameObject);
            return;
        }

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        PlayerDataManager.reset();
    }
}
