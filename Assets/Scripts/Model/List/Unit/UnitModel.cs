using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VOHUtil;


namespace VOHModel
{
    public class UnitModel
    {
        public const string excelPath = "/Datasheet/UnitData.xlsx";

        private int row = -1;
        private List<List<string>> excelData;

        // data
        private string id = "";
        private string assetPath = "";
        private float assetScale = 1f;
        private float hp = 0f;
        private float mp = 0f;
        private float defense = 0f;
        private float moveSpeed = 0f;
        private string weapon = "";

        UnitModel()
        {

        }

        public UnitModel(int row, int colCount)
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

        protected void applyData()
        {
            this.id = ExcelParser.getDataWithType<string>(ref excelData, 0, 0);
            this.assetPath = ExcelParser.getDataWithType<string>(ref excelData, 0, 1);
            this.assetScale = ExcelParser.getDataWithType<float>(ref excelData, 0, 2);
            this.hp = ExcelParser.getDataWithType<float>(ref excelData, 0, 3);
            this.mp = ExcelParser.getDataWithType<float>(ref excelData, 0, 4);
            this.defense = ExcelParser.getDataWithType<float>(ref excelData, 0, 5);
            this.moveSpeed = ExcelParser.getDataWithType<float>(ref excelData, 0, 6);
            this.weapon = ExcelParser.getDataWithType<string>(ref excelData, 0, 7);
        }

        public int getRow()
        {
            return this.row;
        }
        public string getID()
        {
            return this.id;
        }
        public string getAssetPath()
        {
            return this.assetPath;
        }
        public float getAssetScale()
        {
            return this.assetScale;
        }
        public float getHP()
        {
            return this.hp;
        }
        public float getMP()
        {
            return this.mp;
        }
        public float getDefense()
        {
            return this.defense;
        }
        public float getMoveSpeed()
        {
            return this.moveSpeed;
        }
        public string getWeapon()
        {
            return this.weapon;
        }
    }
}
