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

    private List<UnitModel> unitModelList = new List<UnitModel>();
    private List<CardModel> cardModelList = new List<CardModel>();

    private PlayerDataManager() { }

    // todo Server ���� ������ �޾Ƽ� �����ϸ� �� ��?
    public void setUnitListByID(List<string> idList)
    {
        for(int i = 0; i < idList.Count; i++)
        {
            UnitModel model = UnitModelContainer.getInstance().findModelByID(idList[i]);
            if(this.hasModel(model))
                continue;

            this.unitModelList.Add(model);
        }
    }

    public void addUnitByID(string id)
    {
        UnitModel model = UnitModelContainer.getInstance().findModelByID(id);
        if(this.hasModel(model))
            return;

        this.unitModelList.Add(model);
    }

    public bool hasModel(UnitModel model)
    {
        return this.unitModelList.FindIndex((m) => m == model) > -1;
    }

    public List<UnitModel> getPlayerShipList()
    {
        return this.unitModelList;
    }
}
