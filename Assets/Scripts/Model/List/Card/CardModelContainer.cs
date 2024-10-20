using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VOHModel
{
    public class CardModelContainer
    {
        private static CardModelContainer instance = null;
        public static CardModelContainer getInstance()
        {
            if (instance == null)
                instance = new CardModelContainer();

            return instance;
        }
        public static void reset()
        {
            instance = null;
        }

        public const int UnitCount = 38;

        private List<CardModel> unitModels;

        CardModelContainer()
        {
            for (int i = 0; i < UnitCount; i++)
            {
                CardModel unit = new CardModel(i, 8);
                unitModels.Add(unit);
            }
        }

        public CardModel findModelByUnitID(string id)
        {
            for (int i = 0; i < this.unitModels.Count; i++)
            {
                if (this.unitModels[i].getUnitID() == id)
                {
                    return this.unitModels[i];
                }
            }

            return null;
        }

        public CardModel findModelByID(string id)
        {
            for (int i = 0; i < this.unitModels.Count; i++)
            {
                if (this.unitModels[i].getID() == id)
                {
                    return this.unitModels[i];
                }
            }

            return null;
        }

        public CardModel findModelByRow(int row)
        {
            for (int i = 0; i < this.unitModels.Count; i++)
            {
                if (this.unitModels[i].getRow() == row)
                {
                    return this.unitModels[i];
                }
            }

            return null;
        }
    }
}
