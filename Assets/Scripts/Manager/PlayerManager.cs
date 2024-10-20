using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using VOHModel;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager instance = null;

    private List<int> _shipArrangePosList = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        // Initialize Data Manager.
        PlayerDataManager.getInstance();
        var idlist = new List<string>();
        idlist.Add("Unit_nomal_ad_JinMu");
        idlist.Add("Unit_nomal_ad_Drake");
        idlist.Add("Unit_nomal_ad_Gaiseric");
        idlist.Add("Unit_nomal_ad_London");
        idlist.Add("Unit_nomal_ad_TuyenQuang");
        PlayerDataManager.getInstance().setUnitListByID(idlist);
        this.increaseShipArrangeCapacity(idlist.Count);
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


    // Ship
    public List<UnitModel> getPlayerShipList()
    {
        return PlayerDataManager.getInstance().getPlayerShipList();
    }

    public bool setShipArrangePos(string id, int targetindex, bool isSwap) 
    {
        if(targetindex > this._shipArrangePosList.Count - 1)
        {
            Debug.LogError($"ShipArrangePopup : {targetindex} is out of range.");
            return false;
        }

        int shipIndex = this._getShipIndex(id);
        int prevIndex = this._shipArrangePosList[shipIndex];
        int stagedShipIndex = -1;
        for(int i=0; i<this._shipArrangePosList.Count; i++)
        {
            if(this._shipArrangePosList[i] == targetindex)
            {
                stagedShipIndex = i;
                break;
            }
        }

        this._shipArrangePosList[shipIndex] = targetindex;

        if(isSwap)
            this._shipArrangePosList[stagedShipIndex] = prevIndex;

        return true;
    }

    public void increaseShipArrangeCapacity(int capacity)
    {
        for(int i=0; i<capacity; i++)
        {
            this._shipArrangePosList.Add(-1);
        }
    }

    private int _getShipIndex(string id)
    {   
        for(int retVal=0; retVal<PlayerDataManager.getInstance().getPlayerShipList().Count; retVal++)
        {
            if(PlayerDataManager.getInstance().getPlayerShipList()[retVal].getID() == id)
            {
                return retVal;
            }
        }

        return -1;
    }
    public List<int> getShipPosIndexList()
    {
        return this._shipArrangePosList;
    }
}
