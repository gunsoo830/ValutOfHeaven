using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace VOHModel
{
    public static class UnitDefines
    {
        public static class UnitID
        {
            public const string JinMu = "Unit_nomal_ad_JinMu";
            public const string Gaiseric = "Unit_nomal_ad_Gaiseric";
            public const string Drake = "Unit_nomal_ad_Drake";
            public const string London = "Unit_nomal_ad_London";
            public const string TuyenQuang = "Unit_nomal_ad_TuyenQuang";
        }
    }

    public class UnitModelContainer
    {
        private static UnitModelContainer instance = null;
        public static UnitModelContainer getInstance()
        {
            if(instance == null)
                instance = new UnitModelContainer();

            return instance;
        }
        public static void reset()
        {
            instance = null;
        }

        public const int UnitCount = 5;
        private List<UnitModel> unitModels = new List<UnitModel>();


        UnitModelContainer()
        {
            for(int i=0; i<UnitCount; i++)
            {
                UnitModel unit = new UnitModel(i + 1, 10);
                unitModels.Add(unit);
            }
        }

        public UnitModel findModelByID(string id)
        {
            UnitModel retVal = null;
            for(int i=0; i<this.unitModels.Count; i++)
            {
                if (this.unitModels[i].getID() == id)
                {
                    retVal = this.unitModels[i];
                    break;
                }
            }

            return retVal;
        }

        public UnitModel findModelByRow(int row)
        {
            UnitModel retVal = null;
            for(int i=0; i<this.unitModels.Count; i++)
            {
                if (this.unitModels[i].getRow() == row)
                {
                    retVal = this.unitModels[i];
                    break;
                }
            }

            return retVal;
        }
    }
}
