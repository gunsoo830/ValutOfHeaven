using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VOHModel
{
    public class UnitModelContainer
    {
        private static UnitModelContainer instance = null;
        public static UnitModelContainer getInstance()
        {
            if(instance == null)
                instance = new UnitModelContainer();

            return instance;
        }

        public const int UnitCount = 38;

        private List<UnitModel> unitModels;

        UnitModelContainer()
        {
            for(int i=0; i<UnitCount; i++)
            {
                UnitModel unit = new UnitModel(i, 8);
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
