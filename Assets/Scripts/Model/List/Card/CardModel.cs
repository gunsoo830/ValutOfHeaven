using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VOHUtil;

namespace VOHModel
{
    public class CardModel
    {
        public const string excelPath = "/Datasheet/CardData.xlsx";

        private int row = -1;
        private List<List<string>> excelData;

        // data
        private string id = "";
        private int tier = 0;
        private string icon = "";
        private string unitID = "";
        private string nameKey = "";
        private string typeKey = "";
        private string descKey = "";

        public CardModel(int row, int colCount)
        {
            this.row = row;
            this.init(colCount);
        }

        private bool init(int colCount)
        {
            try
            {
                excelData = ExcelParser.getInstance().ParseExcel(excelPath, 0, row, row + 1, 0, colCount);
                this.applyData();
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError("Parsing Excel Data is failed.");
                return false;
            }
        }

        private void applyData()
        {
            this.id = ExcelParser.getDataWithType<string>(ref excelData, 0, 0);
            this.tier = ExcelParser.getDataWithType<int>(ref excelData, 0, 1);
            this.icon = ExcelParser.getDataWithType<string>(ref excelData, 0, 2);
            this.unitID = ExcelParser.getDataWithType<string>(ref excelData, 0, 3);
            this.nameKey = ExcelParser.getDataWithType<string>(ref excelData, 0, 4);
            this.typeKey = ExcelParser.getDataWithType<string>(ref excelData, 0, 5);
            this.descKey = ExcelParser.getDataWithType<string>(ref excelData, 0, 6);
        }

        public int getRow() {  return this.row; }
        public string getID() { return this.id; }
        public int getTier() { return this.tier; }
        public string getIcon() { return this.icon; }
        public string getUnitID() { return this.unitID; }
        public string getNameKey() { return this.nameKey; }
        public string getTypeKey() { return this.typeKey; }
        public string getDescKey() {  return this.descKey; }

    }
}
