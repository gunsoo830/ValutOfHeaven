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

    private List<UnitModel> unitModelList;
    private List<CardModel> cardModelList;

    private PlayerDataManager() { }

    // todo Server ���� ������ �޾Ƽ� �����ϸ� �� ��?
    public void setUnitListByID(List<string> idList)
    {
        for(int i = 0; i < idList.Count; i++)
        {
            UnitModel model = UnitModelContainer.getInstance().findModelByID(idList[i]);
            this.unitModelList.Add(model);
        }
    }

    public void addUnitByID(string id)
    {
        UnitModel model = UnitModelContainer.getInstance().findModelByID(id);
        this.unitModelList.Add(model);
    }
}
