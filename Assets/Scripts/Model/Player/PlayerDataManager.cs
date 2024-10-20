using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VOHModel;

public class PlayerDataManager
{
    private static PlayerDataManager instance = null;
    public static PlayerDataManager getInstance()
    {
        if(instance == null)
            instance = new PlayerDataManager();

        return instance;
    }
    public static void reset()
    {
        instance = null;
        UnitModelContainer.reset();
        CardModelContainer.reset();
    }

    private List<UnitModel> unitModelList = new List<UnitModel>();
    private List<CardModel> cardModelList = new List<CardModel>();
    private List<int> _shipArrangePosList = new List<int>();

    private PlayerDataManager() { }

    // todo Server ���� ������ �޾Ƽ� �����ϸ� �� ��?
    public void addUnitListByID(List<string> idList)
    {
        for(int i = 0; i < idList.Count; i++)
        {
            UnitModel model = UnitModelContainer.getInstance().findModelByID(idList[i]);
            if(this.hasUnit(model))
                continue;

            this.unitModelList.Add(model);
        }
    }

    public void addUnitByID(string id, int posIndex)
    {
        UnitModel model = UnitModelContainer.getInstance().findModelByID(id);
        if(this.hasUnit(model))
            return;

        this.unitModelList.Add(model);
        this._shipArrangePosList.Add(-1);

        if (posIndex > -1)
            this.setShipArrangePos(id, posIndex, false);
    }

    public bool hasUnit(UnitModel model)
    {
        return this.unitModelList.FindIndex((m) => m == model) > -1;
    }

    public List<UnitModel> getPlayerShipList()
    {
        return this.unitModelList;
    }

    // Ship Position
    public List<int> getShipArrangePosList()
    {
        return this._shipArrangePosList;
    }
    public bool setShipArrangePos(string id, int targetindex, bool isSwap)
    {
        if (targetindex > this._shipArrangePosList.Count - 1)
        {
            Debug.LogError($"PlayerDataManager : {targetindex} is out of range.");
            return false;
        }

        int shipIndex = this._getShipIndex(id);
        int prevIndex = this._shipArrangePosList[shipIndex];
        int stagedShipIndex = -1;
        for (int i = 0; i < this._shipArrangePosList.Count; i++)
        {
            if (this._shipArrangePosList[i] == targetindex)
            {
                stagedShipIndex = i;
                break;
            }
        }

        this._shipArrangePosList[shipIndex] = targetindex;

        if (isSwap)
            this._shipArrangePosList[stagedShipIndex] = prevIndex;

        return true;
    }
    private int _getShipIndex(string id)
    {
        for (int retVal = 0; retVal < this.getPlayerShipList().Count; retVal++)
        {
            if (this.getPlayerShipList()[retVal].getID() == id)
            {
                return retVal;
            }
        }

        return -1;
    }
}
